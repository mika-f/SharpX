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

using CSharpSyntaxKind = Microsoft.CodeAnalysis.CSharp.SyntaxKind;
using ExpressionSyntax = SharpX.Hlsl.Syntax.ExpressionSyntax;
using SimpleNameSyntax = SharpX.Hlsl.Syntax.SimpleNameSyntax;
using StatementSyntax = SharpX.Hlsl.Syntax.StatementSyntax;

namespace SharpX.Hlsl.CSharp;

internal class NodeVisitor : CompositeCSharpSyntaxVisitor<HlslSyntaxNode>
{
    private readonly SemanticModel _semanticModel;

    public NodeVisitor(IBackendVisitorArgs<HlslSyntaxNode> args) : base(args)
    {
        _semanticModel = args.SemanticModel;
    }

    public override HlslSyntaxNode? DefaultVisit(SyntaxNode node)
    {
        return null;
    }

    public override HlslSyntaxNode? VisitIdentifierName(IdentifierNameSyntax node)
    {
        return SyntaxFactory.IdentifierName(GetHlslName(node));
    }

    public override HlslSyntaxNode? VisitGenericName(GenericNameSyntax node)
    {
        if (HasAlternativeName(node))
            return SyntaxFactory.IdentifierName(GetHlslName(node));

        var arguments = node.TypeArgumentList.Arguments.Select(w => (Syntax.TypeSyntax?)Visit(node))
                            .Where(w => w != null)
                            .OfType<Syntax.TypeSyntax>();

        return SyntaxFactory.GenericName(SyntaxFactory.Identifier(node.Identifier.ValueText), SyntaxFactory.TypeArgumentList(SyntaxFactory.SeparatedList(arguments.ToArray())));
    }

    public override HlslSyntaxNode? VisitParenthesizedExpression(ParenthesizedExpressionSyntax node)
    {
        var expression = (ExpressionSyntax?)Visit(node.Expression);
        if (expression == null)
            return null;

        return SyntaxFactory.ParenthesizedExpression(expression);
    }

    public override HlslSyntaxNode? VisitPrefixUnaryExpression(PrefixUnaryExpressionSyntax node)
    {
        var operand = (ExpressionSyntax?)Visit(node.Operand);
        if (operand == null)
            return null;

        return node.Kind() switch
        {
            CSharpSyntaxKind.UnaryPlusExpression => SyntaxFactory.PrefixUnaryExpression(SyntaxKind.UnaryPlusExpression, operand),
            CSharpSyntaxKind.UnaryMinusExpression => SyntaxFactory.PrefixUnaryExpression(SyntaxKind.UnaryMinusExpression, operand),
            CSharpSyntaxKind.BitwiseNotExpression => SyntaxFactory.PrefixUnaryExpression(SyntaxKind.BitwiseNotExpression, operand),
            CSharpSyntaxKind.LogicalNotExpression => SyntaxFactory.PrefixUnaryExpression(SyntaxKind.LogicalNotExpression, operand),
            CSharpSyntaxKind.PreIncrementExpression => SyntaxFactory.PrefixUnaryExpression(SyntaxKind.PreIncrementExpression, operand),
            CSharpSyntaxKind.PreDecrementExpression => SyntaxFactory.PrefixUnaryExpression(SyntaxKind.PreDecrementExpression, operand),
            CSharpSyntaxKind.IndexExpression => SyntaxFactory.PrefixUnaryExpression(SyntaxKind.IndexExpression, operand),
            _ => null
        };
    }

    public override HlslSyntaxNode? VisitPostfixUnaryExpression(PostfixUnaryExpressionSyntax node)
    {
        var operand = (ExpressionSyntax?)Visit(node.Operand);
        if (operand == null)
            return null;

        return node.Kind() switch
        {
            CSharpSyntaxKind.PostIncrementExpression => SyntaxFactory.PostfixUnaryExpression(SyntaxKind.PostIncrementExpression, operand),
            CSharpSyntaxKind.PostDecrementExpression => SyntaxFactory.PostfixUnaryExpression(SyntaxKind.PostDecrementExpression, operand),
            _ => null
        };
    }

