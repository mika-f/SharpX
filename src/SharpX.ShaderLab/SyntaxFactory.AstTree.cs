// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.Core.Extensions;
using SharpX.ShaderLab.Syntax;
using SharpX.ShaderLab.Syntax.InternalSyntax;

namespace SharpX.ShaderLab;

public partial class SyntaxFactory
{
    public static IdentifierNameSyntax IdentifierName(SyntaxToken identifier)
    {
        return (IdentifierNameSyntax)SyntaxFactoryInternal.IdentifierName(
            (SyntaxTokenInternal)identifier.Node!
        ).CreateRed();
    }

    public static QualifiedNameSyntax QualifiedName(NameSyntax left, SyntaxToken dotToken, SimpleNameSyntax right)
    {
        return (QualifiedNameSyntax)SyntaxFactoryInternal.QualifiedName(
            (NameSyntaxInternal)left.Green,
            (SyntaxTokenInternal)dotToken.Node!,
            (SimpleNameSyntaxInternal)right.Green
        ).CreateRed();
    }

    public static QualifiedNameSyntax QualifiedName(NameSyntax left, SimpleNameSyntax right)
    {
        return QualifiedName(left, Token(SyntaxKind.DotToken), right);
    }


    public static IdentifierNameSyntax IdentifierName(string identifier)
    {
        return IdentifierName(Identifier(identifier));
    }

    public static EqualsValueClauseSyntax EqualsValueClause(SyntaxToken equalsToken, ExpressionSyntax value)
    {
        return (EqualsValueClauseSyntax)SyntaxFactoryInternal.EqualsValueClause(
            (SyntaxTokenInternal)equalsToken.Node!,
            (ExpressionSyntaxInternal)value.Green
        ).CreateRed();
    }

    public static EqualsValueClauseSyntax EqualsValueClause(ExpressionSyntax value)
    {
        return EqualsValueClause(Token(SyntaxKind.EqualsToken), value);
    }

    public static ArgumentListSyntax ArgumentList(SyntaxToken openParenToken, SeparatedSyntaxList<ArgumentSyntax> arguments, SyntaxToken closeParenToken)
    {
        return (ArgumentListSyntax)SyntaxFactoryInternal.ArgumentList(
            (SyntaxTokenInternal)openParenToken.Node!,
            arguments.Node.ToGreenSeparatedList<ArgumentSyntaxInternal>(),
            (SyntaxTokenInternal)closeParenToken.Node!
        ).CreateRed();
    }

    public static ArgumentListSyntax ArgumentList(params ArgumentSyntax[] arguments)
    {
        return ArgumentList(Token(SyntaxKind.OpenParenToken), SeparatedList(arguments), Token(SyntaxKind.CloseParenToken));
    }

