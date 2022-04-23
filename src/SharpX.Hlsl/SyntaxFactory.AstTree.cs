﻿// ------------------------------------------------------------------------------------------
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
}