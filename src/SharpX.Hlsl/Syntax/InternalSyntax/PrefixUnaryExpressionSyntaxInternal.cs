// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;

namespace SharpX.Hlsl.Syntax.InternalSyntax;

internal class PrefixUnaryExpressionSyntaxInternal : ExpressionSyntaxInternal
{
    public SyntaxTokenInternal OperatorToken { get; }

    public ExpressionSyntaxInternal Operand { get; }

    public PrefixUnaryExpressionSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal operatorToken, ExpressionSyntaxInternal operand) : base(kind)
    {
        SlotCount = 2;

        AdjustWidth(operatorToken);
        OperatorToken = operatorToken;

        AdjustWidth(operand);
        Operand = operand;
    }

    public PrefixUnaryExpressionSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal operatorToken, ExpressionSyntaxInternal operand, DiagnosticInfo[]? diagnostics) : base(kind, diagnostics)
    {
        SlotCount = 2;

        AdjustWidth(operatorToken);
        OperatorToken = operatorToken;

        AdjustWidth(operand);
        Operand = operand;
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new PrefixUnaryExpressionSyntaxInternal(Kind, OperatorToken, Operand, diagnostics);
    }

    public override GreenNode? GetSlot(int index)
    {
        return index switch
        {
            0 => OperatorToken,
            1 => Operand,
            _ => null
        };
    }

    public override SyntaxNode CreateRed(SyntaxNode? parent, int position)
    {
        throw new NotImplementedException();
    }
}