    public static ArgumentSyntax Argument(ExpressionSyntax expression)
    {
        return (ArgumentSyntax)SyntaxFactoryInternal.Argument(
            (ExpressionSyntaxInternal)expression.Green
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

    public static AttributeListSyntax AttributeList(params AttributeSyntax[] attributes)
    {
        return AttributeList(Token(SyntaxKind.OpenBracketToken), SeparatedList(attributes), Token(SyntaxKind.CloseBracketToken));
    }

    public static AttributeSyntax Attribute(NameSyntax name, ArgumentListSyntax argumentList)
    {
        return (AttributeSyntax)SyntaxFactoryInternal.Attribute(
            (NameSyntaxInternal)name.Green,
            (ArgumentListSyntaxInternal)argumentList.Green
        ).CreateRed();
    }

    public static LiteralExpressionSyntax LiteralExpression(SyntaxKind kind, SyntaxToken token)
    {
        return (LiteralExpressionSyntax)SyntaxFactoryInternal.LiteralExpression(
            kind,
            (SyntaxTokenInternal)token.Node!
        ).CreateRed();
    }

    public static TextureLiteralExpressionSyntax TextureLiteralExpression(LiteralExpressionSyntax value, SyntaxToken openBraceToken, SyntaxToken closeBraceToken)
    {
        return (TextureLiteralExpressionSyntax)SyntaxFactoryInternal.TextureLiteralExpression(
            (LiteralExpressionSyntaxInternal)value.Green,
            (SyntaxTokenInternal)openBraceToken.Node!,
            (SyntaxTokenInternal)closeBraceToken.Node!
        ).CreateRed();
    }

    public static TextureLiteralExpressionSyntax TextureLiteralExpression(LiteralExpressionSyntax value)
    {
        return TextureLiteralExpression(value, Token(SyntaxKind.OpenBraceToken), Token(SyntaxKind.CloseBraceToken));
    }

    public static TagsDeclarationSyntax TagsDeclaration(SyntaxToken tagsKeyword, SyntaxToken openBraceToken, SyntaxList<TagDeclarationSyntax> tags, SyntaxToken closeBraceToken)
    {
        return (TagsDeclarationSyntax)SyntaxFactoryInternal.TagsDeclaration(
            (SyntaxTokenInternal)tagsKeyword.Node!,
            (SyntaxTokenInternal)openBraceToken.Node!,
            tags.Node.ToGreenList<TagDeclarationSyntaxInternal>(),
            (SyntaxTokenInternal)closeBraceToken.Node!
        ).CreateRed();
    }

    public static TagsDeclarationSyntax TagsDeclaration(params TagDeclarationSyntax[] tags)
    {
        return TagsDeclaration(Token(SyntaxKind.TagsKeyword), Token(SyntaxKind.OpenBraceToken), List(tags), Token(SyntaxKind.CloseBraceToken));
    }

    public static TagDeclarationSyntax TagDeclaration(SyntaxToken key, SyntaxToken equalsToken, SyntaxToken value)
    {
        return (TagDeclarationSyntax)SyntaxFactoryInternal.TagDeclaration(
            (SyntaxTokenInternal)key.Node!,
            (SyntaxTokenInternal)equalsToken.Node!,
            (SyntaxTokenInternal)value.Node!
        ).CreateRed();
    }

    public static TagDeclarationSyntax TagDeclaration(string key, string value)
    {
        return TagDeclaration(Literal(key), Token(SyntaxKind.EqualsToken), Literal(value));
    }

    public static FallbackDeclarationSyntax FallbackDeclaration(SyntaxToken fallbackKeyword, SyntaxToken shaderNameOrOffKeyword)
    {
        return (FallbackDeclarationSyntax)SyntaxFactoryInternal.FallbackDeclaration(
            (SyntaxTokenInternal)fallbackKeyword.Node!,
            (SyntaxTokenInternal)shaderNameOrOffKeyword.Node!
        ).CreateRed();
    }

    public static FallbackDeclarationSyntax FallbackDeclaration(SyntaxToken shaderNameOrOffKeyword)
    {
        return FallbackDeclaration(Token(SyntaxKind.FallbackKeyword), shaderNameOrOffKeyword);
    }

    public static FallbackDeclarationSyntax FallbackDeclaration(string shaderName)
    {
        return FallbackDeclaration(Literal(shaderName));
    }

    public static FallbackDeclarationSyntax FallbackDeclaration()
    {
        return FallbackDeclaration(Token(SyntaxKind.OffKeyword));
    }

    public static CustomEditorDeclarationSyntax CustomEditorDeclaration(SyntaxToken customEditorKeyword, SyntaxToken fullyQualifiedInspectorName)
    {
        return (CustomEditorDeclarationSyntax)SyntaxFactoryInternal.CustomEditorDeclaration(
            (SyntaxTokenInternal)customEditorKeyword.Node!,
            (SyntaxTokenInternal)fullyQualifiedInspectorName.Node!
        ).CreateRed();
    }

    public static CustomEditorDeclarationSyntax CustomEditorDeclaration(SyntaxToken fullyQualifiedInspectorName)
    {
        return CustomEditorDeclaration(Token(SyntaxKind.CustomEditorKeyword), fullyQualifiedInspectorName);
    }

    public static CustomEditorDeclarationSyntax CustomEditorDeclaration(string fullyQualifiedInspectorName)
    {
        return CustomEditorDeclaration(Literal(fullyQualifiedInspectorName));
    }
}