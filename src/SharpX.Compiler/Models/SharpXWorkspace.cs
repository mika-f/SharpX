// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using System.Collections.Immutable;
using System.Text;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;

namespace SharpX.Compiler.Models;

internal class SharpXWorkspace
{
    private const string AssemblyName = "SharpX.InternalAssembly";
    private readonly Workspace _workspace;

    public ProjectId ProjectId { get; }

    public Project Project => Solution.GetProject(ProjectId)!;

    public Solution Solution { get; }

    private SharpXWorkspace(Workspace workspace, ProjectId projectId, Solution solution)
    {
        _workspace = workspace;
        ProjectId = projectId;
        Solution = solution;
    }

    public static SharpXWorkspace Create()
    {
        var workspace = new AdhocWorkspace();
        var projectId = ProjectId.CreateNewId(AssemblyName);
        var solution = workspace.CurrentSolution.AddProject(projectId, AssemblyName, AssemblyName, LanguageNames.CSharp)
                                .WithProjectMetadataReferences(projectId, DefaultMetadataReferences().Select(w => MetadataReference.CreateFromFile(w)))
                                .WithProjectParseOptions(projectId, CSharpParseOptions.Default)
                                .WithProjectCompilationOptions(projectId, new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        return new SharpXWorkspace(workspace, projectId, solution);
    }

    public SharpXWorkspace WithSolution(Solution solution)
    {
        return new SharpXWorkspace(_workspace, ProjectId, solution);
    }

    public SharpXWorkspace WithDocumentText(DocumentId id, string path)
    {
        using var fs = new FileStream(path, FileMode.Open, FileAccess.Read);
        using var sr = new StreamReader(fs);
        var source = SourceText.From(sr.ReadToEnd(), Encoding.UTF8);
        return WithSolution(Solution.WithDocumentText(id, source));
    }

    public SharpXWorkspace WithMetadataReferences(IEnumerable<MetadataReference> references)
    {
        return WithSolution(Solution.WithProjectMetadataReferences(ProjectId, references));
    }

    public SharpXWorkspace AddMetadataReferences(params MetadataReference[] references)
    {
        var refs = Project.MetadataReferences;
        return WithMetadataReferences(refs.Concat(references));
    }

    public SharpXWorkspace AddDocuments(params string[] paths)
    {
        var documents = new List<DocumentInfo>();

        foreach (var path in paths)
        {
            var documentId = DocumentId.CreateNewId(ProjectId, path);
            using var fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            using var sr = new StreamReader(fs);
            var source = SourceText.From(sr.ReadToEnd(), Encoding.UTF8);
            var version = VersionStamp.Create();
            var textAndVersion = TextAndVersion.Create(source, version);
            var loader = TextLoader.From(textAndVersion);

            documents.Add(DocumentInfo.Create(documentId, path, null, SourceCodeKind.Regular, loader, path));
        }

        return WithSolution(Solution.AddDocuments(documents.ToImmutableArray()));
    }

    public ImmutableArray<DocumentId> GetAllDocumentIds()
    {
        return GetAllDocuments().Select(w => w.Id).ToImmutableArray();
    }

    public ImmutableArray<Document> GetAllDocuments()
    {
        return Project.Documents.ToImmutableArray();
    }

    public SharpXWorkspace RemoveDocuments(IEnumerable<DocumentId> ids)
    {
        return WithSolution(Solution.RemoveDocuments(ids.ToImmutableArray()));
    }

    public DocumentId? GetDocumentIdFromPath(string path)
    {
        var document = Project.Documents.FirstOrDefault(w => w.FilePath == path);
        return document?.Id;
    }

    private static IEnumerable<string> DefaultMetadataReferences()
    {
        var runtime = Path.GetDirectoryName(typeof(object).Assembly.Location);
        if (string.IsNullOrEmpty(runtime))
            throw new NotSupportedException();


        yield return Path.Combine(runtime, "mscorlib.dll");
        yield return Path.Combine(runtime, "System.dll");
        yield return Path.Combine(runtime, "System.Core.dll");
        yield return Path.Combine(runtime, "System.Runtime.dll");
        yield return Path.Combine(runtime, "System.Private.CoreLib.dll");
        yield return Path.Combine(runtime, "System.Collections.dll");
        yield return Path.Combine(runtime, "System.Collections.Immutable.dll");
        yield return Path.Combine(runtime, "System.Linq.dll");
    }
}