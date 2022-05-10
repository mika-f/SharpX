// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;

namespace SharpX.ShaderLab.Syntax.InternalSyntax;

public class NameDeclarationSyntax : BaseCommandDeclarationSyntax
{
    public override SyntaxToken Keyword => new(this, ((NameDeclarationSyntaxInternal)Green).Keyword, Position, 0);

    public SyntaxToken Name => new(this, ((NameDeclarationSyntaxInternal)Green).Name, GetChildPosition(1), GetChildIndex(1));

    internal NameDeclarationSyntax(ShaderLabSyntaxNodeInternal node, SyntaxNode? parent, int position) : base(node, parent, position) { }

    public override SyntaxNode? GetNodeSlot(int index)
    {
        return null;
    }

    public override SyntaxNode? GetCachedSlot(int index)
    {
        return null;
    }

    public NameDeclarationSyntax Update(SyntaxToken keyword, SyntaxToken name)
    {
        if (keyword != Keyword || name != Name)
            return SyntaxFactory.NameDeclaration(keyword, name);
        return this;
    }

    public NameDeclarationSyntax WithKeyword(SyntaxToken keyword)
    {
        return Update(keyword, Name);
    }

    public NameDeclarationSyntax WithName(SyntaxToken name)
    {
        return Update(Keyword, name);
    }


    public override TResult? Accept<TResult>(ShaderLabSyntaxVisitor<TResult> visitor) where TResult : default
    {
        return visitor.VisitNameDeclaration(this);
    }
}