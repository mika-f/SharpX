// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.Hlsl.Syntax.InternalSyntax;

namespace SharpX.Hlsl.Syntax;

public class AssignmentExpressionSyntax : ExpressionSyntax
{
    private ExpressionSyntax? _left;
    private ExpressionSyntax? _right;

    public ExpressionSyntax Left => GetRedAtZero(ref _left)!;

    public SyntaxToken OperatorToken => new(this, ((AssignmentExpressionSyntaxInternal)Green).OperatorToken, GetChildPosition(1), GetChildIndex(1));

    public ExpressionSyntax Right => GetRed(ref _right, 2)!;

    internal AssignmentExpressionSyntax(HlslSyntaxNodeInternal node, SyntaxNode? parent, int position) : base(node, parent, position) { }

    public override SyntaxNode? GetNodeSlot(int index)
    {
        return index switch
        {
            0 => GetRedAtZero(ref _left)!,
            2 => GetRed(ref _right, 2),
            _ => null
        };
    }

    public override SyntaxNode? GetCachedSlot(int index)
    {
        return index switch
        {
            0 => _left,
            2 => _right,
            _ => null
        };
    }

    public AssignmentExpressionSyntax Update(ExpressionSyntax left, SyntaxToken operatorToken, ExpressionSyntax right)
    {
        if (left != Left || operatorToken != OperatorToken || right != Right)
            return SyntaxFactory.AssignmentExpression(Kind, left, operatorToken, right);
        return this;
    }

    public AssignmentExpressionSyntax WithLeft(ExpressionSyntax left)
    {
        return Update(left, OperatorToken, Right);
    }

    public AssignmentExpressionSyntax WithOperatorToken(SyntaxToken operatorToken)
    {
        return Update(Left, operatorToken, Right);
    }

    public AssignmentExpressionSyntax WithRight(ExpressionSyntax right)
    {
        return Update(Left, OperatorToken, right);
    }

    public override TResult? Accept<TResult>(HlslSyntaxVisitor<TResult> visitor) where TResult : default
    {
        return visitor.VisitAssignmentExpression(this);
    }
}