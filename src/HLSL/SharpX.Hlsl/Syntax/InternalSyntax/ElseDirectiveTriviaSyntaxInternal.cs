// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;

namespace SharpX.Hlsl.Syntax.InternalSyntax;

internal class ElseDirectiveTriviaSyntaxInternal : BranchingDirectiveTriviaSyntaxInternal
{
    public override SyntaxTokenInternal HashToken { get; }

    public SyntaxTokenInternal ElseKeyword { get; }

    public override SyntaxTokenInternal EndOfDirectiveToken { get; }

    public ElseDirectiveTriviaSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal hashToken, SyntaxTokenInternal elseKeyword, SyntaxTokenInternal endOfDirectiveToken) : base(kind)
    {
        SlotCount = 3;

        AdjustWidth(hashToken);
        HashToken = hashToken;

        AdjustWidth(elseKeyword);
        ElseKeyword = elseKeyword;

        AdjustWidth(endOfDirectiveToken);
        EndOfDirectiveToken = endOfDirectiveToken;
    }

    public ElseDirectiveTriviaSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal hashToken, SyntaxTokenInternal elseKeyword, SyntaxTokenInternal endOfDirectiveToken, DiagnosticInfo[]? diagnostics) : base(kind, diagnostics)
    {
        SlotCount = 3;

        AdjustWidth(hashToken);
        HashToken = hashToken;

        AdjustWidth(elseKeyword);
        ElseKeyword = elseKeyword;

        AdjustWidth(endOfDirectiveToken);
        EndOfDirectiveToken = endOfDirectiveToken;
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new ElseDirectiveTriviaSyntaxInternal(Kind, HashToken, ElseKeyword, EndOfDirectiveToken, diagnostics);
    }

    public override GreenNode? GetSlot(int index)
    {
        return index switch
        {
            0 => HashToken,
            1 => ElseKeyword,
            2 => EndOfDirectiveToken,
            _ => null
        };
    }

    public override SyntaxNode CreateRed(SyntaxNode? parent, int position)
    {
        throw new NotImplementedException();
    }

    public override TResult? Accept<TResult>(HlslSyntaxVisitorInternal<TResult> visitor) where TResult : default
    {
        throw new NotImplementedException();
    }
}