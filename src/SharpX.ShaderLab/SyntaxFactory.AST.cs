// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.ShaderLab.Syntax;
using SharpX.ShaderLab.Syntax.InternalSyntax;

namespace SharpX.ShaderLab;

public partial class SyntaxFactory
{
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
}