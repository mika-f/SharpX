// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.Core.Syntax;
using SharpX.Hlsl.Syntax;

// ReSharper disable ReturnTypeCanBeNotNullable

namespace SharpX.Hlsl;

/// <summary>
///     based on <see cref="Microsoft.CodeAnalysis.CSharp.CSharpSyntaxRewriter" />
/// </summary>
public class HlslSyntaxRewriter : HlslSyntaxVisitor<SyntaxNode?>
{
    public override SyntaxNode? VisitIdentifierName(IdentifierNameSyntax node)
    {
        return node.Update(VisitToken(node.Identifier));
    }

    public override SyntaxNode? VisitGenericName(GenericNameSyntax node)
    {
        return node.Update(
            VisitToken(node.Identifier),
            (TypeArgumentListSyntax?)Visit(node) ?? throw new ArgumentNullException()
        );
    }

    public override SyntaxNode? VisitTypeArgumentList(TypeArgumentListSyntax node)
    {
        return node.Update(
            VisitToken(node.LessThanToken),
            VisitList(node.Arguments),
            VisitToken(node.GreaterThanToken)
        );
    }

    public override SyntaxNode? VisitPredefinedType(PredefinedTypeSyntax node)
    {
        return node.Update(VisitToken(node.Keyword));
    }

    public override SyntaxNode? VisitArrayType(ArrayTypeSyntax node)
    {
        return node.Update(
            (TypeSyntax?)Visit(node.ElementType) ?? throw new ArgumentNullException(),
            VisitList(node.RankSpecifiers)
        );
    }

    public override SyntaxNode? VisitArrayRankSpecifier(ArrayRankSpecifierSyntax node)
    {
        return node.Update(
            VisitToken(node.OpenBracketToken),
            VisitList(node.Sizes),
            VisitToken(node.CloseBracketToken)
        );
    }

    public override SyntaxNode? VisitParenthesizedExpression(ParenthesizedExpressionSyntax node)
    {
        return node.Update(
            VisitToken(node.OpenParenToken),
            (ExpressionSyntax?)Visit(node.Expression) ?? throw new ArgumentNullException(),
            VisitToken(node.CloseParenToken)
        );
    }

    public override SyntaxNode? VisitPrefixUnaryExpression(PrefixUnaryExpressionSyntax node)
    {
        return node.Update(
            VisitToken(node.OperatorToken),
            (ExpressionSyntax?)Visit(node.Operand) ?? throw new ArgumentNullException()
        );
    }

    public override SyntaxNode? VisitPostfixUnaryExpression(PostfixUnaryExpressionSyntax node)
    {
        return node.Update(
            (ExpressionSyntax?)Visit(node.Operand) ?? throw new ArgumentNullException(),
            VisitToken(node.OperatorToken)
        );
    }

    public override SyntaxNode? VisitMemberAccessExpression(MemberAccessExpressionSyntax node)
    {
        return node.Update(
            (ExpressionSyntax?)Visit(node.Expression) ?? throw new ArgumentNullException(),
            VisitToken(node.OperatorToken),
            (SimpleNameSyntax?)Visit(node.Name) ?? throw new ArgumentNullException()
        );
    }

    public override SyntaxNode? VisitBinaryExpression(BinaryExpressionSyntax node)
    {
        return node.Update(
            (ExpressionSyntax?)Visit(node.Left) ?? throw new ArgumentNullException(),
            VisitToken(node.OperatorToken),
            (ExpressionSyntax?)Visit(node.Right) ?? throw new ArgumentNullException()
        );
    }

    public override SyntaxNode? VisitAssignmentExpression(AssignmentExpressionSyntax node)
    {
        return node.Update(
            (ExpressionSyntax?)Visit(node.Left) ?? throw new ArgumentNullException(),
            VisitToken(node.OperatorToken),
            (ExpressionSyntax?)Visit(node.Right) ?? throw new ArgumentNullException()
        );
    }

