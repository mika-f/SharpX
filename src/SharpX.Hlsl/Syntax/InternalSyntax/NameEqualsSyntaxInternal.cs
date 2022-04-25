// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;

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

    public NameEqualsSyntaxInternal(SyntaxKind kind, IdentifierNameSyntaxInternal name, SyntaxTokenInternal equalsToken, DiagnosticInfo[]? diagnostics) : base(kind, diagnostics)
    {
        SlotCount = 2;

        AdjustWidth(name);
        Name = name;

        AdjustWidth(equalsToken);
        EqualsToken = equalsToken;
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new NameEqualsSyntaxInternal(Kind, Name, EqualsToken, diagnostics);
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
}