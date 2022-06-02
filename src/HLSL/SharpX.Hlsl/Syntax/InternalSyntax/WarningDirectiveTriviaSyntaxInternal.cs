// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;

namespace SharpX.Hlsl.Syntax.InternalSyntax;

internal class WarningDirectiveTriviaSyntaxInternal : DirectiveTriviaSyntaxInternal
{
    public override SyntaxTokenInternal HashToken { get; }

    public SyntaxTokenInternal WarningKeyword { get; }

    public override SyntaxTokenInternal EndOfDirectiveToken { get; }

    public WarningDirectiveTriviaSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal hashToken, SyntaxTokenInternal warningKeyword, SyntaxTokenInternal endOfDirectiveToken) : base(kind)
    {
        SlotCount = 3;

        AdjustWidth(hashToken);
        HashToken = hashToken;

        AdjustWidth(warningKeyword);
        WarningKeyword = warningKeyword;

        AdjustWidth(endOfDirectiveToken);
        EndOfDirectiveToken = endOfDirectiveToken;
    }

    public WarningDirectiveTriviaSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal hashToken, SyntaxTokenInternal warningKeyword, SyntaxTokenInternal endOfDirectiveToken, DiagnosticInfo[]? diagnostics) : base(kind, diagnostics)
    {
        SlotCount = 3;

        AdjustWidth(hashToken);
        HashToken = hashToken;

        AdjustWidth(warningKeyword);
        WarningKeyword = warningKeyword;

        AdjustWidth(endOfDirectiveToken);
        EndOfDirectiveToken = endOfDirectiveToken;
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new WarningDirectiveTriviaSyntaxInternal(Kind, HashToken, WarningKeyword, EndOfDirectiveToken, diagnostics);
    }

    public override GreenNode? GetSlot(int index)
    {
        return index switch
        {
            0 => HashToken,
            1 => WarningKeyword,
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