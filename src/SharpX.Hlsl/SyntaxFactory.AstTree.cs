// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.Core.Extensions;
using SharpX.Hlsl.Syntax;
using SharpX.Hlsl.Syntax.InternalSyntax;

using SyntaxFactoryInternal = SharpX.Hlsl.Syntax.InternalSyntax.SyntaxFactory;


namespace SharpX.Hlsl;

public static partial class SyntaxFactory
{
    public static IdentifierNameSyntax IdentifierName(SyntaxToken identifier)
    {
        return (IdentifierNameSyntax)SyntaxFactoryInternal.IdentifierName((SyntaxTokenInternal)identifier.Node!).CreateRed();
    }

    public static GenericNameSyntax GenericName(SyntaxToken identifier, TypeArgumentListSyntax typeArgumentList)
    {
        return (GenericNameSyntax)SyntaxFactoryInternal.GenericName((SyntaxTokenInternal)identifier.Node!, (TypeArgumentListSyntaxInternal)typeArgumentList.Green).CreateRed();
    }

    public static GenericNameSyntax GenericName(SyntaxToken identifier)
    {
        return GenericName(identifier, TypeArgumentList());
    }

    public static GenericNameSyntax GenericName(string identifier)
    {
        return GenericName(Identifier(identifier), TypeArgumentList());
    }

    public static TypeArgumentListSyntax TypeArgumentList(SyntaxToken lessThanToken, SeparatedSyntaxList<TypeSyntax> arguments, SyntaxToken greaterThanToken)
    {
        return (TypeArgumentListSyntax)SyntaxFactoryInternal.TypeArgumentList(
            (SyntaxTokenInternal)lessThanToken.Node!,
            arguments.Node.ToGreenSeparatedList<TypeSyntaxInternal>(),
            (SyntaxTokenInternal)greaterThanToken.Node!
        ).CreateRed();
    }

    public static TypeArgumentListSyntax TypeArgumentList(SeparatedSyntaxList<TypeSyntax> arguments = default)
    {
        return TypeArgumentList(Token(SyntaxKind.LessThanToken), arguments, Token(SyntaxKind.GreaterThanToken));
    }

    public static PredefinedTypeSyntax PredefinedType(SyntaxToken keyword)
    {
        return (PredefinedTypeSyntax)SyntaxFactoryInternal.PredefinedType((SyntaxTokenInternal)keyword.Node!).CreateRed();
    }

    public static ArrayTypeSyntax ArrayType(TypeSyntax elementType, SyntaxList<ArrayRankSpecifierSyntax> rankSpecifiers = default)
    {
        return (ArrayTypeSyntax)SyntaxFactoryInternal.ArrayType((TypeSyntaxInternal)elementType.Green, rankSpecifiers.Node.ToGreenList<ArrayRankSpecifierSyntaxInternal>()).CreateRed();
    }

    public static ArrayRankSpecifierSyntax ArrayRankSpecifier(SyntaxToken openBracketToken, SeparatedSyntaxList<ExpressionSyntax> sizes, SyntaxToken closeBracketToken)
    {
        return (ArrayRankSpecifierSyntax)SyntaxFactoryInternal.ArrayRankSpecifier(
            (SyntaxTokenInternal)openBracketToken.Node!,
            sizes.Node.ToGreenSeparatedList<ExpressionSyntaxInternal>(),
            (SyntaxTokenInternal)closeBracketToken.Node!
        ).CreateRed();
    }

    public static ArrayRankSpecifierSyntax ArrayRankSpecifier(SeparatedSyntaxList<ExpressionSyntax> sizes = default)
    {
        return ArrayRankSpecifier(Token(SyntaxKind.OpenBracketToken), sizes, Token(SyntaxKind.CloseBracketToken));
    }

    public static ParenthesizedExpressionSyntax ParenthesizedExpression(SyntaxToken openParenToken, ExpressionSyntax expression, SyntaxToken closeParenToken)
    {
        return (ParenthesizedExpressionSyntax)SyntaxFactoryInternal.ParenthesizedExpression(
            (SyntaxTokenInternal)openParenToken.Node!,
            (ExpressionSyntaxInternal)expression.Green,
            (SyntaxTokenInternal)closeParenToken.Node!
        ).CreateRed();
    }

