// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using System.Collections.Immutable;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.Loader;

using Microsoft.CodeAnalysis;

using SharpX.Compiler.Extensions;
using SharpX.Compiler.Interfaces;
using SharpX.Compiler.Models;
using SharpX.Compiler.Models.Abstractions;
using SharpX.Composition.Attributes;
using SharpX.Composition.Interfaces;
using SharpX.Core.Extensions;

namespace SharpX.Compiler;

public class CSharpCompiler : IDisposable
{
    private readonly List<IErrorMessage> _errors;
    private readonly List<AssemblyLoadContext> _hosts;
    private readonly CSharpCompilerOptions _options;
    private BackendRegistry? _registry;
    private SharpXWorkspace? _workspace;

    public IReadOnlyCollection<IErrorMessage> Errors => _errors.AsReadOnly();

    private CSharpCompiler(CSharpCompilerOptions options, List<AssemblyLoadContext> hosts, SharpXWorkspace? workspace = null, BackendRegistry? registry = null)
    {
        _options = options;
        _workspace = workspace;
        _hosts = hosts;
        _registry = registry;
        _errors = new List<IErrorMessage>();
    }

    public CSharpCompiler() : this(CSharpCompilerOptions.Default) { }

    public CSharpCompiler(CSharpCompilerOptions options) : this(options, new List<AssemblyLoadContext>()) { }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    public CSharpCompiler WithSources(List<string>? sources)
    {
        return new CSharpCompiler(_options with { Sources = sources }, _hosts, _workspace, _registry);
    }

    public async Task<bool> LoadLanguagesAsync(CancellationToken ct)
    {
        var isSuccessful = true;
        foreach (var path in _options.Languages)
        {
            ct.ThrowIfCancellationRequested();

            if (File.Exists(path))
                isSuccessful &= await LoadLanguagePluginAtPathAsync(path);
        }

        return isSuccessful;
    }

    private Task<bool> LoadLanguagePluginAtPathAsync(string path)
    {
        try
        {
            var hasEntryPoint = false;
            var assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(path);
            foreach (var type in assembly.GetTypes())
                switch (type)
                {
                    case { } when type.GetCustomAttribute<LanguageAttribute>() != null && typeof(ILanguage).IsAssignableFrom(type):
                        hasEntryPoint = true;
                        break;
                }


            if (hasEntryPoint)
                return Task.FromResult(true);

            return Task.FromResult(false);
        }
        catch (Exception e)
        {
            if (Debugger.IsAttached)
                Debugger.Break();

            return Task.FromResult(false);
        }
    }

    public async Task<bool> LoadPluginsAsync(CancellationToken ct)
    {
        _registry = new BackendRegistry();

        var isSuccessful = true;
        foreach (var path in _options.Plugins)
        {
            ct.ThrowIfCancellationRequested();
            if (File.Exists(path))
                isSuccessful &= await LoadBackendPluginAtPathAsync(path);
        }

        return isSuccessful;
    }

    private async Task<bool> LoadBackendPluginAtPathAsync(string path)
    {
        var host = new SharpXPluginHost(path);
        _hosts.Add(host);

        try
        {
            var assembly = host.LoadFromAssemblyName(new AssemblyName(Path.GetFileNameWithoutExtension(path)));
            foreach (var type in assembly.GetTypes())
                switch (type)
                {
                    case { } when type.GetCustomAttribute<BackendAttribute>() != null && typeof(IBackend).IsAssignableFrom(type):
                        await ExecutePluginEntryPointAsync(type);
                        break;
                }

            return true;
        }
        catch (Exception e)
        {
            if (Debugger.IsAttached)
                Debugger.Break();

            return false;
        }
    }

    private Task ExecutePluginEntryPointAsync(Type t)
    {
        var instance = Activator.CreateInstance(t) as IBackend;
        instance?.EntryPoint(_registry!);

        return Task.CompletedTask;
    }

