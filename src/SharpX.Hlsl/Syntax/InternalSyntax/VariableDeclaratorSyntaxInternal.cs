// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;

namespace SharpX.Hlsl.Syntax.InternalSyntax;

internal class VariableDeclaratorSyntaxInternal : HlslSyntaxNodeInternal
{
    public SyntaxTokenInternal Identifier { get; }

    public EqualsValueClauseSyntaxInternal? Initializer { get; }

    public VariableDeclaratorSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal identifier, EqualsValueClauseSyntaxInternal? initializer) : base(kind)
    {
        SlotCount = 2;

        AdjustWidth(identifier);
        Identifier = identifier;

        if (initializer != null)
        {
            AdjustWidth(initializer);
            Initializer = initializer;
        }
    }

    public VariableDeclaratorSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal identifier, EqualsValueClauseSyntaxInternal? initializer, DiagnosticInfo[]? diagnostics) : base(kind, diagnostics)
    {
        SlotCount = 2;

        AdjustWidth(identifier);
        Identifier = identifier;

        if (initializer != null)
        {
            AdjustWidth(initializer);
            Initializer = initializer;
        }
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new VariableDeclaratorSyntaxInternal(Kind, Identifier, Initializer, diagnostics);
    }

    public override GreenNode? GetSlot(int index)
    {
        return index switch
        {
            0 => Identifier,
            1 => Initializer,
            _ => null
        };
    }

    public override SyntaxNode CreateRed(SyntaxNode? parent, int position)
    {
        throw new NotImplementedException();
    }
}