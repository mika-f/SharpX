// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using Microsoft.CodeAnalysis;

using SharpX.Core;

using SyntaxNode = SharpX.Core.SyntaxNode;

namespace SharpX.Hlsl.Syntax.InternalSyntax;

internal class PredefinedTypeSyntaxInternal : TypeSyntaxInternal
{
    public SyntaxTokenInternal Keyword { get; }

    public PredefinedTypeSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal keyword) : base(kind)
    {
        SlotCount = 1;

        AdjustWidth(keyword);
        Keyword = keyword;
    }

    public PredefinedTypeSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal keyword, DiagnosticInfo[]? diagnostics, SyntaxAnnotation[]? annotations) : base(kind, diagnostics, annotations)
    {
        SlotCount = 1;

        AdjustWidth(keyword);
        Keyword = keyword;
    }

    public override GreenNode SetAnnotations(SyntaxAnnotation[]? annotations)
    {
        return new PredefinedTypeSyntaxInternal(Kind, Keyword, GetDiagnostics(), annotations);
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new PredefinedTypeSyntaxInternal(Kind, Keyword, diagnostics, GetAnnotations());
    }

    public override GreenNode? GetSlot(int index)
    {
        return index switch
        {
            0 => Keyword,
            _ => null
        };
    }

    public override SyntaxNode CreateRed(SyntaxNode? parent, int position)
    {
        return new PredefinedTypeSyntax(this, parent, position);
    }
}