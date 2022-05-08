﻿// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.ShaderLab.Syntax;
using SharpX.ShaderLab.Syntax.InternalSyntax;

namespace SharpX.ShaderLab;

public partial class SyntaxFactory
{
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