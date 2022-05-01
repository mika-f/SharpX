// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace SharpX.Hlsl.SourceGenerator;

[Generator(LanguageNames.CSharp)]
public sealed class FunctionGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(GenerateInitialStaticCodes);

        var attributes = context.CompilationProvider.Select(static (compilation, token) =>
        {
            token.ThrowIfCancellationRequested();
            return compilation.GetTypeByMetadataName("SharpX.Hlsl.SourceGenerator.Attributes.FunctionSourceAttribute") ?? throw new NullReferenceException("");
        }).WithComparer(SymbolEqualityComparer.Default);

        var providers = context.SyntaxProvider.CreateSyntaxProvider(Predicate, Transform)
                               .Combine(attributes)
                               .Select(PostTransform)
                               .Where(w => w is (not null, _))
                               .WithComparer(new SyntaxProviderComparer());

        var options = context.AnalyzerConfigOptionsProvider.Select(SelectOptions).WithComparer(new MSBuildOptionsComparer());

        context.RegisterSourceOutput(providers.Combine(options), GenerateActualSource);
    }

    private static void GenerateInitialStaticCodes(IncrementalGeneratorPostInitializationContext context)
    {
        context.CancellationToken.ThrowIfCancellationRequested();

        context.AddSource("FunctionSource.g.cs", @"
namespace SharpX.Hlsl.SourceGenerator.Attributes;

[global::System.AttributeUsage(global::System.AttributeTargets.Class | global::System.AttributeTargets.Interface, AllowMultiple = false)]
public class FunctionSourceAttribute : global::System.Attribute
{
    public string Source { get; }

    public FunctionSourceAttribute(string source)
    {
        Source = source;
    }
}
");
    }

    private static bool Predicate(SyntaxNode node, CancellationToken token)
    {
        token.ThrowIfCancellationRequested();
        return node is ClassDeclarationSyntax;
    }

    private static INamedTypeSymbol? Transform(GeneratorSyntaxContext context, CancellationToken token)
    {
        token.ThrowIfCancellationRequested();

        var decl = (context.Node as ClassDeclarationSyntax)!;
        var symbol = context.SemanticModel.GetDeclaredSymbol(decl, token);
        return decl.Modifiers.Any(w => w.IsKind(SyntaxKind.PartialKeyword)) ? symbol : default;
    }

    private static (INamedTypeSymbol? Symbol, string? Source) PostTransform((INamedTypeSymbol? Left, INamedTypeSymbol Right) tuple, CancellationToken token)
    {
        var left = tuple.Left;
        if (left == null)
            return default;

        foreach (var attribute in left.GetAttributes().Where(attribute => attribute.AttributeClass?.Equals(tuple.Right, SymbolEqualityComparer.Default) == true))
        {
            token.ThrowIfCancellationRequested();
            return (Symbol: left, Source: attribute.ConstructorArguments[0].Value as string);
        }

        return default;
    }

    private static MSBuildOptions SelectOptions(AnalyzerConfigOptionsProvider provider, CancellationToken token)
    {
        token.ThrowIfCancellationRequested();


        if (!provider.GlobalOptions.TryGetValue("build_property.ProjectDir", out var projectDir))
            projectDir = "";

        return new MSBuildOptions(projectDir);
    }

    private static void GenerateActualSource(SourceProductionContext context, ((INamedTypeSymbol?, string?) Left, MSBuildOptions Right) arg2)
    {
        context.CancellationToken.ThrowIfCancellationRequested();
    }
}