    public static ParenthesizedExpressionSyntax ParenthesizedExpression(ExpressionSyntax expression)
    {
        return ParenthesizedExpression(Token(SyntaxKind.OpenParenToken), expression, Token(SyntaxKind.CloseParenToken));
    }

    public static PrefixUnaryExpressionSyntax PrefixUnaryExpression(SyntaxKind kind, SyntaxToken operatorToken, ExpressionSyntax operand)
    {
        return (PrefixUnaryExpressionSyntax)SyntaxFactoryInternal.PrefixUnaryExpression(
            kind,
            (SyntaxTokenInternal)operatorToken.Node!,
            (ExpressionSyntaxInternal)operand.Green
        ).CreateRed();
    }

    public static PrefixUnaryExpressionSyntax PrefixUnaryExpression(SyntaxKind kind, ExpressionSyntax operand)
    {
        return PrefixUnaryExpression(kind, Token(GetPrefixUnaryExpressionOperatorTokenKind(kind)), operand);
    }

    private static SyntaxKind GetPrefixUnaryExpressionOperatorTokenKind(SyntaxKind kind)
    {
        return kind switch
        {
            SyntaxKind.UnaryPlusExpression => SyntaxKind.PlusToken,
            SyntaxKind.UnaryMinusExpression => SyntaxKind.MinusToken,
            SyntaxKind.BitwiseNotExpression => SyntaxKind.TildeToken,
            SyntaxKind.LogicalNotExpression => SyntaxKind.ExclamationToken,
            SyntaxKind.PreIncrementExpression => SyntaxKind.PlusPlusToken,
            SyntaxKind.PreDecrementExpression => SyntaxKind.MinusMinusToken,
            SyntaxKind.IndexExpression => SyntaxKind.CaretToken,
            _ => throw new ArgumentOutOfRangeException(nameof(kind))
        };
    }

    public static PostfixUnaryExpressionSyntax PostfixUnaryExpression(SyntaxKind kind, ExpressionSyntax operand, SyntaxToken operatorToken)
    {
        return (PostfixUnaryExpressionSyntax)SyntaxFactoryInternal.PostfixUnaryExpression(
            kind,
            (ExpressionSyntaxInternal)operand.Green,
            (SyntaxTokenInternal)operatorToken.Node!
        ).CreateRed();
    }

    public static PostfixUnaryExpressionSyntax PostfixUnaryExpression(SyntaxKind kind, ExpressionSyntax operand)
    {
        return PostfixUnaryExpression(kind, operand, Token(GetPostfixUnaryExpressionOperatorTokenKind(kind)));
    }

    private static SyntaxKind GetPostfixUnaryExpressionOperatorTokenKind(SyntaxKind kind)
    {
        return kind switch
        {
            SyntaxKind.PostIncrementExpression => SyntaxKind.PlusPlusToken,
            SyntaxKind.PostDecrementExpression => SyntaxKind.MinusMinusToken,
            _ => throw new ArgumentOutOfRangeException(nameof(kind))
        };
    }

    public static MemberAccessExpressionSyntax MemberAccessExpression(ExpressionSyntax expression, SyntaxToken operatorToken, SimpleNameSyntax name)
    {
        return (MemberAccessExpressionSyntax)SyntaxFactoryInternal.MemberAccessExpression(
            (ExpressionSyntaxInternal)expression.Green,
            (SyntaxTokenInternal)operatorToken.Node!,
            (SimpleNameSyntaxInternal)name.Green
        ).CreateRed();
    }

    public static BinaryExpressionSyntax BinaryExpression(SyntaxKind kind, ExpressionSyntax left, SyntaxToken operatorToken, ExpressionSyntax right)
    {
        return (BinaryExpressionSyntax)SyntaxFactoryInternal.BinaryExpression(
            kind,
            (ExpressionSyntaxInternal)left.Green,
            (SyntaxTokenInternal)operatorToken.Node!,
            (ExpressionSyntaxInternal)right.Green
        ).CreateRed();
    }

    public static BinaryExpressionSyntax BinaryExpression(SyntaxKind kind, ExpressionSyntax left, ExpressionSyntax right)
    {
        return BinaryExpression(kind, left, Token(GetBinaryExpressionOperatorTokenKind(kind)), right);
    }

