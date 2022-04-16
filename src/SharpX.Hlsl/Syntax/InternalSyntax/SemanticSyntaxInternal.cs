// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;

namespace SharpX.Hlsl.Syntax.InternalSyntax;

internal class SemanticSyntaxInternal : HlslSyntaxNodeInternal
{
    public SyntaxTokenInternal ColonToken { get; }

    public IdentifierNameSyntaxInternal Identifier { get; }

    public SemanticSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal colonToken, IdentifierNameSyntaxInternal identifier) : base(kind)
    {
        SlotCount = 2;

        AdjustWidth(colonToken);
        ColonToken = colonToken;

        AdjustWidth(identifier);
        Identifier = identifier;
    }

    public SemanticSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal colonToken, IdentifierNameSyntaxInternal identifier, DiagnosticInfo[]? diagnostics) : base(kind, diagnostics)
    {
        SlotCount = 2;

        AdjustWidth(colonToken);
        ColonToken = colonToken;

        AdjustWidth(identifier);
        Identifier = identifier;
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new SemanticSyntaxInternal(Kind, ColonToken, Identifier, diagnostics);
    }

    public override GreenNode? GetSlot(int index)
    {
        return index switch
        {
            0 => ColonToken,
            1 => Identifier,
            _ => null
        };
    }

    public override SyntaxNode CreateRed(SyntaxNode? parent, int position)
    {
        throw new NotImplementedException();
    }
}