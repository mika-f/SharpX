// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using Microsoft.CodeAnalysis;

using SharpX.Core;

using SyntaxNode = SharpX.Core.SyntaxNode;

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

    public ParenthesizedExpressionSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal openParenToken, ExpressionSyntaxInternal expression, SyntaxTokenInternal closeParenToken, DiagnosticInfo[]? diagnostics, SyntaxAnnotation[]? annotations) : base(kind, diagnostics, annotations)
    {
        SlotCount = 3;

        AdjustWidth(openParenToken);
        OpenParenToken = openParenToken;

        AdjustWidth(expression);
        Expression = expression;

        AdjustWidth(closeParenToken);
        CloseParenToken = closeParenToken;
    }

    public override GreenNode SetAnnotations(SyntaxAnnotation[]? annotations)
    {
        return new ParenthesizedExpressionSyntaxInternal(Kind, OpenParenToken, Expression, CloseParenToken, GetDiagnostics(), annotations);
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new ParenthesizedExpressionSyntaxInternal(Kind, OpenParenToken, Expression, CloseParenToken, diagnostics, GetAnnotations());
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
        return new ParenthesizedExpressionSyntax(this, parent, position);
    }

    public override TResult? Accept<TResult>(HlslSyntaxVisitorInternal<TResult> visitor) where TResult : default
    {
        return visitor.VisitParenthesizedExpression(this);
    }
}