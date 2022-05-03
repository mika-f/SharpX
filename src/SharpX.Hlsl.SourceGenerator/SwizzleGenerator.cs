// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

using SharpX.Hlsl.SourceGenerator.Extensions;

namespace SharpX.Hlsl.SourceGenerator;

[Generator(LanguageNames.CSharp)]
public sealed class SwizzleGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(GenerateInitialStaticCodes);

        var attributes = context.CompilationProvider.Select(static (compilation, token) =>
        {
            token.ThrowIfCancellationRequested();
            return compilation.GetTypeByMetadataName("SharpX.Hlsl.SourceGenerator.Attributes.SwizzleAttribute") ?? throw new NullReferenceException("");
        }).WithComparer(SymbolEqualityComparer.Default);


        var providers = context.SyntaxProvider.CreateSyntaxProvider(Predicate, Transform)
                               .Combine(attributes)
                               .Select(PostTransform)
                               .Where(w => w is not (null, _))
                               .WithComparer(new SyntaxProviderComparer());

        var options = context.AnalyzerConfigOptionsProvider.Select(SelectOptions).WithComparer(new MSBuildOptionsComparer());

        context.RegisterSourceOutput(providers.Combine(options), GenerateActualSource);
    }

    private static void GenerateInitialStaticCodes(IncrementalGeneratorPostInitializationContext context)
    {
        context.CancellationToken.ThrowIfCancellationRequested();

        context.AddSource("Swizzle.g.cs", @"
namespace SharpX.Hlsl.SourceGenerator.Attributes;

[global::System.AttributeUsage(global::System.AttributeTargets.Class | global::System.AttributeTargets.Interface, AllowMultiple = true)]
public class SwizzleAttribute : global::System.Attribute
{
    public SwizzleAttribute(params string[] attributes) {}
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

    private static (INamedTypeSymbol Symbol, List<List<string>> Attributes) PostTransform((INamedTypeSymbol? Left, INamedTypeSymbol Right) tuple, CancellationToken token)
    {
        var symbol = tuple.Left;
        if (symbol == null)
            return default;

        var list = new List<List<string>>();
        var attr = tuple.Right;
        foreach (var attribute in symbol.GetAttributes().Where(attribute => attribute.AttributeClass?.Equals(attr, SymbolEqualityComparer.Default) == true))
        {
            token.ThrowIfCancellationRequested();

            var attributes = attribute.ConstructorArguments.SelectMany(w => w.Values.Select(w => w.Value as string));
            list.Add(attributes.Cast<string>().ToList());
        }

        return (Symbol: symbol, Attributes: list.ToList());
    }

    private static MSBuildOptions SelectOptions(AnalyzerConfigOptionsProvider provider, CancellationToken token)
    {
        token.ThrowIfCancellationRequested();

        var isDesignTimeBuild = provider.GlobalOptions.TryGetValue("build_property.DesignTimeBuild", out var designTimeBuild) && designTimeBuild == "true";
        if (!provider.GlobalOptions.TryGetValue("build_property.ProjectDir", out var projectDir))
            projectDir = "";

        return new MSBuildOptions(projectDir, isDesignTimeBuild);
    }


    private static void GenerateActualSource(SourceProductionContext context, ((INamedTypeSymbol, List<List<string>>) Left, MSBuildOptions Right) tuple)
    {
        context.CancellationToken.ThrowIfCancellationRequested();

        var symbol = tuple.Left.Item1;
        var allAttributes = tuple.Left.Item2;

        var source = new StringBuilder();
        source.AppendLine($"namespace {symbol.ContainingNamespace.ToDisplayString()};");
        source.AppendLine($"public partial class {symbol.Name}<T> {{");

        foreach (var attributes in allAttributes)
            for (var i = 0; i < attributes.Count; i++)
            {
                if (i + 1 > 4)
                    continue;

                if (tuple.Right.IsDesignTimeBuild)
                    continue;

                var signatures = attributes.Combination(i + 1, true);
                foreach (var components in signatures)
                {
                    var signature = string.Concat(components);
                    var accessors = components.Distinct().Count() == components.Length ? "{ get; set; }" : "{ get; }";

                    source.AppendLine($"    [global::SharpX.Hlsl.Primitives.Attributes.Compiler.Name(\"{signature.ToLowerInvariant()}\")]");
                    source.AppendLine($"    public global::SharpX.Hlsl.Primitives.Types.Vector{i + 1}<T> {signature} {accessors}");
                    source.AppendLine();
                }
            }

        source.AppendLine("}");

        context.AddSource($"{symbol.Name}.g.cs", source.ToString());
    }


    private class SyntaxProviderComparer : IEqualityComparer<ValueTuple<INamedTypeSymbol, List<List<string>>>>
    {
        public bool Equals((INamedTypeSymbol, List<List<string>>) x, (INamedTypeSymbol, List<List<string>>) y)
        {
            var i = 0;
            return x.Item1.Equals(y.Item1, SymbolEqualityComparer.Default) && x.Item2.All(w => w.SequenceEqual(y.Item2[i++]));
        }

        public int GetHashCode((INamedTypeSymbol, List<List<string>>) obj)
        {
            return SymbolEqualityComparer.Default.GetHashCode(obj.Item1);
        }
    }
}