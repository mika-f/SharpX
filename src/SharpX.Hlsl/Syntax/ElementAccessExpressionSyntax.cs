// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.Hlsl.Syntax.InternalSyntax;

namespace SharpX.Hlsl.Syntax;

public class ElementAccessExpressionSyntax : ExpressionSyntax
{
    private ExpressionSyntax? _expression;
    private BracketedArgumentListSyntax? _argumentList;

    public ExpressionSyntax Expression => GetRedAtZero(ref _expression)!;

    public BracketedArgumentListSyntax ArgumentList => GetRed(ref _argumentList, 1)!;

    internal ElementAccessExpressionSyntax(HlslSyntaxNodeInternal node, SyntaxNode? parent, int position) : base(node, parent, position) { }

    public override SyntaxNode? GetNodeSlot(int index)
    {
        return index switch
        {
            0 => GetRedAtZero(ref _expression),
            1 => GetRed(ref _argumentList, 1),
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

    public ElementAccessExpressionSyntax Update(ExpressionSyntax expression, BracketedArgumentListSyntax argumentList)
    {
        if (expression != Expression || argumentList != ArgumentList)
            return SyntaxFactory.ElementAccessExpression(expression, argumentList);
        return this;
    }

    public ElementAccessExpressionSyntax WithExpression(ExpressionSyntax expression)
    {
        return Update(expression, ArgumentList);
    }

    public ElementAccessExpressionSyntax WithArgumentList(BracketedArgumentListSyntax argumentList)
    {
        return Update(Expression, argumentList);
    }

    public ElementAccessExpressionSyntax AddArguments(params ArgumentSyntax[] items)
    {
        return WithArgumentList(ArgumentList.AddArguments(items));
    }
}