    private static SyntaxKind GetBinaryExpressionOperatorTokenKind(SyntaxKind kind)
    {
        return kind switch
        {
            SyntaxKind.AddExpression => SyntaxKind.PlusToken,
            SyntaxKind.SubtractExpression => SyntaxKind.MinusToken,
            SyntaxKind.MultiplyExpression => SyntaxKind.AsteriskToken,
            SyntaxKind.DivideExpression => SyntaxKind.SlashToken,
            SyntaxKind.ModuloExpression => SyntaxKind.PercentToken,
            SyntaxKind.LogicalOrExpression => SyntaxKind.BarBarToken,
            SyntaxKind.LogicalAndExpression => SyntaxKind.AmpersandAmpersandToken,
            SyntaxKind.BitwiseOrExpression => SyntaxKind.BarToken,
            SyntaxKind.ExclusiveOrExpression => SyntaxKind.CaretToken,
            SyntaxKind.EqualsExpression => SyntaxKind.EqualsEqualsToken,
            SyntaxKind.NotEqualsExpression => SyntaxKind.ExclamationEqualsToken,
            SyntaxKind.LessThanExpression => SyntaxKind.LessThanToken,
            SyntaxKind.LessThanOrEqualExpression => SyntaxKind.LessThanEqualsToken,
            SyntaxKind.GreaterThanExpression => SyntaxKind.GreaterThanToken,
            SyntaxKind.GreaterThanOrEqualExpression => SyntaxKind.GreaterThanEqualsToken,
            _ => throw new ArgumentOutOfRangeException(nameof(kind))
        };
    }

    public static AssignmentExpressionSyntax AssignmentExpression(SyntaxKind kind, ExpressionSyntax left, SyntaxToken operatorToken, ExpressionSyntax right)
    {
        return (AssignmentExpressionSyntax)SyntaxFactoryInternal.AssignmentExpression(
            kind,
            (ExpressionSyntaxInternal)left.Green,
            (SyntaxTokenInternal)operatorToken.Node!,
            (ExpressionSyntaxInternal)right.Green
        ).CreateRed();
    }

    public static AssignmentExpressionSyntax AssignmentExpression(SyntaxKind kind, ExpressionSyntax left, ExpressionSyntax right)
    {
        return AssignmentExpression(kind, left, Token(GetAssignmentExpressionOperatorTokenKind(kind)), right);
    }

    private static SyntaxKind GetAssignmentExpressionOperatorTokenKind(SyntaxKind kind)
    {
        return kind switch
        {
            SyntaxKind.SimpleAssignmentExpression => SyntaxKind.EqualsToken,
            SyntaxKind.AddAssignmentExpression => SyntaxKind.PlusEqualsToken,
            SyntaxKind.SubtractAssignmentExpression => SyntaxKind.MinusEqualsToken,
            SyntaxKind.MultiplyAssignmentExpression => SyntaxKind.AsteriskEqualsToken,
            SyntaxKind.DivideAssignmentExpression => SyntaxKind.SlashEqualsToken,
            SyntaxKind.ModuloAssignmentExpression => SyntaxKind.PercentEqualsToken,
            SyntaxKind.AndAssignmentExpression => SyntaxKind.AmpersandEqualsToken,
            SyntaxKind.ExclusiveOrAssignmentExpression => SyntaxKind.CaretEqualsToken,
            SyntaxKind.OrAssignmentExpression => SyntaxKind.BarEqualsToken,
            SyntaxKind.LeftShiftAssignmentExpression => SyntaxKind.LessThanLessThanEqualsToken,
            SyntaxKind.RightShiftAssignmentExpression => SyntaxKind.GreaterThanGreaterThanEqualsToken,
            _ => throw new ArgumentOutOfRangeException(nameof(kind))
        };
    }

    public static ConditionalExpressionSyntax ConditionalExpression(ExpressionSyntax condition, SyntaxToken questionToken, ExpressionSyntax whenTrue, SyntaxToken colonToken, ExpressionSyntax whenFalse)
    {
        return (ConditionalExpressionSyntax)SyntaxFactoryInternal.ConditionalExpression(
            (ExpressionSyntaxInternal)condition.Green,
            (SyntaxTokenInternal)questionToken.Node!,
            (ExpressionSyntaxInternal)whenTrue.Green,
            (SyntaxTokenInternal)colonToken.Node!,
            (ExpressionSyntaxInternal)whenFalse.Green
        ).CreateRed();
    }

