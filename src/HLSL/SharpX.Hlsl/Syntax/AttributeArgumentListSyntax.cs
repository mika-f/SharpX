// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.Hlsl.Syntax.InternalSyntax;

namespace SharpX.Hlsl.Syntax;

public class AttributeArgumentListSyntax : HlslSyntaxNode
{
    private SyntaxNode? _arguments;

    public SyntaxToken OpenParenToken => new(this, ((AttributeArgumentListSyntaxInternal)Green).OpenParenToken, Position, 0);

    public SeparatedSyntaxList<AttributeArgumentSyntax> Arguments
    {
        get
        {
            var red = GetRed(ref _arguments, 1);
            return red != null ? new SeparatedSyntaxList<AttributeArgumentSyntax>(red, GetChildIndex(1)) : default;
        }
    }

    public SyntaxToken CloseParenToken => new(this, ((AttributeArgumentListSyntaxInternal)Green).CloseParenToken, GetChildPosition(2), GetChildIndex(2));


    internal AttributeArgumentListSyntax(GreenNode node, SyntaxNode? parent, int position) : base(node, parent, position) { }

    public override SyntaxNode? GetNodeSlot(int index)
    {
        return index == 1 ? GetRed(ref _arguments, 1)! : null;
    }

    public override SyntaxNode? GetCachedSlot(int index)
    {
        return index == 1 ? _arguments : null;
    }

    public AttributeArgumentListSyntax Update(SyntaxToken openParenToken, SeparatedSyntaxList<AttributeArgumentSyntax> arguments, SyntaxToken closeParenToken)
    {
        if (openParenToken != OpenParenToken || arguments != Arguments || closeParenToken != CloseParenToken)
            return SyntaxFactory.AttributeArgumentList(openParenToken, arguments, closeParenToken);
        return this;
    }

    public AttributeArgumentListSyntax WithOpenParenToken(SyntaxToken openParenToken)
    {
        return Update(openParenToken, Arguments, CloseParenToken);
    }

    public AttributeArgumentListSyntax WithArguments(SeparatedSyntaxList<AttributeArgumentSyntax> arguments)
    {
        return Update(OpenParenToken, arguments, CloseParenToken);
    }

    public AttributeArgumentListSyntax WithCloseParenToken(SyntaxToken closeParenToken)
    {
        return Update(OpenParenToken, Arguments, closeParenToken);
    }

    public AttributeArgumentListSyntax AddArguments(params AttributeArgumentSyntax[] items)
    {
        return WithArguments(Arguments.AddRange(items));
    }

    public override TResult? Accept<TResult>(HlslSyntaxVisitor<TResult> visitor) where TResult : default
    {
        return visitor.VisitAttributeArgumentList(this);
    }
}