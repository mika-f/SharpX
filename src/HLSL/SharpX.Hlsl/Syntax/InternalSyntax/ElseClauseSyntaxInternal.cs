// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;

namespace SharpX.Hlsl.Syntax.InternalSyntax;

internal class ElseClauseSyntaxInternal : HlslSyntaxNodeInternal
{
    public SyntaxTokenInternal ElseKeyword { get; }

    public StatementSyntaxInternal Statement { get; }

    public ElseClauseSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal elseKeyword, StatementSyntaxInternal statement) : base(kind)
    {
        SlotCount = 2;

        AdjustWidth(elseKeyword);
        ElseKeyword = elseKeyword;

        AdjustWidth(statement);
        Statement = statement;
    }

    public ElseClauseSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal elseKeyword, StatementSyntaxInternal statement, DiagnosticInfo[]? diagnostics) : base(kind, diagnostics)
    {
        SlotCount = 2;

        AdjustWidth(elseKeyword);
        ElseKeyword = elseKeyword;

        AdjustWidth(statement);
        Statement = statement;
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new ElseClauseSyntaxInternal(Kind, ElseKeyword, Statement, diagnostics);
    }

    public override GreenNode? GetSlot(int index)
    {
        return index switch
        {
            0 => ElseKeyword,
            1 => Statement,
            _ => null
        };
    }

    public override SyntaxNode CreateRed(SyntaxNode? parent, int position)
    {
        return new ElseClauseSyntax(this, parent, position);
    }

    public override TResult? Accept<TResult>(HlslSyntaxVisitorInternal<TResult> visitor) where TResult : default
    {
        return visitor.VisitElseClause(this);
    }
}