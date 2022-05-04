// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.Hlsl.Syntax.InternalSyntax;

namespace SharpX.Hlsl.Syntax;

public class PrefixUnaryExpressionSyntax : ExpressionSyntax
{
    private ExpressionSyntax? _operand;

    public SyntaxToken OperatorToken => new(this, ((PrefixUnaryExpressionSyntaxInternal)Green).OperatorToken, Position, 0);

    public ExpressionSyntax Operand => GetRed(ref _operand, 1)!;

    internal PrefixUnaryExpressionSyntax(HlslSyntaxNodeInternal node, SyntaxNode? parent, int position) : base(node, parent, position) { }

    public override SyntaxNode? GetNodeSlot(int index)
    {
        return index == 1 ? GetRed(ref _operand, 1)! : null;
    }

    public override SyntaxNode? GetCachedSlot(int index)
    {
        return index == 1 ? _operand : null;
    }

    public PrefixUnaryExpressionSyntax Update(SyntaxToken operatorToken, ExpressionSyntax operand)
    {
        if (operatorToken != OperatorToken || operand != Operand)
            return SyntaxFactory.PrefixUnaryExpression(Kind, operatorToken, operand);
        return this;
    }

    public PrefixUnaryExpressionSyntax WithOperatorToken(SyntaxToken operatorToken)
    {
        return Update(operatorToken, Operand);
    }

    public PrefixUnaryExpressionSyntax WithOperand(ExpressionSyntax operand)
    {
        return Update(OperatorToken, operand);
    }

    public override TResult? Accept<TResult>(HlslSyntaxVisitor<TResult> visitor) where TResult : default
    {
        return visitor.VisitPrefixUnaryExpression(this);
    }
}