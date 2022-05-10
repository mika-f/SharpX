// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.ShaderLab.Syntax.InternalSyntax;

namespace SharpX.ShaderLab.Syntax;

public class AttributeSyntax : ShaderLabSyntaxNode
{
    private ArgumentListSyntax? _argumentList;
    private NameSyntax? _name;

    public NameSyntax Name => GetRedAtZero(ref _name)!;

    public ArgumentListSyntax ArgumentList => GetRed(ref _argumentList, 1)!;

    internal AttributeSyntax(ShaderLabSyntaxNodeInternal node, SyntaxNode? parent, int position) : base(node, parent, position) { }

    public override SyntaxNode? GetNodeSlot(int index)
    {
        return index switch
        {
            0 => GetRedAtZero(ref _argumentList),
            1 => GetRed(ref _name, 1),
            _ => null
        };
    }

    public override SyntaxNode? GetCachedSlot(int index)
    {
        return index switch
        {
            0 => _argumentList,
            1 => _name,
            _ => null
        };
    }

    public AttributeSyntax Update(NameSyntax name, ArgumentListSyntax argumentList)
    {
        if (name != Name || argumentList != ArgumentList)
            return SyntaxFactory.Attribute(name, argumentList);
        return this;
    }

    public AttributeSyntax WithName(NameSyntax name)
    {
        return Update(name, ArgumentList);
    }

    public AttributeSyntax WithArgumentList(ArgumentListSyntax argumentList)
    {
        return Update(Name, argumentList);
    }

    public override TResult? Accept<TResult>(ShaderLabSyntaxVisitor<TResult> visitor) where TResult : default
    {
        return visitor.VisitAttribute(this);
    }
}