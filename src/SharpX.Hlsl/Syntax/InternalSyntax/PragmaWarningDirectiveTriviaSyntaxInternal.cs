// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;

namespace SharpX.Hlsl.Syntax.InternalSyntax;

internal class PragmaWarningDirectiveTriviaSyntaxInternal : DirectiveTriviaSyntaxInternal
{
    public override SyntaxTokenInternal HashToken { get; }

    public SyntaxTokenInternal PragmaKeyword { get; }

    public SyntaxTokenInternal WarningKeyword { get; }

    public SyntaxTokenInternal OpenParenToken { get; }

    public WarningSpecifierListSyntaxInternal WarningSpecifiers { get; }

    public SyntaxTokenInternal CloseParenToken { get; }

    public override SyntaxTokenInternal EndOfDirectiveToken { get; }

    public PragmaWarningDirectiveTriviaSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal hashToken, SyntaxTokenInternal pragmaKeyword, SyntaxTokenInternal warningKeyword, SyntaxTokenInternal openParenToken, WarningSpecifierListSyntaxInternal warningSpecifiers, SyntaxTokenInternal closeParenToken,
                                                      SyntaxTokenInternal endOfDirectiveToken) : base(kind)
    {
        SlotCount = 7;

        AdjustWidth(hashToken);
        HashToken = hashToken;

        AdjustWidth(pragmaKeyword);
        PragmaKeyword = pragmaKeyword;

        AdjustWidth(warningKeyword);
        WarningKeyword = warningKeyword;

        AdjustWidth(openParenToken);
        OpenParenToken = openParenToken;

        AdjustWidth(warningSpecifiers);
        WarningSpecifiers = warningSpecifiers;

        AdjustWidth(closeParenToken);
        CloseParenToken = closeParenToken;

        AdjustWidth(endOfDirectiveToken);
        EndOfDirectiveToken = endOfDirectiveToken;
    }

    public PragmaWarningDirectiveTriviaSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal hashToken, SyntaxTokenInternal pragmaKeyword, SyntaxTokenInternal warningKeyword, SyntaxTokenInternal openParenToken, WarningSpecifierListSyntaxInternal warningSpecifiers, SyntaxTokenInternal closeParenToken,
                                                      SyntaxTokenInternal endOfDirectiveToken, DiagnosticInfo[]? diagnostics) : base(kind, diagnostics)
    {
        SlotCount = 7;

        AdjustWidth(hashToken);
        HashToken = hashToken;

        AdjustWidth(pragmaKeyword);
        PragmaKeyword = pragmaKeyword;

        AdjustWidth(warningKeyword);
        WarningKeyword = warningKeyword;

        AdjustWidth(openParenToken);
        OpenParenToken = openParenToken;

        AdjustWidth(warningSpecifiers);
        WarningSpecifiers = warningSpecifiers;

        AdjustWidth(closeParenToken);
        CloseParenToken = closeParenToken;

        AdjustWidth(endOfDirectiveToken);
        EndOfDirectiveToken = endOfDirectiveToken;
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new PragmaWarningDirectiveTriviaSyntaxInternal(Kind, HashToken, PragmaKeyword, WarningKeyword, OpenParenToken, WarningSpecifiers, CloseParenToken, EndOfDirectiveToken, diagnostics);
    }

    public override GreenNode? GetSlot(int index)
    {
        return index switch
        {
            0 => HashToken,
            1 => PragmaKeyword,
            2 => WarningKeyword,
            3 => OpenParenToken,
            4 => WarningSpecifiers,
            5 => CloseParenToken,
            6 => EndOfDirectiveToken,
            _ => null
        };
    }

    public override SyntaxNode CreateRed(SyntaxNode? parent, int position)
    {
        throw new NotImplementedException();
    }
}