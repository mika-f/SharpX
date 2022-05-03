// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

using SharpX.Hlsl.SourceGenerator.Extensions;
using SharpX.Hlsl.SourceGenerator.TypeScript;
using SharpX.Hlsl.SourceGenerator.TypeScript.Syntax;

using SyntaxKind = Microsoft.CodeAnalysis.CSharp.SyntaxKind;
using TypeDeclarationSyntax = SharpX.Hlsl.SourceGenerator.TypeScript.Syntax.TypeDeclarationSyntax;
using TypeSyntax = SharpX.Hlsl.SourceGenerator.TypeScript.Syntax.TypeSyntax;

namespace SharpX.Hlsl.SourceGenerator;

[Generator(LanguageNames.CSharp)]
public sealed class FunctionGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(GenerateInitialStaticCodes);


        var options = context.AnalyzerConfigOptionsProvider.Select(SelectOptions).WithComparer(new MSBuildOptionsComparer());

        var attributes = context.CompilationProvider.Select(static (compilation, token) =>
        {
            token.ThrowIfCancellationRequested();
            return compilation.GetTypeByMetadataName("SharpX.Hlsl.SourceGenerator.Attributes.FunctionSourceAttribute") ?? throw new NullReferenceException("");
        }).WithComparer(SymbolEqualityComparer.Default);

        var additionalTexts = context.AdditionalTextsProvider.Where(w => w.Path.EndsWith(".d.ts"))
                                     .Collect();

        var providers = context.SyntaxProvider.CreateSyntaxProvider(Predicate, Transform)
                               .Combine(attributes)
                               .Combine(additionalTexts)
                               .Combine(options)
                               .Select(PostTransform)
                               .Where(w => w is (not null, _))
                               .WithComparer(new SyntaxProviderComparer());

        context.RegisterSourceOutput(providers, GenerateActualSource);
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

    private static (INamedTypeSymbol? Symbol, string? Source) PostTransform((((INamedTypeSymbol? Left, INamedTypeSymbol Right) Left, ImmutableArray<AdditionalText> Right) Left, MSBuildOptions Right) tuple, CancellationToken token)
    {
        var dir = tuple.Right.ProjectDirectory;
        if (string.IsNullOrWhiteSpace(dir))
            return default;

        var left = tuple.Left.Left.Left;
        if (left == null)
            return default;

        var attr = tuple.Left.Left.Right;
        var sources = tuple.Left.Right;
        foreach (var attribute in left.GetAttributes().Where(attribute => attribute.AttributeClass?.Equals(attr, SymbolEqualityComparer.Default) == true))
        {
            token.ThrowIfCancellationRequested();

            var reference = Path.GetFullPath(Path.Combine(dir, (string)attribute.ConstructorArguments[0].Value!));
            var source = sources.FirstOrDefault(w => w.Path == reference);
            return (Symbol: left, Source: source?.GetText()?.ToString());
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

    private static void GenerateActualSource(SourceProductionContext context, (INamedTypeSymbol?, string?) tuple)
    {
        context.CancellationToken.ThrowIfCancellationRequested();

        var symbol = tuple.Item1;
        if (symbol == null)
            return;

        var dts = tuple.Item2;
        if (string.IsNullOrWhiteSpace(dts))
            return;

        var ts = Parser.ParseString(dts!);
        var exports = ts.Members.OfType<ExportStatementSyntax>().ToArray();
        if (exports.None())
            return;

        var source = new StringBuilder();
        source.AppendLine($"namespace {symbol.ContainingNamespace.ToDisplayString()};");
        source.AppendLine($"public partial class {symbol.Name} {{");

        var signatures = new List<CSharpSignature>();

        foreach (var member in exports.SelectMany(w => w.Members))
        {
            if (member is not TypeDeclarationSyntax t)
                continue;

            signatures.AddRange(t.Functions.SelectMany(ExpandSignatures));
        }

        foreach (var signature in signatures.Distinct())
        {
            source.AppendLine($"[global::SharpX.Hlsl.Primitives.Attributes.Compiler.Name(\"{signature.Name}\")]");
            source.AppendLine($"public static extern {signature.Signature};");
        }

        source.AppendLine("}");

        context.AddSource($"{symbol.Name}.g.cs", source.ToString());
    }

    private static List<CSharpSignature> ExpandSignatures(FunctionDeclarationSyntax function)
    {
        var name = function.Identifier.ToFullString();
        var list = new List<CSharpSignature>();

        // single and no expanding
        if (function.Generics == null)
        {
            if (function.Parameters.Count == 0)
            {
                list.Add(new CSharpSignature(name, $"{function.ReturnType.ToFullString()} {name.ToUpperCamelCase()}()"));
            }
            else if (function.Parameters.All(w => w.Type.ToFullString() == function.ReturnType.ToFullString()))
            {
                var signatures = ExpandType(function.Parameters.First().Type);
                foreach (var signature in signatures)
                {
                    var sb = new StringBuilder();
                    sb.Append($"{signature} {name.ToUpperCamelCase()}(");

                    for (var i = 0; i < function.Parameters.Count; i++)
                    {
                        if (i != 0)
                            sb.Append(", ");
                        sb.Append($"{signature} {function.Parameters[i].Name.ToFullString()}");
                    }

                    sb.Append(")");

                    list.Add(new CSharpSignature(name, sb.ToString()));
                }
            }
        }


        return list.Distinct().ToList();
    }

    private static List<string> ExpandType(TypeSyntax t)
    {
        if (t is SimpleTypeSyntax s)
            return new List<string> { s.Identifier.ToFullString() };
        if (t is GenericTypeSyntax g)
            switch (g.Identifier.ToFullString())
            {
                case "Scalar":
                    return ExpandScalar(g.Generics);

                case "Vector":
                    return ExpandVector(g.Generics);

                case "Matrix":
                    return ExpandMatrix(g.Generics);

                case "Out":
                    return new List<string>();
            }

        throw new ArgumentOutOfRangeException(nameof(t));
    }

    private static List<string> ExpandScalar(GenericsDeclarationSyntax generics)
    {
        var list = new List<string>();

        if (generics.Generics.Count == 1)
        {
            var types = generics.Generics[0].OrTypes.Append(generics.Generics[0].T);
            foreach (var t in types)
                list.Add(t.ToFullString());
        }
        else
        {
            throw new ArgumentException();
        }

        return list;
    }

    private static List<string> ExpandVector(GenericsDeclarationSyntax generics)
    {
        var list = new List<string>();

        // no specify vector length
        if (generics.Generics.Count == 1)
        {
            var types = generics.Generics[0].OrTypes.Append(generics.Generics[0].T);
            foreach (var t in types)
            {
                var component = t.ToFullString();
                for (var i = 1; i <= 4; i++)
                    list.Add(i == 1 ? component : $"global::SharpX.Hlsl.Primitives.Types.Vector{i}<{component}>");
            }
        }
        else if (generics.Generics.Count == 2)
        {
            var types = generics.Generics[0].OrTypes.Append(generics.Generics[0].T);
            foreach (var t in types)
            {
                var component = t.ToFullString();
                var length = generics.Generics[1].T.ToFullString();

                list.Add(length == "1" ? component : $"global::SharpX.Hlsl.Primitives.Types.Vector{length}<{component}>");
            }
        }
        else
        {
            throw new ArgumentException();
        }

        return list;
    }

    private static List<string> ExpandMatrix(GenericsDeclarationSyntax generics)
    {
        var list = new List<string>();

        return list;
    }
}