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

using AttributeListSyntax = SharpX.Hlsl.Syntax.AttributeListSyntax;
using CSharpSyntaxKind = Microsoft.CodeAnalysis.CSharp.SyntaxKind;
using ExpressionSyntax = SharpX.Hlsl.Syntax.ExpressionSyntax;
using FieldDeclarationSyntax = SharpX.Hlsl.Syntax.FieldDeclarationSyntax;
using SimpleNameSyntax = SharpX.Hlsl.Syntax.SimpleNameSyntax;
using StatementSyntax = SharpX.Hlsl.Syntax.StatementSyntax;
using VariableDeclaratorSyntax = SharpX.Hlsl.Syntax.VariableDeclaratorSyntax;

namespace SharpX.Hlsl.CSharp;

internal class NodeVisitor : CompositeCSharpSyntaxVisitor<HlslSyntaxNode>
{
    private readonly SemanticModel _semanticModel;

    public NodeVisitor(IBackendVisitorArgs<HlslSyntaxNode> args) : base(args)
    {
        _semanticModel = args.SemanticModel;
    }

    public override HlslSyntaxNode? VisitIdentifierName(IdentifierNameSyntax node)
    {
        return SyntaxFactory.IdentifierName(GetHlslName(node));
    }

    public override HlslSyntaxNode? VisitGenericName(GenericNameSyntax node)
    {
        if (HasNameAttribute(node))
            return SyntaxFactory.IdentifierName(GetHlslName(node));

        var arguments = node.TypeArgumentList.Arguments.Select(w => (Syntax.TypeSyntax?)Visit(node))
                            .Where(w => w != null)
                            .OfType<Syntax.TypeSyntax>();

        return SyntaxFactory.GenericName(SyntaxFactory.Identifier(node.Identifier.ValueText), SyntaxFactory.TypeArgumentList(SyntaxFactory.SeparatedList(arguments.ToArray())));
    }

    public override HlslSyntaxNode? VisitMemberAccessExpression(MemberAccessExpressionSyntax node)
    {
        var expression = (ExpressionSyntax?)Visit(node.Expression);
        var name = (SimpleNameSyntax?)Visit(node.Name);

        if (expression == null || name == null)
            return null;

        return SyntaxFactory.MemberAccessExpression(expression, name);
    }

    public override HlslSyntaxNode? VisitBinaryExpression(BinaryExpressionSyntax node)
    {
        var left = (ExpressionSyntax?)Visit(node.Left);
        var right = (ExpressionSyntax?)Visit(node.Right);

        if (left == null || right == null)
            return null;

        return node.Kind() switch
        {
            CSharpSyntaxKind.AddExpression => SyntaxFactory.BinaryExpression(SyntaxKind.AddExpression, left, right),
            CSharpSyntaxKind.SubtractExpression => SyntaxFactory.BinaryExpression(SyntaxKind.SubtractExpression, left, right),
            CSharpSyntaxKind.MultiplyExpression => SyntaxFactory.BinaryExpression(SyntaxKind.MultiplyExpression, left, right),
            CSharpSyntaxKind.DivideExpression => SyntaxFactory.BinaryExpression(SyntaxKind.DivideExpression, left, right),
            CSharpSyntaxKind.ModuloExpression => SyntaxFactory.BinaryExpression(SyntaxKind.ModuloExpression, left, right),
            CSharpSyntaxKind.LogicalOrExpression => SyntaxFactory.BinaryExpression(SyntaxKind.LogicalOrExpression, left, right),
            CSharpSyntaxKind.LogicalAndExpression => SyntaxFactory.BinaryExpression(SyntaxKind.LogicalAndExpression, left, right),
            CSharpSyntaxKind.BitwiseOrExpression => SyntaxFactory.BinaryExpression(SyntaxKind.BitwiseOrExpression, left, right),
            CSharpSyntaxKind.ExclusiveOrExpression => SyntaxFactory.BinaryExpression(SyntaxKind.ExclusiveOrExpression, left, right),
            CSharpSyntaxKind.EqualsExpression => SyntaxFactory.BinaryExpression(SyntaxKind.EqualsExpression, left, right),
            CSharpSyntaxKind.NotEqualsExpression => SyntaxFactory.BinaryExpression(SyntaxKind.NotEqualsExpression, left, right),
            CSharpSyntaxKind.LessThanExpression => SyntaxFactory.BinaryExpression(SyntaxKind.LessThanExpression, left, right),
            CSharpSyntaxKind.LessThanOrEqualExpression => SyntaxFactory.BinaryExpression(SyntaxKind.LessThanEqualsToken, left, right),
            CSharpSyntaxKind.GreaterThanExpression => SyntaxFactory.BinaryExpression(SyntaxKind.GreaterThanExpression, left, right),
            CSharpSyntaxKind.GreaterThanOrEqualExpression => SyntaxFactory.BinaryExpression(SyntaxKind.GreaterThanOrEqualExpression, left, right),
            _ => null
        };
    }