    public async Task<bool> CompileAsync(CancellationToken ct)
    {
        _errors.Clear();

        var container = _registry?.GetLanguageContainer(_options.Target);
        if (container == null)
        {
            _errors.Add(new SharpXCompilerDiagnostic(DiagnosticSeverity.Warning, $"could not find target runtime: {_options.Target}"));
            return true;
        }

        if (_workspace == null)
        {
            _workspace = SharpXWorkspace.Create();
            _workspace = _workspace.AddMetadataReferences(_options.Libraries.Select(w => MetadataReference.CreateFromFile(w)).Cast<MetadataReference>().ToArray());
            _workspace = _workspace.AddMetadataReferences(container.References.Select(w => MetadataReference.CreateFromFile(w)).Cast<MetadataReference>().ToArray());
        }


        _workspace = EnumerableSources();

        var isSuccessful = await PrecompileCSharpSourcesAsync(ct);
        if (!isSuccessful)
            return false;

        isSuccessful &= await CompileCSharpSourcesAsync(container, (language, node, model) =>
        {
            if (node == null)
                return default;
            return _registry?.GetLanguageContainer(language)?.RunAsync(node, model);
        }, ct);
        return isSuccessful;
    }

    private SharpXWorkspace EnumerableSources()
    {
        var items = ScanSourceDirectory(_options.Sources).ToImmutableArray();
        var workspace = _workspace!;

        foreach (var item in items)
        {
            var documentId = workspace.GetDocumentIdFromPath(item);
            workspace = documentId == null ? workspace.AddDocuments(item) : workspace.WithDocumentText(documentId, item);
        }

        var removed = workspace.GetAllDocumentIds()
                               .Except(items.Select(w => workspace.GetDocumentIdFromPath(w)))
                               .NonNullable();

        return workspace.RemoveDocuments(removed);
    }

    private async Task<bool> PrecompileCSharpSourcesAsync(CancellationToken ct)
    {
        var workspace = _workspace!;
        var diagnostics = await workspace.GetAllDocuments()
                                         .ToAsyncEnumerable()
                                         .SelectAwait(async w => await w.GetSyntaxTreeAsync(ct))
                                         .NonNullable()
                                         .SelectMany(w => w.GetDiagnostics(ct).ToAsyncEnumerable())
                                         .ToListAsync(ct);

        if (diagnostics.None())
            return true;

        _errors.AddRange(diagnostics.Select(w => w.ToErrorMessage()));
        return diagnostics.All(w => w.Severity != DiagnosticSeverity.Error);
    }

    private async Task<bool> CompileCSharpSourcesAsync(BackendContainer container, Func<string, SyntaxNode?, SemanticModel, Core.SyntaxNode?> invoker, CancellationToken ct)
    {
        var workspace = _workspace!;

        var lockObj = new object();

        await Parallel.ForEachAsync(workspace.GetAllDocuments(), ct, async (w, c) =>
        {
            var syntax = await w.GetSyntaxRootAsync(c);
            var model = await w.GetSemanticModelAsync(c);

            if (syntax == null || model == null)
                lock (lockObj)
                {
                    _errors.Add(new SharpXCompilerDiagnostic(DiagnosticSeverity.Error, $"failed to get SemanticModel and/or SyntaxRoot for compiling - {w.FilePath}"));
                    return;
                }

            var source = container.RunAsync(syntax, model, (language, node) => invoker.Invoke(language, node, model));

            lock (lockObj)
            {
                if (source == null)
                {
                    _errors.Add(new SharpXCompilerDiagnostic(DiagnosticSeverity.Warning, $"root visitor returns null: {w.FilePath}"));
                }
                else
                {
                    var str = source.NormalizeWhitespace().ToFullString();
                    var fileUri = new Uri(Path.GetFullPath(w.FilePath!), UriKind.Absolute);
                    var baseUri = new Uri(Path.GetFullPath(_options.BaseUrl), UriKind.Absolute);

                    var relative = baseUri.MakeRelativeUri(fileUri);
                    var extension = container.ExtensionCallback.Invoke(source);
                    var @out = Path.GetFullPath(Path.Combine(_options.Output, Path.GetFileNameWithoutExtension(relative.ToString()) + "." + extension));
                    var dir = Path.GetDirectoryName(@out) ?? throw new ArgumentNullException();
                    if (!Directory.Exists(dir))
                        Directory.CreateDirectory(dir);

                    File.WriteAllText(@out, str);
                }
            }
        });

        return true;
    }

    private static IEnumerable<string> ScanSourceDirectory(List<string>? sources)
    {
        var items = new List<string>();

        if (sources == null)
            return items;

        foreach (var source in sources)
        {
            if (string.IsNullOrWhiteSpace(source))
                continue;

            if (File.Exists(source))
            {
                items.Add(source);
                continue;
            }

            if (!Directory.Exists(source))
                continue;

            items.AddRange(Directory.GetFileSystemEntries(source, "*.cs", SearchOption.AllDirectories));
        }

        return items;
    }
}