// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core.Syntax.InternalSyntax;

namespace SharpX.Hlsl.Syntax.InternalSyntax;

internal static partial class SyntaxFactory
{
    public static IdentifierNameSyntaxInternal IdentifierName(SyntaxTokenInternal identifier)
    {
        if (identifier.Kind != SyntaxKind.IdentifierToken)
            throw new ArgumentException(nameof(identifier));

        return new IdentifierNameSyntaxInternal(SyntaxKind.IdentifierName, identifier);
    }

    public static GenericNameSyntaxInternal GenericName(SyntaxTokenInternal identifier, TypeArgumentListSyntaxInternal typeArgumentList)
    {
        if (identifier.Kind != SyntaxKind.IdentifierToken)
            throw new ArgumentException(nameof(identifier));

        return new GenericNameSyntaxInternal(SyntaxKind.GenericName, identifier, typeArgumentList);
    }

    public static TypeArgumentListSyntaxInternal TypeArgumentList(SyntaxTokenInternal lessThanToken, SeparatedSyntaxListInternal<TypeSyntaxInternal> arguments, SyntaxTokenInternal greaterThanToken)
    {
        if (lessThanToken.Kind != SyntaxKind.LessThanToken)
            throw new ArgumentException(nameof(lessThanToken));
        if (greaterThanToken.Kind != SyntaxKind.GreaterThanToken)
            throw new ArgumentException(nameof(greaterThanToken));

        return new TypeArgumentListSyntaxInternal(SyntaxKind.TypeArgumentList, lessThanToken, arguments.Node, greaterThanToken);
    }

    public static PredefinedTypeSyntaxInternal PredefinedType(SyntaxTokenInternal keyword)
    {
        switch (keyword.Kind)
        {
            case SyntaxKind.BoolKeyword:
            case SyntaxKind.FloatKeyword:
            case SyntaxKind.DoubleKeyword:
            case SyntaxKind.HalfKeyword:
            case SyntaxKind.IntKeyword:
            case SyntaxKind.Min10FloatKeyword:
            case SyntaxKind.Min16FloatKeyword:
            case SyntaxKind.Min12IntKeyword:
            case SyntaxKind.Min16IntKeyword:
            case SyntaxKind.Min16UintKeyword:
            case SyntaxKind.UintKeyword:
            case SyntaxKind.VoidKeyword:
                break;

            default:
                throw new ArgumentException(nameof(keyword));
        }

        return new PredefinedTypeSyntaxInternal(SyntaxKind.PredefinedType, keyword);
    }

    public static ArrayTypeSyntaxInternal ArrayType(TypeSyntaxInternal elementType, SyntaxListInternal<ArrayRankSpecifierSyntaxInternal> rankSpecifiers)
    {
        return new ArrayTypeSyntaxInternal(SyntaxKind.ArrayType, elementType, rankSpecifiers.Node);
    }

    public static ArrayRankSpecifierSyntaxInternal ArrayRankSpecifier(SyntaxTokenInternal openBracketToken, SeparatedSyntaxListInternal<ExpressionSyntaxInternal> sizes, SyntaxTokenInternal closeBracketToken)
    {
        if (openBracketToken.Kind != SyntaxKind.OpenBracketToken)
            throw new ArgumentException(nameof(openBracketToken));
        if (closeBracketToken.Kind != SyntaxKind.CloseBracketToken)
            throw new ArgumentException(nameof(closeBracketToken));

        return new ArrayRankSpecifierSyntaxInternal(SyntaxKind.ArrayRankSpecifier, openBracketToken, sizes.Node, closeBracketToken);
    }

    public static ParenthesizedExpressionSyntaxInternal ParenthesizedExpression(SyntaxTokenInternal openParenToken, ExpressionSyntaxInternal expression, SyntaxTokenInternal closeParenToken)
    {
        if (openParenToken.Kind != SyntaxKind.OpenParenToken)
            throw new ArgumentException(nameof(openParenToken));
        if (closeParenToken.Kind != SyntaxKind.CloseParenToken)
            throw new ArgumentException(nameof(closeParenToken));

        return new ParenthesizedExpressionSyntaxInternal(SyntaxKind.ParenthesizedExpression, openParenToken, expression, closeParenToken);
    }

    public static PrefixUnaryExpressionSyntaxInternal PrefixUnaryExpression(SyntaxKind kind, SyntaxTokenInternal operatorToken, ExpressionSyntaxInternal operand)
    {
        switch (kind)
        {
            case SyntaxKind.UnaryPlusExpression:
            case SyntaxKind.UnaryMinusExpression:
            case SyntaxKind.BitwiseNotExpression:
            case SyntaxKind.LogicalNotExpression:
            case SyntaxKind.PreIncrementExpression:
            case SyntaxKind.PreDecrementExpression:
            case SyntaxKind.IndexExpression:
                break;

            default:
                throw new ArgumentException(nameof(kind));
        }

        switch (operatorToken.Kind)
        {
            case SyntaxKind.PlusToken:
            case SyntaxKind.MinusToken:
            case SyntaxKind.TildeToken:
            case SyntaxKind.ExclamationToken:
            case SyntaxKind.PlusPlusToken:
            case SyntaxKind.MinusMinusToken:
            case SyntaxKind.AmpersandToken:
            case SyntaxKind.AsteriskToken:
            case SyntaxKind.CaretToken:
                break;

            default:
                throw new ArgumentException(nameof(operatorToken));
        }

        return new PrefixUnaryExpressionSyntaxInternal(kind, operatorToken, operand);
    }

    public static PostfixUnaryExpressionSyntaxInternal PostfixUnaryExpression(SyntaxKind kind, ExpressionSyntaxInternal operand, SyntaxTokenInternal operatorToken)
    {
        switch (kind)
        {
            case SyntaxKind.PostIncrementExpression:
            case SyntaxKind.PostDecrementExpression:
                break;

            default:
                throw new ArgumentException(nameof(kind));
        }

        switch (operatorToken.Kind)
        {
            case SyntaxKind.PlusPlusToken:
            case SyntaxKind.MinusMinusToken:
                break;

            default:
                throw new ArgumentException(nameof(operatorToken));
        }

        return new PostfixUnaryExpressionSyntaxInternal(kind, operand, operatorToken);
    }

    public static MemberAccessExpressionSyntaxInternal MemberAccessExpression(ExpressionSyntaxInternal expression, SyntaxTokenInternal dotToken, SimpleNameSyntaxInternal name)
    {
        if (dotToken.Kind != SyntaxKind.DotToken)
            throw new ArgumentException(nameof(dotToken));

        return new MemberAccessExpressionSyntaxInternal(SyntaxKind.SimpleMemberAccessExpression, expression, dotToken, name);
    }

