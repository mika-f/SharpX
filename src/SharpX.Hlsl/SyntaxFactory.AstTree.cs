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
}