    public static ConditionalExpressionSyntax ConditionalExpression(ExpressionSyntax condition, ExpressionSyntax whenTrue, ExpressionSyntax whenFalse)
    {
        return ConditionalExpression(condition, Token(SyntaxKind.QuestionToken), whenTrue, Token(SyntaxKind.ColonToken), whenFalse);
    }

    public static LiteralExpressionSyntax LiteralExpression(SyntaxKind kind, SyntaxToken token)
    {
        return (LiteralExpressionSyntax)SyntaxFactoryInternal.LiteralExpression(kind, (SyntaxTokenInternal)token.Node!).CreateRed();
    }

    public static InvocationExpressionSyntax InvocationExpression(ExpressionSyntax expression, ArgumentListSyntax argumentList)
    {
        return (InvocationExpressionSyntax)SyntaxFactoryInternal.InvocationExpression(
            (ExpressionSyntaxInternal)expression.Green,
            (ArgumentListSyntaxInternal)argumentList.Green
        ).CreateRed();
    }

    public static InvocationExpressionSyntax InvocationExpression(ExpressionSyntax expression)
    {
        return InvocationExpression(expression, ArgumentList());
    }

    public static ElementAccessExpressionSyntax ElementAccessExpression(ExpressionSyntax expression, BracketedArgumentListSyntax argumentList)
    {
        return (ElementAccessExpressionSyntax)SyntaxFactoryInternal.ElementAccessExpression(
            (ExpressionSyntaxInternal)expression.Green,
            (BracketedArgumentListSyntaxInternal)argumentList.Green
        ).CreateRed();
    }

    public static ElementAccessExpressionSyntax ElementAccessExpression(ExpressionSyntax expression)
    {
        return ElementAccessExpression(expression, BracketedArgumentList());
    }

    public static ArgumentListSyntax ArgumentList(SyntaxToken openParenToken, SeparatedSyntaxList<ArgumentSyntax> arguments, SyntaxToken closeParenToken)
    {
        return (ArgumentListSyntax)SyntaxFactoryInternal.ArgumentList(
            (SyntaxTokenInternal)openParenToken.Node!,
            arguments.Node.ToGreenSeparatedList<ArgumentSyntaxInternal>(),
            (SyntaxTokenInternal)closeParenToken.Node!
        ).CreateRed();
    }

    public static ArgumentListSyntax ArgumentList(SeparatedSyntaxList<ArgumentSyntax> arguments = default)
    {
        return ArgumentList(Token(SyntaxKind.OpenParenToken), arguments, Token(SyntaxKind.CloseParenToken));
    }

    public static BracketedArgumentListSyntax BracketedArgumentList(SyntaxToken openBracketToken, SeparatedSyntaxList<ArgumentSyntax> arguments, SyntaxToken closeBracketToken)
    {
        return (BracketedArgumentListSyntax)SyntaxFactoryInternal.BracketedArgumentList(
            (SyntaxTokenInternal)openBracketToken.Node!,
            arguments.Node.ToGreenSeparatedList<ArgumentSyntaxInternal>(),
            (SyntaxTokenInternal)closeBracketToken.Node!
        ).CreateRed();
    }

    public static BracketedArgumentListSyntax BracketedArgumentList(SeparatedSyntaxList<ArgumentSyntax> arguments = default)
    {
        return BracketedArgumentList(Token(SyntaxKind.OpenBracketToken), arguments, Token(SyntaxKind.CloseBracketToken));
    }


    public static ArgumentSyntax Argument(SyntaxToken refKindKeyword, ExpressionSyntax expression)
    {
        return (ArgumentSyntax)SyntaxFactoryInternal.Argument(
            (SyntaxTokenInternal?)refKindKeyword.Node,
            (ExpressionSyntaxInternal)expression.Green
        ).CreateRed();
    }

    public static ArgumentSyntax Argument(ExpressionSyntax expression)
    {
        return Argument(default, expression);
    }