    public override HlslSyntaxNode? VisitMemberAccessExpression(MemberAccessExpressionSyntax node)
    {
        var hasInlineAttributeOnReceiver = HasInlineAttribute(node.Expression);
        var expression = (ExpressionSyntax?)Visit(node.Expression);
        var name = (SimpleNameSyntax?)Visit(node.Name);

        if (expression == null || name == null)
            return null;

        if (hasInlineAttributeOnReceiver)
            return name;

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

        return node.Kind() switch
        {
            CSharpSyntaxKind.SimpleAssignmentExpression => SyntaxFactory.AssignmentExpression(SyntaxKind.SimpleAssignmentExpression, left, right),
            CSharpSyntaxKind.AddAssignmentExpression => SyntaxFactory.AssignmentExpression(SyntaxKind.AddAssignmentExpression, left, right),
            CSharpSyntaxKind.SubtractAssignmentExpression => SyntaxFactory.AssignmentExpression(SyntaxKind.SubtractAssignmentExpression, left, right),
            CSharpSyntaxKind.MultiplyAssignmentExpression => SyntaxFactory.AssignmentExpression(SyntaxKind.MultiplyAssignmentExpression, left, right),
            CSharpSyntaxKind.DivideAssignmentExpression => SyntaxFactory.AssignmentExpression(SyntaxKind.DivideAssignmentExpression, left, right),
            CSharpSyntaxKind.ModuloAssignmentExpression => SyntaxFactory.AssignmentExpression(SyntaxKind.ModuloAssignmentExpression, left, right),
            CSharpSyntaxKind.AndAssignmentExpression => SyntaxFactory.AssignmentExpression(SyntaxKind.AndAssignmentExpression, left, right),
            CSharpSyntaxKind.ExclusiveOrAssignmentExpression => SyntaxFactory.AssignmentExpression(SyntaxKind.ExclusiveOrAssignmentExpression, left, right),
            CSharpSyntaxKind.OrAssignmentExpression => SyntaxFactory.AssignmentExpression(SyntaxKind.OrAssignmentExpression, left, right),
            CSharpSyntaxKind.LeftShiftAssignmentExpression => SyntaxFactory.AssignmentExpression(SyntaxKind.LeftShiftAssignmentExpression, left, right),
            CSharpSyntaxKind.RightShiftAssignmentExpression => SyntaxFactory.AssignmentExpression(SyntaxKind.RightShiftAssignmentExpression, left, right),
            _ => null
        };
    }

