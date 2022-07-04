// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.Hlsl.Syntax;

namespace SharpX.Hlsl;

public abstract class HlslSyntaxVisitor<TResult>
{
    public virtual TResult? VisitIdentifierName(IdentifierNameSyntax node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitGenericName(GenericNameSyntax node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitTypeArgumentList(TypeArgumentListSyntax node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitPredefinedType(PredefinedTypeSyntax node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitArrayType(ArrayTypeSyntax node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitArrayRankSpecifier(ArrayRankSpecifierSyntax node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitParenthesizedExpression(ParenthesizedExpressionSyntax node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitPrefixUnaryExpression(PrefixUnaryExpressionSyntax node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitPostfixUnaryExpression(PostfixUnaryExpressionSyntax node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitMemberAccessExpression(MemberAccessExpressionSyntax node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitBinaryExpression(BinaryExpressionSyntax node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitAssignmentExpression(AssignmentExpressionSyntax node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitConditionalExpression(ConditionalExpressionSyntax node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitLiteralExpression(LiteralExpressionSyntax node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitInvocationExpression(InvocationExpressionSyntax node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitElementAccessExpression(ElementAccessExpressionSyntax node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitArgumentList(ArgumentListSyntax node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitBracketedArgumentList(BracketedArgumentListSyntax node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitArgument(ArgumentSyntax node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitCastExpression(CastExpressionSyntax node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitInitializerExpression(InitializerExpressionSyntax node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitArrayCreationExpression(ArrayCreationExpressionSyntax node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitBlock(BlockSyntax node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitLocalDeclaration(LocalDeclarationStatementSyntax node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitVariableDeclaration(VariableDeclarationSyntax node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitVariableDeclarator(VariableDeclaratorSyntax node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitEqualsValueClause(EqualsValueClauseSyntax node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitExpressionStatement(ExpressionStatementSyntax node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitEmptyStatement(EmptyStatementSyntax node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitBreakStatement(BreakStatementSyntax node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitContinueStatement(ContinueStatementSyntax node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitReturnStatement(ReturnStatementSyntax node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitWhileStatement(WhileStatementSyntax node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitDoStatement(DoStatementSyntax node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitForStatement(ForStatementSyntax node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitIfStatement(IfStatementSyntax node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitElseClause(ElseClauseSyntax node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitSwitchStatement(SwitchStatementSyntax node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitSwitchSection(SwitchSectionSyntax node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitCaseSwitchLabel(CaseSwitchLabelSyntax node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitDefaultSwitchLabel(DefaultSwitchLabelSyntax node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitCompilationUnit(CompilationUnitSyntax node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitAttributeList(AttributeListSyntax node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitAttribute(AttributeSyntax node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitAttributeArgumentList(AttributeArgumentListSyntax node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitAttributeArgument(AttributeArgumentSyntax node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitNameEquals(NameEqualsSyntax node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitTopLevelModule(TopLevelModuleSyntax node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitStructDeclaration(StructDeclarationSyntax node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitTechniqueDeclaration(TechniqueDeclarationSyntax node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitPassDeclaration(PassDeclarationSyntax node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitConstantBufferDeclaration(ConstantBufferDeclarationSyntax node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitFieldDeclaration(FieldDeclarationSyntax node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitMethodDeclaration(MethodDeclarationSyntax node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitSemantics(SemanticSyntax node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitRegister(RegisterSyntax node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitParameterList(ParameterListSyntax node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitParameter(ParameterSyntax node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitIncludeDirective(IncludeDirectiveSyntax node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitPragmaDirectiveTrivia(PragmaDirectiveTriviaSyntax node)
    {
        return DefaultVisit(node);
    }

    #region Base Implementations

    public virtual TResult? Visit(SyntaxNode? node)
    {
        if (node != null)
            return ((HlslSyntaxNode)node).Accept(this);

        return default;
    }

    public virtual TResult? DefaultVisit(SyntaxNode node)
    {
        return default;
    }

    #endregion
}