    public static CastExpressionSyntax CastExpression(SyntaxToken openParenToken, TypeSyntax type, SyntaxToken closeParenToken, ExpressionSyntax expression)
    {
        return (CastExpressionSyntax)SyntaxFactoryInternal.CastExpression(
            (SyntaxTokenInternal)openParenToken.Node!,
            (TypeSyntaxInternal)type.Green,
            (SyntaxTokenInternal)closeParenToken.Node!,
            (ExpressionSyntaxInternal)expression.Green
        ).CreateRed();
    }

    public static CastExpressionSyntax CastExpression(TypeSyntax type, ExpressionSyntax expression)
    {
        return CastExpression(Token(SyntaxKind.OpenParenToken), type, Token(SyntaxKind.CloseParenToken), expression);
    }

    public static InitializerExpressionSyntax InitializerExpression(SyntaxKind kind, SyntaxToken openBraceToken, SeparatedSyntaxList<ExpressionSyntax> expressions, SyntaxToken closeBraceToken)
    {
        return (InitializerExpressionSyntax)SyntaxFactoryInternal.InitializerExpression(
            kind,
            (SyntaxTokenInternal)openBraceToken.Node!,
            expressions.Node.ToGreenSeparatedList<ExpressionSyntaxInternal>(),
            (SyntaxTokenInternal)closeBraceToken.Node!
        ).CreateRed();
    }

    public static InitializerExpressionSyntax InitializerExpression(SyntaxKind kind, SeparatedSyntaxList<ExpressionSyntax> expressions)
    {
        return InitializerExpression(kind, Token(SyntaxKind.OpenBraceToken), expressions, Token(SyntaxKind.CloseBraceToken));
    }

    public static ArrayCreationExpressionSyntax ArrayCreationExpression(ArrayTypeSyntax type, InitializerExpressionSyntax? initializer)
    {
        return (ArrayCreationExpressionSyntax)SyntaxFactoryInternal.ArrayCreationExpression(
            (ArrayTypeSyntaxInternal)type.Green,
            (InitializerExpressionSyntaxInternal?)initializer?.Green
        ).CreateRed();
    }

    public static AttributeListSyntax AttributeList(SyntaxToken openBracketToken, SeparatedSyntaxList<AttributeSyntax> attributes, SyntaxToken closeBracketToken)
    {
        return (AttributeListSyntax)SyntaxFactoryInternal.AttributeList(
            (SyntaxTokenInternal)openBracketToken.Node!,
            attributes.Node.ToGreenSeparatedList<AttributeSyntaxInternal>(),
            (SyntaxTokenInternal)closeBracketToken.Node!
        ).CreateRed();
    }

    public static AttributeListSyntax AttributeList(SeparatedSyntaxList<AttributeSyntax> attributes)
    {
        return AttributeList(Token(SyntaxKind.OpenBracketToken), attributes, Token(SyntaxKind.CloseBracketToken));
    }

    public static AttributeSyntax Attribute(NameSyntax name, AttributeArgumentListSyntax? argumentList = default)
    {
        return (AttributeSyntax)SyntaxFactoryInternal.Attribute(
            (NameSyntaxInternal)name.Green,
            (AttributeArgumentListSyntaxInternal?)argumentList?.Green
        ).CreateRed();
    }

    public static AttributeArgumentListSyntax AttributeArgumentList(SyntaxToken openParenToken, SeparatedSyntaxList<AttributeArgumentSyntax> arguments, SyntaxToken closeParenToken)
    {
        return (AttributeArgumentListSyntax)SyntaxFactoryInternal.AttributeArgumentList(
            (SyntaxTokenInternal)openParenToken.Node!,
            arguments.Node.ToGreenSeparatedList<AttributeArgumentSyntaxInternal>(),
            (SyntaxTokenInternal)closeParenToken.Node!
        ).CreateRed();
    }

    public static AttributeArgumentListSyntax AttributeArgumentList(SeparatedSyntaxList<AttributeArgumentSyntax> arguments = default)
    {
        return AttributeArgumentList(Token(SyntaxKind.OpenParenToken), arguments, Token(SyntaxKind.CloseParenToken));
    }

    public static AttributeArgumentSyntax AttributeArgument(ExpressionSyntax expression)
    {
        return (AttributeArgumentSyntax)SyntaxFactoryInternal.AttributeArgument((ExpressionSyntaxInternal)expression.Green).CreateRed();
    }

