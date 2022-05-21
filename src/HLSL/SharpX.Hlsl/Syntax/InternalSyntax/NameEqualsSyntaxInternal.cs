// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using Microsoft.CodeAnalysis;

using SharpX.Core;

using SyntaxNode = SharpX.Core.SyntaxNode;

namespace SharpX.Hlsl.Syntax.InternalSyntax;

internal class NameEqualsSyntaxInternal : HlslSyntaxNodeInternal
{
    public IdentifierNameSyntaxInternal Name { get; }

    public SyntaxTokenInternal EqualsToken { get; }

    public NameEqualsSyntaxInternal(SyntaxKind kind, IdentifierNameSyntaxInternal name, SyntaxTokenInternal equalsToken) : base(kind)
    {
        SlotCount = 2;

        AdjustWidth(name);
        Name = name;

        AdjustWidth(equalsToken);
        EqualsToken = equalsToken;
    }

    public NameEqualsSyntaxInternal(SyntaxKind kind, IdentifierNameSyntaxInternal name, SyntaxTokenInternal equalsToken, DiagnosticInfo[]? diagnostics, SyntaxAnnotation[]? annotations) : base(kind, diagnostics, annotations)
    {
        SlotCount = 2;

        AdjustWidth(name);
        Name = name;

        AdjustWidth(equalsToken);
        EqualsToken = equalsToken;
    }

    public override GreenNode SetAnnotations(SyntaxAnnotation[]? annotations)
    {
        return new NameEqualsSyntaxInternal(Kind, Name, EqualsToken, GetDiagnostics(), annotations);
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new NameEqualsSyntaxInternal(Kind, Name, EqualsToken, diagnostics, GetAnnotations());
    }

    public override GreenNode? GetSlot(int index)
    {
        return index switch
        {
            0 => Name,
            1 => EqualsToken,
            _ => null
        };
    }

    public override SyntaxNode CreateRed(SyntaxNode? parent, int position)
    {
        return new NameEqualsSyntax(this, parent, position);
    }

    public override TResult? Accept<TResult>(HlslSyntaxVisitorInternal<TResult> visitor) where TResult : default
    {
        return visitor.VisitNameEquals(this);
    }
}