    public override SyntaxNode? VisitConditionalExpression(ConditionalExpressionSyntax node)
    {
        return node.Update(
            (ExpressionSyntax?)Visit(node.Condition) ?? throw new ArgumentNullException(),
            VisitToken(node.QuestionToken),
            (ExpressionSyntax?)Visit(node.WhenTrue) ?? throw new ArgumentNullException(),
            VisitToken(node.ColonToken),
            (ExpressionSyntax?)Visit(node.WhenFalse) ?? throw new ArgumentNullException()
        );
    }

    public override SyntaxNode? VisitLiteralExpression(LiteralExpressionSyntax node)
    {
        return node.Update(VisitToken(node.Token));
    }

    public override SyntaxNode? VisitInvocationExpression(InvocationExpressionSyntax node)
    {
        return node.Update(
            (ExpressionSyntax?)Visit(node.Expression) ?? throw new ArgumentNullException(),
            (ArgumentListSyntax?)Visit(node.ArgumentList) ?? throw new ArgumentNullException()
        );
    }

    public override SyntaxNode? VisitElementAccessExpression(ElementAccessExpressionSyntax node)
    {
        return node.Update(
            (ExpressionSyntax?)Visit(node.Expression) ?? throw new ArgumentNullException(),
            (BracketedArgumentListSyntax?)Visit(node.ArgumentList) ?? throw new ArgumentNullException()
        );
    }

    public override SyntaxNode? VisitArgumentList(ArgumentListSyntax node)
    {
        return node.Update(
            VisitToken(node.OpenParenToken),
            VisitList(node.Arguments),
            VisitToken(node.CloseParenToken)
        );
    }

    public override SyntaxNode? VisitBracketedArgumentList(BracketedArgumentListSyntax node)
    {
        return node.Update(
            VisitToken(node.OpenBracketToken),
            VisitList(node.Arguments),
            VisitToken(node.CloseBracketToken)
        );
    }

    public override SyntaxNode? VisitArgument(ArgumentSyntax node)
    {
        return node.Update(
            VisitToken(node.RedKindKeyword),
            (ExpressionSyntax?)Visit(node.Expression) ?? throw new ArgumentNullException()
        );
    }

    public override SyntaxNode? VisitCastExpression(CastExpressionSyntax node)
    {
        return node.Update(
            VisitToken(node.OpenParenToken),
            (TypeSyntax?)Visit(node.Type) ?? throw new ArgumentNullException(),
            VisitToken(node.CloseParenToken),
            (ExpressionSyntax?)Visit(node.Expression) ?? throw new ArgumentNullException()
        );
    }

    public override SyntaxNode? VisitInitializerExpression(InitializerExpressionSyntax node)
    {
        return node.Update(
            VisitToken(node.OpenBraceToken),
            VisitList(node.Expressions),
            VisitToken(node.CloseBraceToken)
        );
    }

    public override SyntaxNode? VisitArrayCreationExpression(ArrayCreationExpressionSyntax node)
    {
        return node.Update(
            (ArrayTypeSyntax?)Visit(node.Type) ?? throw new ArgumentNullException(),
            (InitializerExpressionSyntax?)Visit(node.Initializer)
        );
    }

    public override SyntaxNode? VisitBlock(BlockSyntax node)
    {
        return node.Update(
            VisitList(node.AttributeLists),
            VisitToken(node.OpenBraceToken),
            VisitList(node.Statements),
            VisitToken(node.CloseBraceToken)
        );
    }

    public override SyntaxNode? VisitLocalDeclaration(LocalDeclarationStatementSyntax node)
    {
        return node.Update(
            VisitList(node.AttributeLists),
            VisitList(node.Modifiers),
            (VariableDeclarationSyntax?)Visit(node.Declaration) ?? throw new ArgumentNullException(),
            VisitToken(node.SemicolonToken)
        );
    }

    public override SyntaxNode? VisitVariableDeclaration(VariableDeclarationSyntax node)
    {
        return node.Update(
            (TypeSyntax?)Visit(node.Type) ?? throw new ArgumentNullException(),
            VisitList(node.Variables)
        );
    }

