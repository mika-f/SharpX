// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;

namespace SharpX.Hlsl.Syntax.InternalSyntax;

internal class PragmaMessageDirectiveTriviaSyntaxInternal : DirectiveTriviaSyntaxInternal
{
    public override SyntaxTokenInternal HashToken { get; }

    public SyntaxTokenInternal PragmaKeyword { get; }

    public SyntaxTokenInternal MessageKeyword { get; }

    public SyntaxTokenInternal Message { get; }

    public override SyntaxTokenInternal EndOfDirectiveToken { get; }

    public PragmaMessageDirectiveTriviaSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal hashToken, SyntaxTokenInternal pragmaKeyword, SyntaxTokenInternal messageKeyword, SyntaxTokenInternal message, SyntaxTokenInternal endOfDirectiveToken) : base(kind)
    {
        SlotCount = 5;

        AdjustWidth(hashToken);
        HashToken = hashToken;

        AdjustWidth(pragmaKeyword);
        PragmaKeyword = pragmaKeyword;

        AdjustWidth(messageKeyword);
        MessageKeyword = messageKeyword;

        AdjustWidth(message);
        Message = message;

        AdjustWidth(endOfDirectiveToken);
        EndOfDirectiveToken = endOfDirectiveToken;
    }

    public PragmaMessageDirectiveTriviaSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal hashToken, SyntaxTokenInternal pragmaKeyword, SyntaxTokenInternal messageKeyword, SyntaxTokenInternal message, SyntaxTokenInternal endOfDirectiveToken, DiagnosticInfo[]? diagnostics) : base(kind, diagnostics)
    {
        SlotCount = 5;

        AdjustWidth(hashToken);
        HashToken = hashToken;

        AdjustWidth(pragmaKeyword);
        PragmaKeyword = pragmaKeyword;

        AdjustWidth(messageKeyword);
        MessageKeyword = messageKeyword;

        AdjustWidth(message);
        Message = message;

        AdjustWidth(endOfDirectiveToken);
        EndOfDirectiveToken = endOfDirectiveToken;
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new PragmaMessageDirectiveTriviaSyntaxInternal(Kind, HashToken, PragmaKeyword, MessageKeyword, Message, EndOfDirectiveToken, diagnostics);
    }

    public override GreenNode? GetSlot(int index)
    {
        return index switch
        {
            0 => HashToken,
            1 => PragmaKeyword,
            2 => MessageKeyword,
            3 => Message,
            4 => EndOfDirectiveToken,
            _ => null
        };
    }

    public override SyntaxNode CreateRed(SyntaxNode? parent, int position)
    {
        throw new NotImplementedException();
    }
}