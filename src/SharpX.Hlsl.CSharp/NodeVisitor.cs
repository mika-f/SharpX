// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using System.Text.RegularExpressions;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpX.Composition.CSharp;
using SharpX.Composition.Interfaces;
using SharpX.Hlsl.Primitives.Attributes;
using SharpX.Hlsl.Primitives.Attributes.Compiler;

using FieldDeclarationSyntax = SharpX.Hlsl.Syntax.FieldDeclarationSyntax;

namespace SharpX.Hlsl.CSharp;

internal class NodeVisitor : CompositeCSharpSyntaxVisitor<HlslSyntaxNode>
{
    private readonly SemanticModel _semanticModel;

    public NodeVisitor(IBackendVisitorArgs<HlslSyntaxNode> args) : base(args)
    {
        _semanticModel = args.SemanticModel;
    }

    public override HlslSyntaxNode VisitCompilationUnit(CompilationUnitSyntax node)
    {
        var members = node.Members.Select(w => (Syntax.MemberDeclarationSyntax?)Visit(w)).Where(w => w != null).Select(w => w!);
        return SyntaxFactory.CompilationUnit(SyntaxFactory.List(members));
    }

    public override HlslSyntaxNode? VisitClassDeclaration(ClassDeclarationSyntax node)
    {
        return base.VisitClassDeclaration(node);
    }

    public override HlslSyntaxNode? VisitStructDeclaration(StructDeclarationSyntax node)
    {
        var identifier = SyntaxFactory.Identifier(node.Identifier.ValueText);

        // if struct has [Inline] attribute, the members extract into global scope.
        if (HasInlineAttribute(node))
            return null;
        // if struct has [CBuffer] attribute, it compiles into cbuffer declaration

        if (HasCBufferAttribute(node)) return null;

        var members = node.Members.Select(w => (Syntax.MemberDeclarationSyntax?)Visit(w))
                          .Where(w => w != null)
                          .OfType<FieldDeclarationSyntax>();


        return SyntaxFactory.StructDeclaration(identifier, SyntaxFactory.List(members));
    }

    public override HlslSyntaxNode? VisitPropertyDeclaration(PropertyDeclarationSyntax node)
    {
        var t = SyntaxFactory.IdentifierName(GetHlslName(node.Type));
        var identifier = SyntaxFactory.Identifier(node.Identifier.ValueText);

        var field = SyntaxFactory.FieldDeclaration(t, identifier);
        if (HasSemanticsAttribute(node))
            return field.WithSemantics(SyntaxFactory.Semantics(SyntaxFactory.IdentifierName(GetAttributeData(node, typeof(SemanticAttribute))[0])));
        return field;
    }

    #region Helpers

    #region Namings

    private static Regex _typeArgumentsRegex = new(@"&[A-Z]+?", RegexOptions.Compiled);

    private string GetHlslName(TypeSyntax t)
    {
        if (t is GenericNameSyntax g)
        {
            var hasComponentAttribute = HasComponentAttribute(g);
            if (hasComponentAttribute)
            {
                var template = GetAttributeData(g, typeof(ComponentAttribute))[0];
                var generics = GetCurrentSymbol(g) as INamedTypeSymbol;
                var arguments = generics?.TypeArguments.Select(GetHlslName) ?? Array.Empty<string>();

                foreach (var argument in arguments)
                    template = _typeArgumentsRegex.Replace(template, argument);

                return template;
            }

            return g.ToFullString();
        }
        else
        {
            var hasComponentAttribute = HasComponentAttribute(t);
            return hasComponentAttribute ? GetAttributeData(t, typeof(ComponentAttribute))[0] : t.ToFullString();
        }
    }

    private string GetHlslName(ITypeSymbol s)
    {
        if (HasAttribute(s, typeof(ComponentAttribute)))
            return GetAttributeData(s, typeof(ComponentAttribute))[0];

        return s.ToDisplayString();
    }

    #endregion

    #region Attributes

    private string[] GetAttributeData(SyntaxNode node, Type t, bool isArray = false)
    {
        var decl = GetDeclarationSymbol(node);
        if (decl == null)
            return Array.Empty<string>();
        return GetAttributeData(decl, t, isArray);
    }

    private string[] GetAttributeData(ISymbol decl, Type t, bool isArray = false)
    {
        var s = _semanticModel.Compilation.GetTypeByMetadataName(t.FullName ?? throw new ArgumentNullException());
        var attr = decl.GetAttributes().FirstOrDefault(w => w.AttributeClass?.Equals(s, SymbolEqualityComparer.Default) == true);
        if (attr == null)
            return Array.Empty<string>();

        return isArray ? attr.ConstructorArguments.SelectMany(w => w.Values.Select(v => v.ToString()!)).ToArray() : attr.ConstructorArguments.Select(w => w.Value?.ToString() ?? string.Empty).ToArray();
    }

    private bool HasCBufferAttribute(MemberDeclarationSyntax member)
    {
        return HasAttribute(member, typeof(CBufferAttribute));
    }

    private bool HasComponentAttribute(TypeSyntax t)
    {
        return HasAttribute(t, typeof(ComponentAttribute));
    }

    private bool HasInlineAttribute(MemberDeclarationSyntax member)
    {
        return HasAttribute(member, typeof(InlineAttribute));
    }

    private bool HasSemanticsAttribute(MemberDeclarationSyntax member)
    {
        return HasAttribute(member, typeof(SemanticAttribute));
    }

    private bool HasAttribute(SyntaxNode node, Type t)
    {
        var decl = GetDeclarationSymbol(node);
        if (decl == null)
            return false;

        return HasAttribute(decl, t);
    }

    private bool HasAttribute(ISymbol decl, Type t)
    {
        var s = _semanticModel.Compilation.GetTypeByMetadataName(t.FullName ?? throw new ArgumentNullException());
        var attrs = decl.GetAttributes();
        return attrs.Any(w => w.AttributeClass?.Equals(s, SymbolEqualityComparer.Default) == true);
    }

    private ISymbol? GetCurrentSymbol(SyntaxNode node)
    {
        var decl = _semanticModel.GetDeclaredSymbol(node);
        if (decl != null)
            return decl;

        var info = _semanticModel.GetSymbolInfo(node);
        if (info.Symbol is not INamedTypeSymbol baseDecl)
            return null;

        return baseDecl;
    }

    private ISymbol? GetDeclarationSymbol(SyntaxNode node)
    {
        var decl = _semanticModel.GetDeclaredSymbol(node);
        if (decl != null)
            return decl;

        var info = _semanticModel.GetSymbolInfo(node);
        if (info.Symbol is not INamedTypeSymbol baseDecl)
            return null;

        return baseDecl.ConstructedFrom;
    }

    #endregion

    #endregion
}