    public override SyntaxNode? VisitVariableDeclarator(VariableDeclaratorSyntax node)
    {
        return node.Update(
            VisitToken(node.Identifier),
            (EqualsValueClauseSyntax?)Visit(node.Initializer)
        );
    }

    public override SyntaxNode? VisitEqualsValueClause(EqualsValueClauseSyntax node)
    {
        return node.Update(
            VisitToken(node.EqualsToken),
            (ExpressionSyntax?)Visit(node.Expression) ?? throw new ArgumentNullException()
        );
    }

    public override SyntaxNode? VisitExpressionStatement(ExpressionStatementSyntax node)
    {
        return node.Update(
            VisitList(node.AttributeLists),
            (ExpressionSyntax?)Visit(node.Expression) ?? throw new ArgumentNullException(),
            VisitToken(node.SemicolonToken)
        );
    }

    public override SyntaxNode? VisitEmptyStatement(EmptyStatementSyntax node)
    {
        return node.Update(
            VisitList(node.AttributeLists),
            VisitToken(node.SemicolonToken)
        );
    }

    public override SyntaxNode? VisitBreakStatement(BreakStatementSyntax node)
    {
        return node.Update(
            VisitList(node.AttributeLists),
            VisitToken(node.BreakKeyword),
            VisitToken(node.SemicolonToken)
        );
    }

    public override SyntaxNode? VisitContinueStatement(ContinueStatementSyntax node)
    {
        return node.Update(
            VisitList(node.AttributeLists),
            VisitToken(node.ContinueKeyword),
            VisitToken(node.SemicolonToken)
        );
    }

    public override SyntaxNode? VisitReturnStatement(ReturnStatementSyntax node)
    {
        return node.Update(
            VisitList(node.AttributeLists),
            VisitToken(node.ReturnKeyword),
            (ExpressionSyntax?)Visit(node.Expression),
            VisitToken(node.SemicolonToken)
        );
    }

    public override SyntaxNode? VisitWhileStatement(WhileStatementSyntax node)
    {
        return node.Update(
            VisitList(node.AttributeLists),
            VisitToken(node.WhileKeyword),
            VisitToken(node.OpenParenToken),
            (ExpressionSyntax?)Visit(node.Condition) ?? throw new ArgumentNullException(),
            VisitToken(node.CloseParenToken),
            (StatementSyntax?)Visit(node.Statement) ?? throw new ArgumentNullException()
        );
    }

    public override SyntaxNode? VisitDoStatement(DoStatementSyntax node)
    {
        return node.Update(
            VisitList(node.AttributeLists),
            VisitToken(node.DoKeyword),
            (StatementSyntax?)Visit(node.Statement) ?? throw new ArgumentNullException(),
            VisitToken(node.WhileKeyword),
            VisitToken(node.OpenParenToken),
            (ExpressionSyntax?)Visit(node.Condition) ?? throw new ArgumentNullException(),
            VisitToken(node.CloseParenToken),
            VisitToken(node.SemicolonToken)
        );
    }

    public override SyntaxNode? VisitForStatement(ForStatementSyntax node)
    {
        return node.Update(
            VisitList(node.AttributeLists),
            VisitToken(node.ForKeyword),
            VisitToken(node.OpenParenToken),
            (VariableDeclarationSyntax?)Visit(node.Declaration),
            VisitList(node.Initializers),
            VisitToken(node.FirstSemicolonToken),
            (ExpressionSyntax?)Visit(node.Condition),
            VisitToken(node.SecondSemicolonToken),
            VisitList(node.Incrementors),
            VisitToken(node.CloseParenToken),
            (StatementSyntax?)Visit(node.Statement) ?? throw new ArgumentNullException()
        );
    }

    public override SyntaxNode? VisitIfStatement(IfStatementSyntax node)
    {
        return node.Update(
            VisitList(node.AttributeLists),
            VisitToken(node.IfKeyword),
            VisitToken(node.OpenParenToken),
            (ExpressionSyntax?)Visit(node.Condition) ?? throw new ArgumentNullException(),
            VisitToken(node.CloseParenToken),
            (StatementSyntax?)Visit(node.Statement) ?? throw new ArgumentNullException(),
            (ElseClauseSyntax?)Visit(node.Else)
        );
    }

