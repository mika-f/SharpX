// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;

namespace SharpX.Hlsl.Syntax.InternalSyntax;

internal class LiteralExpressionSyntaxInternal : ExpressionSyntaxInternal
{
    public SyntaxTokenInternal Token { get; }

    public LiteralExpressionSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal token) : base(kind)
    {
        SlotCount = 1;

        AdjustWidth(token);
        Token = token;
    }

    public LiteralExpressionSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal token, DiagnosticInfo[]? diagnostics) : base(kind, diagnostics)
    {
        SlotCount = 1;

        AdjustWidth(token);
        Token = token;
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new LiteralExpressionSyntaxInternal(Kind, Token, diagnostics);
    }

    public override GreenNode? GetSlot(int index)
    {
        return index switch
        {
            0 => Token,
            _ => null
        };
    }

    public override SyntaxNode CreateRed(SyntaxNode? parent, int position)
    {
        return new LiteralExpressionSyntax(this, parent, position);
    }

    public override TResult? Accept<TResult>(HlslSyntaxVisitorInternal<TResult> visitor) where TResult : default
    {
        return visitor.VisitLiteralExpression(this);
    }
}