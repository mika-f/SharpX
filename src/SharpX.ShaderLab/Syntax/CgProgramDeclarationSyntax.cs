// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.ShaderLab.Syntax.InternalSyntax;

namespace SharpX.ShaderLab.Syntax;

public class CgProgramDeclarationSyntax : ShaderLabSyntaxNode
{
    private SyntaxNode? _source;

    public SyntaxToken CgProgramKeyword => new(this, ((CgProgramDeclarationSyntaxInternal)Green).CgProgramKeyword, Position, 0);

    public SyntaxNode Source => GetRed(ref _source, 1)!;

    public SyntaxToken EndCgKeyword => new(this, ((CgProgramDeclarationSyntaxInternal)Green).EndCgKeyword, GetChildPosition(2), GetChildIndex(2));

    internal CgProgramDeclarationSyntax(ShaderLabSyntaxNodeInternal node, SyntaxNode? parent, int position) : base(node, parent, position) { }

    public override SyntaxNode? GetNodeSlot(int index)
    {
        return index == 1 ? GetRed(ref _source, 1) : null;
    }

    public override SyntaxNode? GetCachedSlot(int index)
    {
        return index == 1 ? _source : null;
    }

    public CgProgramDeclarationSyntax Update(SyntaxToken cgProgramKeyword, SyntaxNode source, SyntaxToken endCgKeyword)
    {
        if (cgProgramKeyword != CgProgramKeyword || source != Source || endCgKeyword != EndCgKeyword)
            return SyntaxFactory.CgProgramDeclaration(cgProgramKeyword, source, endCgKeyword);
        return this;
    }

    public CgProgramDeclarationSyntax WithCgProgramKeyword(SyntaxToken cgProgramKeyword)
    {
        return Update(cgProgramKeyword, Source, EndCgKeyword);
    }

    public CgProgramDeclarationSyntax WithSource(SyntaxNode source)
    {
        return Update(CgProgramKeyword, source, EndCgKeyword);
    }

    public CgProgramDeclarationSyntax WithEndCgKeyword(SyntaxToken endCgKeyword)
    {
        return Update(CgProgramKeyword, Source, endCgKeyword);
    }

    public override TResult? Accept<TResult>(ShaderLabSyntaxVisitor<TResult> visitor) where TResult : default
    {
        return visitor.VisitCgProgramDeclaration(this);
    }
}