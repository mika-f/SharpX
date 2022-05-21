// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.ShaderLab.Syntax.InternalSyntax;

namespace SharpX.ShaderLab.Syntax;

public class UsePassDeclarationSyntax : BasePassDeclarationSyntax
{
    public override SyntaxToken Keyword => new(this, ((UsePassDeclarationSyntaxInternal)Green).Keyword, Position, 0);

    public SyntaxToken PassReference => new(this, ((UsePassDeclarationSyntaxInternal)Green).PassReference, GetChildPosition(1), GetChildIndex(1));

    internal UsePassDeclarationSyntax(ShaderLabSyntaxNodeInternal node, SyntaxNode? parent, int position) : base(node, parent, position) { }

    public override SyntaxNode? GetNodeSlot(int index)
    {
        return null;
    }

    public override SyntaxNode? GetCachedSlot(int index)
    {
        return null;
    }

    public UsePassDeclarationSyntax Update(SyntaxToken keyword, SyntaxToken passReference)
    {
        if (keyword != Keyword || passReference != PassReference)
            return SyntaxFactory.UsePassDeclaration(keyword, passReference);
        return this;
    }

    public UsePassDeclarationSyntax WithKeyword(SyntaxToken keyword)
    {
        return Update(keyword, PassReference);
    }

    public UsePassDeclarationSyntax WithPassReference(SyntaxToken passReference)
    {
        return Update(Keyword, passReference);
    }

    public override TResult? Accept<TResult>(ShaderLabSyntaxVisitor<TResult> visitor) where TResult : default
    {
        return visitor.VisitUsePassDeclaration(this);
    }
}