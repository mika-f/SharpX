// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using System.Globalization;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpX.Composition.CSharp;
using SharpX.Composition.Interfaces;
using SharpX.Core.Extensions;
using SharpX.Hlsl.Primitives.Attributes.Compiler;
using SharpX.ShaderLab.Primitives.Attributes.Compiler;
using SharpX.ShaderLab.Syntax;

using AttributeSyntax = SharpX.ShaderLab.Syntax.AttributeSyntax;
using CompilationUnitSyntax = SharpX.Hlsl.Syntax.CompilationUnitSyntax;
using ExpressionSyntax = SharpX.ShaderLab.Syntax.ExpressionSyntax;
using FieldDeclarationSyntax = SharpX.Hlsl.Syntax.FieldDeclarationSyntax;
using PropertyDeclarationSyntax = Microsoft.CodeAnalysis.CSharp.Syntax.PropertyDeclarationSyntax;

namespace SharpX.ShaderLab.CSharp.Enum;

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
            var source = (HlslSourceSyntax?)decl.CgInclude?.Source;
            var compilation = (CompilationUnitSyntax?)source?.Sources;
            if (compilation == null)
                return newNode;

            foreach (var member in _globalFields)
                compilation = compilation.AddMembers(member.NormalizeWhitespace().WithLeadingTrivia(Hlsl.SyntaxFactory.Whitespace("    ")));

            return decl.WithCgInclude(SyntaxFactory.CgIncludeDeclaration(SyntaxFactory.HlslSource(compilation)));
        }

        return base.VisitClassDeclaration(oldNode, newNode);
    }

    public override ShaderLabSyntaxNode? VisitPropertyDeclaration(PropertyDeclarationSyntax node)
    {
        var e = IsEnum(node);
        if (!e)
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
                var arguments = data.ConstructorArguments.SelectMany(w => ToDisplayString(w)).Select(w => SyntaxFactory.LiteralExpression(SyntaxKind.StringLiteralExpression, SyntaxFactory.Literal(w)));
                var firstArgument = data.ConstructorArguments.First();
                if (firstArgument.Value is INamedTypeSymbol { TypeKind: TypeKind.Enum } s)
                {
                    var alternative = s.GetAttributes().FirstOrDefault(w => w.AttributeClass!.Equals(GetSymbol(typeof(UnityNameAttribute)), SymbolEqualityComparer.Default));
                    if (alternative != null)
                        arguments = alternative.ConstructorArguments.SelectMany(w => ToDisplayString(w)).Select(w => SyntaxFactory.LiteralExpression(SyntaxKind.StringLiteralExpression, SyntaxFactory.Literal(w)));
                }

                var argumentList = SyntaxFactory.ArgumentList(arguments.Select(SyntaxFactory.Argument).ToArray());
                var attr = SyntaxFactory.Attribute(name, argumentList.Arguments.Count > 0 ? argumentList : null);
                attributeList.Add(attr);
            }

        _globalFields.Add(Hlsl.SyntaxFactory.FieldDeclaration(Hlsl.SyntaxFactory.IdentifierName("int"), Hlsl.SyntaxFactory.Identifier(identifier)));
        return SyntaxFactory.PropertyDeclaration(attributeList.Count > 0 ? SyntaxFactory.AttributeList(attributeList.ToArray()) : null, identifier, displayName, t, null, @default);
    }

    private bool IsEnum(PropertyDeclarationSyntax node)
    {
        var t = _args.SemanticModel.GetSymbolInfo(node.Type);
        if (t.Symbol is not INamedTypeSymbol s)
            return false;
        return s.TypeKind == TypeKind.Enum;
    }

    private ExpressionSyntax GetUnityDeclaredDefaultValue(PropertyDeclarationSyntax node)
    {
        var @default = HasAttribute(node, typeof(DefaultValueAttribute)) ? GetAttributeData(node, typeof(DefaultValueAttribute))[0][0]!.ToString()! : "";
        return SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, SyntaxFactory.Literal(@default));
    }


    #region Helpers

    private static List<string> ToDisplayString(object obj)
    {
        var t = obj.GetType();
        switch (true)
        {
            case { } when t == typeof(string):
                return new List<string> { (string)obj };

            case { } when t == typeof(bool):
                return new List<string> { (bool)obj ? "True" : "False" };

            case { } when t == typeof(float):
                return new List<string> { ((float)obj).ToString(CultureInfo.InvariantCulture) };

            case { } when t == typeof(double):
                return new List<string> { ((double)obj).ToString(CultureInfo.InvariantCulture) };

            case { } when t == typeof(TypedConstant):
            {
                var tc = (TypedConstant)obj;
                if (tc.Kind == TypedConstantKind.Array)
                    return tc.Values.SelectMany(w => ToDisplayString(w)).ToList();
                return new List<string> { ((TypedConstant)obj).Value!.ToString() };
            }

            case { } when obj is INamedTypeSymbol symbol:
                return new List<string> { symbol.ToDisplayString() };
        }

        return new List<string>();
    }


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