    public override HlslSyntaxNode? VisitAssignmentExpression(AssignmentExpressionSyntax node)
    {
        var left = (ExpressionSyntax?)Visit(node.Left);
        var right = (ExpressionSyntax?)Visit(node.Right);

        if (left == null || right == null)
            return null;

        return SyntaxFactory.AssignmentExpression(SyntaxKind.SimpleAssignmentExpression, left, right);
    }

    public override HlslSyntaxNode? VisitLiteralExpression(LiteralExpressionSyntax node)
    {
        return node.Kind() switch
        {
            CSharpSyntaxKind.StringLiteralExpression => SyntaxFactory.LiteralExpression(SyntaxKind.StringLiteralExpression, SyntaxFactory.Literal(node.Token.ToString())),
            CSharpSyntaxKind.NumericLiteralExpression => SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, SyntaxFactory.Literal(node.Token.ToString())),
            _ => null
        };
    }

    public override HlslSyntaxNode? VisitInvocationExpression(InvocationExpressionSyntax node)
    {
        var expression = (ExpressionSyntax?)Visit(node.Expression);
        if (expression == null)
            return null;

        var arguments = node.ArgumentList.Arguments.Select(w => (Syntax.ArgumentSyntax?)Visit(w))
                            .Where(w => w != null)
                            .OfType<Syntax.ArgumentSyntax>();

        var argumentList = SyntaxFactory.ArgumentList(SyntaxFactory.SeparatedList(arguments.ToArray()));

        return SyntaxFactory.InvocationExpression(expression, argumentList);
    }

    public override HlslSyntaxNode? VisitArgument(ArgumentSyntax node)
    {
        var expression = (ExpressionSyntax?)Visit(node.Expression);
        if (node.RefKindKeyword != default)
            return null;
        if (expression == null)
            return null;

        return SyntaxFactory.Argument(expression);
    }

    public override HlslSyntaxNode? VisitExpressionStatement(ExpressionStatementSyntax node)
    {
        var expression = (ExpressionSyntax?)Visit(node.Expression);
        if (expression == null)
            return null;

        return SyntaxFactory.ExpressionStatement(SyntaxFactory.List<AttributeListSyntax>(), expression);
    }

    public override HlslSyntaxNode VisitCompilationUnit(CompilationUnitSyntax node)
    {
        var members = node.Members.Select(w => (Syntax.MemberDeclarationSyntax?)Visit(w)).Where(w => w != null).Select(w => w!);
        return SyntaxFactory.CompilationUnit(SyntaxFactory.List(members));
    }

    public override HlslSyntaxNode? VisitClassDeclaration(ClassDeclarationSyntax node)
    {
        if (!HasInlineAttribute(node))
            return null;

        var members = node.Members.Select(w => (Syntax.MemberDeclarationSyntax?)Visit(w))
                          .Where(w => w != null)
                          .OfType<Syntax.MethodDeclarationSyntax>();
        return SyntaxFactory.TopLevelModule(SyntaxFactory.List(members.Cast<Syntax.MemberDeclarationSyntax>().ToArray()));
    }

    public override HlslSyntaxNode? VisitStructDeclaration(StructDeclarationSyntax node)
    {
        var identifier = SyntaxFactory.Identifier(node.Identifier.ValueText);
        var members = node.Members.Select(w => (Syntax.MemberDeclarationSyntax?)Visit(w))
                          .Where(w => w != null)
                          .OfType<FieldDeclarationSyntax>();

        // if struct has [Inline] attribute, the members extract into global scope.
        if (HasInlineAttribute(node))
            return SyntaxFactory.TopLevelModule(SyntaxFactory.List(members.Cast<Syntax.MemberDeclarationSyntax>().ToArray()));

        // if struct has [CBuffer] attribute, it compiles into cbuffer declaration
        if (HasCBufferAttribute(node))
            return null;

        // normal struct, compiles into struct simply.
        return SyntaxFactory.StructDeclaration(identifier, SyntaxFactory.List(members));
    }

    public override HlslSyntaxNode? VisitMethodDeclaration(MethodDeclarationSyntax node)
    {
        var identifier = SyntaxFactory.Identifier(node.Identifier.ValueText);
        var @return = SyntaxFactory.IdentifierName(GetHlslName(node.ReturnType));
        var parameters = node.ParameterList.Parameters.Select(w => (Syntax.ParameterSyntax?)Visit(w))
                             .Where(w => w != null)
                             .OfType<Syntax.ParameterSyntax>()
                             .ToArray();

        var parameterList = SyntaxFactory.ParameterList(parameters.ToArray());

        if (node.ExpressionBody != null)
            return null;
        if (node.Body == null)
            return null;

        var statements = node.Body.Statements.Select(w => (StatementSyntax?)Visit(w))
                             .Where(w => w != null)
                             .OfType<StatementSyntax>()
                             .ToArray();

        return SyntaxFactory.MethodDeclaration(SyntaxFactory.List<AttributeListSyntax>(), @return, identifier, parameterList, null, SyntaxFactory.Block(statements));
    }

    public override HlslSyntaxNode? VisitPropertyDeclaration(PropertyDeclarationSyntax node)
    {
        var t = SyntaxFactory.IdentifierName(GetHlslName(node.Type));
        var identifier = SyntaxFactory.Identifier(node.Identifier.ValueText);

        var field = SyntaxFactory.FieldDeclaration(t, identifier);
        if (HasSemanticsAttribute(node))
            return field.WithSemantics(SyntaxFactory.Semantics(GetAttributeData(node, typeof(SemanticAttribute))[0]));
        if (HasRegisterAttribute(node))
            return field.WithRegister(SyntaxFactory.Register(GetAttributeData(node, typeof(RegisterAttribute))[0]));
        return field;
    }

    public override HlslSyntaxNode? VisitParameter(ParameterSyntax node)
    {
        if (node.Type == null)
            return null;

        var type = SyntaxFactory.IdentifierName(GetHlslName(node.Type));
        var identifier = SyntaxFactory.Identifier(node.Identifier.ValueText);

        return SyntaxFactory.Parameter(SyntaxFactory.List<AttributeListSyntax>(), SyntaxFactory.TokenList(), type, identifier);
    }

    #region Helpers

    #region Namings

    private static readonly Regex TypeArgumentsRegex = new(@"&[A-Z]+?", RegexOptions.Compiled);

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
                    template = TypeArgumentsRegex.Replace(template, argument);

                return template.Trim();
            }

            return g.ToFullString().Trim();
        }
        else
        {
            var hasComponentAttribute = HasComponentAttribute(t);
            return hasComponentAttribute ? GetAttributeData(t, typeof(ComponentAttribute))[0].Trim() : t.ToFullString().Trim();
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

    private string[] GetAttributeData(SyntaxNode node, Type t, bool isArray = false, bool isReturnAttr = false)
    {
        var decl = GetDeclarationSymbol(node);
        if (decl == null)
            return Array.Empty<string>();
        return GetAttributeData(decl, t, isArray, isReturnAttr);
    }

    private string[] GetAttributeData(ISymbol decl, Type t, bool isArray = false, bool isReturnAttr = false)
    {
        var s = _semanticModel.Compilation.GetTypeByMetadataName(t.FullName ?? throw new ArgumentNullException());
        var attr = (isReturnAttr && decl is IMethodSymbol m ? m.GetReturnTypeAttributes() : decl.GetAttributes()).FirstOrDefault(w => w.AttributeClass?.Equals(s, SymbolEqualityComparer.Default) == true);
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

    private bool HasNameAttribute(SyntaxNode t)
    {
        return HasAttribute(t, typeof(NameAttribute));
    }

    private bool HasInlineAttribute(MemberDeclarationSyntax member)
    {
        return HasAttribute(member, typeof(InlineAttribute));
    }

    private bool HasRegisterAttribute(MemberDeclarationSyntax member)
    {
        return HasAttribute(member, typeof(RegisterAttribute));
    }

    private bool HasSemanticsAttribute(MemberDeclarationSyntax member, bool isReturnAttr = false)
    {
        return HasAttribute(member, typeof(SemanticAttribute), isReturnAttr);
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
        var s = _semanticModel.Compilation.GetTypeByMetadataName(t.FullName ?? throw new ArgumentNullException());
        var attrs = isReturnAttr && decl is IMethodSymbol m ? m.GetReturnTypeAttributes() : decl.GetAttributes();
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
        if (info.Symbol is INamedTypeSymbol baseDecl)
            return baseDecl.ConstructedFrom;
        if (info.Symbol is IMethodSymbol methodDecl)
            return methodDecl;

        return null;
    }

    #endregion

    #endregion
}