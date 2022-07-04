// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using System.Collections.Immutable;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.Loader;
using System.Text.RegularExpressions;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

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
    private readonly List<SharpXPluginHost> _hosts;
    private readonly CSharpCompilerOptions _options;
    private BackendRegistry? _registry;
    private SharpXWorkspace? _workspace;

    public IReadOnlyCollection<IErrorMessage> Errors => _errors.AsReadOnly();

    private CSharpCompiler(CSharpCompilerOptions options, List<SharpXPluginHost> hosts, SharpXWorkspace? workspace = null, BackendRegistry? registry = null)
    {
        _options = options;
        _hosts = hosts;
        _workspace = workspace;
        _registry = registry;
        _errors = new List<IErrorMessage>();
    }

    public CSharpCompiler() : this(CSharpCompilerOptions.Default) { }

    public CSharpCompiler(CSharpCompilerOptions options) : this(options, new List<SharpXPluginHost>()) { }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    public CSharpCompiler WithSources(List<string>? sources)
    {
        return new CSharpCompiler(_options with { Sources = sources }, _hosts, _workspace, _registry);
    }

    public async Task<bool> LoadPluginsAsync(CancellationToken ct)
    {
        _registry = new BackendRegistry();

        var isSuccessful = true;
        foreach (var path in _options.Languages)
        {
            ct.ThrowIfCancellationRequested();
            if (File.Exists(path))
                isSuccessful &= await LoadRequirementsPluginIntoDefaultContextAtPathAsync(path);

            if (!isSuccessful)
                return false;
        }

        foreach (var path in _options.Libraries)
        {
            ct.ThrowIfCancellationRequested();
            if (File.Exists(path))
                isSuccessful &= await LoadBackendPluginAtPathAsync(path, false);

            if (!isSuccessful)
                return false;
        }

        foreach (var path in _options.Plugins)
        {
            ct.ThrowIfCancellationRequested();
            if (File.Exists(path))
                isSuccessful &= await LoadBackendPluginAtPathAsync(path);
            else
                isSuccessful = false;

            if (!isSuccessful)
                return false;
        }

        return isSuccessful;
    }

    private async Task<bool> LoadRequirementsPluginIntoDefaultContextAtPathAsync(string path)
    {
        try
        {
            var isLoaded = false;
            var assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(path);
            foreach (var type in assembly.GetTypes())
                switch (type)
                {
                    case { } when type.GetCustomAttribute<BackendAttribute>() != null && typeof(IBackend).IsAssignableFrom(type):
                        await ExecutePluginEntryPointAsync(type);
                        isLoaded = true;
                        break;
                }

            return isLoaded;
        }
        catch (Exception e)
        {
            if (Debugger.IsAttached)
                Debugger.Break();

            return false;
        }
    }

    private async Task<bool> LoadBackendPluginAtPathAsync(string path, bool doExecute = true)
    {
        var host = new SharpXPluginHost(path);
        _hosts.Add(host);

        try
        {
            var isLoaded = false;
            var assembly = host.LoadFromAssemblyPath(path);
            foreach (var type in assembly.GetTypes())
                switch (type)
                {
                    case { } when type.GetCustomAttribute<BackendAttribute>() != null && typeof(IBackend).IsAssignableFrom(type):
                        await ExecutePluginEntryPointAsync(type);
                        isLoaded = true;
                        break;
                }

            return (doExecute && isLoaded) || !doExecute;
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

        var (isSuccessful, dict) = await PrecompileCSharpSourcesAsync(ct);
        if (!isSuccessful)
            return false;

        Func<string, SyntaxNode?, SemanticModel, Dictionary<INamedTypeSymbol, string>, Core.SyntaxNode?>? invoker = null;
        invoker = (language, node, model, mappings) =>
        {
            if (node == null)
                return default;
            return _registry?.GetLanguageContainer(language)?.RunAsync(node, model, mappings, (l, n) => invoker!.Invoke(l, n, model, mappings));
        };


        isSuccessful &= await CompileCSharpSourcesAsync(container, dict, invoker, ct);
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

    private async Task<(bool, Dictionary<INamedTypeSymbol, string>?)> PrecompileCSharpSourcesAsync(CancellationToken ct)
    {
        var workspace = _workspace!;
        var diagnostics = await workspace.GetAllDocuments()
                                         .ToAsyncEnumerable()
                                         .SelectAwait(async w => await w.GetSyntaxTreeAsync(ct))
                                         .NonNullable()
                                         .SelectMany(w => w.GetDiagnostics(ct).ToAsyncEnumerable())
                                         .ToListAsync(ct);

        if (diagnostics.None())
        {
            var t = await workspace.GetAllDocuments()
                                   .ToAsyncEnumerable()
                                   .SelectManyAwait(async ws =>
                                   {
                                       var m = await ws.GetSemanticModelAsync(ct);
                                       if (m == null)
                                           return new List<(string, INamedTypeSymbol)>().ToAsyncEnumerable();

                                       var tree = await ws.GetSyntaxTreeAsync(ct);
                                       var compilation = tree?.GetCompilationUnitRoot(ct);
                                       var declarations = compilation?.DescendantNodes().Where(w => w is ClassDeclarationSyntax or InterfaceDeclarationSyntax or StructDeclarationSyntax or RecordDeclarationSyntax).ToList() ?? new List<SyntaxNode>();
                                       var guid = ws.Id.Id.ToString()!;
                                       return declarations.Select(w => m.GetDeclaredSymbol(w) as INamedTypeSymbol).NonNullable().Select(w => (guid, w)).ToAsyncEnumerable();
                                   })
                                   .ToDictionaryAsync(w => w.Item2, w => w.Item1, ct);

            return (true, t);
        }

        _errors.AddRange(diagnostics.Select(w => w.ToErrorMessage()));
        return (diagnostics.All(w => w.Severity != DiagnosticSeverity.Error), null);
    }

    private async Task<bool> CompileCSharpSourcesAsync(BackendContainer container, Dictionary<INamedTypeSymbol, string> fileMappings, Func<string, SyntaxNode?, SemanticModel, Dictionary<INamedTypeSymbol, string>, Core.SyntaxNode?> invoker, CancellationToken ct)
    {
        var workspace = _workspace!;

        var lockObj = new object();
        var contentFileMappings = new Dictionary<string, string>();
        var actualFileMappings = new Dictionary<string, (string Absolute, string Relative)>();

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

            var source = container.RunAsync(syntax, model, fileMappings, (language, node) => invoker.Invoke(language, node, model, fileMappings));

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
                    var baseDir = Path.GetDirectoryName(relative.ToString());
                    var baseName = Path.GetFileNameWithoutExtension(relative.ToString());
                    var extension = container.ExtensionCallback?.Invoke(source) ?? Path.GetExtension(relative.ToString());
                    var @out = Path.GetFullPath(Path.Combine(_options.Output, baseDir ?? "", baseName + "." + extension));
                    var id = w.Id.Id.ToString();

                    actualFileMappings.Add(id, (@out, Path.Combine(baseDir ?? "", baseName + "." + extension)));
                    contentFileMappings.Add(id, str);
                }
            }
        });

        var regex = new Regex("<#ref (?<guid>.*?)>", RegexOptions.Compiled);

        foreach (var (guid, c) in contentFileMappings)
        {
            var content = c;
            foreach (Match match in regex.Matches(content))
            {
                var fileReference = match.Groups["guid"].Value;
                content = content.Replace(match.Value, actualFileMappings[fileReference].Relative);
            }

            var @out = actualFileMappings[guid].Absolute;
            var dir = Path.GetDirectoryName(@out) ?? throw new ArgumentNullException();
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            await File.WriteAllTextAsync(@out, content, ct);
        }

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