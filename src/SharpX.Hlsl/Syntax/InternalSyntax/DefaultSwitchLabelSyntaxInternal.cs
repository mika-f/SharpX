// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using Microsoft.CodeAnalysis;

using SharpX.Core;

using SyntaxNode = SharpX.Core.SyntaxNode;

namespace SharpX.Hlsl.Syntax.InternalSyntax;

internal class DefaultSwitchLabelSyntaxInternal : SwitchLabelSyntaxInternal
{
    public SyntaxTokenInternal DefaultKeyword { get; }

    public SyntaxTokenInternal ColonToken { get; }

    public DefaultSwitchLabelSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal defaultKeyword, SyntaxTokenInternal colonToken) : base(kind)
    {
        SlotCount = 2;

        AdjustWidth(defaultKeyword);
        DefaultKeyword = defaultKeyword;

        AdjustWidth(colonToken);
        ColonToken = colonToken;
    }

    public DefaultSwitchLabelSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal defaultKeyword, SyntaxTokenInternal colonToken, DiagnosticInfo[]? diagnostics, SyntaxAnnotation[]? annotations) : base(kind, diagnostics, annotations)
    {
        SlotCount = 2;

        AdjustWidth(defaultKeyword);
        DefaultKeyword = defaultKeyword;

        AdjustWidth(colonToken);
        ColonToken = colonToken;
    }

    public override GreenNode SetAnnotations(SyntaxAnnotation[]? annotations)
    {
        return new DefaultSwitchLabelSyntaxInternal(Kind, DefaultKeyword, ColonToken, GetDiagnostics(), annotations);
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new DefaultSwitchLabelSyntaxInternal(Kind, DefaultKeyword, ColonToken, diagnostics, GetAnnotations());
    }

    public override GreenNode? GetSlot(int index)
    {
        return index switch
        {
            0 => DefaultKeyword,
            1 => ColonToken,
            _ => null
        };
    }

    public override SyntaxNode CreateRed(SyntaxNode? parent, int position)
    {
        return new DefaultSwitchLabelSyntax(this, parent, position);
    }
}