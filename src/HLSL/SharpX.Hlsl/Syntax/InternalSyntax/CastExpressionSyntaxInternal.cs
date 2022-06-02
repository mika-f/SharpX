// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;

namespace SharpX.Hlsl.Syntax.InternalSyntax;

internal class CastExpressionSyntaxInternal : ExpressionSyntaxInternal
{
    public SyntaxTokenInternal OpenParenToken { get; }

    public TypeSyntaxInternal Type { get; }

    public SyntaxTokenInternal CloseParenToken { get; }

    public ExpressionSyntaxInternal Expression { get; }


    public CastExpressionSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal openParenToken, TypeSyntaxInternal type, SyntaxTokenInternal closeParenToken, ExpressionSyntaxInternal expression) : base(kind)
    {
        SlotCount = 4;

        AdjustWidth(openParenToken);
        OpenParenToken = openParenToken;

        AdjustWidth(type);
        Type = type;

        AdjustWidth(closeParenToken);
        CloseParenToken = closeParenToken;

        AdjustWidth(expression);
        Expression = expression;
    }

    public CastExpressionSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal openParenToken, TypeSyntaxInternal type, SyntaxTokenInternal closeParenToken, ExpressionSyntaxInternal expression, DiagnosticInfo[]? diagnostics) : base(kind, diagnostics)
    {
        SlotCount = 4;

        AdjustWidth(openParenToken);
        OpenParenToken = openParenToken;

        AdjustWidth(type);
        Type = type;

        AdjustWidth(closeParenToken);
        CloseParenToken = closeParenToken;

        AdjustWidth(expression);
        Expression = expression;
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new CastExpressionSyntaxInternal(Kind, OpenParenToken, Type, CloseParenToken, Expression, diagnostics);
    }

    public override GreenNode? GetSlot(int index)
    {
        return index switch
        {
            0 => OpenParenToken,
            1 => Type,
            2 => CloseParenToken,
            3 => Expression,
            _ => null
        };
    }

    public override SyntaxNode CreateRed(SyntaxNode? parent, int position)
    {
        return new CastExpressionSyntax(this, parent, position);
    }

    public override TResult? Accept<TResult>(HlslSyntaxVisitorInternal<TResult> visitor) where TResult : default
    {
        return visitor.VisitCastExpression(this);
    }
}