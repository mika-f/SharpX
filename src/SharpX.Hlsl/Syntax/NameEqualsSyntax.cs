// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.Hlsl.Syntax.InternalSyntax;

namespace SharpX.Hlsl.Syntax;

public class NameEqualsSyntax : HlslSyntaxNode
{
    private IdentifierNameSyntax? _name;

    public IdentifierNameSyntax Name => GetRedAtZero(ref _name)!;

    public SyntaxToken EqualsToken => new(this, ((NameEqualsSyntaxInternal)Green).EqualsToken, GetChildPosition(1), GetChildIndex(1));

    internal NameEqualsSyntax(GreenNode node, SyntaxNode? parent, int position) : base(node, parent, position) { }

    public override SyntaxNode? GetNodeSlot(int index)
    {
        return index == 0 ? GetRedAtZero(ref _name) : null;
    }

    public override SyntaxNode? GetCachedSlot(int index)
    {
        return index == 0 ? _name : null;
    }

    public NameEqualsSyntax Update(IdentifierNameSyntax name, SyntaxToken equalsToken)
    {
        if (name != Name || equalsToken != EqualsToken)
            return SyntaxFactory.NameEquals(name, equalsToken);
        return this;
    }

    public NameEqualsSyntax WithName(IdentifierNameSyntax name)
    {
        return Update(name, EqualsToken);
    }

    public NameEqualsSyntax WithEqualsToken(SyntaxToken equalsToken)
    {
        return Update(Name, equalsToken);
    }

    public override TResult? Accept<TResult>(HlslSyntaxVisitor<TResult> visitor) where TResult : default
    {
        return visitor.VisitNameEquals(this);
    }
}