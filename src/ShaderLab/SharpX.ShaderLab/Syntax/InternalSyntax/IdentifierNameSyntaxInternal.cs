// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;

namespace SharpX.ShaderLab.Syntax.InternalSyntax;

internal class IdentifierNameSyntaxInternal : SimpleNameSyntaxInternal
{
    public override SyntaxTokenInternal Identifier { get; }

    public IdentifierNameSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal identifier) : base(kind)
    {
        SlotCount = 1;

        AdjustWidth(identifier);
        Identifier = identifier;
    }

    public IdentifierNameSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal identifier, DiagnosticInfo[]? diagnostics) : base(kind, diagnostics)
    {
        SlotCount = 1;

        AdjustWidth(identifier);
        Identifier = identifier;
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new IdentifierNameSyntaxInternal(Kind, Identifier, diagnostics);
    }

    public override GreenNode? GetSlot(int index)
    {
        return index == 0 ? Identifier : null;
    }

    public override SyntaxNode CreateRed(SyntaxNode? parent, int position)
    {
        return new IdentifierNameSyntax(this, parent, position);
    }
}