    public static BinaryExpressionSyntaxInternal BinaryExpression(SyntaxKind kind, ExpressionSyntaxInternal left, SyntaxTokenInternal operatorToken, ExpressionSyntaxInternal right)
    {
        switch (kind)
        {
            case SyntaxKind.AddExpression:
            case SyntaxKind.SubtractExpression:
            case SyntaxKind.MultiplyExpression:
            case SyntaxKind.DivideExpression:
            case SyntaxKind.ModuloExpression:
            case SyntaxKind.LogicalOrExpression:
            case SyntaxKind.LogicalAndExpression:
            case SyntaxKind.BitwiseOrExpression:
            case SyntaxKind.BitwiseAndExpression:
            case SyntaxKind.ExclusiveOrExpression:
            case SyntaxKind.EqualsExpression:
            case SyntaxKind.NotEqualsExpression:
            case SyntaxKind.LessThanExpression:
            case SyntaxKind.LessThanOrEqualExpression:
            case SyntaxKind.GreaterThanExpression:
            case SyntaxKind.GreaterThanOrEqualExpression:
                break;

            default:
                throw new ArgumentException(nameof(kind));
        }

        switch (operatorToken.Kind)
        {
            case SyntaxKind.PlusToken:
            case SyntaxKind.MinusToken:
            case SyntaxKind.AsteriskToken:
            case SyntaxKind.SlashToken:
            case SyntaxKind.PercentToken:
            case SyntaxKind.BarBarToken:
            case SyntaxKind.AmpersandAmpersandToken:
            case SyntaxKind.BarToken:
            case SyntaxKind.CaretToken:
            case SyntaxKind.EqualsEqualsToken:
            case SyntaxKind.ExclamationEqualsToken:
            case SyntaxKind.LessThanToken:
            case SyntaxKind.LessThanEqualsToken:
            case SyntaxKind.GreaterThanToken:
            case SyntaxKind.GreaterThanEqualsToken:
                break;

            default:
                throw new ArgumentException(nameof(operatorToken));
        }

        return new BinaryExpressionSyntaxInternal(kind, left, operatorToken, right);
    }

    public static AssignmentExpressionSyntaxInternal AssignmentExpression(SyntaxKind kind, ExpressionSyntaxInternal left, SyntaxTokenInternal operatorToken, ExpressionSyntaxInternal right)
    {
        switch (kind)
        {
            case SyntaxKind.SimpleAssignmentExpression:
            case SyntaxKind.AddAssignmentExpression:
            case SyntaxKind.SubtractAssignmentExpression:
            case SyntaxKind.MultiplyAssignmentExpression:
            case SyntaxKind.DivideAssignmentExpression:
            case SyntaxKind.ModuloAssignmentExpression:
            case SyntaxKind.AndAssignmentExpression:
            case SyntaxKind.ExclusiveOrAssignmentExpression:
            case SyntaxKind.OrAssignmentExpression:
            case SyntaxKind.LeftShiftAssignmentExpression:
            case SyntaxKind.RightShiftAssignmentExpression:
                break;

            default:
                throw new ArgumentException(nameof(kind));
        }

        switch (operatorToken.Kind)
        {
            case SyntaxKind.EqualsToken:
            case SyntaxKind.PlusEqualsToken:
            case SyntaxKind.MinusEqualsToken:
            case SyntaxKind.AsteriskEqualsToken:
            case SyntaxKind.SlashEqualsToken:
            case SyntaxKind.PercentEqualsToken:
            case SyntaxKind.AmpersandEqualsToken:
            case SyntaxKind.CaretEqualsToken:
            case SyntaxKind.BarEqualsToken:
            case SyntaxKind.LessThanLessThanEqualsToken:
            case SyntaxKind.GreaterThanGreaterThanEqualsToken:
                break;

            default:
                throw new ArgumentException(nameof(operatorToken));
        }

        return new AssignmentExpressionSyntaxInternal(kind, left, operatorToken, right);
    }

    public static ConditionalExpressionSyntaxInternal ConditionalExpression(ExpressionSyntaxInternal condition, SyntaxTokenInternal questionToken, ExpressionSyntaxInternal whenTrue, SyntaxTokenInternal colonToken, ExpressionSyntaxInternal whenFalse)
    {
        if (questionToken.Kind != SyntaxKind.QuestionToken)
            throw new ArgumentException(nameof(questionToken));
        if (colonToken.Kind != SyntaxKind.ColonToken)
            throw new ArgumentException(nameof(colonToken));

        return new ConditionalExpressionSyntaxInternal(SyntaxKind.ConditionalExpression, condition, questionToken, whenTrue, colonToken, whenFalse);
    }

    public static LiteralExpressionSyntaxInternal LiteralExpression(SyntaxKind kind, SyntaxTokenInternal token)
    {
        switch (kind)
        {
            case SyntaxKind.NumericLiteralExpression:
            case SyntaxKind.StringLiteralExpression:
            case SyntaxKind.CharacterLiteralExpression:
            case SyntaxKind.TrueLiteralExpression:
            case SyntaxKind.FalseLiteralExpression:
            case SyntaxKind.NullLiteralExpression:
                break;

            default:
                throw new ArgumentException(nameof(kind));
        }

        switch (token.Kind)
        {
            case SyntaxKind.NumericLiteralToken:
            case SyntaxKind.StringLiteralToken:
            case SyntaxKind.CharacterLiteralToken:
            case SyntaxKind.TrueKeyword:
            case SyntaxKind.FalseKeyword:
            case SyntaxKind.NullKeyword:
                break;

            default:
                throw new ArgumentException(nameof(token));
        }

        return new LiteralExpressionSyntaxInternal(kind, token);
    }

    public static InvocationExpressionSyntaxInternal InvocationExpression(ExpressionSyntaxInternal expression, ArgumentListSyntaxInternal argumentList)
    {
        return new InvocationExpressionSyntaxInternal(SyntaxKind.InvocationExpression, expression, argumentList);
    }

    public static ElementAccessExpressionSyntaxInternal ElementAccessExpression(ExpressionSyntaxInternal expression, BracketedArgumentListSyntaxInternal argumentList)
    {
        return new ElementAccessExpressionSyntaxInternal(SyntaxKind.ElementAccessExpression, expression, argumentList);
    }

    public static ArgumentListSyntaxInternal ArgumentList(SyntaxTokenInternal openParenToken, SeparatedSyntaxListInternal<ArgumentSyntaxInternal> arguments, SyntaxTokenInternal closeParenToken)
    {
        if (openParenToken.Kind != SyntaxKind.OpenParenToken)
            throw new ArgumentException(nameof(openParenToken));
        if (closeParenToken.Kind != SyntaxKind.CloseParenToken)
            throw new ArgumentException(nameof(closeParenToken));

        return new ArgumentListSyntaxInternal(SyntaxKind.ArgumentList, openParenToken, arguments.Node, closeParenToken);
    }

    public static BracketedArgumentListSyntaxInternal BracketedArgumentList(SyntaxTokenInternal openBracketToken, SeparatedSyntaxListInternal<ArgumentSyntaxInternal> arguments, SyntaxTokenInternal closeBracketToken)
    {
        if (openBracketToken.Kind != SyntaxKind.OpenBracketToken)
            throw new ArgumentException(nameof(openBracketToken));
        if (closeBracketToken.Kind != SyntaxKind.CloseBracketToken)
            throw new ArgumentException(nameof(closeBracketToken));

        return new BracketedArgumentListSyntaxInternal(SyntaxKind.BracketedArgumentList, openBracketToken, arguments.Node, closeBracketToken);
    }

    public static ArgumentSyntaxInternal Argument(SyntaxTokenInternal? refKindKeyword, ExpressionSyntaxInternal expression)
    {
        if (refKindKeyword != null)
            switch (refKindKeyword.Kind)
            {
                case SyntaxKind.InKeyword:
                case SyntaxKind.OutKeyword:
                case SyntaxKind.InOutKeyword:
                    break;

                default:
                    throw new ArgumentException(nameof(refKindKeyword));
            }

        return new ArgumentSyntaxInternal(SyntaxKind.Argument, refKindKeyword, expression);
    }

