// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using System.Collections.Immutable;
using System.Diagnostics;
using System.Reflection;

using Microsoft.CodeAnalysis;

using SharpX.Compiler.Extensions;
using SharpX.Compiler.Interfaces;
using SharpX.Compiler.Models;
using SharpX.Compiler.Models.Abstractions;
using SharpX.Composition.Attributes;
using SharpX.Composition.Interfaces;

namespace SharpX.Compiler;

public class CSharpCompiler : IDisposable
{
    private readonly List<IErrorMessage> _errors;
    private readonly CSharpCompilerOptions _options;
    private SharpXPluginHost? _host;
    private SharpXWorkspace? _workspace;
    private BackendRegistry? _registry;

    public IReadOnlyCollection<IErrorMessage> Errors => _errors.AsReadOnly();

    private CSharpCompiler(CSharpCompilerOptions options, SharpXWorkspace? workspace = null, SharpXPluginHost? host = null, BackendRegistry? registry = null)
    {
        _options = options;
        _workspace = workspace;
        _host = host;
        _registry = registry;
        _errors = new List<IErrorMessage>();
    }

    public CSharpCompiler() : this(CSharpCompilerOptions.Default) { }

    public CSharpCompiler(CSharpCompilerOptions options) : this(options, null) { }


    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    public CSharpCompiler WithSources(List<string>? sources)
    {
        return new CSharpCompiler(_options with { Sources = sources }, _workspace, _host, _registry);
    }

    public async Task<bool> LoadPluginsAsync(CancellationToken ct)
    {
        _registry = new BackendRegistry();

        var isSuccessful = true;
        foreach (var path in _options.Plugins)
            if (File.Exists(path))
                isSuccessful &= await LoadPluginAtPath(path);

        return isSuccessful;
    }

    private async Task<bool> LoadPluginAtPath(string path)
    {
        if (_host != null)
            return true;

        _host = new SharpXPluginHost(path);

        try
        {
            var assembly = _host.LoadFromAssemblyName(new AssemblyName(Path.GetFileNameWithoutExtension(path)));
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
                Debug.WriteLine(e.Message);

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
        if (_workspace == null)
        {
            _workspace = SharpXWorkspace.Create();
            _workspace = _workspace.AddMetadataReferences(_options.Libraries.Select(w => MetadataReference.CreateFromFile(w)).Cast<MetadataReference>().ToArray());
        }

        _workspace = EnumerableSources();

        var isSuccessful = await PrecompileCSharpSources(ct);
        if (!isSuccessful)
            return false;

        return true;
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

    private async Task<bool> PrecompileCSharpSources(CancellationToken ct)
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