// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.Hlsl.Syntax.InternalSyntax;

namespace SharpX.Hlsl.Syntax;

public class PredefinedTypeSyntax : TypeSyntax
{
    public SyntaxToken Keyword => new(this, ((PredefinedTypeSyntaxInternal)Green).Keyword, Position, 0);

    internal PredefinedTypeSyntax(HlslSyntaxNodeInternal node, SyntaxNode? parent, int position) : base(node, parent, position) { }

    public override SyntaxNode? GetNodeSlot(int index)
    {
        return null;
    }

    public override SyntaxNode? GetCachedSlot(int index)
    {
        return null;
    }

    public PredefinedTypeSyntax Update(SyntaxToken keyword)
    {
        if (keyword != Keyword)
            return SyntaxFactory.PredefinedType(keyword);

        return this;
    }

    public PredefinedTypeSyntax WithKeyword(SyntaxToken keyword)
    {
        return Update(keyword);
    }

    public override TResult? Accept<TResult>(HlslSyntaxVisitor<TResult> visitor) where TResult : default
    {
        return visitor.VisitPredefinedType(this);
    }
}