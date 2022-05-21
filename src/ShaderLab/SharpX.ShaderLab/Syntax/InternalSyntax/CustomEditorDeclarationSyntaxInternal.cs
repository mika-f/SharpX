// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using Microsoft.CodeAnalysis;

using SharpX.Core;

using SyntaxNode = SharpX.Core.SyntaxNode;

namespace SharpX.ShaderLab.Syntax.InternalSyntax;

internal class CustomEditorDeclarationSyntaxInternal : ShaderLabSyntaxNodeInternal
{
    public SyntaxTokenInternal CustomEditorKeyword { get; }

    public SyntaxTokenInternal FullyQualifiedInspectorName { get; }

    public CustomEditorDeclarationSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal customEditorKeyword, SyntaxTokenInternal fullyQualifiedInspectorName) : base(kind)
    {
        SlotCount = 2;

        AdjustWidth(customEditorKeyword);
        CustomEditorKeyword = customEditorKeyword;

        AdjustWidth(fullyQualifiedInspectorName);
        FullyQualifiedInspectorName = fullyQualifiedInspectorName;
    }

    public CustomEditorDeclarationSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal customEditorKeyword, SyntaxTokenInternal fullyQualifiedInspectorName, DiagnosticInfo[]? diagnostics, SyntaxAnnotation[]? annotations) : base(kind, diagnostics, annotations)
    {
        SlotCount = 2;

        AdjustWidth(customEditorKeyword);
        CustomEditorKeyword = customEditorKeyword;

        AdjustWidth(fullyQualifiedInspectorName);
        FullyQualifiedInspectorName = fullyQualifiedInspectorName;
    }

    public override GreenNode SetAnnotations(SyntaxAnnotation[]? annotations)
    {
        return new CustomEditorDeclarationSyntaxInternal(Kind, CustomEditorKeyword, FullyQualifiedInspectorName, GetDiagnostics(), annotations);
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new CustomEditorDeclarationSyntaxInternal(Kind, CustomEditorKeyword, FullyQualifiedInspectorName, diagnostics, GetAnnotations());
    }

    public override GreenNode? GetSlot(int index)
    {
        return index switch
        {
            0 => CustomEditorKeyword,
            1 => FullyQualifiedInspectorName,
            _ => null
        };
    }

    public override SyntaxNode CreateRed(SyntaxNode? parent, int position)
    {
        return new CustomEditorDeclarationSyntax(this, parent, position);
    }
}