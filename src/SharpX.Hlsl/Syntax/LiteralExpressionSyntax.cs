// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.Hlsl.Syntax.InternalSyntax;

namespace SharpX.Hlsl.Syntax;

public class LiteralExpressionSyntax : ExpressionSyntax
{
    public SyntaxToken Token => new(this, ((LiteralExpressionSyntaxInternal)Green).Token, Position, 0);

    internal LiteralExpressionSyntax(HlslSyntaxNodeInternal node, SyntaxNode? parent, int position) : base(node, parent, position) { }

    public override SyntaxNode? GetNodeSlot(int index)
    {
        return null;
    }

    public override SyntaxNode? GetCachedSlot(int index)
    {
        return null;
    }

    public LiteralExpressionSyntax Update(SyntaxToken token)
    {
        if (token != Token)
            return SyntaxFactory.LiteralExpression(Kind, token);
        return this;
    }

    public LiteralExpressionSyntax WithToken(SyntaxToken token)
    {
        return Update(token);
    }
}