    public override HlslSyntaxNode? VisitConditionalExpression(ConditionalExpressionSyntax node)
    {
        var condition = (ExpressionSyntax?)Visit(node.Condition);
        var whenTrue = (ExpressionSyntax?)Visit(node.WhenTrue);
        var whenFalse = (ExpressionSyntax?)Visit(node.WhenFalse);

        if (condition == null || whenTrue == null || whenFalse == null)
            return null;

        return SyntaxFactory.ConditionalExpression(condition, whenTrue, whenFalse);
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

    public override HlslSyntaxNode? VisitElementAccessExpression(ElementAccessExpressionSyntax node)
    {
        var expression = (ExpressionSyntax?)Visit(node.Expression);
        var arguments = node.ArgumentList.Arguments.Select(w => (Syntax.ArgumentSyntax?)Visit(w))
                            .Where(w => w != null)
                            .OfType<Syntax.ArgumentSyntax>()
                            .ToArray();

        if (expression == null)
            return null;

        return SyntaxFactory.ElementAccessExpression(expression, SyntaxFactory.BracketedArgumentList(SyntaxFactory.SeparatedList(arguments)));
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

    public override HlslSyntaxNode? VisitCastExpression(CastExpressionSyntax node)
    {
        var expression = (ExpressionSyntax?)Visit(node.Expression);
        if (expression == null)
            return null;

        var hasImplicitCastAttribute = HasImplicitCastInCompilerAttribute(node.Type);
        if (hasImplicitCastAttribute)
            return expression;

        var type = (Syntax.TypeSyntax?)Visit(node.Type);
        if (type == null)
            return null;

        return SyntaxFactory.CastExpression(type, expression);
    }

    public override HlslSyntaxNode? VisitObjectCreationExpression(ObjectCreationExpressionSyntax node)
    {
        // if object creation called with not external component, convert to cast expression like this -> var some = (Struct) 0;
        var hasExternalAttribute = HasExternalComponentAttribute(node.Type);
        if (hasExternalAttribute)
        {
            // C# object creation is called constructor with new keyword, but HLSL called constructor same as method invocation
            var expression = SyntaxFactory.IdentifierName(GetHlslName(node.Type));
            var invocation = SyntaxFactory.InvocationExpression(expression);

            if (node.ArgumentList == null)
                return invocation;

            var arguments = node.ArgumentList.Arguments.Select(w => (Syntax.ArgumentSyntax?)Visit(w))
                                .Where(w => w != null)
                                .OfType<Syntax.ArgumentSyntax>()
                                .ToArray();

            return invocation.WithArgumentList(SyntaxFactory.ArgumentList(SyntaxFactory.SeparatedList(arguments)));
        }

        var t = (Syntax.TypeSyntax?)Visit(node.Type);
        if (t == null)
            return null;
        return SyntaxFactory.CastExpression(t, SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, SyntaxFactory.Literal(0)));
    }

    public override HlslSyntaxNode? VisitArrayCreationExpression(ArrayCreationExpressionSyntax node)
    {
        return base.VisitArrayCreationExpression(node);
    }

    public override HlslSyntaxNode? VisitBlock(BlockSyntax node)
    {
        var statements = node.Statements.Select(w => (StatementSyntax?)Visit(w))
                             .Where(w => w != null)
                             .OfType<StatementSyntax>()
                             .ToArray();

        return SyntaxFactory.Block(statements);
    }

    public override HlslSyntaxNode? VisitLocalDeclarationStatement(LocalDeclarationStatementSyntax node)
    {
        if (node.AwaitKeyword != default || node.UsingKeyword != default)
            return null;

        var declaration = (Syntax.VariableDeclarationSyntax?)Visit(node.Declaration);
        if (declaration == null)
            return null;

        return SyntaxFactory.LocalDeclaration(SyntaxFactory.List<Syntax.AttributeListSyntax>(), SyntaxFactory.TokenList(), declaration);
    }

    public override HlslSyntaxNode? VisitVariableDeclaration(VariableDeclarationSyntax node)
    {
        var t = SyntaxFactory.IdentifierName(GetHlslName(node.Type));
        var variables = node.Variables.Select(w => (Syntax.VariableDeclaratorSyntax?)Visit(w))
                            .Where(w => w != null)
                            .OfType<Syntax.VariableDeclaratorSyntax>()
                            .ToArray();

        return SyntaxFactory.VariableDeclaration(t, SyntaxFactory.SeparatedList(variables));
    }

    public override HlslSyntaxNode? VisitVariableDeclarator(VariableDeclaratorSyntax node)
    {
        var identifier = SyntaxFactory.Identifier(node.Identifier.ValueText);
        var initializer = (Syntax.EqualsValueClauseSyntax?)Visit(node.Initializer);

        return SyntaxFactory.VariableDeclarator(identifier, initializer);
    }

    public override HlslSyntaxNode? VisitEqualsValueClause(EqualsValueClauseSyntax node)
    {
        var expression = (ExpressionSyntax?)Visit(node.Value);
        if (expression == null)
            return null;

        return SyntaxFactory.EqualsValueClause(expression);
    }

    public override HlslSyntaxNode? VisitEmptyStatement(EmptyStatementSyntax node)
    {
        return SyntaxFactory.EmptyStatement(SyntaxFactory.List<Syntax.AttributeListSyntax>());
    }
    }

    public override HlslSyntaxNode? VisitReturnStatement(ReturnStatementSyntax node)
    {
        var statement = SyntaxFactory.ReturnStatement(SyntaxFactory.List<Syntax.AttributeListSyntax>());
        if (node.Expression == null)
            return statement;

        var expression = (ExpressionSyntax?)Visit(node.Expression);
        return statement.WithExpression(expression);
    }


    public override HlslSyntaxNode? VisitForStatement(ForStatementSyntax node)
    {
        var declaration = (Syntax.VariableDeclarationSyntax?)Visit(node.Declaration);
        var initializers = node.Initializers.Select(w => (ExpressionSyntax?)Visit(w))
                               .Where(w => w != null)
                               .OfType<ExpressionSyntax>()
                               .ToArray();
        var expression = (ExpressionSyntax?)Visit(node.Condition);
        var incrementors = node.Incrementors.Select(w => (ExpressionSyntax?)Visit(w))
                               .Where(w => w != null)
                               .OfType<ExpressionSyntax>()
                               .ToArray();
        var statement = (StatementSyntax?)Visit(node.Statement);

        if (statement == null)
            return null;

        return SyntaxFactory.ForStatement(
            SyntaxFactory.List<Syntax.AttributeListSyntax>(),
            declaration,
            SyntaxFactory.SeparatedList(initializers),
            expression,
            SyntaxFactory.SeparatedList(incrementors),
            statement
        );
    }