    public static CastExpressionSyntaxInternal CastExpression(SyntaxTokenInternal openParenToken, TypeSyntaxInternal type, SyntaxTokenInternal closeParenToken, ExpressionSyntaxInternal expression)
    {
        if (openParenToken.Kind != SyntaxKind.OpenParenToken)
            throw new ArgumentException(nameof(openParenToken));
        if (closeParenToken.Kind != SyntaxKind.CloseParenToken)
            throw new ArgumentException(nameof(closeParenToken));

        return new CastExpressionSyntaxInternal(SyntaxKind.CastExpression, openParenToken, type, closeParenToken, expression);
    }

    public static ArrayCreationExpressionSyntaxInternal ArrayCreationExpression(ArrayTypeSyntaxInternal type, InitializerExpressionSyntaxInternal? initializer)
    {
        return new ArrayCreationExpressionSyntaxInternal(SyntaxKind.ArrayCreationExpression, type, initializer);
    }

    public static InitializerExpressionSyntaxInternal InitializerExpression(SyntaxKind kind, SyntaxTokenInternal openBraceToken, SeparatedSyntaxListInternal<ExpressionSyntaxInternal> expressions, SyntaxTokenInternal closeBraceToken)
    {
        if (openBraceToken.Kind != SyntaxKind.OpenBraceToken)
            throw new ArgumentException(nameof(openBraceToken));
        if (closeBraceToken.Kind != SyntaxKind.CloseBraceToken)
            throw new ArgumentException(nameof(closeBraceToken));

        switch (kind)
        {
            case SyntaxKind.ArrayInitializerExpression:
                break;

            default:
                throw new ArgumentException(nameof(kind));
        }

        return new InitializerExpressionSyntaxInternal(kind, openBraceToken, expressions.Node, closeBraceToken);
    }

    public static BlockSyntaxInternal Block(SyntaxListInternal<AttributeListSyntaxInternal> attributeLists, SyntaxTokenInternal openBraceToken, SyntaxListInternal<StatementSyntaxInternal> statements, SyntaxTokenInternal closeBraceToken)
    {
        if (openBraceToken.Kind != SyntaxKind.OpenBraceToken)
            throw new ArgumentException(nameof(openBraceToken));
        if (closeBraceToken.Kind != SyntaxKind.CloseBraceToken)
            throw new ArgumentException(nameof(closeBraceToken));

        return new BlockSyntaxInternal(SyntaxKind.Block, attributeLists.Node, openBraceToken, statements.Node, closeBraceToken);
    }

    public static LocalDeclarationStatementSyntaxInternal LocalDeclaration(SyntaxListInternal<AttributeListSyntaxInternal> attributeList, SyntaxListInternal<SyntaxTokenInternal> modifiers, VariableDeclarationSyntaxInternal declaration, SyntaxTokenInternal semicolonToken)
    {
        if (semicolonToken.Kind != SyntaxKind.SemicolonToken)
            throw new ArgumentException(nameof(semicolonToken));

        return new LocalDeclarationStatementSyntaxInternal(SyntaxKind.LocalDeclarationStatement, attributeList.Node, modifiers.Node, declaration, semicolonToken);
    }

    public static VariableDeclarationSyntaxInternal VariableDeclaration(TypeSyntaxInternal type, SeparatedSyntaxListInternal<VariableDeclaratorSyntaxInternal> variables)
    {
        return new VariableDeclarationSyntaxInternal(SyntaxKind.VariableDeclaration, type, variables.Node);
    }

    public static VariableDeclaratorSyntaxInternal VariableDeclarator(SyntaxTokenInternal identifier, EqualsValueClauseSyntaxInternal? initializer)
    {
        if (identifier.Kind != SyntaxKind.IdentifierToken)
            throw new ArgumentException(nameof(identifier));

        return new VariableDeclaratorSyntaxInternal(SyntaxKind.VariableDeclarator, identifier, initializer);
    }

    public static EqualsValueClauseSyntaxInternal EqualsValueClause(SyntaxTokenInternal equalsToken, ExpressionSyntaxInternal value)
    {
        if (equalsToken.Kind != SyntaxKind.EqualsToken)
            throw new ArgumentException(nameof(equalsToken));

        return new EqualsValueClauseSyntaxInternal(SyntaxKind.EqualsValueClause, equalsToken, value);
    }

    public static ExpressionStatementSyntaxInternal ExpressionStatement(SyntaxListInternal<AttributeListSyntaxInternal> attributeLists, ExpressionSyntaxInternal expression, SyntaxTokenInternal semicolonToken)
    {
        if (semicolonToken.Kind != SyntaxKind.SemicolonToken)
            throw new ArgumentException(nameof(semicolonToken));

        return new ExpressionStatementSyntaxInternal(SyntaxKind.ExpressionStatement, attributeLists.Node, expression, semicolonToken);
    }

    public static EmptyStatementSyntaxInternal EmptyStatement(SyntaxListInternal<AttributeListSyntaxInternal> attributeLists, SyntaxTokenInternal semicolonToken)
    {
        if (semicolonToken.Kind != SyntaxKind.SemicolonToken)
            throw new ArgumentException(nameof(semicolonToken));

        return new EmptyStatementSyntaxInternal(SyntaxKind.EmptyStatement, attributeLists.Node, semicolonToken);
    }

    public static BreakStatementSyntaxInternal BreakStatement(SyntaxListInternal<AttributeListSyntaxInternal> attributeLists, SyntaxTokenInternal breakKeyword, SyntaxTokenInternal semicolonToken)
    {
        if (breakKeyword.Kind != SyntaxKind.BreakKeyword)
            throw new ArgumentException(nameof(breakKeyword));
        if (semicolonToken.Kind != SyntaxKind.SemicolonToken)
            throw new ArgumentException(nameof(semicolonToken));

        return new BreakStatementSyntaxInternal(SyntaxKind.BreakStatement, attributeLists.Node, breakKeyword, semicolonToken);
    }

    public static ContinueStatementSyntaxInternal ContinueStatement(SyntaxListInternal<AttributeListSyntaxInternal> attributeLists, SyntaxTokenInternal continueKeyword, SyntaxTokenInternal semicolonToken)
    {
        if (continueKeyword.Kind != SyntaxKind.ContinueKeyword)
            throw new ArgumentException(nameof(continueKeyword));
        if (semicolonToken.Kind != SyntaxKind.SemicolonToken)
            throw new ArgumentException(nameof(semicolonToken));

        return new ContinueStatementSyntaxInternal(SyntaxKind.ContinueStatement, attributeLists.Node, continueKeyword, semicolonToken);
    }

    public static ReturnStatementSyntaxInternal ReturnStatement(SyntaxListInternal<AttributeListSyntaxInternal> attributeLists, SyntaxTokenInternal returnKeyword, ExpressionSyntaxInternal? expression, SyntaxTokenInternal semicolonToken)
    {
        if (returnKeyword.Kind != SyntaxKind.ReturnKeyword)
            throw new ArgumentException(nameof(returnKeyword));
        if (semicolonToken.Kind != SyntaxKind.SemicolonToken)
            throw new ArgumentException(nameof(semicolonToken));

        return new ReturnStatementSyntaxInternal(SyntaxKind.ReturnStatement, attributeLists.Node, returnKeyword, expression, semicolonToken);
    }

