// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;

namespace SharpX.Hlsl.Syntax.InternalSyntax;

internal class ParenthesizedExpressionSyntaxInternal : ExpressionSyntaxInternal
{
    public SyntaxTokenInternal OpenParenToken { get; }

    public ExpressionSyntaxInternal Expression { get; }

    public SyntaxTokenInternal CloseParenToken { get; }

    public ParenthesizedExpressionSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal openParenToken, ExpressionSyntaxInternal expression, SyntaxTokenInternal closeParenToken) : base(kind)
    {
        SlotCount = 3;

        AdjustWidth(openParenToken);
        OpenParenToken = openParenToken;

        AdjustWidth(expression);
        Expression = expression;

        AdjustWidth(closeParenToken);
        CloseParenToken = closeParenToken;
    }

    public ParenthesizedExpressionSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal openParenToken, ExpressionSyntaxInternal expression, SyntaxTokenInternal closeParenToken, DiagnosticInfo[]? diagnostics) : base(kind, diagnostics)
    {
        SlotCount = 3;

        AdjustWidth(openParenToken);
        OpenParenToken = openParenToken;

        AdjustWidth(expression);
        Expression = expression;

        AdjustWidth(closeParenToken);
        CloseParenToken = closeParenToken;
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new ParenthesizedExpressionSyntaxInternal(Kind, OpenParenToken, Expression, CloseParenToken, diagnostics);
    }

    public override GreenNode? GetSlot(int index)
    {
        return index switch
        {
            0 => OpenParenToken,
            1 => Expression,
            2 => CloseParenToken,
            _ => null
        };
    }

    public override SyntaxNode CreateRed(SyntaxNode? parent, int position)
    {
        throw new NotImplementedException();
    }
}