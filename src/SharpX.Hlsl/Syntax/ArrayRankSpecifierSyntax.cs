// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.Hlsl.Syntax.InternalSyntax;

namespace SharpX.Hlsl.Syntax;

public class ArrayRankSpecifierSyntax : HlslSyntaxNode
{
    private SyntaxNode? _sizes;

    public SyntaxToken OpenBracketToken => new(this, ((ArrayRankSpecifierSyntaxInternal)Green).OpenBracketToken, Position, 0);

    public SeparatedSyntaxList<ExpressionSyntax> Sizes
    {
        get
        {
            var red = GetRed(ref _sizes, 1);
            return red != null ? new SeparatedSyntaxList<ExpressionSyntax>(red, GetChildIndex(1)) : default;
        }
    }

    public SyntaxToken CloseBracketToken => new(this, ((ArrayRankSpecifierSyntaxInternal)Green).CloseBracketToken, GetChildPosition(2), GetChildIndex(2));

    internal ArrayRankSpecifierSyntax(HlslSyntaxNodeInternal node, SyntaxNode? parent, int position) : base(node, parent, position) { }

    public override SyntaxNode? GetNodeSlot(int index)
    {
        return index == 1 ? GetRed(ref _sizes, 1)! : null;
    }

    public override SyntaxNode? GetCachedSlot(int index)
    {
        return index == 1 ? _sizes : null;
    }

    public ArrayRankSpecifierSyntax Update(SyntaxToken openBracketToken, SeparatedSyntaxList<ExpressionSyntax> sizes, SyntaxToken closeBracketToken)
    {
        if (openBracketToken != OpenBracketToken || sizes != Sizes || closeBracketToken != CloseBracketToken)
            return SyntaxFactory.ArrayRankSpecifier(openBracketToken, sizes, closeBracketToken);
        return this;
    }

    public ArrayRankSpecifierSyntax WithOpenBracketToken(SyntaxToken openBracketToken)
    {
        return Update(openBracketToken, Sizes, CloseBracketToken);
    }

    public ArrayRankSpecifierSyntax WithSizes(SeparatedSyntaxList<ExpressionSyntax> sizes)
    {
        return Update(OpenBracketToken, sizes, CloseBracketToken);
    }

    public ArrayRankSpecifierSyntax WithCloseBracketToken(SyntaxToken closeBracketToken)
    {
        return Update(OpenBracketToken, Sizes, closeBracketToken);
    }

    public ArrayRankSpecifierSyntax AddSizes(params ExpressionSyntax[] items)
    {
        return WithSizes(Sizes.AddRange(items));
    }

    public override TResult? Accept<TResult>(HlslSyntaxVisitor<TResult> visitor) where TResult : default
    {
        return visitor.VisitArrayRankSpecifier(this);
    }
}