    public static WhileStatementSyntaxInternal WhileStatement(SyntaxListInternal<AttributeListSyntaxInternal> attributeLists, SyntaxTokenInternal whileKeyword, SyntaxTokenInternal openParenToken, ExpressionSyntaxInternal condition, SyntaxTokenInternal closeParenToken,
                                                              StatementSyntaxInternal statement)
    {
        if (whileKeyword.Kind != SyntaxKind.WhileKeyword)
            throw new ArgumentException(nameof(whileKeyword));
        if (openParenToken.Kind != SyntaxKind.OpenParenToken)
            throw new ArgumentException(nameof(openParenToken));
        if (closeParenToken.Kind != SyntaxKind.CloseParenToken)
            throw new ArgumentException(nameof(closeParenToken));

        return new WhileStatementSyntaxInternal(SyntaxKind.WhileStatement, attributeLists.Node, whileKeyword, openParenToken, condition, closeParenToken, statement);
    }

    public static DoStatementSyntaxInternal DoStatement(SyntaxListInternal<AttributeListSyntaxInternal> attributeLists, SyntaxTokenInternal doKeyword, StatementSyntaxInternal statement, SyntaxTokenInternal whileKeyword, SyntaxTokenInternal openParenToken, ExpressionSyntaxInternal condition,
                                                        SyntaxTokenInternal closeParenToken, SyntaxTokenInternal semicolonToken)
    {
        if (doKeyword.Kind != SyntaxKind.DoKeyword)
            throw new ArgumentException(nameof(doKeyword));
        if (whileKeyword.Kind != SyntaxKind.WhileKeyword)
            throw new ArgumentException(nameof(whileKeyword));
        if (openParenToken.Kind != SyntaxKind.OpenParenToken)
            throw new ArgumentException(nameof(openParenToken));
        if (closeParenToken.Kind != SyntaxKind.CloseParenToken)
            throw new ArgumentException(nameof(closeParenToken));
        if (semicolonToken.Kind != SyntaxKind.SemicolonToken)
            throw new ArgumentException(nameof(semicolonToken));

        return new DoStatementSyntaxInternal(SyntaxKind.DoStatement, attributeLists.Node, doKeyword, statement, whileKeyword, openParenToken, condition, closeParenToken, semicolonToken);
    }

    public static ForStatementSyntaxInternal ForStatement(SyntaxListInternal<AttributeListSyntaxInternal> attributeLists, SyntaxTokenInternal forKeyword, SyntaxTokenInternal openParenToken, VariableDeclarationSyntaxInternal? declaration,
                                                          SeparatedSyntaxListInternal<ExpressionSyntaxInternal> initializers, SyntaxTokenInternal firstSemicolonToken, ExpressionSyntaxInternal? condition, SyntaxTokenInternal secondSemicolonToken,
                                                          SeparatedSyntaxListInternal<ExpressionSyntaxInternal> incrementors, SyntaxTokenInternal closeParenToken, StatementSyntaxInternal statement)
    {
        if (forKeyword.Kind != SyntaxKind.ForKeyword)
            throw new ArgumentException(nameof(forKeyword));
        if (openParenToken.Kind != SyntaxKind.OpenParenToken)
            throw new ArgumentException(nameof(openParenToken));
        if (firstSemicolonToken.Kind != SyntaxKind.SemicolonToken)
            throw new ArgumentException(nameof(firstSemicolonToken));
        if (secondSemicolonToken.Kind != SyntaxKind.SemicolonToken)
            throw new ArgumentException(nameof(secondSemicolonToken));
        if (closeParenToken.Kind != SyntaxKind.CloseParenToken)
            throw new ArgumentException(nameof(closeParenToken));

        return new ForStatementSyntaxInternal(SyntaxKind.ForStatement, attributeLists.Node, forKeyword, openParenToken, declaration, initializers.Node, firstSemicolonToken, condition, secondSemicolonToken, incrementors.Node, closeParenToken, statement);
    }

    public static IfStatementSyntaxInternal IfStatement(SyntaxListInternal<AttributeListSyntaxInternal> attributeLists, SyntaxTokenInternal ifKeyword, SyntaxTokenInternal openParenToken, ExpressionSyntaxInternal condition, SyntaxTokenInternal closeParenToken, StatementSyntaxInternal statement,
                                                        ElseClauseSyntaxInternal? @else)
    {
        if (ifKeyword.Kind != SyntaxKind.IfKeyword)
            throw new ArgumentException(nameof(ifKeyword));
        if (openParenToken.Kind != SyntaxKind.OpenParenToken)
            throw new ArgumentException(nameof(openParenToken));
        if (closeParenToken.Kind != SyntaxKind.CloseParenToken)
            throw new ArgumentException(nameof(closeParenToken));

        return new IfStatementSyntaxInternal(SyntaxKind.IfStatement, attributeLists.Node, ifKeyword, openParenToken, condition, closeParenToken, statement, @else);
    }

    public static ElseClauseSyntaxInternal ElseClause(SyntaxTokenInternal elseKeyword, StatementSyntaxInternal statement)
    {
        if (elseKeyword.Kind != SyntaxKind.ElseKeyword)
            throw new ArgumentException(nameof(elseKeyword));

        return new ElseClauseSyntaxInternal(SyntaxKind.ElseClause, elseKeyword, statement);
    }

    public static SwitchStatementSyntaxInternal SwitchStatement(SyntaxListInternal<AttributeListSyntaxInternal> attributeLists, SyntaxTokenInternal switchKeyword, SyntaxTokenInternal openParenToken, ExpressionSyntaxInternal expression, SyntaxTokenInternal closeParenToken,
                                                                SyntaxTokenInternal openBraceToken, SyntaxListInternal<SwitchSectionSyntaxInternal> sections, SyntaxTokenInternal closeBraceToken)
    {
        if (switchKeyword.Kind != SyntaxKind.SwitchKeyword)
            throw new ArgumentException(nameof(switchKeyword));
        if (openParenToken.Kind != SyntaxKind.OpenParenToken)
            throw new ArgumentException(nameof(openParenToken));
        if (closeParenToken.Kind != SyntaxKind.CloseParenToken)
            throw new ArgumentException(nameof(closeParenToken));
        if (openBraceToken.Kind != SyntaxKind.OpenBraceToken)
            throw new ArgumentException(nameof(openBraceToken));
        if (closeBraceToken.Kind != SyntaxKind.CloseBraceToken)
            throw new ArgumentException(nameof(closeBraceToken));

        return new SwitchStatementSyntaxInternal(SyntaxKind.SwitchStatement, attributeLists.Node, switchKeyword, openParenToken, expression, closeParenToken, openBraceToken, sections.Node, closeBraceToken);
    }

    public static SwitchSectionSyntaxInternal SwitchSection(SyntaxListInternal<SwitchLabelSyntaxInternal> labels, SyntaxListInternal<StatementSyntaxInternal> statements)
    {
        return new SwitchSectionSyntaxInternal(SyntaxKind.SwitchSection, labels.Node, statements.Node);
    }

