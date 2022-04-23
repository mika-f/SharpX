// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.Hlsl.Syntax.InternalSyntax;

namespace SharpX.Hlsl.Syntax;

public class EqualsValueClauseSyntax : HlslSyntaxNode
{
    private ExpressionSyntax? _expression;
    public SyntaxToken EqualsToken => new(this, ((EqualsValueClauseSyntaxInternal)Green).EqualsToken, Position, 0);

    public ExpressionSyntax Expression => GetRed(ref _expression, 1)!;

    internal EqualsValueClauseSyntax(HlslSyntaxNodeInternal node, SyntaxNode? parent, int position) : base(node, parent, position) { }

    public override SyntaxNode? GetNodeSlot(int index)
    {
        return index == 1 ? GetRed(ref _expression, 1)! : null;
    }

    public override SyntaxNode? GetCachedSlot(int index)
    {
        return index == 1 ? _expression : null;
    }

    public EqualsValueClauseSyntax Update(SyntaxToken equalsToken, ExpressionSyntax expression)
    {
        if (equalsToken != EqualsToken || expression != Expression)
            return SyntaxFactory.EqualsValueClause(equalsToken, expression);
        return this;
    }

    public EqualsValueClauseSyntax WithEqualsToken(SyntaxToken equalsToken)
    {
        return Update(equalsToken, Expression);
    }

    public EqualsValueClauseSyntax WithExpression(ExpressionSyntax expression)
    {
        return Update(EqualsToken, expression);
    }
}