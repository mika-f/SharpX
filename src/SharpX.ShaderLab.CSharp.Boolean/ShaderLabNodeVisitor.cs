// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpX.Composition.CSharp;
using SharpX.Composition.Interfaces;
using SharpX.Core.Extensions;
using SharpX.Hlsl.Primitives.Attributes.Compiler;
using SharpX.ShaderLab.Primitives.Attributes.Compiler;
using SharpX.ShaderLab.Syntax;

using AttributeSyntax = SharpX.ShaderLab.Syntax.AttributeSyntax;
using ExpressionSyntax = SharpX.ShaderLab.Syntax.ExpressionSyntax;
using FieldDeclarationSyntax = SharpX.Hlsl.Syntax.FieldDeclarationSyntax;
using PropertyDeclarationSyntax = Microsoft.CodeAnalysis.CSharp.Syntax.PropertyDeclarationSyntax;

namespace SharpX.ShaderLab.CSharp.Boolean;

public class ShaderLabNodeVisitor : CompositeCSharpSyntaxVisitor<ShaderLabSyntaxNode>
{
    private readonly IBackendVisitorArgs<ShaderLabSyntaxNode> _args;
    private readonly List<FieldDeclarationSyntax> _globalFields;

    public ShaderLabNodeVisitor(IBackendVisitorArgs<ShaderLabSyntaxNode> args) : base(args)
    {
        _args = args;
        _globalFields = new List<FieldDeclarationSyntax>();
    }

    public override ShaderLabSyntaxNode? VisitClassDeclaration(ClassDeclarationSyntax oldNode, ShaderLabSyntaxNode? newNode)
    {
        if (newNode is ShaderDeclarationSyntax decl)
        {
            var sources = (HlslSourceSyntax?)decl.CgInclude?.Source;
            if (sources == null)
                return newNode;
            return decl.WithCgInclude(SyntaxFactory.CgIncludeDeclaration(SyntaxFactory.HlslSource(sources.Sources.AddRange(_globalFields.Select(w => w.NormalizeWhitespace().WithLeadingTrivia(Hlsl.SyntaxFactory.Whitespace("    ")))))));
        }

        return base.VisitClassDeclaration(oldNode, newNode);
    }

    public override ShaderLabSyntaxNode? VisitPropertyDeclaration(PropertyDeclarationSyntax node)
    {
        var b = IsBoolean(node);
        if (!b)
            return null;

        if (!node.Modifiers.Any(Microsoft.CodeAnalysis.CSharp.SyntaxKind.StaticKeyword))
            return null;

        var displayName = node.Identifier.ToFullString().Trim();
        var identifier = HasAttribute(node, typeof(NameAttribute)) ? GetAttributeData(node, typeof(NameAttribute))[0][0]!.ToString()! : displayName;
        var t = SyntaxFactory.IdentifierName("Int");
        var @default = SyntaxFactory.EqualsValueClause(GetUnityDeclaredDefaultValue(node));

        var attributeList = new List<AttributeSyntax>();
        var attributes = GetAttributes(node);
        if (attributes.Any(w => w.AttributeClass!.BaseType?.Equals(GetSymbol(typeof(PropertyAttribute)), SymbolEqualityComparer.Default) == true))
            foreach (var data in attributes.Where(w => w.AttributeClass!.BaseType?.Equals(GetSymbol(typeof(PropertyAttribute)), SymbolEqualityComparer.Default) == true))
            {
                var name = SyntaxFactory.IdentifierName(data.AttributeClass!.Name.Substring(0, data.AttributeClass!.Name.LastIndexOf("Attribute", StringComparison.Ordinal)));
                var argumentList = SyntaxFactory.ArgumentList();
                var attr = SyntaxFactory.Attribute(name, argumentList.Arguments.Count > 0 ? argumentList : null);
                attributeList.Add(attr);
            }

        _globalFields.Add(Hlsl.SyntaxFactory.FieldDeclaration(Hlsl.SyntaxFactory.IdentifierName("int"), Hlsl.SyntaxFactory.Identifier(identifier)));
        return SyntaxFactory.PropertyDeclaration(attributeList.Count > 0 ? SyntaxFactory.AttributeList(attributeList.ToArray()) : null, identifier, displayName, t, null, @default);
    }

    private bool IsBoolean(PropertyDeclarationSyntax node)
    {
        var b = _args.SemanticModel.Compilation.GetTypeByMetadataName(typeof(bool).FullName!);
        var t = _args.SemanticModel.GetSymbolInfo(node.Type);
        if (t.Symbol == null)
            return false;

        return t.Symbol.Equals(b, SymbolEqualityComparer.Default);
    }

    private ExpressionSyntax GetUnityDeclaredDefaultValue(PropertyDeclarationSyntax node)
    {
        var @default = HasAttribute(node, typeof(DefaultValueAttribute)) ? GetAttributeData(node, typeof(DefaultValueAttribute))[0][0]!.ToString()! : "";
        return SyntaxFactory.LiteralExpression(SyntaxKind.StringLiteralExpression, SyntaxFactory.Literal(@default == "True" ? "1" : "0"));
    }


    #region Helpers

    #region Attributes

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

    private List<AttributeData> GetAttributes(SyntaxNode node)
    {
        var decl = GetDeclarationSymbol(node);
        if (decl == null)
            return new List<AttributeData>();
        return GetAttributes(decl);
    }

    private List<AttributeData> GetAttributes(ISymbol symbol)
    {
        return symbol.GetAttributes().ToList();
    }

    private ISymbol? GetSymbol(Type t)
    {
        return _args.SemanticModel.Compilation.GetTypeByMetadataName(t.FullName ?? throw new ArgumentNullException());
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

    #endregion

    #endregion
}