    public static CaseSwitchLabelSyntaxInternal CaseSwitchLabel(SyntaxTokenInternal caseKeyword, ExpressionSyntaxInternal value, SyntaxTokenInternal colonToken)
    {
        if (caseKeyword.Kind != SyntaxKind.CaseKeyword)
            throw new ArgumentException(nameof(caseKeyword));
        if (colonToken.Kind != SyntaxKind.ColonToken)
            throw new ArgumentException(nameof(colonToken));

        return new CaseSwitchLabelSyntaxInternal(SyntaxKind.CaseSwitchLabel, caseKeyword, value, colonToken);
    }

    public static DefaultSwitchLabelSyntaxInternal DefaultSwitchLabel(SyntaxTokenInternal defaultKeyword, SyntaxTokenInternal colonToken)
    {
        if (defaultKeyword.Kind != SyntaxKind.DefaultKeyword)
            throw new ArgumentException(nameof(defaultKeyword));
        if (colonToken.Kind != SyntaxKind.ColonToken)
            throw new ArgumentException(nameof(colonToken));

        return new DefaultSwitchLabelSyntaxInternal(SyntaxKind.DefaultSwitchLabel, defaultKeyword, colonToken);
    }

    public static CompilationUnitSyntaxInternal CompilationUnit(SyntaxListInternal<MemberDeclarationSyntaxInternal> members, SyntaxTokenInternal endOfFileToken)
    {
        if (endOfFileToken.Kind != SyntaxKind.EndOfFileToken)
            throw new ArgumentException(nameof(endOfFileToken));

        return new CompilationUnitSyntaxInternal(SyntaxKind.CompilationUnit, members.Node, endOfFileToken);
    }

    public static AttributeListSyntaxInternal AttributeList(SyntaxTokenInternal openBracketToken, SeparatedSyntaxListInternal<AttributeSyntaxInternal> attributes, SyntaxTokenInternal closeBracketToken)
    {
        if (openBracketToken.Kind != SyntaxKind.OpenBracketToken)
            throw new ArgumentException(nameof(openBracketToken));
        if (closeBracketToken.Kind != SyntaxKind.CloseBracketToken)
            throw new ArgumentException(nameof(closeBracketToken));

        return new AttributeListSyntaxInternal(SyntaxKind.AttributeList, openBracketToken, attributes.Node, closeBracketToken);
    }

    public static AttributeSyntaxInternal Attribute(NameSyntaxInternal name, AttributeArgumentListSyntaxInternal? argumentList)
    {
        return new AttributeSyntaxInternal(SyntaxKind.Attribute, name, argumentList);
    }

    public static AttributeArgumentListSyntaxInternal AttributeArgumentList(SyntaxTokenInternal openParenToken, SeparatedSyntaxListInternal<AttributeArgumentSyntaxInternal> arguments, SyntaxTokenInternal closeParenToken)
    {
        if (openParenToken.Kind != SyntaxKind.OpenParenToken)
            throw new ArgumentException(nameof(openParenToken));
        if (closeParenToken.Kind != SyntaxKind.CloseParenToken)
            throw new ArgumentException(nameof(closeParenToken));

        return new AttributeArgumentListSyntaxInternal(SyntaxKind.AttributeArgumentList, openParenToken, arguments.Node, closeParenToken);
    }

    public static AttributeArgumentSyntaxInternal AttributeArgument(ExpressionSyntaxInternal expression)
    {
        return new AttributeArgumentSyntaxInternal(SyntaxKind.AttributeArgument, expression);
    }

    public static NameEqualsSyntaxInternal NameEquals(IdentifierNameSyntaxInternal name, SyntaxTokenInternal equalsToken)
    {
        if (equalsToken.Kind != SyntaxKind.EqualsToken)
            throw new ArgumentException(nameof(equalsToken));

        return new NameEqualsSyntaxInternal(SyntaxKind.NameEquals, name, equalsToken);
    }

    public static StructDeclarationSyntaxInternal StructDeclaration(SyntaxTokenInternal structKeyword, SyntaxTokenInternal identifier, SyntaxTokenInternal openBraceToken, SyntaxListInternal<FieldDeclarationSyntaxInternal> members, SyntaxTokenInternal closeBraceToken,
                                                                    SyntaxTokenInternal semicolonToken)
    {
        if (structKeyword.Kind != SyntaxKind.StructKeyword)
            throw new ArgumentException(nameof(structKeyword));
        if (identifier.Kind != SyntaxKind.IdentifierToken)
            throw new ArgumentException(nameof(identifier));
        if (openBraceToken.Kind != SyntaxKind.OpenBraceToken)
            throw new ArgumentException(nameof(openBraceToken));
        if (closeBraceToken.Kind != SyntaxKind.CloseBraceToken)
            throw new ArgumentException(nameof(closeBraceToken));
        if (semicolonToken.Kind != SyntaxKind.SemicolonToken)
            throw new ArgumentException(nameof(semicolonToken));

        return new StructDeclarationSyntaxInternal(SyntaxKind.StructDeclaration, structKeyword, identifier, openBraceToken, members.Node, closeBraceToken, semicolonToken);
    }

    public static TechniqueDeclarationSyntaxInternal TechniqueDeclaration(SyntaxTokenInternal techniqueKeyword, SyntaxTokenInternal identifier, SyntaxTokenInternal openBraceToken, SyntaxListInternal<PassDeclarationSyntaxInternal> members, SyntaxTokenInternal closeBraceToken)
    {
        if (identifier.Kind != SyntaxKind.IdentifierToken)
            throw new ArgumentException(nameof(identifier));
        if (openBraceToken.Kind != SyntaxKind.OpenBraceToken)
            throw new ArgumentException(nameof(openBraceToken));
        if (closeBraceToken.Kind != SyntaxKind.CloseBraceToken)
            throw new ArgumentException(nameof(closeBraceToken));

        switch (techniqueKeyword.Kind)
        {
            case SyntaxKind.TechniqueKeyword:
            case SyntaxKind.Technique10Keyword:
            case SyntaxKind.Technique11Keyword:
                break;

            default:
                throw new ArgumentException(nameof(techniqueKeyword));
        }

        return new TechniqueDeclarationSyntaxInternal(SyntaxKind.TechniqueDeclaration, techniqueKeyword, identifier, openBraceToken, members.Node, closeBraceToken);
    }

    public static PassDeclarationSyntaxInternal PassDeclaration(SyntaxTokenInternal passKeyword, SyntaxTokenInternal identifier, SyntaxTokenInternal openBraceToken, SyntaxListInternal<StatementSyntaxInternal> members, SyntaxTokenInternal closeBraceToken)
    {
        if (passKeyword.Kind != SyntaxKind.PassKeyword)
            throw new ArgumentException(nameof(passKeyword));
        if (identifier.Kind != SyntaxKind.IdentifierToken)
            throw new ArgumentException(nameof(identifier));
        if (openBraceToken.Kind != SyntaxKind.OpenBraceToken)
            throw new ArgumentException(nameof(openBraceToken));
        if (closeBraceToken.Kind != SyntaxKind.CloseBraceToken)
            throw new ArgumentException(nameof(closeBraceToken));

        return new PassDeclarationSyntaxInternal(SyntaxKind.PassDeclaration, passKeyword, identifier, openBraceToken, members.Node, closeBraceToken);
    }

