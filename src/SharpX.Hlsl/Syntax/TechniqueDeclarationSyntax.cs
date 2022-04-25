// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.Hlsl.Syntax.InternalSyntax;

namespace SharpX.Hlsl.Syntax;

public class TechniqueDeclarationSyntax : TypeDeclarationSyntax
{
    private SyntaxNode? _members;

    public override SyntaxToken Keyword => new(this, ((TechniqueDeclarationSyntaxInternal)Green).Keyword, Position, 0);

    public override SyntaxToken Identifier => new(this, ((TechniqueDeclarationSyntaxInternal)Green).Identifier, GetChildPosition(1), GetChildIndex(1));

    public override SyntaxToken OpenBraceToken => new(this, ((TechniqueDeclarationSyntaxInternal)Green).OpenBraceToken, GetChildPosition(2), GetChildIndex(2));

    public override SyntaxList<MemberDeclarationSyntax> Members => new(GetRed(ref _members, 3));

    public override SyntaxToken CloseBraceToken => new(this, ((TechniqueDeclarationSyntaxInternal)Green).CloseBraceToken, GetChildPosition(4), GetChildIndex(4));

    internal TechniqueDeclarationSyntax(HlslSyntaxNodeInternal node, SyntaxNode? parent, int position) : base(node, parent, position) { }

    public override SyntaxNode? GetNodeSlot(int index)
    {
        return index == 3 ? GetRed(ref _members, 3) : null;
    }

    public override SyntaxNode? GetCachedSlot(int index)
    {
        return index == 3 ? _members : null;
    }

    private TechniqueDeclarationSyntax Update(SyntaxToken keyword, SyntaxToken identifier, SyntaxToken openBraceToken, SyntaxList<PassDeclarationSyntax> members, SyntaxToken closeBraceToken)
    {
        return Update(keyword, identifier, openBraceToken, new SyntaxList<MemberDeclarationSyntax>(members), closeBraceToken);
    }

    private TechniqueDeclarationSyntax Update(SyntaxToken keyword, SyntaxToken identifier, SyntaxToken openBraceToken, SyntaxList<MemberDeclarationSyntax> members, SyntaxToken closeBraceToken)
    {
        if (keyword != Keyword || identifier != Identifier || openBraceToken != OpenBraceToken || members != Members || closeBraceToken != CloseBraceToken)
            return SyntaxFactory.TechniqueDeclaration(keyword, identifier, openBraceToken, new SyntaxList<PassDeclarationSyntax>(members.Cast<PassDeclarationSyntax>()), closeBraceToken);
        return this;
    }

    public TechniqueDeclarationSyntax WithKeyword(SyntaxToken keyword)
    {
        return Update(keyword, Identifier, OpenBraceToken, Members, CloseBraceToken);
    }

    public TechniqueDeclarationSyntax WithIdentifier(SyntaxToken identifier)
    {
        return Update(Keyword, identifier, OpenBraceToken, Members, CloseBraceToken);
    }

    public TechniqueDeclarationSyntax WithOpenBraceToken(SyntaxToken openBraceToken)
    {
        return Update(Keyword, Identifier, openBraceToken, Members, CloseBraceToken);
    }

    public TechniqueDeclarationSyntax WithMembers(SyntaxList<PassDeclarationSyntax> members)
    {
        return Update(Keyword, Identifier, OpenBraceToken, members, CloseBraceToken);
    }

    private TechniqueDeclarationSyntax WithMembers(SyntaxList<MemberDeclarationSyntax> members)
    {
        return Update(Keyword, Identifier, OpenBraceToken, members, CloseBraceToken);
    }

    public TechniqueDeclarationSyntax WithCloseBraceToken(SyntaxToken closeBraceToken)
    {
        return Update(Keyword, Identifier, OpenBraceToken, Members, closeBraceToken);
    }

    public TechniqueDeclarationSyntax AddMembers(params PassDeclarationSyntax[] items)
    {
        return WithMembers(Members.AddRange(items));
    }
}