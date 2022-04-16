// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;

namespace SharpX.Hlsl.Syntax.InternalSyntax;

internal class LineDirectiveTriviaSyntaxInternal : DirectiveTriviaSyntaxInternal
{
    public override SyntaxTokenInternal HashToken { get; }

    public SyntaxTokenInternal LineKeyword { get; }

    public SyntaxTokenInternal Line { get; }

    public SyntaxTokenInternal? File { get; }

    public override SyntaxTokenInternal EndOfDirectiveToken { get; }

    public LineDirectiveTriviaSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal hashToken, SyntaxTokenInternal lineKeyword, SyntaxTokenInternal line, SyntaxTokenInternal? file, SyntaxTokenInternal endOfDirectiveToken) : base(kind)
    {
        SlotCount = 5;

        AdjustWidth(hashToken);
        HashToken = hashToken;

        AdjustWidth(lineKeyword);
        LineKeyword = lineKeyword;

        AdjustWidth(line);
        Line = line;

        if (file != null)
        {
            AdjustWidth(file);
            File = file;
        }

        AdjustWidth(endOfDirectiveToken);
        EndOfDirectiveToken = endOfDirectiveToken;
    }

    public LineDirectiveTriviaSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal hashToken, SyntaxTokenInternal lineKeyword, SyntaxTokenInternal line, SyntaxTokenInternal? file, SyntaxTokenInternal endOfDirectiveToken, DiagnosticInfo[]? diagnostics) : base(kind, diagnostics)
    {
        SlotCount = 5;

        AdjustWidth(hashToken);
        HashToken = hashToken;

        AdjustWidth(lineKeyword);
        LineKeyword = lineKeyword;

        AdjustWidth(line);
        Line = line;

        if (file != null)
        {
            AdjustWidth(file);
            File = file;
        }

        AdjustWidth(endOfDirectiveToken);
        EndOfDirectiveToken = endOfDirectiveToken;
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new LineDirectiveTriviaSyntaxInternal(Kind, HashToken, LineKeyword, Line, File, EndOfDirectiveToken, diagnostics);
    }

    public override GreenNode? GetSlot(int index)
    {
        return index switch
        {
            0 => HashToken,
            1 => LineKeyword,
            2 => Line,
            3 => File,
            4 => EndOfDirectiveToken,
            _ => null
        };
    }

    public override SyntaxNode CreateRed(SyntaxNode? parent, int position)
    {
        throw new NotImplementedException();
    }
}