    public static FieldDeclarationSyntaxInternal FieldDeclaration(TypeSyntaxInternal type, SyntaxTokenInternal identifier, BracketedArgumentListSyntaxInternal? arguments, SemanticSyntaxInternal? semantics, EqualsValueClauseSyntaxInternal? initializer, SyntaxTokenInternal semicolonToken)
    {
        if (identifier.Kind != SyntaxKind.IdentifierToken)
            throw new ArgumentException(nameof(identifier));
        if (semicolonToken.Kind != SyntaxKind.SemicolonToken)
            throw new ArgumentException(nameof(semicolonToken));

        return new FieldDeclarationSyntaxInternal(SyntaxKind.FieldDeclaration, type, identifier, arguments, semantics, initializer, semicolonToken);
    }

    public static MethodDeclarationSyntaxInternal MethodDeclaration(SyntaxListInternal<AttributeListSyntaxInternal> attributeLists, TypeSyntaxInternal returnType, SyntaxTokenInternal identifier, ParameterListSyntaxInternal parameterList, SemanticSyntaxInternal? semantics, BlockSyntaxInternal body)
    {
        if (identifier.Kind != SyntaxKind.IdentifierToken)
            throw new ArgumentException(nameof(identifier));

        return new MethodDeclarationSyntaxInternal(SyntaxKind.MethodDeclaration, attributeLists.Node, returnType, identifier, parameterList, semantics, body);
    }

    public static ParameterListSyntaxInternal ParameterList(SyntaxTokenInternal openParenToken, SeparatedSyntaxListInternal<ParameterSyntaxInternal> parameters, SyntaxTokenInternal closeParenToken)
    {
        if (openParenToken.Kind != SyntaxKind.OpenParenToken)
            throw new ArgumentException(nameof(openParenToken));
        if (closeParenToken.Kind != SyntaxKind.CloseParenToken)
            throw new ArgumentException(nameof(closeParenToken));

        return new ParameterListSyntaxInternal(SyntaxKind.ParameterList, openParenToken, parameters.Node, closeParenToken);
    }

    public static ParameterSyntaxInternal Parameter(SyntaxListInternal<AttributeListSyntaxInternal> attributeLists, SyntaxListInternal<SyntaxTokenInternal> modifiers, TypeSyntaxInternal type, SyntaxTokenInternal identifier, EqualsValueClauseSyntaxInternal? @default,
                                                    SemanticSyntaxInternal? semantics)
    {
        if (identifier.Kind != SyntaxKind.IdentifierToken)
            throw new ArgumentException(nameof(identifier));

        return new ParameterSyntaxInternal(SyntaxKind.Parameter, attributeLists.Node, modifiers.Node, type, identifier, @default, semantics);
    }

    public static IfDirectiveTriviaSyntaxInternal IfDirectiveTrivia(SyntaxTokenInternal hashToken, SyntaxTokenInternal ifKeyword, ExpressionSyntaxInternal condition, SyntaxTokenInternal endOfDirectiveToken)
    {
        if (hashToken.Kind != SyntaxKind.HashToken)
            throw new ArgumentException(nameof(hashToken));
        if (ifKeyword.Kind != SyntaxKind.IfKeyword)
            throw new ArgumentException(nameof(ifKeyword));
        if (endOfDirectiveToken.Kind != SyntaxKind.EndOfDirectiveToken)
            throw new ArgumentException(nameof(endOfDirectiveToken));

        return new IfDirectiveTriviaSyntaxInternal(SyntaxKind.IfDirectiveTrivia, hashToken, ifKeyword, condition, endOfDirectiveToken);
    }

    public static ElifDirectiveTriviaSyntaxInternal ElifDirectiveTrivia(SyntaxTokenInternal hashToken, SyntaxTokenInternal elifKeyword, ExpressionSyntaxInternal condition, SyntaxTokenInternal endOfDirectiveToken)
    {
        if (hashToken.Kind != SyntaxKind.HashToken)
            throw new ArgumentException(nameof(hashToken));
        if (elifKeyword.Kind != SyntaxKind.ElifKeyword)
            throw new ArgumentException(nameof(elifKeyword));
        if (endOfDirectiveToken.Kind != SyntaxKind.EndOfDirectiveToken)
            throw new ArgumentException(nameof(endOfDirectiveToken));

        return new ElifDirectiveTriviaSyntaxInternal(SyntaxKind.ElifDirectiveTrivia, hashToken, elifKeyword, condition, endOfDirectiveToken);
    }

    public static ElseDirectiveTriviaSyntaxInternal ElseDirectiveTrivia(SyntaxTokenInternal hashToken, SyntaxTokenInternal elseKeyword, SyntaxTokenInternal endOfDirectiveToken)
    {
        if (hashToken.Kind != SyntaxKind.HashToken)
            throw new ArgumentException(nameof(hashToken));
        if (elseKeyword.Kind != SyntaxKind.ElseKeyword)
            throw new ArgumentException(nameof(elseKeyword));
        if (endOfDirectiveToken.Kind != SyntaxKind.EndOfDirectiveToken)
            throw new ArgumentException(nameof(endOfDirectiveToken));

        return new ElseDirectiveTriviaSyntaxInternal(SyntaxKind.ElseDirectiveTrivia, hashToken, elseKeyword, endOfDirectiveToken);
    }

    public static EndIfDirectiveTriviaSyntaxInternal EndIfDirectiveTrivia(SyntaxTokenInternal hashToken, SyntaxTokenInternal endifKeyword, SyntaxTokenInternal endOfDirectiveToken)
    {
        if (hashToken.Kind != SyntaxKind.HashToken)
            throw new ArgumentException(nameof(hashToken));
        if (endifKeyword.Kind != SyntaxKind.EndIfKeyword)
            throw new ArgumentException(nameof(endifKeyword));
        if (endOfDirectiveToken.Kind != SyntaxKind.EndOfDirectiveToken)
            throw new ArgumentException(nameof(endOfDirectiveToken));

        return new EndIfDirectiveTriviaSyntaxInternal(SyntaxKind.EndIfDirectiveTrivia, hashToken, endifKeyword, endOfDirectiveToken);
    }

    public static IfDefDirectiveTriviaSyntaxInternal IfDefDirectiveTrivia(SyntaxTokenInternal hashToken, SyntaxTokenInternal ifDefKeyword, ExpressionSyntaxInternal condition, SyntaxTokenInternal endOfDirectiveToken)
    {
        if (hashToken.Kind != SyntaxKind.HashToken)
            throw new ArgumentException(nameof(hashToken));
        if (ifDefKeyword.Kind != SyntaxKind.IfdefKeyword)
            throw new ArgumentException(nameof(ifDefKeyword));
        if (endOfDirectiveToken.Kind != SyntaxKind.EndOfDirectiveToken)
            throw new ArgumentException(nameof(endOfDirectiveToken));

        return new IfDefDirectiveTriviaSyntaxInternal(SyntaxKind.IfDefDirectiveTrivia, hashToken, ifDefKeyword, condition, endOfDirectiveToken);
    }

