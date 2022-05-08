// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

namespace SharpX.ShaderLab.Syntax.InternalSyntax;

internal partial class SyntaxFactoryInternal
{
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