    public override SyntaxNode? VisitElseClause(ElseClauseSyntax node)
    {
        return node.Update(
            VisitToken(node.ElseKeyword),
            (StatementSyntax?)Visit(node.Statement) ?? throw new ArgumentNullException()
        );
    }

    public override SyntaxNode? VisitSwitchStatement(SwitchStatementSyntax node)
    {
        return node.Update(
            VisitList(node.AttributeLists),
            VisitToken(node.SwitchKeyword),
            VisitToken(node.OpenParenToken),
            (ExpressionSyntax?)Visit(node.Expression) ?? throw new ArgumentNullException(),
            VisitToken(node.CloseParenToken),
            VisitToken(node.OpenBraceToken),
            VisitList(node.Sections),
            VisitToken(node.CloseBraceToken)
        );
    }

    public override SyntaxNode? VisitSwitchSection(SwitchSectionSyntax node)
    {
        return node.Update(
            VisitList(node.Labels),
            VisitList(node.Statements)
        );
    }

    public override SyntaxNode? VisitCaseSwitchLabel(CaseSwitchLabelSyntax node)
    {
        return node.Update(
            VisitToken(node.CaseKeyword),
            (ExpressionSyntax?)Visit(node.Value) ?? throw new ArgumentNullException(),
            VisitToken(node.ColonToken)
        );
    }

    public override SyntaxNode? VisitDefaultSwitchLabel(DefaultSwitchLabelSyntax node)
    {
        return node.Update(
            VisitToken(node.DefaultKeyword),
            VisitToken(node.ColonToken)
        );
    }

    public override SyntaxNode? VisitCompilationUnit(CompilationUnitSyntax node)
    {
        return node.Update(
            VisitList(node.Members),
            VisitToken(node.EndOfFileToken)
        );
    }

    public override SyntaxNode? VisitAttributeList(AttributeListSyntax node)
    {
        return node.Update(
            VisitToken(node.OpenBracketToken),
            VisitList(node.Attributes),
            VisitToken(node.CloseBracketToken)
        );
    }

    public override SyntaxNode? VisitAttribute(AttributeSyntax node)
    {
        return node.Update(
            (NameSyntax?)Visit(node.Name) ?? throw new ArgumentNullException(),
            (AttributeArgumentListSyntax?)Visit(node.ArgumentList)
        );
    }

    public override SyntaxNode? VisitAttributeArgumentList(AttributeArgumentListSyntax node)
    {
        return node.Update(
            VisitToken(node.CloseParenToken),
            VisitList(node.Arguments),
            VisitToken(node.CloseParenToken)
        );
    }

    public override SyntaxNode? VisitAttributeArgument(AttributeArgumentSyntax node)
    {
        return node.Update((ExpressionSyntax?)node.Expression ?? throw new ArgumentNullException());
    }

    public override SyntaxNode? VisitNameEquals(NameEqualsSyntax node)
    {
        return node.Update(
            (IdentifierNameSyntax?)Visit(node.Name) ?? throw new ArgumentNullException(),
            VisitToken(node.EqualsToken)
        );
    }

    public override SyntaxNode? VisitStructDeclaration(StructDeclarationSyntax node)
    {
        return node.Update(
            VisitToken(node.Keyword),
            VisitToken(node.Identifier),
            VisitToken(node.CloseBraceToken),
            VisitList(node.Members),
            VisitToken(node.CloseBraceToken),
            VisitToken(node.SemicolonToken)
        );
    }

    public override SyntaxNode? VisitTechniqueDeclaration(TechniqueDeclarationSyntax node)
    {
        return node.Update(
            VisitToken(node.Keyword),
            VisitToken(node.Identifier),
            VisitToken(node.OpenBraceToken),
            VisitList(node.Members),
            VisitToken(node.CloseBraceToken)
        );
    }

