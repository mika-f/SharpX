// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.Hlsl.Syntax.InternalSyntax;

namespace SharpX.Hlsl.Syntax;

public class ArgumentSyntax : HlslSyntaxNode
{
    private ExpressionSyntax? _expression;

    public SyntaxToken RedKindKeyword
    {
        get
        {
            var slot = ((ArgumentSyntaxInternal)Green).RefKindKeyword;
            return slot != null ? new SyntaxToken(this, slot, Position, 0) : default;
        }
    }

    public ExpressionSyntax Expression => GetRed(ref _expression, 1)!;

    internal ArgumentSyntax(HlslSyntaxNodeInternal node, SyntaxNode? parent, int position) : base(node, parent, position) { }

    public override SyntaxNode? GetNodeSlot(int index)
    {
        return index == 1 ? GetRed(ref _expression, 1) : null;
    }

    public override SyntaxNode? GetCachedSlot(int index)
    {
        return index == 1 ? _expression : null;
    }

    public ArgumentSyntax Update(SyntaxToken refKindKeyword, ExpressionSyntax expression)
    {
        if (refKindKeyword != RedKindKeyword || expression != Expression)
            return SyntaxFactory.Argument(refKindKeyword, expression);
        return this;
    }

    public ArgumentSyntax WithRefKindKeyword(SyntaxToken refKindKeyword)
    {
        return Update(refKindKeyword, Expression);
    }

    public ArgumentSyntax WithExpression(ExpressionSyntax expression)
    {
        return Update(RedKindKeyword, expression);
    }

    public override TResult? Accept<TResult>(HlslSyntaxVisitor<TResult> visitor) where TResult : default
    {
        return visitor.VisitArgument(this);
    }
}