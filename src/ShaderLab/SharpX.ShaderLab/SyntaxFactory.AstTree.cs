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

    public static AttributeSyntax Attribute(NameSyntax name, ArgumentListSyntax? argumentList)
    {
        return (AttributeSyntax)SyntaxFactoryInternal.Attribute(
            (NameSyntaxInternal)name.Green,
            (ArgumentListSyntaxInternal?)argumentList?.Green
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

    public static CompilationUnitSyntax CompilationUnit(ShaderDeclarationSyntax shader, SyntaxToken endOfFileToken)
    {
        return (CompilationUnitSyntax)SyntaxFactoryInternal.CompilationUnit(
            (ShaderDeclarationSyntaxInternal)shader.Green,
            (SyntaxTokenInternal)endOfFileToken.Node!
        ).CreateRed();
    }

    public static CompilationUnitSyntax CompilationUnit(ShaderDeclarationSyntax shader)
    {
        return CompilationUnit(shader, Token(SyntaxKind.EndOfFileToken));
    }

    public static ShaderDeclarationSyntax ShaderDeclaration(SyntaxToken shaderKeyword, SyntaxToken identifier, SyntaxToken openBraceToken, PropertiesDeclarationSyntax? properties, CgIncludeDeclarationSyntax? cgInclude, SyntaxList<SubShaderDeclarationSyntax> subShaders,
                                                            FallbackDeclarationSyntax? fallback, CustomEditorDeclarationSyntax? customEditor, SyntaxToken closeBraceToken)
    {
        return (ShaderDeclarationSyntax)SyntaxFactoryInternal.ShaderDeclaration(
            (SyntaxTokenInternal)shaderKeyword.Node!,
            (SyntaxTokenInternal)identifier.Node!,
            (SyntaxTokenInternal)openBraceToken.Node!,
            (PropertiesDeclarationSyntaxInternal?)properties?.Green,
            (CgIncludeDeclarationSyntaxInternal?)cgInclude?.Green,
            subShaders.Node.ToGreenList<SubShaderDeclarationSyntaxInternal>(),
            (FallbackDeclarationSyntaxInternal?)fallback?.Green,
            (CustomEditorDeclarationSyntaxInternal?)customEditor?.Green,
            (SyntaxTokenInternal)closeBraceToken.Node!
        ).CreateRed();
    }

    public static ShaderDeclarationSyntax ShaderDeclaration(string identifier, PropertiesDeclarationSyntax? properties, CgIncludeDeclarationSyntax? cgInclude, SyntaxList<SubShaderDeclarationSyntax> subShaders, FallbackDeclarationSyntax? fallback, CustomEditorDeclarationSyntax? customEditor)
    {
        return ShaderDeclaration(Token(SyntaxKind.ShaderKeyword), StringLiteral(identifier), Token(SyntaxKind.OpenBraceToken), properties, cgInclude, subShaders, fallback, customEditor, Token(SyntaxKind.CloseBraceToken));
    }

    public static PropertiesDeclarationSyntax PropertiesDeclaration(SyntaxToken propertiesKeyword, SyntaxToken openBraceToken, SyntaxList<PropertyDeclarationSyntax> properties, SyntaxToken closeBraceToken)
    {
        return (PropertiesDeclarationSyntax)SyntaxFactoryInternal.PropertiesDeclaration(
            (SyntaxTokenInternal)propertiesKeyword.Node!,
            (SyntaxTokenInternal)openBraceToken.Node!,
            properties.Node.ToGreenList<PropertyDeclarationSyntaxInternal>(),
            (SyntaxTokenInternal)closeBraceToken.Node!
        ).CreateRed();
    }

    public static PropertiesDeclarationSyntax PropertiesDeclaration(params PropertyDeclarationSyntax[] properties)
    {
        return PropertiesDeclaration(Token(SyntaxKind.PropertiesKeyword), Token(SyntaxKind.OpenBraceToken), List(properties), Token(SyntaxKind.CloseBraceToken));
    }

    public static PropertyDeclarationSyntax PropertyDeclaration(AttributeListSyntax? attributeList, SyntaxToken identifier, SyntaxToken openParenToken, SyntaxToken displayName, SyntaxToken commaToken, SimpleNameSyntax type, ArgumentListSyntax? argumentList, SyntaxToken closeParenToken,
                                                                EqualsValueClauseSyntax @default)
    {
        return (PropertyDeclarationSyntax)SyntaxFactoryInternal.PropertyDeclaration(
            (AttributeListSyntaxInternal?)attributeList?.Green,
            (SyntaxTokenInternal)identifier.Node!,
            (SyntaxTokenInternal)openParenToken.Node!,
            (SyntaxTokenInternal)displayName.Node!,
            (SyntaxTokenInternal)commaToken.Node!,
            (SimpleNameSyntaxInternal)type.Green,
            (ArgumentListSyntaxInternal?)argumentList?.Green,
            (SyntaxTokenInternal)closeParenToken.Node!,
            (EqualsValueClauseSyntaxInternal)@default.Green
        ).CreateRed();
    }

    public static PropertyDeclarationSyntax PropertyDeclaration(AttributeListSyntax? attributeList, string identifier, string displayName, SimpleNameSyntax type, ArgumentListSyntax? argumentList, EqualsValueClauseSyntax @default)
    {
        return PropertyDeclaration(attributeList, Identifier(identifier), Token(SyntaxKind.OpenParenToken), StringLiteral(displayName), Token(SyntaxKind.CommaToken), type, argumentList, Token(SyntaxKind.CloseParenToken), @default);
    }

    public static SubShaderDeclarationSyntax SubShaderDeclaration(SyntaxToken subShaderKeyword, SyntaxToken openBraceToken, TagsDeclarationSyntax? tags, SyntaxList<CommandDeclarationSyntax> commands, CgIncludeDeclarationSyntax? cgInclude, SyntaxList<BasePassDeclarationSyntax> passes,
                                                                  SyntaxToken closeBraceToken)
    {
        return (SubShaderDeclarationSyntax)SyntaxFactoryInternal.SubShaderDeclaration(
            (SyntaxTokenInternal)subShaderKeyword.Node!,
            (SyntaxTokenInternal)openBraceToken.Node!,
            (TagsDeclarationSyntaxInternal?)tags?.Green,
            commands.Node.ToGreenList<CommandDeclarationSyntaxInternal>(),
            (CgIncludeDeclarationSyntaxInternal?)cgInclude?.Green,
            passes.Node.ToGreenList<BasePassDeclarationSyntaxInternal>(),
            (SyntaxTokenInternal)closeBraceToken.Node!
        ).CreateRed();
    }

    public static SubShaderDeclarationSyntax SubShaderDeclaration(TagsDeclarationSyntax? tags, SyntaxList<CommandDeclarationSyntax> commands, CgIncludeDeclarationSyntax? cgInclude, SyntaxList<BasePassDeclarationSyntax> passes)
    {
        return SubShaderDeclaration(
            Token(SyntaxKind.SubShaderKeyword),
            Token(SyntaxKind.OpenBraceToken),
            tags,
            commands,
            cgInclude,
            passes,
            Token(SyntaxKind.CloseBraceToken)
        );
    }

    public static CgIncludeDeclarationSyntax CgIncludeDeclaration(SyntaxToken cgIncludeKeyword, SyntaxNode source, SyntaxToken endCgKeyword)
    {
        return (CgIncludeDeclarationSyntax)SyntaxFactoryInternal.CgIncludeDeclaration(
            (SyntaxTokenInternal)cgIncludeKeyword.Node!,
            source.Green,
            (SyntaxTokenInternal)endCgKeyword.Node!
        ).CreateRed();
    }

    public static CgIncludeDeclarationSyntax CgIncludeDeclaration(SyntaxNode source)
    {
        return CgIncludeDeclaration(Token(SyntaxKind.CgIncludeKeyword), source, Token(SyntaxKind.EndCgKeyword));
    }

    public static CgProgramDeclarationSyntax CgProgramDeclaration(SyntaxToken cgProgramKeyword, SyntaxNode source, SyntaxToken endCgKeyword)
    {
        return (CgProgramDeclarationSyntax)SyntaxFactoryInternal.CgProgramDeclaration(
            (SyntaxTokenInternal)cgProgramKeyword.Node!,
            source.Green,
            (SyntaxTokenInternal)endCgKeyword.Node!
        ).CreateRed();
    }

    public static CgProgramDeclarationSyntax CgProgramDeclaration(SyntaxNode source)
    {
        return CgProgramDeclaration(Token(SyntaxKind.CgProgramKeyword), source, Token(SyntaxKind.EndCgKeyword));
    }

    public static PassDeclarationSyntax PassDeclaration(SyntaxToken keyword, SyntaxToken openBraceToken, TagsDeclarationSyntax? tags, SyntaxList<BaseCommandDeclarationSyntax> commands, CgProgramDeclarationSyntax cgProgram, SyntaxToken closeBraceToken)
    {
        return (PassDeclarationSyntax)SyntaxFactoryInternal.PassDeclaration(
            (SyntaxTokenInternal)keyword.Node!,
            (SyntaxTokenInternal)openBraceToken.Node!,
            (TagsDeclarationSyntaxInternal?)tags?.Green,
            commands.Node.ToGreenList<BaseCommandDeclarationSyntaxInternal>(),
            (CgProgramDeclarationSyntaxInternal)cgProgram.Green,
            (SyntaxTokenInternal)closeBraceToken.Node!
        ).CreateRed();
    }

    public static PassDeclarationSyntax PassDeclaration(TagsDeclarationSyntax? tags, SyntaxList<BaseCommandDeclarationSyntax> commands, CgProgramDeclarationSyntax cgProgram)
    {
        return PassDeclaration(Token(SyntaxKind.PassKeyword), Token(SyntaxKind.OpenBraceToken), tags, commands, cgProgram, Token(SyntaxKind.CloseBraceToken));
    }

    public static GrabPassDeclarationSyntax GrabPassDeclaration(SyntaxToken keyword, SyntaxToken openBraceToken, SyntaxToken? identifier, TagsDeclarationSyntax? tags, NameDeclarationSyntax? name, SyntaxToken closeBraceToken)
    {
        return (GrabPassDeclarationSyntax)SyntaxFactoryInternal.GrabPassDeclaration(
            (SyntaxTokenInternal)keyword.Node!,
            (SyntaxTokenInternal)openBraceToken.Node!,
            (SyntaxTokenInternal?)identifier?.Node,
            (TagsDeclarationSyntaxInternal?)tags?.Green,
            (NameDeclarationSyntaxInternal?)name?.Green,
            (SyntaxTokenInternal)closeBraceToken.Node!
        ).CreateRed();
    }

    public static GrabPassDeclarationSyntax GrabPassDeclaration(string? identifier = null, TagsDeclarationSyntax? tags = null, NameDeclarationSyntax? name = null)
    {
        return GrabPassDeclaration(
            Token(SyntaxKind.GrabPassKeyword),
            Token(SyntaxKind.OpenBraceToken),
            identifier != null ? Identifier(identifier) : null,
            tags,
            name,
            Token(SyntaxKind.CloseBraceToken)
        );
    }

    public static UsePassDeclarationSyntax UsePassDeclaration(SyntaxToken keyword, SyntaxToken passReference)
    {
        return (UsePassDeclarationSyntax)SyntaxFactoryInternal.UsePassDeclaration(
            (SyntaxTokenInternal)keyword.Node!,
            (SyntaxTokenInternal)passReference.Node!
        ).CreateRed();
    }

    public static UsePassDeclarationSyntax UsePassDeclaration(string passReference)
    {
        return UsePassDeclaration(Token(SyntaxKind.UsePassKeyword), StringLiteral(passReference));
    }

    public static CommandDeclarationSyntax CommandDeclaration(SyntaxToken keyword, SeparatedSyntaxList<IdentifierNameSyntax> arguments)
    {
        return (CommandDeclarationSyntax)SyntaxFactoryInternal.CommandDeclaration(
            (SyntaxTokenInternal)keyword.Node!,
            arguments.Node.ToGreenSeparatedList<IdentifierNameSyntaxInternal>()
        ).CreateRed();
    }

    public static CommandDeclarationSyntax CommandDeclaration(string keyword, params string[] identifiers)
    {
        return CommandDeclaration(Identifier(keyword), SeparatedList(identifiers.Select(IdentifierName)));
    }

    public static StencilDeclarationSyntax StencilDeclaration(SyntaxToken keyword, SyntaxToken openBraceToken, SyntaxList<CommandDeclarationSyntax> commands, SyntaxToken closeBraceToken)
    {
        return (StencilDeclarationSyntax)SyntaxFactoryInternal.StencilDeclaration(
            (SyntaxTokenInternal)keyword.Node!,
            (SyntaxTokenInternal)openBraceToken.Node!,
            commands.Node.ToGreenList<CommandDeclarationSyntaxInternal>(),
            (SyntaxTokenInternal)closeBraceToken.Node!
        ).CreateRed();
    }

    public static StencilDeclarationSyntax StencilDeclaration(params CommandDeclarationSyntax[] commands)
    {
        return StencilDeclaration(Token(SyntaxKind.StencilKeyword), Token(SyntaxKind.OpenBraceToken), List(commands), Token(SyntaxKind.CloseBraceToken));
    }

    public static NameDeclarationSyntax NameDeclaration(SyntaxToken keyword, SyntaxToken name)
    {
        return (NameDeclarationSyntax)SyntaxFactoryInternal.NameDeclaration(
            (SyntaxTokenInternal)keyword.Node!,
            (SyntaxTokenInternal)name.Node!
        ).CreateRed();
    }

    public static NameDeclarationSyntax NameDeclaration(string name)
    {
        return NameDeclaration(Token(SyntaxKind.NameKeyword), StringLiteral(name));
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
        return TagDeclaration(StringLiteral(key), Token(SyntaxKind.EqualsToken), StringLiteral(value));
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
        return FallbackDeclaration(StringLiteral(shaderName));
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
        return CustomEditorDeclaration(StringLiteral(fullyQualifiedInspectorName));
    }

    public static HlslSourceSyntax HlslSource(SyntaxList<SyntaxNode> sources)
    {
        return (HlslSourceSyntax)SyntaxFactoryInternal.HlslSource(sources.Node.ToGreenList<GreenNode>()).CreateRed();
    }
}