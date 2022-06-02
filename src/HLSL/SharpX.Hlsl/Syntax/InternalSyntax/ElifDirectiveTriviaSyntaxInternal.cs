// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;

namespace SharpX.Hlsl.Syntax.InternalSyntax;

internal class ElifDirectiveTriviaSyntaxInternal : ConditionalDirectiveTriviaSyntaxInternal
{
    public override SyntaxTokenInternal HashToken { get; }

    public SyntaxTokenInternal ElifKeyword { get; }

    public override ExpressionSyntaxInternal Condition { get; }

    public override SyntaxTokenInternal EndOfDirectiveToken { get; }

    public ElifDirectiveTriviaSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal hashToken, SyntaxTokenInternal elifKeyword, ExpressionSyntaxInternal condition, SyntaxTokenInternal endOfDirectiveToken) : base(kind)
    {
        SlotCount = 4;

        AdjustWidth(hashToken);
        HashToken = hashToken;

        AdjustWidth(elifKeyword);
        ElifKeyword = elifKeyword;

        AdjustWidth(condition);
        Condition = condition;

        AdjustWidth(endOfDirectiveToken);
        EndOfDirectiveToken = endOfDirectiveToken;
    }

    public ElifDirectiveTriviaSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal hashToken, SyntaxTokenInternal elifKeyword, ExpressionSyntaxInternal condition, SyntaxTokenInternal endOfDirectiveToken, DiagnosticInfo[]? diagnostics) :
        base(kind, diagnostics)
    {
        SlotCount = 4;

        AdjustWidth(hashToken);
        HashToken = hashToken;

        AdjustWidth(elifKeyword);
        ElifKeyword = elifKeyword;

        AdjustWidth(condition);
        Condition = condition;

        AdjustWidth(endOfDirectiveToken);
        EndOfDirectiveToken = endOfDirectiveToken;
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new ElifDirectiveTriviaSyntaxInternal(Kind, HashToken, ElifKeyword, Condition, EndOfDirectiveToken, diagnostics);
    }

    public override GreenNode? GetSlot(int index)
    {
        return index switch
        {
            0 => HashToken,
            1 => ElifKeyword,
            2 => Condition,
            3 => EndOfDirectiveToken,
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