    public override SyntaxNode? VisitPassDeclaration(PassDeclarationSyntax node)
    {
        return node.Update(
            VisitToken(node.Keyword),
            VisitToken(node.Identifier),
            VisitToken(node.OpenBraceToken),
            VisitList(node.Statements),
            VisitToken(node.CloseBraceToken)
        );
    }

    public override SyntaxNode? VisitFieldDeclaration(FieldDeclarationSyntax node)
    {
        return node.Update(
            (TypeSyntax?)Visit(node.Type) ?? throw new ArgumentNullException(),
            VisitToken(node.Identifier),
            (BracketedArgumentListSyntax?)Visit(node.Arguments),
            (SemanticSyntax?)Visit(node.Semantics),
            (EqualsValueClauseSyntax?)Visit(node.Initializer),
            VisitToken(node.SemicolonToken)
        );
    }

    public override SyntaxNode? VisitMethodDeclaration(MethodDeclarationSyntax node)
    {
        return node.Update(
            VisitList(node.AttributeLists),
            (TypeSyntax?)Visit(node.ReturnType) ?? throw new ArgumentNullException(),
            VisitToken(node.Identifier),
            (ParameterListSyntax?)Visit(node.ParameterList) ?? throw new ArgumentNullException(),
            (SemanticSyntax?)Visit(node.ReturnSemantics),
            (BlockSyntax?)Visit(node.Body) ?? throw new ArgumentNullException()
        );
    }

    public override SyntaxNode? VisitSemantics(SemanticSyntax node)
    {
        return node.Update(
            VisitToken(node.ColonToken),
            (IdentifierNameSyntax?)Visit(node.Identifier) ?? throw new ArgumentNullException()
        );
    }

    public override SyntaxNode? VisitParameterList(ParameterListSyntax node)
    {
        return node.Update(
            VisitToken(node.OpenParenToken),
            VisitList(node.Parameters),
            VisitToken(node.CloseParenToken)
        );
    }

    public override SyntaxNode? VisitParameter(ParameterSyntax node)
    {
        return node.Update(
            VisitList(node.AttributeLists),
            VisitList(node.Modifiers),
            (TypeSyntax?)Visit(node.Type) ?? throw new ArgumentNullException(),
            VisitToken(node.Identifier),
            (EqualsValueClauseSyntax?)Visit(node.Default),
            (SemanticSyntax?)Visit(node.Semantics)
        );
    }

    public virtual SyntaxToken VisitToken(SyntaxToken token)
    {
        var node = token.Node;
        if (node == null)
            return token;

        var leadingTrivia = node.GetLeadingTrivia();
        var trailingTrivia = node.GetTrailingTrivia();

        if (leadingTrivia != null)
        {
            var leading = VisitList(new SyntaxTriviaList(token, leadingTrivia));

            if (trailingTrivia != null)
            {
                var index = leadingTrivia.IsList ? leadingTrivia.SlotCount : 1;
                var trailing = VisitList(new SyntaxTriviaList(token, trailingTrivia, token.Position + node.FullWidth - trailingTrivia.FullWidth, index));

                if (leading.Node != leadingTrivia)
                    token = token.WithLeadingTrivia(leading);

                return trailing.Node != trailingTrivia ? token.WithTrailingTrivia(trailing) : token;
            }

            return leading.Node != leadingTrivia ? token.WithLeadingTrivia(leading) : token;
        }

        if (trailingTrivia != null)
        {
            var trailing = VisitList(new SyntaxTriviaList(token, trailingTrivia, token.Position + node.FullWidth - trailingTrivia.FullWidth));
            return trailing.Node != trailingTrivia ? token.WithTrailingTrivia(trailing) : token;
        }

        return token;
    }

    public virtual SyntaxTrivia VisitTrivia(SyntaxTrivia trivia)
    {
        /*
        if (this.VisitIntoStructuredTrivia && trivia.HasStructure)
        {
            var structure = (HlslSyntaxNode)trivia.GetStructure()!;
            var newStructure = (StructuredTriviaSyntax?)this.Visit(structure);
            if (newStructure != structure)
            {
                if (newStructure != null)
                    return SyntaxFactory.Trivia(newStructure);
                return default;
            }
        }
        */

        return trivia;
    }