    public static BlockSyntax Block(SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken openBraceToken, SyntaxList<StatementSyntax> statements, SyntaxToken closeBraceToken)
    {
        return (BlockSyntax)SyntaxFactoryInternal.Block(
            attributeLists.Node.ToGreenList<AttributeListSyntaxInternal>(),
            (SyntaxTokenInternal)openBraceToken.Node!,
            statements.Node.ToGreenList<StatementSyntaxInternal>(),
            (SyntaxTokenInternal)closeBraceToken.Node!
        ).CreateRed();
    }

    public static BlockSyntax Block(SyntaxList<StatementSyntax> statements)
    {
        return Block(List<AttributeListSyntax>(), Token(SyntaxKind.OpenBraceToken), statements, Token(SyntaxKind.CloseBraceToken));
    }

    public static LocalDeclarationStatementSyntax LocalDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, VariableDeclarationSyntax declaration, SyntaxToken semicolonToken)
    {
        return (LocalDeclarationStatementSyntax)SyntaxFactoryInternal.LocalDeclaration(
            attributeLists.Node.ToGreenList<AttributeListSyntaxInternal>(),
            modifiers.Node.ToGreenList<SyntaxTokenInternal>(),
            (VariableDeclarationSyntaxInternal)declaration.Green,
            (SyntaxTokenInternal)semicolonToken.Node!
        ).CreateRed();
    }

    public static LocalDeclarationStatementSyntax LocalDeclaration(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, VariableDeclarationSyntax declarations)
    {
        return LocalDeclaration(attributeLists, modifiers, declarations, Token(SyntaxKind.SemicolonToken));
    }

    public static LocalDeclarationStatementSyntax LocalDeclaration(VariableDeclarationSyntax declaration)
    {
        return LocalDeclaration(default, default, declaration);
    }

    public static VariableDeclarationSyntax VariableDeclaration(TypeSyntax type, SeparatedSyntaxList<VariableDeclaratorSyntax> variables)
    {
        return (VariableDeclarationSyntax)SyntaxFactoryInternal.VariableDeclaration(
            (TypeSyntaxInternal)type.Green,
            variables.Node.ToGreenSeparatedList<VariableDeclaratorSyntaxInternal>()
        ).CreateRed();
    }

    public static VariableDeclaratorSyntax VariableDeclarator(SyntaxToken identifier, EqualsValueClauseSyntax? initializer = default)
    {
        return (VariableDeclaratorSyntax)SyntaxFactoryInternal.VariableDeclarator(
            (SyntaxTokenInternal)identifier.Node!,
            (EqualsValueClauseSyntaxInternal?)initializer?.Green
        ).CreateRed();
    }

    public static EqualsValueClauseSyntax EqualsValueClause(SyntaxToken equalsToken, ExpressionSyntax expression)
    {
        return (EqualsValueClauseSyntax)SyntaxFactoryInternal.EqualsValueClause(
            (SyntaxTokenInternal)equalsToken.Node!,
            (ExpressionSyntaxInternal)expression.Green
        ).CreateRed();
    }

    public static EqualsValueClauseSyntax EqualsValueClause(ExpressionSyntax expression)
    {
        return EqualsValueClause(Token(SyntaxKind.EqualsToken), expression);
    }

    public static ExpressionStatementSyntax ExpressionStatement(SyntaxList<AttributeListSyntax> attributeLists, ExpressionSyntax expression, SyntaxToken semicolonToken)
    {
        return (ExpressionStatementSyntax)SyntaxFactoryInternal.ExpressionStatement(
            attributeLists.Node.ToGreenList<AttributeListSyntaxInternal>(),
            (ExpressionSyntaxInternal)expression.Green,
            (SyntaxTokenInternal)semicolonToken.Node!
        ).CreateRed();
    }

    public static ExpressionStatementSyntax ExpressionStatement(SyntaxList<AttributeListSyntax> attributeLists, ExpressionSyntax expression)
    {
        return ExpressionStatement(attributeLists, expression, Token(SyntaxKind.SemicolonToken));
    }

    public static EmptyStatementSyntax EmptyStatement(SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken semicolonToken)
    {
        return (EmptyStatementSyntax)SyntaxFactoryInternal.EmptyStatement(
            attributeLists.Node.ToGreenList<AttributeListSyntaxInternal>(),
            (SyntaxTokenInternal)semicolonToken.Node!
        ).CreateRed();
    }

    public static EmptyStatementSyntax EmptyStatement(SyntaxList<AttributeListSyntax> attributeLists)
    {
        return EmptyStatement(attributeLists, Token(SyntaxKind.SemicolonToken));
    }

    public static BreakStatementSyntax BreakStatement(SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken breakKeyword, SyntaxToken semicolonToken)
    {
        return (BreakStatementSyntax)SyntaxFactoryInternal.BreakStatement(
            attributeLists.Node.ToGreenList<AttributeListSyntaxInternal>(),
            (SyntaxTokenInternal)breakKeyword.Node!,
            (SyntaxTokenInternal)semicolonToken.Node!
        ).CreateRed();
    }

    public static BreakStatementSyntax BreakStatement(SyntaxList<AttributeListSyntax> attributeLists)
    {
        return BreakStatement(attributeLists, Token(SyntaxKind.BreakKeyword), Token(SyntaxKind.SemicolonToken));
    }

    public static ContinueStatementSyntax ContinueStatement(SyntaxList<AttributeListSyntax> attributeList, SyntaxToken continueKeyword, SyntaxToken semicolonToken)
    {
        return (ContinueStatementSyntax)SyntaxFactoryInternal.ContinueStatement(
            attributeList.Node.ToGreenList<AttributeListSyntaxInternal>(),
            (SyntaxTokenInternal)continueKeyword.Node!,
            (SyntaxTokenInternal)semicolonToken.Node!
        ).CreateRed();
    }

    public static ContinueStatementSyntax ContinueStatement(SyntaxList<AttributeListSyntax> attributeList)
    {
        return ContinueStatement(attributeList, Token(SyntaxKind.ContinueKeyword), Token(SyntaxKind.SemicolonToken));
    }

    public static ReturnStatementSyntax ReturnStatement(SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken returnKeyword, ExpressionSyntax? expression, SyntaxToken semicolonToken)
    {
        return (ReturnStatementSyntax)SyntaxFactoryInternal.ReturnStatement(
            attributeLists.Node.ToGreenList<AttributeListSyntaxInternal>(),
            (SyntaxTokenInternal)returnKeyword.Node!,
            (ExpressionSyntaxInternal?)expression?.Green,
            (SyntaxTokenInternal)semicolonToken.Node!
        ).CreateRed();
    }

    public static ReturnStatementSyntax ReturnStatement(SyntaxList<AttributeListSyntax> attributeLists, ExpressionSyntax? expression = null)
    {
        return ReturnStatement(attributeLists, Token(SyntaxKind.ReturnKeyword), expression, Token(SyntaxKind.SemicolonToken));
    }

    public static WhileStatementSyntax WhileStatement(SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken whileKeyword, SyntaxToken openParenToken, ExpressionSyntax condition, SyntaxToken closeParenToken, StatementSyntax statement)
    {
        return (WhileStatementSyntax)SyntaxFactoryInternal.WhileStatement(
            attributeLists.Node.ToGreenList<AttributeListSyntaxInternal>(),
            (SyntaxTokenInternal)whileKeyword.Node!,
            (SyntaxTokenInternal)openParenToken.Node!,
            (ExpressionSyntaxInternal)condition.Green,
            (SyntaxTokenInternal)closeParenToken.Node!,
            (StatementSyntaxInternal)statement.Green
        ).CreateRed();
    }

    public static WhileStatementSyntax WhileStatement(SyntaxList<AttributeListSyntax> attributeLists, ExpressionSyntax condition, StatementSyntax statement)
    {
        return WhileStatement(attributeLists, Token(SyntaxKind.WhileKeyword), Token(SyntaxKind.OpenParenToken), condition, Token(SyntaxKind.CloseParenToken), statement);
    }

    public static DoStatementSyntax DoStatement(SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken doKeyword, StatementSyntax statement, SyntaxToken whileKeyword, SyntaxToken openParenToken, ExpressionSyntax condition, SyntaxToken closeParenToken, SyntaxToken semicolonToken)
    {
        return (DoStatementSyntax)SyntaxFactoryInternal.DoStatement(
            attributeLists.Node.ToGreenList<AttributeListSyntaxInternal>(),
            (SyntaxTokenInternal)doKeyword.Node!,
            (StatementSyntaxInternal)statement.Green,
            (SyntaxTokenInternal)whileKeyword.Node!,
            (SyntaxTokenInternal)openParenToken.Node!,
            (ExpressionSyntaxInternal)condition.Green,
            (SyntaxTokenInternal)closeParenToken.Node!,
            (SyntaxTokenInternal)semicolonToken.Node!
        ).CreateRed();
    }

    public static DoStatementSyntax DoStatement(SyntaxList<AttributeListSyntax> attributeLists, StatementSyntax statement, ExpressionSyntax condition)
    {
        return DoStatement(attributeLists, Token(SyntaxKind.DoKeyword), statement, Token(SyntaxKind.WhileKeyword), Token(SyntaxKind.OpenParenToken), condition, Token(SyntaxKind.CloseParenToken), Token(SyntaxKind.SemicolonToken));
    }

    public static ForStatementSyntax ForStatement(SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken forKeyword, SyntaxToken openParenToken, VariableDeclarationSyntax? declaration, SeparatedSyntaxList<ExpressionSyntax> initializers, SyntaxToken firstSemicolonToken,
                                                  ExpressionSyntax? condition, SyntaxToken secondSemicolonToken, SeparatedSyntaxList<ExpressionSyntax> incrementoes, SyntaxToken closeParenToken, StatementSyntax statement)
    {
        return (ForStatementSyntax)SyntaxFactoryInternal.ForStatement(
            attributeLists.Node.ToGreenList<AttributeListSyntaxInternal>(),
            (SyntaxTokenInternal)forKeyword.Node!,
            (SyntaxTokenInternal)openParenToken.Node!,
            (VariableDeclarationSyntaxInternal?)declaration?.Green,
            initializers.Node.ToGreenSeparatedList<ExpressionSyntaxInternal>(),
            (SyntaxTokenInternal)firstSemicolonToken.Node!,
            (ExpressionSyntaxInternal?)condition?.Green,
            (SyntaxTokenInternal)secondSemicolonToken.Node!,
            incrementoes.Node.ToGreenSeparatedList<ExpressionSyntaxInternal>(),
            (SyntaxTokenInternal)closeParenToken.Node!,
            (StatementSyntaxInternal)statement.Green
        ).CreateRed();
    }

    public static ForStatementSyntax ForStatement(SyntaxList<AttributeListSyntax> attributeLists, VariableDeclarationSyntax? declaration, SeparatedSyntaxList<ExpressionSyntax> initializers, ExpressionSyntax? expression, SeparatedSyntaxList<ExpressionSyntax> incrementors, StatementSyntax statement)
    {
        return ForStatement(
            attributeLists,
            Token(SyntaxKind.ForKeyword),
            Token(SyntaxKind.OpenParenToken),
            declaration,
            initializers,
            Token(SyntaxKind.SemicolonToken),
            expression,
            Token(SyntaxKind.SemicolonToken),
            incrementors,
            Token(SyntaxKind.CloseParenToken),
            statement
        );
    }

    public static IfStatementSyntax IfStatement(SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken ifKeyword, SyntaxToken openParenToken, ExpressionSyntax condition, SyntaxToken closeParenToken, StatementSyntax statement, ElseClauseSyntax? @else)
    {
        return (IfStatementSyntax)SyntaxFactoryInternal.IfStatement(
            attributeLists.Node.ToGreenList<AttributeListSyntaxInternal>(),
            (SyntaxTokenInternal)ifKeyword.Node!,
            (SyntaxTokenInternal)openParenToken.Node!,
            (ExpressionSyntaxInternal)condition.Green,
            (SyntaxTokenInternal)closeParenToken.Node!,
            (StatementSyntaxInternal)statement.Green,
            (ElseClauseSyntaxInternal?)@else?.Green
        ).CreateRed();
    }

    public static IfStatementSyntax IfStatement(SyntaxList<AttributeListSyntax> attributeLists, ExpressionSyntax condition, StatementSyntax statement, ElseClauseSyntax? @else = default)
    {
        return IfStatement(attributeLists, Token(SyntaxKind.IfKeyword), Token(SyntaxKind.OpenParenToken), condition, Token(SyntaxKind.CloseParenToken), statement, @else);
    }
}