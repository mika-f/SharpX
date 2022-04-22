// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;

namespace SharpX.Hlsl.Syntax;

public class AttributeArgumentSyntax : HlslSyntaxNode
{
    private ExpressionSyntax? _expression;

    public ExpressionSyntax Expression => GetRedAtZero(ref _expression)!;

    internal AttributeArgumentSyntax(GreenNode node, SyntaxNode? parent, int position) : base(node, parent, position) { }

    public override SyntaxNode? GetNodeSlot(int index)
    {
        return index == 0 ? GetRedAtZero(ref _expression)! : null;
    }

    public override SyntaxNode? GetCachedSlot(int index)
    {
        return index == 0 ? _expression : null;
    }

    public AttributeArgumentSyntax Update(ExpressionSyntax expression)
    {
        if (expression != Expression)
            return SyntaxFactory.AttributeArgument(expression);
        return this;
    }

    public AttributeArgumentSyntax WithExpression(ExpressionSyntax expression)
    {
        return Update(expression);
    }
}