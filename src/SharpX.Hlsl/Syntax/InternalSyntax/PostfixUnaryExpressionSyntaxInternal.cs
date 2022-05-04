// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using Microsoft.CodeAnalysis;

using SharpX.Core;

using SyntaxNode = SharpX.Core.SyntaxNode;

namespace SharpX.Hlsl.Syntax.InternalSyntax;

internal class PostfixUnaryExpressionSyntaxInternal : ExpressionSyntaxInternal
{
    public ExpressionSyntaxInternal Operand { get; }

    public SyntaxTokenInternal OperatorToken { get; }

    public PostfixUnaryExpressionSyntaxInternal(SyntaxKind kind, ExpressionSyntaxInternal operand, SyntaxTokenInternal operatorToken) : base(kind)
    {
        SlotCount = 2;

        AdjustWidth(operand);
        Operand = operand;

        AdjustWidth(operatorToken);
        OperatorToken = operatorToken;
    }

    public PostfixUnaryExpressionSyntaxInternal(SyntaxKind kind, ExpressionSyntaxInternal operand, SyntaxTokenInternal operatorToken, DiagnosticInfo[]? diagnostics, SyntaxAnnotation[]? annotations) : base(kind, diagnostics, annotations)
    {
        SlotCount = 2;

        AdjustWidth(operand);
        Operand = operand;

        AdjustWidth(operatorToken);
        OperatorToken = operatorToken;
    }

    public override GreenNode SetAnnotations(SyntaxAnnotation[]? annotations)
    {
        return new PostfixUnaryExpressionSyntaxInternal(Kind, Operand, OperatorToken, GetDiagnostics(), annotations);
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new PostfixUnaryExpressionSyntaxInternal(Kind, Operand, OperatorToken, diagnostics, GetAnnotations());
    }

    public override GreenNode? GetSlot(int index)
    {
        return index switch
        {
            0 => Operand,
            1 => OperatorToken,
            _ => null
        };
    }

    public override SyntaxNode CreateRed(SyntaxNode? parent, int position)
    {
        return new PostfixUnaryExpressionSyntax(this, parent, position);
    }

    public override TResult? Accept<TResult>(HlslSyntaxVisitorInternal<TResult> visitor) where TResult : default
    {
        return visitor.VisitPostfixUnaryExpression(this);
    }
}