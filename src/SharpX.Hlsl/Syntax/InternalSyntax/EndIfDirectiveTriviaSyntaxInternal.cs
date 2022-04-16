// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;

namespace SharpX.Hlsl.Syntax.InternalSyntax;

internal class EndIfDirectiveTriviaSyntaxInternal : DirectiveTriviaSyntaxInternal
{
    public override SyntaxTokenInternal HashToken { get; }

    public SyntaxTokenInternal EndIfKeyword { get; }

    public override SyntaxTokenInternal EndOfDirectiveToken { get; }

    public EndIfDirectiveTriviaSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal hashToken, SyntaxTokenInternal endIfKeyword, SyntaxTokenInternal endOfDirectiveToken) : base(kind)
    {
        SlotCount = 3;

        AdjustWidth(hashToken);
        HashToken = hashToken;

        AdjustWidth(endIfKeyword);
        EndIfKeyword = endIfKeyword;

        AdjustWidth(endOfDirectiveToken);
        EndOfDirectiveToken = endOfDirectiveToken;
    }

    public EndIfDirectiveTriviaSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal hashToken, SyntaxTokenInternal endIfKeyword, SyntaxTokenInternal endOfDirectiveToken, DiagnosticInfo[]? diagnostics) : base(kind, diagnostics)
    {
        SlotCount = 3;

        AdjustWidth(hashToken);
        HashToken = hashToken;

        AdjustWidth(endIfKeyword);
        EndIfKeyword = endIfKeyword;

        AdjustWidth(endOfDirectiveToken);
        EndOfDirectiveToken = endOfDirectiveToken;
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new EndIfDirectiveTriviaSyntaxInternal(Kind, HashToken, EndIfKeyword, EndOfDirectiveToken, diagnostics);
    }

    public override GreenNode? GetSlot(int index)
    {
        return index switch
        {
            0 => HashToken,
            1 => EndIfKeyword,
            2 => EndOfDirectiveToken,
            _ => null
        };
    }

    public override SyntaxNode CreateRed(SyntaxNode? parent, int position)
    {
        throw new NotImplementedException();
    }
}