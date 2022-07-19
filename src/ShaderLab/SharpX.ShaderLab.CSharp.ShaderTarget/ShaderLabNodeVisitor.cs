// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpX.Composition.CSharp;
using SharpX.Composition.Interfaces;
using SharpX.Core.Extensions;
using SharpX.ShaderLab.Primitives.ShaderTarget;
using SharpX.ShaderLab.Syntax;

using CompilationUnitSyntax = SharpX.Hlsl.Syntax.CompilationUnitSyntax;

namespace SharpX.ShaderLab.CSharp.ShaderTarget;

public class ShaderLabNodeVisitor : CompositeCSharpSyntaxVisitor<ShaderLabSyntaxNode>
{
    private readonly IBackendVisitorArgs<ShaderLabSyntaxNode> _args;

    public ShaderLabNodeVisitor(IBackendVisitorArgs<ShaderLabSyntaxNode> args) : base(args)
    {
        _args = args;
    }

    public override ShaderLabSyntaxNode? VisitClassDeclaration(ClassDeclarationSyntax oldNode, ShaderLabSyntaxNode? newNode)
    {
        if (newNode is PassDeclarationSyntax w)
            if (HasAttribute(oldNode, typeof(ShaderFeatureAttribute)))
            {
                var flags = (ShaderFeatures)(GetAttributeData(oldNode, typeof(ShaderFeatureAttribute))[0][0] ?? ShaderFeatures.Derivatives);
                var minVersion = 0f;
                if ((flags & ShaderFeatures.Derivatives) == ShaderFeatures.Derivatives)
                    minVersion = Math.Max(minVersion, 2.5f);
                if ((flags & ShaderFeatures.Interpolators10) == ShaderFeatures.Interpolators10)
                    minVersion = Math.Max(minVersion, 3.0f);
                if ((flags & ShaderFeatures.SampleLod) == ShaderFeatures.SampleLod)
                    minVersion = Math.Max(minVersion, 3.0f);
                if ((flags & ShaderFeatures.FragCoord) == ShaderFeatures.FragCoord)
                    minVersion = Math.Max(minVersion, 3.0f);
                if ((flags & ShaderFeatures.Interpolators15) == ShaderFeatures.Interpolators15)
                    minVersion = Math.Max(minVersion, 3.5f);
                if ((flags & ShaderFeatures.MultipleRenderTarget4) == ShaderFeatures.MultipleRenderTarget4)
                    minVersion = Math.Max(minVersion, 3.5f);
                if ((flags & ShaderFeatures.Integers) == ShaderFeatures.Integers)
                    minVersion = Math.Max(minVersion, 3.5f);
                if ((flags & ShaderFeatures.Array2D) == ShaderFeatures.Array2D)
                    minVersion = Math.Max(minVersion, 3.5f);
                if ((flags & ShaderFeatures.Instancing) == ShaderFeatures.Instancing)
                    minVersion = Math.Max(minVersion, 3.5f);
                if ((flags & ShaderFeatures.Geometry) == ShaderFeatures.Geometry)
                    minVersion = Math.Max(minVersion, 4.0f);
                if ((flags & ShaderFeatures.Compute) == ShaderFeatures.Compute)
                    minVersion = Math.Max(minVersion, 4.5f);
                if ((flags & ShaderFeatures.RandomWrite) == ShaderFeatures.RandomWrite)
                    minVersion = Math.Max(minVersion, 4.5f);
                if ((flags & ShaderFeatures.ArrayCube) == ShaderFeatures.ArrayCube)
                    minVersion = Math.Max(minVersion, 4.6f);
                if ((flags & ShaderFeatures.TessellationHardware) == ShaderFeatures.TessellationHardware)
                    minVersion = Math.Max(minVersion, 4.6f);
                if ((flags & ShaderFeatures.Tessellation) == ShaderFeatures.Tessellation)
                    minVersion = Math.Max(minVersion, 4.6f);

                var features = new List<string>();
                if ((flags & ShaderFeatures.Interpolators32) == ShaderFeatures.Interpolators32)
                    features.Add("interpolators32");
                if ((flags & ShaderFeatures.MultipleRenderTarget8) == ShaderFeatures.MultipleRenderTarget8)
                    features.Add("mrt8");
                if ((flags & ShaderFeatures.MultiSamplingTextureAccess) == ShaderFeatures.MultiSamplingTextureAccess)
                    features.Add("msaatex");
                if ((flags & ShaderFeatures.SparseTexture) == ShaderFeatures.SparseTexture)
                    features.Add("sparsetex");
                if ((flags & ShaderFeatures.FrameBufferFetch) == ShaderFeatures.FrameBufferFetch)
                    features.Add("framebufferfetch");

                var hlsl = (w.CgProgram.Source as HlslSourceSyntax)?.Sources as CompilationUnitSyntax;
                if (hlsl == null)
                    return newNode;

                if (minVersion > 0.0f || features.Count > 0)
                {
                    var leadings = hlsl.GetLeadingTrivia().ToList();

                    foreach (var feature in features)
                    {
                        var trivia = Hlsl.SyntaxFactory.PragmaDirectiveTrivia(Hlsl.SyntaxFactory.Identifier("require"), Hlsl.SyntaxFactory.Identifier(feature))
                                         .NormalizeWhitespace()
                                         .WithLeadingTrivia(Hlsl.SyntaxFactory.Whitespace("            "))
                                         .WithTrailingTrivia(Hlsl.SyntaxFactory.EndOfLine("\n"));

                        leadings.Insert(0, Hlsl.SyntaxFactory.Trivia(trivia));
                    }

                    if (minVersion > 0.0f)
                    {
                        var trivia = Hlsl.SyntaxFactory.PragmaDirectiveTrivia(Hlsl.SyntaxFactory.Identifier("target"), Hlsl.SyntaxFactory.Identifier(minVersion.ToString("F1")))
                                         .NormalizeWhitespace()
                                         .WithLeadingTrivia(Hlsl.SyntaxFactory.Whitespace("            "))
                                         .WithTrailingTrivia(Hlsl.SyntaxFactory.EndOfLine("\n"));
                        ;
                        leadings.Insert(0, Hlsl.SyntaxFactory.Trivia(trivia));
                    }

                    hlsl = hlsl.WithLeadingTrivia(leadings.ToArray());
                    return w.WithCgProgram(SyntaxFactory.CgProgramDeclaration(SyntaxFactory.HlslSource(hlsl)));
                }
            }

        return newNode;
    }

