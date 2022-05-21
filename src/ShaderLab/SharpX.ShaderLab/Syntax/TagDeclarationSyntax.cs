// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.ShaderLab.Syntax.InternalSyntax;

namespace SharpX.ShaderLab.Syntax;

public class TagDeclarationSyntax : ShaderLabSyntaxNode
{
    public SyntaxToken Key => new(this, ((TagDeclarationSyntaxInternal)Green).Key, Position, 0);

    public SyntaxToken EqualsToken => new(this, ((TagDeclarationSyntaxInternal)Green).EqualsToken, GetChildPosition(1), GetChildIndex(1));

    public SyntaxToken Value => new(this, ((TagDeclarationSyntaxInternal)Green).Value, GetChildPosition(2), GetChildIndex(2));

    internal TagDeclarationSyntax(ShaderLabSyntaxNodeInternal node, SyntaxNode? parent, int position) : base(node, parent, position) { }

    public override SyntaxNode? GetNodeSlot(int index)
    {
        return null;
    }

    public override SyntaxNode? GetCachedSlot(int index)
    {
        return null;
    }

    public TagDeclarationSyntax Update(SyntaxToken key, SyntaxToken equalsToken, SyntaxToken value)
    {
        if (key != Key || equalsToken != EqualsToken || value != Value)
            return SyntaxFactory.TagDeclaration(key, equalsToken, value);
        return this;
    }

    public TagDeclarationSyntax WithKey(SyntaxToken key)
    {
        return Update(key, EqualsToken, Value);
    }

    public TagDeclarationSyntax WithEqualsToken(SyntaxToken equalsToken)
    {
        return Update(Key, equalsToken, Value);
    }

    public TagDeclarationSyntax WithValue(SyntaxToken value)
    {
        return Update(Key, EqualsToken, value);
    }

    public override TResult? Accept<TResult>(ShaderLabSyntaxVisitor<TResult> visitor) where TResult : default
    {
        return visitor.VisitTagDeclaration(this);
    }
}