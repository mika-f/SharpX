// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;

namespace SharpX.Hlsl.Syntax;

public class AttributeSyntax : HlslSyntaxNode
{
    private AttributeArgumentListSyntax? _argumentList;
    private NameSyntax? _name;

    public NameSyntax Name => GetRedAtZero(ref _name)!;

    public AttributeArgumentListSyntax? ArgumentList => GetRed(ref _argumentList, 1);

    internal AttributeSyntax(GreenNode node, SyntaxNode? parent, int position) : base(node, parent, position) { }

    public override SyntaxNode? GetNodeSlot(int index)
    {
        return index switch
        {
            0 => GetRedAtZero(ref _name)!,
            1 => GetRed(ref _argumentList, 1)!,
            _ => null
        };
    }

    public override SyntaxNode? GetCachedSlot(int index)
    {
        return index switch
        {
            0 => _name,
            1 => _argumentList,
            _ => null
        };
    }

    public AttributeSyntax Update(NameSyntax name, AttributeArgumentListSyntax? argumentList)
    {
        if (name != Name || argumentList != ArgumentList)
            return SyntaxFactory.Attribute(name, argumentList);
        return this;
    }

    public AttributeSyntax WithName(NameSyntax name)
    {
        return Update(name, ArgumentList);
    }

    public AttributeSyntax WithAttributeArgumentList(AttributeArgumentListSyntax? argumentList)
    {
        return Update(Name, argumentList);
    }

    public override TResult? Accept<TResult>(HlslSyntaxVisitor<TResult> visitor) where TResult : default
    {
        return visitor.VisitAttribute(this);
    }
}