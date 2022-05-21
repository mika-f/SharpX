// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.ShaderLab.Syntax.InternalSyntax;

namespace SharpX.ShaderLab.Syntax;

public class AttributeListSyntax : ShaderLabSyntaxNode
{
    private SyntaxNode? _attributes;

    public SyntaxToken OpenBracketToken => new(this, ((AttributeListSyntaxInternal)Green).OpenBracketToken, Position, 0);

    public SeparatedSyntaxList<AttributeSyntax> Attributes
    {
        get
        {
            var red = GetRed(ref _attributes, 1);
            return red != null ? new SeparatedSyntaxList<AttributeSyntax>(red, GetChildIndex(1)) : default;
        }
    }

    public SyntaxToken CloseBracketToken => new(this, ((AttributeListSyntaxInternal)Green).CloseBracketToken, GetChildPosition(2), GetChildIndex(2));

    internal AttributeListSyntax(ShaderLabSyntaxNodeInternal node, SyntaxNode? parent, int position) : base(node, parent, position) { }

    public override SyntaxNode? GetNodeSlot(int index)
    {
        return index == 1 ? GetRed(ref _attributes, 1) : null;
    }

    public override SyntaxNode? GetCachedSlot(int index)
    {
        return index == 1 ? _attributes : null;
    }

    public AttributeListSyntax Update(SyntaxToken openBracketToken, SeparatedSyntaxList<AttributeSyntax> attributes, SyntaxToken closeBracketToken)
    {
        if (openBracketToken != OpenBracketToken || attributes != Attributes || closeBracketToken != CloseBracketToken)
            return SyntaxFactory.AttributeList(openBracketToken, attributes, closeBracketToken);
        return this;
    }

    public AttributeListSyntax WithOpenBracketToken(SyntaxToken openBracketToken)
    {
        return Update(openBracketToken, Attributes, CloseBracketToken);
    }

    public AttributeListSyntax WithAttributes(SeparatedSyntaxList<AttributeSyntax> attributes)
    {
        return Update(OpenBracketToken, attributes, CloseBracketToken);
    }

    public AttributeListSyntax WithCloseBracketToken(SyntaxToken closeBracketToken)
    {
        return Update(OpenBracketToken, Attributes, closeBracketToken);
    }

    public AttributeListSyntax AddAttributes(params AttributeSyntax[] items)
    {
        return WithAttributes(Attributes.AddRange(items));
    }

    public override TResult? Accept<TResult>(ShaderLabSyntaxVisitor<TResult> visitor) where TResult : default
    {
        return visitor.VisitAttributeList(this);
    }
}