// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.Hlsl.Syntax.InternalSyntax;

namespace SharpX.Hlsl.Syntax;

public class StructDeclarationSyntax : TypeDeclarationSyntax
{
    private SyntaxNode? _members;

    public override SyntaxToken Keyword => new(this, ((StructDeclarationSyntaxInternal)Green).Keyword, Position, 0);

    public override SyntaxToken Identifier => new(this, ((StructDeclarationSyntaxInternal)Green).Identifier, GetChildPosition(1), GetChildIndex(1));

    public override SyntaxToken OpenBraceToken => new(this, ((StructDeclarationSyntaxInternal)Green).OpenBraceToken, GetChildPosition(2), GetChildIndex(2));

    public override SyntaxList<MemberDeclarationSyntax> Members => new(GetRed(ref _members, 3));

    public override SyntaxToken CloseBraceToken => new(this, ((StructDeclarationSyntaxInternal)Green).CloseBraceToken, GetChildPosition(4), GetChildIndex(4));

    public SyntaxToken SemicolonToken => new(this, ((StructDeclarationSyntaxInternal)Green).SemicolonToken, GetChildPosition(5), GetChildIndex(5));


    internal StructDeclarationSyntax(HlslSyntaxNodeInternal node, SyntaxNode? parent, int position) : base(node, parent, position) { }

    public override SyntaxNode? GetNodeSlot(int index)
    {
        return index == 3 ? GetRed(ref _members, 3) : null;
    }

    public override SyntaxNode? GetCachedSlot(int index)
    {
        return index == 3 ? _members : null;
    }

    public StructDeclarationSyntax Update(SyntaxToken keyword, SyntaxToken identifier, SyntaxToken openBraceToken, SyntaxList<MemberDeclarationSyntax> members, SyntaxToken closeBraceToken, SyntaxToken semicolonToken)
    {
        if (keyword != Keyword || identifier != Identifier || openBraceToken != OpenBraceToken || members != Members || closeBraceToken != CloseBraceToken || semicolonToken != SemicolonToken)
            return SyntaxFactory.StructDeclaration(keyword, identifier, openBraceToken, new SyntaxList<FieldDeclarationSyntax>(members.Cast<FieldDeclarationSyntax>()), closeBraceToken, semicolonToken);
        return this;
    }

    public StructDeclarationSyntax WithKeyword(SyntaxToken keyword)
    {
        return Update(keyword, Identifier, OpenBraceToken, Members, CloseBraceToken, SemicolonToken);
    }

    public StructDeclarationSyntax WithIdentifier(SyntaxToken identifier)
    {
        return Update(Keyword, identifier, OpenBraceToken, Members, CloseBraceToken, SemicolonToken);
    }

    public StructDeclarationSyntax WithOpenBraceToken(SyntaxToken openBraceToken)
    {
        return Update(Keyword, Identifier, openBraceToken, Members, CloseBraceToken, SemicolonToken);
    }

    public StructDeclarationSyntax WithMembers(SyntaxList<FieldDeclarationSyntax> members)
    {
        return Update(Keyword, Identifier, OpenBraceToken, new SyntaxList<MemberDeclarationSyntax>(members), CloseBraceToken, SemicolonToken);
    }

    private StructDeclarationSyntax WithMembers(SyntaxList<MemberDeclarationSyntax> members)
    {
        return Update(Keyword, Identifier, OpenBraceToken, members, CloseBraceToken, SemicolonToken);
    }

    public StructDeclarationSyntax WithCloseBraceToken(SyntaxToken closeBraceToken)
    {
        return Update(Keyword, Identifier, OpenBraceToken, Members, closeBraceToken, SemicolonToken);
    }

    public StructDeclarationSyntax WithSemicolonToken(SyntaxToken semicolonToken)
    {
        return Update(Keyword, Identifier, OpenBraceToken, Members, CloseBraceToken, semicolonToken);
    }

    public StructDeclarationSyntax AddMembers(params FieldDeclarationSyntax[] items)
    {
        return WithMembers(Members.AddRange(items.Cast<MemberDeclarationSyntax>().ToArray()));
    }
}