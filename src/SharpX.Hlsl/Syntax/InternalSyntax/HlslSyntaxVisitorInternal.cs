// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

namespace SharpX.Hlsl.Syntax.InternalSyntax;

internal abstract class HlslSyntaxVisitorInternal<TResult>
{
    public virtual TResult? VisitIdentifierName(IdentifierNameSyntaxInternal node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitGenericName(GenericNameSyntaxInternal node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitTypeArgumentList(TypeArgumentListSyntaxInternal node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitPredefinedType(PredefinedTypeSyntaxInternal node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitArrayType(ArrayTypeSyntaxInternal node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitArrayRankSpecifier(ArrayRankSpecifierSyntaxInternal node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitParenthesizedExpression(ParenthesizedExpressionSyntaxInternal node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitPrefixUnaryExpression(PrefixUnaryExpressionSyntaxInternal node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitPostfixUnaryExpression(PostfixUnaryExpressionSyntaxInternal node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitMemberAccessExpression(MemberAccessExpressionSyntaxInternal node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitBinaryExpression(BinaryExpressionSyntaxInternal node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitAssignmentExpression(AssignmentExpressionSyntaxInternal node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitConditionalExpression(ConditionalExpressionSyntaxInternal node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitLiteralExpression(LiteralExpressionSyntaxInternal node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitInvocationExpression(InvocationExpressionSyntaxInternal node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitElementAccessExpression(ElementAccessExpressionSyntaxInternal node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitArgumentList(ArgumentListSyntaxInternal node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitBracketedArgumentList(BracketedArgumentListSyntaxInternal node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitArgument(ArgumentSyntaxInternal node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitCastExpression(CastExpressionSyntaxInternal node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitInitializerExpression(InitializerExpressionSyntaxInternal node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitArrayCreationExpression(ArrayCreationExpressionSyntaxInternal node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitBlock(BlockSyntaxInternal node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitLocalDeclaration(LocalDeclarationStatementSyntaxInternal node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitVariableDeclaration(VariableDeclarationSyntaxInternal node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitEqualsValueClause(EqualsValueClauseSyntaxInternal node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitExpressionStatement(ExpressionStatementSyntaxInternal node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitEmptyStatement(EmptyStatementSyntaxInternal node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitBreakStatement(BreakStatementSyntaxInternal node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitContinueStatement(ContinueStatementSyntaxInternal node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitReturnStatement(ReturnStatementSyntaxInternal node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitWhileStatement(WhileStatementSyntaxInternal node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitDoStatement(DoStatementSyntaxInternal node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitForStatement(ForStatementSyntaxInternal node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitIfStatement(IfStatementSyntaxInternal node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitElseClause(ElseClauseSyntaxInternal node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitSwitchStatement(SwitchStatementSyntaxInternal node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitSwitchSection(SwitchSectionSyntaxInternal node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitCaseSwitchLabel(CaseSwitchLabelSyntaxInternal node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitDefaultSwitchLabel(DefaultSwitchLabelSyntaxInternal node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitCompilationUnit(CompilationUnitSyntaxInternal node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitAttributeList(AttributeListSyntaxInternal node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitAttribute(AttributeSyntaxInternal node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitAttributeArgumentList(AttributeArgumentListSyntaxInternal node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitAttributeArgument(AttributeArgumentSyntaxInternal node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitNameEquals(NameEqualsSyntaxInternal node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitStructDeclaration(StructDeclarationSyntaxInternal node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitTechniqueDeclaration(TechniqueDeclarationSyntaxInternal node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitPassDeclaration(PassDeclarationSyntaxInternal node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitFieldDeclaration(FieldDeclarationSyntaxInternal node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitMethodDeclaration(MethodDeclarationSyntaxInternal node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitSemantics(SemanticSyntaxInternal node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitRegister(RegisterSyntaxInternal node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitParameterList(ParameterListSyntaxInternal node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitParameter(ParameterSyntaxInternal node)
    {
        return DefaultVisit(node);
    }

    #region Base Implementations

    public virtual TResult? Visit(HlslSyntaxNodeInternal? node)
    {
        if (node == null)
            return default;

        return node.Accept(this);
    }

    public virtual TResult? VisitToken(SyntaxTokenInternal token)
    {
        return DefaultVisit(token);
    }

    public virtual TResult? VisitTrivia(SyntaxTriviaInternal trivia)
    {
        return DefaultVisit(trivia);
    }

    public virtual TResult? DefaultVisit(HlslSyntaxNodeInternal node)
    {
        return default;
    }

    #endregion
}