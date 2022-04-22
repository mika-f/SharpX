// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.Hlsl.Syntax.InternalSyntax;

namespace SharpX.Hlsl.Syntax;

public class InvocationExpressionSyntax : ExpressionSyntax
{
    private ArgumentListSyntax? _argumentList;
    private ExpressionSyntax? _expression;

    public ExpressionSyntax Expression => GetRedAtZero(ref _expression)!;

    public ArgumentListSyntax ArgumentList => GetRed(ref _argumentList, 1)!;

    internal InvocationExpressionSyntax(HlslSyntaxNodeInternal node, SyntaxNode? parent, int position) : base(node, parent, position) { }

    public override SyntaxNode? GetNodeSlot(int index)
    {
        return index switch
        {
            0 => GetRedAtZero(ref _expression)!,
            1 => GetRed(ref _argumentList, 1)!,
            _ => null
        };
    }

    public override SyntaxNode? GetCachedSlot(int index)
    {
        return index switch
        {
            0 => _expression,
            1 => _argumentList,
            _ => null
        };
    }

    public InvocationExpressionSyntax Update(ExpressionSyntax expression, ArgumentListSyntax argumentList)
    {
        if (expression != Expression || argumentList != ArgumentList)
            return SyntaxFactory.InvocationExpression(expression, argumentList);
        return this;
    }

    public InvocationExpressionSyntax WithExpression(ExpressionSyntax expression)
    {
        return Update(expression, ArgumentList);
    }

    public InvocationExpressionSyntax WithArgumentList(ArgumentListSyntax argumentList)
    {
        return Update(Expression, argumentList);
    }
}