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
}