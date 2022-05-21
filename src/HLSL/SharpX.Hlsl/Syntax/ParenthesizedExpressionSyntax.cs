// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.Hlsl.Syntax.InternalSyntax;

namespace SharpX.Hlsl.Syntax;

public class ParenthesizedExpressionSyntax : ExpressionSyntax
{
    private ExpressionSyntax? _expression;

    public SyntaxToken OpenParenToken => new(this, ((ParenthesizedExpressionSyntaxInternal)Green).OpenParenToken, Position, 0);

    public ExpressionSyntax Expression => GetRed(ref _expression, 1)!;

    public SyntaxToken CloseParenToken => new(this, ((ParenthesizedExpressionSyntaxInternal)Green).CloseParenToken, GetChildPosition(2), GetChildIndex(2));

    internal ParenthesizedExpressionSyntax(HlslSyntaxNodeInternal node, SyntaxNode? parent, int position) : base(node, parent, position) { }

    public override SyntaxNode? GetNodeSlot(int index)
    {
        return index == 1 ? GetRed(ref _expression, 1)! : null;
    }

    public override SyntaxNode? GetCachedSlot(int index)
    {
        return index == 1 ? _expression : null;
    }

    public ParenthesizedExpressionSyntax Update(SyntaxToken openParenToken, ExpressionSyntax expression, SyntaxToken closeParenToken)
    {
        if (openParenToken != OpenParenToken || expression != Expression || closeParenToken != CloseParenToken)
            return SyntaxFactory.ParenthesizedExpression(openParenToken, expression, closeParenToken);
        return this;
    }

    public ParenthesizedExpressionSyntax WithOpenParenToken(SyntaxToken openParenToken)
    {
        return Update(openParenToken, Expression, CloseParenToken);
    }

    public ParenthesizedExpressionSyntax WithExpression(ExpressionSyntax expression)
    {
        return Update(OpenParenToken, expression, CloseParenToken);
    }

    public ParenthesizedExpressionSyntax WithCloseParenToken(SyntaxToken closeParenToken)
    {
        return Update(OpenParenToken, Expression, closeParenToken);
    }

    public override TResult? Accept<TResult>(HlslSyntaxVisitor<TResult> visitor) where TResult : default
    {
        return visitor.VisitParenthesizedExpression(this);
    }
}