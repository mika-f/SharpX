// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using Microsoft.CodeAnalysis;

using SharpX.Core;

using SyntaxNode = SharpX.Core.SyntaxNode;

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

    public PrefixUnaryExpressionSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal operatorToken, ExpressionSyntaxInternal operand, DiagnosticInfo[]? diagnostics, SyntaxAnnotation[]? annotations) : base(kind, diagnostics, annotations)
    {
        SlotCount = 2;

        AdjustWidth(operatorToken);
        OperatorToken = operatorToken;

        AdjustWidth(operand);
        Operand = operand;
    }

    public override GreenNode SetAnnotations(SyntaxAnnotation[]? annotations)
    {
        return new PrefixUnaryExpressionSyntaxInternal(Kind, OperatorToken, Operand, GetDiagnostics(), annotations);
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new PrefixUnaryExpressionSyntaxInternal(Kind, OperatorToken, Operand, diagnostics, GetAnnotations());
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
        return new PrefixUnaryExpressionSyntax(this, parent, position);
    }
}