    public static IfnDefDirectiveTriviaSyntaxInternal IfnDefDirectiveTrivia(SyntaxTokenInternal hashToken, SyntaxTokenInternal ifnDefKeyword, ExpressionSyntaxInternal condition, SyntaxTokenInternal endOfDirectiveToken)
    {
        if (hashToken.Kind != SyntaxKind.HashToken)
            throw new ArgumentException(nameof(hashToken));
        if (ifnDefKeyword.Kind != SyntaxKind.IfndefKeyword)
            throw new ArgumentException(nameof(ifnDefKeyword));
        if (endOfDirectiveToken.Kind != SyntaxKind.EndOfDirectiveToken)
            throw new ArgumentException(nameof(endOfDirectiveToken));

        return new IfnDefDirectiveTriviaSyntaxInternal(SyntaxKind.IfnDefDirectiveTrivia, hashToken, ifnDefKeyword, condition, endOfDirectiveToken);
    }

    public static ErrorDirectiveTriviaSyntaxInternal ErrorDirectiveTrivia(SyntaxTokenInternal hashToken, SyntaxTokenInternal errorKeyword, SyntaxTokenInternal endOfDirectiveToken)
    {
        if (hashToken.Kind != SyntaxKind.HashToken)
            throw new ArgumentException(nameof(hashToken));
        if (errorKeyword.Kind != SyntaxKind.ErrorKeyword)
            throw new ArgumentException(nameof(errorKeyword));
        if (endOfDirectiveToken.Kind != SyntaxKind.EndOfDirectiveToken)
            throw new ArgumentException(nameof(endOfDirectiveToken));

        return new ErrorDirectiveTriviaSyntaxInternal(SyntaxKind.ErrorDirectiveTrivia, hashToken, errorKeyword, endOfDirectiveToken);
    }

    public static WarningDirectiveTriviaSyntaxInternal WarningDirectiveTrivia(SyntaxTokenInternal hashToken, SyntaxTokenInternal warningKeyword, SyntaxTokenInternal endOfDirectiveToken)
    {
        if (hashToken.Kind != SyntaxKind.HashToken)
            throw new ArgumentException(nameof(hashToken));
        if (warningKeyword.Kind != SyntaxKind.WarningKeyword)
            throw new ArgumentException(nameof(warningKeyword));
        if (endOfDirectiveToken.Kind != SyntaxKind.EndOfDirectiveToken)
            throw new ArgumentException(nameof(endOfDirectiveToken));

        return new WarningDirectiveTriviaSyntaxInternal(SyntaxKind.WarningDirectiveTrivia, hashToken, warningKeyword, endOfDirectiveToken);
    }

    public static DefineDirectiveTriviaSyntaxInternal DefineDirectiveTrivia(SyntaxTokenInternal hashToken, SyntaxTokenInternal defineKeyword, SyntaxTokenInternal name, SyntaxTokenInternal endOfDirectiveToken)
    {
        if (hashToken.Kind != SyntaxKind.HashToken)
            throw new ArgumentException(nameof(hashToken));
        if (defineKeyword.Kind != SyntaxKind.DefineKeyword)
            throw new ArgumentException(nameof(defineKeyword));
        if (endOfDirectiveToken.Kind != SyntaxKind.EndOfDirectiveToken)
            throw new ArgumentException(nameof(endOfDirectiveToken));

        return new DefineDirectiveTriviaSyntaxInternal(SyntaxKind.DefineDirectiveTrivia, hashToken, defineKeyword, name, endOfDirectiveToken);
    }

    public static UndefDirectiveTriviaSyntaxInternal UndefDirectiveTrivia(SyntaxTokenInternal hashToken, SyntaxTokenInternal undefKeyword, SyntaxTokenInternal name, SyntaxTokenInternal endOfDirectiveToken)
    {
        if (hashToken.Kind != SyntaxKind.HashToken)
            throw new ArgumentException(nameof(hashToken));
        if (undefKeyword.Kind != SyntaxKind.UndefKeyword)
            throw new ArgumentException(nameof(undefKeyword));
        if (endOfDirectiveToken.Kind != SyntaxKind.EndOfDirectiveToken)
            throw new ArgumentException(nameof(endOfDirectiveToken));

        return new UndefDirectiveTriviaSyntaxInternal(SyntaxKind.UndefDirectiveTrivia, hashToken, undefKeyword, name, endOfDirectiveToken);
    }

    public static LineDirectiveTriviaSyntaxInternal LineDirectiveTrivia(SyntaxTokenInternal hashToken, SyntaxTokenInternal lineKeyword, SyntaxTokenInternal line, SyntaxTokenInternal? file, SyntaxTokenInternal endOfDirectiveToken)
    {
        if (hashToken.Kind != SyntaxKind.HashToken)
            throw new ArgumentException(nameof(hashToken));
        if (lineKeyword.Kind != SyntaxKind.LineKeyword)
            throw new ArgumentException(nameof(lineKeyword));
        if (endOfDirectiveToken.Kind != SyntaxKind.EndOfDirectiveToken)
            throw new ArgumentException(nameof(endOfDirectiveToken));

        switch (line.Kind)
        {
            case SyntaxKind.NumericLiteralToken:
            case SyntaxKind.DefaultKeyword:
                break;

            default:
                throw new ArgumentException(nameof(line));
        }

        if (file != null)
            switch (file.Kind)
            {
                case SyntaxKind.StringLiteralToken:
                case SyntaxKind.None:
                    break;

                default:
                    throw new ArgumentException(nameof(file));
            }

        return new LineDirectiveTriviaSyntaxInternal(SyntaxKind.LineDirectiveTrivia, hashToken, lineKeyword, line, file, endOfDirectiveToken);
    }

    public static IncludeDirectiveSyntaxInternal IncludeDirectiveTrivia(SyntaxTokenInternal hashToken, SyntaxTokenInternal includeKeyword, SyntaxTokenInternal file, SyntaxTokenInternal endOfDirectiveToken)
    {
        if (hashToken.Kind != SyntaxKind.HashToken)
            throw new ArgumentException(nameof(hashToken));
        if (includeKeyword.Kind != SyntaxKind.IncludeKeyword)
            throw new ArgumentException(nameof(includeKeyword));
        if (endOfDirectiveToken.Kind != SyntaxKind.EndOfDirectiveToken)
            throw new ArgumentException(nameof(endOfDirectiveToken));

        switch (file.Kind)
        {
            case SyntaxKind.StringLiteralToken:
            case SyntaxKind.IncludeReferenceLiteralToken:
                break;

            default:
                throw new ArgumentException(nameof(file));
        }

        return new IncludeDirectiveSyntaxInternal(SyntaxKind.IncludeDirectiveTrivia, hashToken, includeKeyword, file, endOfDirectiveToken);
    }

