// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;

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

    public PostfixUnaryExpressionSyntaxInternal(SyntaxKind kind, ExpressionSyntaxInternal operand, SyntaxTokenInternal operatorToken, DiagnosticInfo[]? diagnostics) : base(kind, diagnostics)
    {
        SlotCount = 2;

        AdjustWidth(operand);
        Operand = operand;

        AdjustWidth(operatorToken);
        OperatorToken = operatorToken;
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new PostfixUnaryExpressionSyntaxInternal(Kind, Operand, OperatorToken, diagnostics);
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
}