    public override HlslSyntaxNode? VisitIfStatement(IfStatementSyntax node)
    {
        var condition = (ExpressionSyntax?)Visit(node.Condition);
        var statement = (StatementSyntax?)Visit(node.Statement);

        if (condition == null || statement == null)
            return null;

        var @else = (Syntax.ElseClauseSyntax?)Visit(node.Else);

        return SyntaxFactory.IfStatement(SyntaxFactory.List<Syntax.AttributeListSyntax>(), condition, statement, @else);
    }

    public override HlslSyntaxNode? VisitElseClause(ElseClauseSyntax node)
    {
        var statement = (StatementSyntax?)Visit(node.Statement);
        if (statement == null)
            return null;

        return SyntaxFactory.ElseClause(statement);
    }

    public override HlslSyntaxNode? VisitExpressionStatement(ExpressionStatementSyntax node)
    {
        var expression = (ExpressionSyntax?)Visit(node.Expression);
        if (expression == null)
            return null;

        return SyntaxFactory.ExpressionStatement(SyntaxFactory.List<Syntax.AttributeListSyntax>(), expression);
    }

    public override HlslSyntaxNode? VisitCompilationUnit(CompilationUnitSyntax node)
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
                          .OfType<Syntax.FieldDeclarationSyntax>();

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

        var declaration = SyntaxFactory.MethodDeclaration(SyntaxFactory.List<Syntax.AttributeListSyntax>(), @return, identifier, parameterList, null, SyntaxFactory.Block(statements));
        if (HasSemanticsAttribute(node, true))
            return declaration.WithReturnSemantics(SyntaxFactory.Semantics(GetAttributeData(node, typeof(SemanticAttribute), isReturnAttr: true)[0]));
        return declaration;
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

        var parameter = SyntaxFactory.Parameter(SyntaxFactory.List<Syntax.AttributeListSyntax>(), SyntaxFactory.TokenList(), type, identifier);
        if (HasSemanticsAttribute(node))
            return parameter.WithSemantics(SyntaxFactory.Semantics(GetAttributeData(node, typeof(SemanticAttribute))[0]));
        return parameter;
    }

    #region Helpers

    #region Namings

    private static readonly Regex TypeArgumentsRegex = new(@"&[A-Z]+?", RegexOptions.Compiled);

    private bool HasAlternativeName(SyntaxNode node)
    {
        return HasNameAttribute(node) || HasComponentAttribute(node);
    }

    private string GetHlslName(TypeSyntax t)
    {
        var hasComponentAttribute = HasComponentAttribute(t);
        if (hasComponentAttribute)
        {
            var symbol = GetCurrentSymbol(t);
            if (symbol is INamedTypeSymbol s)
            {
                var template = GetAttributeData(s, typeof(ComponentAttribute))[0];
                var arguments = s.TypeArguments.Select(GetHlslName) ?? Array.Empty<string>();

                foreach (var argument in arguments)
                    template = TypeArgumentsRegex.Replace(template, argument);

                return template.Trim();
            }
        }

        var hasNameAttribute = HasNameAttribute(t);
        if (hasNameAttribute)
            return GetAttributeData(t, typeof(NameAttribute))[0];

        if (t is IdentifierNameSyntax i)
        {
            if (i.IsVar)
            {
                var s = GetCurrentSymbol(i);
                return s.ToDisplayString();
            }

            return i.Identifier.ToFullString();
        }

        return t.ToFullString();
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

    private bool HasComponentAttribute(SyntaxNode node)
    {
        return HasAttribute(node, typeof(ComponentAttribute));
    }

    private bool HasExternalComponentAttribute(SyntaxNode node)
    {
        return HasAttribute(node, typeof(ExternalComponentAttribute));
    }

    private bool HasImplicitCastInCompilerAttribute(SyntaxNode node)
    {
        return HasAttribute(node, typeof(ImplicitCastInCompilerAttribute));
    }

    private bool HasNameAttribute(SyntaxNode t)
    {
        return HasAttribute(t, typeof(NameAttribute));
    }

    private bool HasInlineAttribute(SyntaxNode member)
    {
        return HasAttribute(member, typeof(InlineAttribute));
    }

    private bool HasRegisterAttribute(MemberDeclarationSyntax member)
    {
        return HasAttribute(member, typeof(RegisterAttribute));
    }

    private bool HasSemanticsAttribute(SyntaxNode member, bool isReturnAttr = false)
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
        if (info.Symbol is IPropertySymbol propertyDecl)
            return propertyDecl;

        return null;
    }

    #endregion

    #endregion
}