    public static PragmaDefDirectiveTriviaSyntaxInternal PragmaDefDirectiveTrivia(SyntaxTokenInternal hashToken, SyntaxTokenInternal pragmaKeyword, SyntaxTokenInternal defKeyword, SyntaxTokenInternal openParenToken, SyntaxTokenInternal target, SyntaxTokenInternal firstCommaToken,
                                                                                  SyntaxTokenInternal register, SyntaxTokenInternal secondCommaToken, SyntaxTokenInternal val1, SyntaxTokenInternal thirdCommaToken, SyntaxTokenInternal val2, SyntaxTokenInternal fourthCommaToken,
                                                                                  SyntaxTokenInternal val3, SyntaxTokenInternal fifthCommaToken, SyntaxTokenInternal val4, SyntaxTokenInternal closeParenToken, SyntaxTokenInternal endOfDirectiveToken)
    {
        if (hashToken.Kind != SyntaxKind.HashToken)
            throw new ArgumentException(nameof(hashToken));
        if (pragmaKeyword.Kind != SyntaxKind.PragmaKeyword)
            throw new ArgumentException(nameof(pragmaKeyword));
        if (defKeyword.Kind != SyntaxKind.DefKeyword)
            throw new ArgumentException(nameof(defKeyword));
        if (openParenToken.Kind != SyntaxKind.OpenParenToken)
            throw new ArgumentException(nameof(openParenToken));
        if (target.Kind != SyntaxKind.IdentifierToken)
            throw new ArgumentException(nameof(target));
        if (firstCommaToken.Kind != SyntaxKind.CommaToken)
            throw new ArgumentException(nameof(firstCommaToken));
        if (register.Kind != SyntaxKind.IdentifierToken)
            throw new ArgumentException(nameof(register));
        if (secondCommaToken.Kind != SyntaxKind.CommaToken)
            throw new ArgumentException(nameof(secondCommaToken));
        if (val1.Kind != SyntaxKind.IdentifierToken)
            throw new ArgumentException(nameof(val1));
        if (thirdCommaToken.Kind != SyntaxKind.CommaToken)
            throw new ArgumentException(nameof(thirdCommaToken));
        if (val2.Kind != SyntaxKind.IdentifierToken)
            throw new ArgumentException(nameof(val2));
        if (fourthCommaToken.Kind != SyntaxKind.CommaToken)
            throw new ArgumentException(nameof(fourthCommaToken));
        if (val3.Kind != SyntaxKind.IdentifierToken)
            throw new ArgumentException(nameof(val3));
        if (fifthCommaToken.Kind != SyntaxKind.CommaToken)
            throw new ArgumentException(nameof(fifthCommaToken));
        if (val4.Kind != SyntaxKind.IdentifierToken)
            throw new ArgumentException(nameof(val4));
        if (closeParenToken.Kind != SyntaxKind.CloseParenToken)
            throw new ArgumentException(nameof(closeParenToken));
        if (endOfDirectiveToken.Kind != SyntaxKind.EndOfDirectiveToken)
            throw new ArgumentException(nameof(endOfDirectiveToken));

        return new PragmaDefDirectiveTriviaSyntaxInternal(SyntaxKind.PragmaDefDirectiveTrivia, hashToken, pragmaKeyword, defKeyword, openParenToken, target, firstCommaToken, register, secondCommaToken, val1, thirdCommaToken, val2, fourthCommaToken, val3, fifthCommaToken, val4, closeParenToken,
                                                          endOfDirectiveToken);
    }

    public static PragmaMessageDirectiveTriviaSyntaxInternal PragmaMessageDirective(SyntaxTokenInternal hashToken, SyntaxTokenInternal pragmaKeyword, SyntaxTokenInternal messageKeyword, SyntaxTokenInternal message, SyntaxTokenInternal endOfDirectiveToken)
    {
        if (hashToken.Kind != SyntaxKind.HashToken)
            throw new ArgumentException(nameof(hashToken));
        if (pragmaKeyword.Kind != SyntaxKind.PragmaKeyword)
            throw new ArgumentException(nameof(pragmaKeyword));
        if (messageKeyword.Kind != SyntaxKind.MessageKeyword)
            throw new ArgumentException(nameof(messageKeyword));
        if (message.Kind != SyntaxKind.StringLiteralToken)
            throw new ArgumentException(nameof(message));
        if (endOfDirectiveToken.Kind != SyntaxKind.EndOfDirectiveToken)
            throw new ArgumentException(nameof(endOfDirectiveToken));

        return new PragmaMessageDirectiveTriviaSyntaxInternal(SyntaxKind.PragmaMessageDirectiveTrivia, hashToken, pragmaKeyword, messageKeyword, message, endOfDirectiveToken);
    }

    public static PragmaPackMatrixDirectiveTriviaSyntaxInternal PragmaPackMatrixDirective(SyntaxTokenInternal hashToken, SyntaxTokenInternal pragmaKeyword, SyntaxTokenInternal packMatrixKeyword, SyntaxTokenInternal openParenToken, SyntaxTokenInternal columnMajorOrRowMajorKeyword,
                                                                                          SyntaxTokenInternal closeParenToken, SyntaxTokenInternal endOfDirectiveToken)
    {
        if (hashToken.Kind != SyntaxKind.HashToken)
            throw new ArgumentException(nameof(hashToken));
        if (pragmaKeyword.Kind != SyntaxKind.PragmaKeyword)
            throw new ArgumentException(nameof(pragmaKeyword));
        if (packMatrixKeyword.Kind != SyntaxKind.PackMatrixKeyword)
            throw new ArgumentException(nameof(packMatrixKeyword));
        if (openParenToken.Kind != SyntaxKind.OpenParenToken)
            throw new ArgumentException(nameof(openParenToken));
        if (closeParenToken.Kind != SyntaxKind.CloseParenToken)
            throw new ArgumentException(nameof(closeParenToken));
        if (endOfDirectiveToken.Kind != SyntaxKind.EndOfDirectiveToken)
            throw new ArgumentException(nameof(endOfDirectiveToken));

        switch (columnMajorOrRowMajorKeyword.Kind)
        {
            case SyntaxKind.ColumnMajorKeyword:
            case SyntaxKind.RowMajorKeyword:
                break;

            default:
                throw new ArgumentException(nameof(columnMajorOrRowMajorKeyword));
        }

        return new PragmaPackMatrixDirectiveTriviaSyntaxInternal(SyntaxKind.PragmaPackMatrixDirectiveTrivia, hashToken, pragmaKeyword, packMatrixKeyword, openParenToken, columnMajorOrRowMajorKeyword, closeParenToken, endOfDirectiveToken);
    }

    public static PragmaWarningDirectiveTriviaSyntaxInternal PragmaWarningDirective(SyntaxTokenInternal hashToken, SyntaxTokenInternal pragmaKeyword, SyntaxTokenInternal warningKeyword, SyntaxTokenInternal openParenToken, WarningSpecifierListSyntaxInternal specifiers,
                                                                                    SyntaxTokenInternal closeParenToken, SyntaxTokenInternal endOfDirectiveToken)
    {
        if (hashToken.Kind != SyntaxKind.HashToken)
            throw new ArgumentException(nameof(hashToken));
        if (pragmaKeyword.Kind != SyntaxKind.PragmaKeyword)
            throw new ArgumentException(nameof(pragmaKeyword));
        if (warningKeyword.Kind != SyntaxKind.WarningKeyword)
            throw new ArgumentException(nameof(warningKeyword));
        if (openParenToken.Kind != SyntaxKind.OpenParenToken)
            throw new ArgumentException(nameof(openParenToken));
        if (closeParenToken.Kind != SyntaxKind.CloseParenToken)
            throw new ArgumentException(nameof(closeParenToken));
        if (endOfDirectiveToken.Kind != SyntaxKind.EndOfDirectiveToken)
            throw new ArgumentException(nameof(endOfDirectiveToken));

        return new PragmaWarningDirectiveTriviaSyntaxInternal(SyntaxKind.PragmaWarningDirectiveTrivia, hashToken, pragmaKeyword, warningKeyword, openParenToken, specifiers, closeParenToken, endOfDirectiveToken);
    }
}