// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.Hlsl.Syntax.InternalSyntax;

namespace SharpX.Hlsl.Syntax;

public class PostfixUnaryExpressionSyntax : ExpressionSyntax
{
    private ExpressionSyntax? _operand;

    public ExpressionSyntax Operand => GetRedAtZero(ref _operand)!;

    public SyntaxToken OperatorToken => new(this, ((PostfixUnaryExpressionSyntaxInternal)Green).OperatorToken, GetChildPosition(1), GetChildIndex(1));

    internal PostfixUnaryExpressionSyntax(HlslSyntaxNodeInternal node, SyntaxNode? parent, int position) : base(node, parent, position) { }

    public override SyntaxNode? GetNodeSlot(int index)
    {
        return index == 0 ? GetRedAtZero(ref _operand)! : null;
    }

    public override SyntaxNode? GetCachedSlot(int index)
    {
        return index == 0 ? _operand : null;
    }

    public PostfixUnaryExpressionSyntax Update(ExpressionSyntax operand, SyntaxToken operatorToken)
    {
        if (operand != Operand || operatorToken != OperatorToken)
            return SyntaxFactory.PostfixUnaryExpression(Kind, operand, operatorToken);
        return this;
    }

    public PostfixUnaryExpressionSyntax WithOperand(ExpressionSyntax operand)
    {
        return Update(operand, OperatorToken);
    }

    public PostfixUnaryExpressionSyntax WithOperatorToken(SyntaxToken operatorToken)
    {
        return Update(Operand, operatorToken);
    }
}