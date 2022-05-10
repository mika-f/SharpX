// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.ShaderLab.Syntax.InternalSyntax;

namespace SharpX.ShaderLab.Syntax;

public class CgIncludeDeclarationSyntax : ShaderLabSyntaxNode
{
    private SyntaxNode? _source;

    public SyntaxToken CgIncludeKeyword => new(this, ((CgIncludeDeclarationSyntaxInternal)Green).CgIncludeKeyword, Position, 0);

    public SyntaxNode Source => GetRed(ref _source, 1)!;

    public SyntaxToken EndCgKeyword => new(this, ((CgIncludeDeclarationSyntaxInternal)Green).CgIncludeKeyword, GetChildPosition(2), GetChildIndex(2));

    internal CgIncludeDeclarationSyntax(ShaderLabSyntaxNodeInternal node, SyntaxNode? parent, int position) : base(node, parent, position) { }

    public override SyntaxNode? GetNodeSlot(int index)
    {
        return index == 1 ? GetRed(ref _source, 1) : null;
    }

    public override SyntaxNode? GetCachedSlot(int index)
    {
        return index == 1 ? _source : null;
    }

    public CgIncludeDeclarationSyntax Update(SyntaxToken cgIncludeKeyword, SyntaxNode source, SyntaxToken endCgKeyword)
    {
        if (cgIncludeKeyword != CgIncludeKeyword || source != Source || endCgKeyword != EndCgKeyword)
            return SyntaxFactory.CgIncludeDeclaration(cgIncludeKeyword, source, endCgKeyword);
        return this;
    }

    public CgIncludeDeclarationSyntax WithCgIncludeKeyword(SyntaxToken cgIncludeKeyword)
    {
        return Update(cgIncludeKeyword, Source, EndCgKeyword);
    }

    public CgIncludeDeclarationSyntax WithSource(SyntaxNode source)
    {
        return Update(CgIncludeKeyword, source, EndCgKeyword);
    }

    public CgIncludeDeclarationSyntax WithEndCgKeyword(SyntaxToken endCgKeyword)
    {
        return Update(CgIncludeKeyword, Source, endCgKeyword);
    }

    public override TResult? Accept<TResult>(ShaderLabSyntaxVisitor<TResult> visitor) where TResult : default
    {
        return visitor.CgIncludeDeclaration(this);
    }
}