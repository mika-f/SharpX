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


    public static IdentifierNameSyntax IdentifierName(string identifier)
    {
        return IdentifierName(Identifier(identifier));
    }

    public static ArgumentSyntax Argument(ExpressionSyntax expression)
    {
        return (ArgumentSyntax)SyntaxFactoryInternal.Argument(
            (ExpressionSyntaxInternal)expression.Green
        ).CreateRed();
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