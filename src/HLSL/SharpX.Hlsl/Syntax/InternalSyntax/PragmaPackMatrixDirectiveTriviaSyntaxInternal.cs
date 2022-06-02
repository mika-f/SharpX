// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;

namespace SharpX.Hlsl.Syntax.InternalSyntax;

internal class PragmaPackMatrixDirectiveTriviaSyntaxInternal : DirectiveTriviaSyntaxInternal
{
    public override SyntaxTokenInternal HashToken { get; }

    public SyntaxTokenInternal PragmaKeyword { get; }

    public SyntaxTokenInternal PackMatrixKeyword { get; }

    public SyntaxTokenInternal OpenParenToken { get; }

    public SyntaxTokenInternal ColumnMajorOrRowMajorKeyword { get; }

    public SyntaxTokenInternal CloseParenToken { get; }

    public override SyntaxTokenInternal EndOfDirectiveToken { get; }

    public PragmaPackMatrixDirectiveTriviaSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal hashToken, SyntaxTokenInternal pragmaKeyword, SyntaxTokenInternal packMatrixKeyword, SyntaxTokenInternal openParenToken, SyntaxTokenInternal columnMajorOrRowMajorKeyword,
                                                         SyntaxTokenInternal closeParenToken, SyntaxTokenInternal endOfDirectiveToken) : base(kind)
    {
        SlotCount = 7;

        AdjustWidth(hashToken);
        HashToken = hashToken;

        AdjustWidth(pragmaKeyword);
        PragmaKeyword = pragmaKeyword;

        AdjustWidth(packMatrixKeyword);
        PackMatrixKeyword = packMatrixKeyword;

        AdjustWidth(openParenToken);
        OpenParenToken = openParenToken;

        AdjustWidth(columnMajorOrRowMajorKeyword);
        ColumnMajorOrRowMajorKeyword = columnMajorOrRowMajorKeyword;

        AdjustWidth(closeParenToken);
        CloseParenToken = closeParenToken;

        AdjustWidth(endOfDirectiveToken);
        EndOfDirectiveToken = endOfDirectiveToken;
    }

    public PragmaPackMatrixDirectiveTriviaSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal hashToken, SyntaxTokenInternal pragmaKeyword, SyntaxTokenInternal packMatrixKeyword, SyntaxTokenInternal openParenToken, SyntaxTokenInternal columnMajorOrRowMajorKeyword,
                                                         SyntaxTokenInternal closeParenToken, SyntaxTokenInternal endOfDirectiveToken, DiagnosticInfo[]? diagnostics) : base(kind, diagnostics)
    {
        SlotCount = 7;

        AdjustWidth(hashToken);
        HashToken = hashToken;

        AdjustWidth(pragmaKeyword);
        PragmaKeyword = pragmaKeyword;

        AdjustWidth(packMatrixKeyword);
        PackMatrixKeyword = packMatrixKeyword;

        AdjustWidth(openParenToken);
        OpenParenToken = openParenToken;

        AdjustWidth(columnMajorOrRowMajorKeyword);
        ColumnMajorOrRowMajorKeyword = columnMajorOrRowMajorKeyword;

        AdjustWidth(closeParenToken);
        CloseParenToken = closeParenToken;

        AdjustWidth(endOfDirectiveToken);
        EndOfDirectiveToken = endOfDirectiveToken;
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new PragmaPackMatrixDirectiveTriviaSyntaxInternal(Kind, HashToken, PragmaKeyword, PackMatrixKeyword, OpenParenToken, ColumnMajorOrRowMajorKeyword, CloseParenToken, EndOfDirectiveToken, diagnostics);
    }

    public override GreenNode? GetSlot(int index)
    {
        return index switch
        {
            0 => HashToken,
            1 => PragmaKeyword,
            2 => PackMatrixKeyword,
            3 => OpenParenToken,
            4 => ColumnMajorOrRowMajorKeyword,
            5 => CloseParenToken,
            6 => EndOfDirectiveToken,
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