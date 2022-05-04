// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.Hlsl.Syntax.InternalSyntax;

namespace SharpX.Hlsl.Syntax;

public class DefaultSwitchLabelSyntax : SwitchLabelSyntax
{
    public SyntaxToken DefaultKeyword => new(this, ((DefaultSwitchLabelSyntaxInternal)Green).DefaultKeyword, Position, 0);

    public SyntaxToken ColonToken => new(this, ((DefaultSwitchLabelSyntaxInternal)Green).ColonToken, GetChildPosition(1), GetChildIndex(1));

    internal DefaultSwitchLabelSyntax(HlslSyntaxNodeInternal node, SyntaxNode? parent, int position) : base(node, parent, position) { }

    public override SyntaxNode? GetNodeSlot(int index)
    {
        return null;
    }

    public override SyntaxNode? GetCachedSlot(int index)
    {
        return null;
    }

    public DefaultSwitchLabelSyntax Update(SyntaxToken defaultKeyword, SyntaxToken colonToken)
    {
        if (defaultKeyword != DefaultKeyword || colonToken != ColonToken)
            return SyntaxFactory.DefaultSwitchLabel(defaultKeyword, colonToken);
        return this;
    }

    public DefaultSwitchLabelSyntax WithDefaultKeyword(SyntaxToken defaultKeyword)
    {
        return Update(defaultKeyword, ColonToken);
    }

    public DefaultSwitchLabelSyntax WithColonToken(SyntaxToken colonToken)
    {
        return Update(DefaultKeyword, colonToken);
    }

    public override TResult? Accept<TResult>(HlslSyntaxVisitor<TResult> visitor) where TResult : default
    {
        return visitor.VisitDefaultSwitchLabel(this);
    }
}