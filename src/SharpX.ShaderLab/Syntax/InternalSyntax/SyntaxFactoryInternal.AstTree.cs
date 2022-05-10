// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core.Syntax.InternalSyntax;

namespace SharpX.ShaderLab.Syntax.InternalSyntax;

internal partial class SyntaxFactoryInternal
{
    public static IdentifierNameSyntaxInternal IdentifierName(SyntaxTokenInternal identifier)
    {
        if (identifier.Kind != SyntaxKind.IdentifierName)
            throw new ArgumentException(nameof(identifier));

        return new IdentifierNameSyntaxInternal(SyntaxKind.IdentifierName, identifier);
    }

    public static QualifiedNameSyntaxInternal QualifiedName(NameSyntaxInternal left, SyntaxTokenInternal dotToken, SimpleNameSyntaxInternal right)
    {
        if (dotToken.Kind != SyntaxKind.DotToken)
            throw new ArgumentException(nameof(dotToken));

        return new QualifiedNameSyntaxInternal(SyntaxKind.QualifiedName, left, dotToken, right);
    }

    public static EqualsValueClauseSyntaxInternal EqualsValueClause(SyntaxTokenInternal equalsToken, ExpressionSyntaxInternal value)
    {
        if (equalsToken.Kind != SyntaxKind.EqualsToken)
            throw new ArgumentException(nameof(equalsToken));

        return new EqualsValueClauseSyntaxInternal(SyntaxKind.EqualsValueClause, equalsToken, value);
    }

    public static ArgumentListSyntaxInternal ArgumentList(SyntaxTokenInternal openParenToken, SeparatedSyntaxListInternal<ArgumentSyntaxInternal> arguments, SyntaxTokenInternal closeParenToken)
    {
        if (openParenToken.Kind != SyntaxKind.OpenParenToken)
            throw new ArgumentException(nameof(openParenToken));
        if (closeParenToken.Kind != SyntaxKind.CloseParenToken)
            throw new ArgumentException(nameof(closeParenToken));

        return new ArgumentListSyntaxInternal(SyntaxKind.ArgumentList, openParenToken, arguments.Node!, closeParenToken);
    }

    public static ArgumentSyntaxInternal Argument(ExpressionSyntaxInternal expression)
    {
        return new ArgumentSyntaxInternal(SyntaxKind.Argument, expression);
    }

    public static AttributeListSyntaxInternal AttributeList(SyntaxTokenInternal openBracketToken, SeparatedSyntaxListInternal<AttributeSyntaxInternal> attributes, SyntaxTokenInternal closeBracketToken)
    {
        if (openBracketToken.Kind != SyntaxKind.OpenBracketToken)
            throw new ArgumentException(nameof(openBracketToken));
        if (closeBracketToken.Kind != SyntaxKind.CloseBracketToken)
            throw new ArgumentException(nameof(closeBracketToken));

        return new AttributeListSyntaxInternal(SyntaxKind.AttributeList, openBracketToken, attributes.Node!, closeBracketToken);
    }

    public static AttributeSyntaxInternal Attribute(NameSyntaxInternal name, ArgumentListSyntaxInternal argumentList)
    {
        return new AttributeSyntaxInternal(SyntaxKind.Attribute, name, argumentList);
    }

    public static LiteralExpressionSyntaxInternal LiteralExpression(SyntaxKind kind, SyntaxTokenInternal value)
    {
        switch (kind)
        {
            case SyntaxKind.NumericLiteralExpression:
            case SyntaxKind.StringLiteralExpression:
                break;

            default:
                throw new ArgumentException(nameof(kind));
        }

        switch (value.Kind)
        {
            case SyntaxKind.NumericLiteralToken:
            case SyntaxKind.StringLiteralToken:
                break;

            default:
                throw new ArgumentException(nameof(value));
        }

        return new LiteralExpressionSyntaxInternal(kind, value);
    }

    public static TextureLiteralExpressionSyntaxInternal TextureLiteralExpression(LiteralExpressionSyntaxInternal value, SyntaxTokenInternal openBraceToken, SyntaxTokenInternal closeBraceToken)
    {
        if (openBraceToken.Kind != SyntaxKind.OpenBraceToken)
            throw new ArgumentException(nameof(openBraceToken));
        if (closeBraceToken.Kind != SyntaxKind.CloseBraceToken)
            throw new ArgumentException(nameof(closeBraceToken));

        return new TextureLiteralExpressionSyntaxInternal(SyntaxKind.TextureLiteralExpression, value, openBraceToken, closeBraceToken);
    }


    public static TagsDeclarationSyntaxInternal TagsDeclaration(SyntaxTokenInternal tagsKeyword, SyntaxTokenInternal openBraceToken, SyntaxListInternal<TagDeclarationSyntaxInternal> tags, SyntaxTokenInternal closeBraceToken)
    {
        if (tagsKeyword.Kind != SyntaxKind.TagsKeyword)
            throw new ArgumentException(nameof(tagsKeyword));
        if (openBraceToken.Kind != SyntaxKind.OpenBraceToken)
            throw new ArgumentException(nameof(openBraceToken));
        if (closeBraceToken.Kind != SyntaxKind.CloseBraceToken)
            throw new ArgumentException(nameof(closeBraceToken));

        return new TagsDeclarationSyntaxInternal(SyntaxKind.TagsDeclaration, tagsKeyword, openBraceToken, tags.Node!, closeBraceToken);
    }

    public static TagDeclarationSyntaxInternal TagDeclaration(SyntaxTokenInternal key, SyntaxTokenInternal equalsToken, SyntaxTokenInternal value)
    {
        if (key.Kind != SyntaxKind.StringLiteralToken)
            throw new ArgumentException(nameof(key));
        if (equalsToken.Kind != SyntaxKind.EqualsToken)
            throw new ArgumentException(nameof(equalsToken));
        if (value.Kind != SyntaxKind.StringLiteralToken)
            throw new ArgumentException(nameof(value));

        return new TagDeclarationSyntaxInternal(SyntaxKind.TagDeclaration, key, equalsToken, value);
    }

    public static FallbackDeclarationSyntaxInternal FallbackDeclaration(SyntaxTokenInternal fallbackKeyword, SyntaxTokenInternal shaderNameOrOffKeyword)
    {
        if (fallbackKeyword.Kind != SyntaxKind.FallbackKeyword)
            throw new ArgumentException(nameof(fallbackKeyword));

        switch (shaderNameOrOffKeyword.Kind)
        {
            case SyntaxKind.StringLiteralToken:
            case SyntaxKind.OffKeyword:
                break;

            default:
                throw new ArgumentException(nameof(shaderNameOrOffKeyword));
        }

        return new FallbackDeclarationSyntaxInternal(SyntaxKind.FallbackDeclaration, fallbackKeyword, shaderNameOrOffKeyword);
    }

    public static CustomEditorDeclarationSyntaxInternal CustomEditorDeclaration(SyntaxTokenInternal customEditorKeyword, SyntaxTokenInternal fullyQualifiedInspectorName)
    {
        if (customEditorKeyword.Kind != SyntaxKind.CustomEditorKeyword)
            throw new ArgumentException(nameof(customEditorKeyword));
        if (fullyQualifiedInspectorName.Kind != SyntaxKind.StringLiteralToken)
            throw new ArgumentException(nameof(fullyQualifiedInspectorName));

        return new CustomEditorDeclarationSyntaxInternal(SyntaxKind.CustomEditorDeclaration, customEditorKeyword, fullyQualifiedInspectorName);
    }
}