    public virtual SyntaxList<TNode> VisitList<TNode>(SyntaxList<TNode> list) where TNode : SyntaxNode
    {
        SyntaxListBuilder? alternate = null;
        for (int i = 0, n = list.Count; i < n; i++)
        {
            var item = list[i];
            var visited = VisitListElement(item);
            if (item != visited && alternate == null)
            {
                alternate = new SyntaxListBuilder(n);
                alternate.AddRange(list, 0, i);
            }

            if (alternate != null && visited != null && visited.RawKind != (int)SyntaxKind.None)
                alternate.Add(visited);
        }

        if (alternate != null) return alternate.ToList();

        return list;
    }

    public virtual TNode? VisitListElement<TNode>(TNode? node) where TNode : SyntaxNode
    {
        return (TNode?)Visit(node);
    }

    public virtual SeparatedSyntaxList<TNode> VisitList<TNode>(SeparatedSyntaxList<TNode> list) where TNode : SyntaxNode
    {
        var count = list.Count;
        var sepCount = list.SeparatedCount;

        SeparatedSyntaxListBuilder<TNode> alternate = default;

        var i = 0;
        for (; i < sepCount; i++)
        {
            var node = list[i];
            var visitedNode = VisitListElement(node);

            var separator = list.GetSeparator(i);
            var visitedSeparator = VisitListSeparator(separator);

            if (alternate.IsNull)
                if (node != visitedNode || separator != visitedSeparator)
                {
                    alternate = new SeparatedSyntaxListBuilder<TNode>(count);
                    alternate.AddRange(list, i);
                }

            if (!alternate.IsNull)
            {
                if (visitedNode != null)
                {
                    alternate.Add(visitedNode);

                    if (visitedSeparator.RawKind == (int)SyntaxKind.None) throw new InvalidOperationException();
                    alternate.AddSeparator(visitedSeparator);
                }
                else
                {
                    if (visitedNode == null) throw new InvalidOperationException();
                }
            }
        }

        if (i < count)
        {
            var node = list[i];
            var visitedNode = VisitListElement(node);

            if (alternate.IsNull)
                if (node != visitedNode)
                {
                    alternate = new SeparatedSyntaxListBuilder<TNode>(count);
                    alternate.AddRange(list, i);
                }

            if (!alternate.IsNull && visitedNode != null) alternate.Add(visitedNode);
        }

        if (!alternate.IsNull)
            return alternate.ToList();

        return list;
    }

    public virtual SyntaxToken VisitListSeparator(SyntaxToken separator)
    {
        return VisitToken(separator);
    }

    public virtual SyntaxTokenList VisitList(SyntaxTokenList list)
    {
        SyntaxTokenListBuilder? alternate = null;
        var count = list.Count;
        var index = -1;

        foreach (var item in list)
        {
            index++;
            var visited = VisitToken(item);
            if (item != visited && alternate == null)
            {
                alternate = new SyntaxTokenListBuilder(count);
                alternate.Add(list, 0, index);
            }

            if (alternate != null && visited.RawKind != (int)SyntaxKind.None) //skip the null check since SyntaxToken is a value type
                alternate.Add(visited);
        }

        if (alternate != null) return alternate.ToList();

        return list;
    }

    public virtual SyntaxTriviaList VisitList(SyntaxTriviaList list)
    {
        var count = list.Count;
        if (count != 0)
        {
            SyntaxTriviaListBuilder? alternate = null;
            var index = -1;

            foreach (var item in list)
            {
                index++;
                var visited = VisitListElement(item);

                //skip the null check since SyntaxTrivia is a value type
                if (visited != item && alternate == null)
                {
                    alternate = new SyntaxTriviaListBuilder(count);
                    alternate.Add(list, 0, index);
                }

                if (alternate != null && visited.RawKind != (int)SyntaxKind.None) alternate.Add(visited);
            }

            if (alternate != null) return alternate.ToList();
        }

        return list;
    }

    public virtual SyntaxTrivia VisitListElement(SyntaxTrivia element)
    {
        return VisitTrivia(element);
    }
}