    private List<object?[]> GetAttributeData(SyntaxNode node, Type t, int at = 0)
    {
        var decl = GetDeclarationSymbol(node);
        if (decl == null)
            return new List<object?[]>();
        return GetAttributeData(decl, t, at);
    }

    private List<object?[]> GetAttributeData(ISymbol decl, Type t, int at = 0)
    {
        var s = _args.SemanticModel.Compilation.GetTypeByMetadataName(t.FullName ?? throw new ArgumentNullException());
        var attr = decl.GetAttributes().Where(w => w.AttributeClass?.Equals(s, SymbolEqualityComparer.Default) == true).ElementAtOrDefault(at);
        if (attr == null)
            return new List<object?[]>();

        return attr.ConstructorArguments.Select(w =>
        {
            if (w.Type!.TypeKind == TypeKind.Array)
                return w.Values.Select(v => v.Value).ToArray();
            return new[] { w.Value };
        }).ToList();
    }

    private bool HasAttribute(SyntaxNode node, Type t, bool isReturnAttr = false)
    {
        var decl = GetDeclarationSymbol(node);
        if (decl == null)
            return false;

        return HasAttribute(decl, t, isReturnAttr);
    }

    private bool HasAttribute(ISymbol decl, Type t, bool isReturnAttr = false)
    {
        var s = _args.SemanticModel.Compilation.GetTypeByMetadataName(t.FullName ?? throw new ArgumentNullException());
        var attrs = isReturnAttr && decl is IMethodSymbol m ? m.GetReturnTypeAttributes() : decl.GetAttributes();
        return attrs.Any(w => w.AttributeClass?.Equals(s, SymbolEqualityComparer.Default) == true);
    }

    private ISymbol? GetDeclarationSymbol(SyntaxNode node)
    {
        var decl = _args.SemanticModel.GetDeclaredSymbol(node);
        if (decl != null)
            return decl;

        var info = _args.SemanticModel.GetSymbolInfo(node);
        if (info.Symbol is INamedTypeSymbol baseDecl)
            return baseDecl.ConstructedFrom;
        if (info.Symbol is IMethodSymbol methodDecl)
            return methodDecl;
        if (info.Symbol is IPropertySymbol propertyDecl)
            return propertyDecl;

        return null;
    }
}