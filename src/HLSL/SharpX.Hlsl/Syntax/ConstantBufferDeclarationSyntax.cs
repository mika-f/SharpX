// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.Hlsl.Syntax.InternalSyntax;

namespace SharpX.Hlsl.Syntax;

public class ConstantBufferDeclarationSyntax : TypeDeclarationSyntax
{
    private SyntaxNode? _members;
    private RegisterSyntax? _register;

    public override SyntaxToken Keyword => new(this, ((ConstantBufferDeclarationSyntaxInternal)Green).Keyword, Position, 0);

    public override SyntaxToken Identifier => new(this, ((ConstantBufferDeclarationSyntaxInternal)Green).Identifier, GetChildPosition(1), GetChildIndex(1));

    public RegisterSyntax? Register => GetRed(ref _register, 2);

    public override SyntaxToken OpenBraceToken => new(this, ((ConstantBufferDeclarationSyntaxInternal)Green).OpenBraceToken, GetChildPosition(3), GetChildIndex(3));

    public override SyntaxList<MemberDeclarationSyntax> Members => new(GetRed(ref _members, 4));

    public override SyntaxToken CloseBraceToken => new(this, ((ConstantBufferDeclarationSyntaxInternal)Green).CloseBraceToken, GetChildPosition(5), GetChildIndex(5));

    public SyntaxToken SemicolonToken => new(this, ((ConstantBufferDeclarationSyntaxInternal)Green).SemicolonToken, GetChildPosition(6), GetChildIndex(6));

    internal ConstantBufferDeclarationSyntax(HlslSyntaxNodeInternal node, SyntaxNode? parent, int position) : base(node, parent, position) { }

    public override SyntaxNode? GetNodeSlot(int index)
    {
        return index switch
        {
            2 => GetRed(ref _register, 2),
            4 => GetRed(ref _members, 4),
            _ => null
        };
    }

    public override SyntaxNode? GetCachedSlot(int index)
    {
        return index switch
        {
            2 => _register,
            4 => _members,
            _ => null
        };
    }

    public ConstantBufferDeclarationSyntax Update(SyntaxToken keyword, SyntaxToken identifier, RegisterSyntax? register, SyntaxToken openBraceToken, SyntaxList<FieldDeclarationSyntax> members, SyntaxToken closeBraceToken, SyntaxToken semicolonToken)
    {
        return Update(keyword, identifier, register, openBraceToken, new SyntaxList<MemberDeclarationSyntax>(members), closeBraceToken, semicolonToken);
    }

    public ConstantBufferDeclarationSyntax Update(SyntaxToken keyword, SyntaxToken identifier, RegisterSyntax? register, SyntaxToken openBraceToken, SyntaxList<MemberDeclarationSyntax> members, SyntaxToken closeBraceToken, SyntaxToken semicolonToken)
    {
        if (keyword != Keyword || identifier != Identifier || register != Register || openBraceToken != OpenBraceToken || members != Members || closeBraceToken != CloseBraceToken || semicolonToken != SemicolonToken)
            return SyntaxFactory.ConstantBufferDeclaration(keyword, identifier, register, openBraceToken, new SyntaxList<FieldDeclarationSyntax>(members.Cast<FieldDeclarationSyntax>()), closeBraceToken, semicolonToken);
        return this;
    }

    public ConstantBufferDeclarationSyntax WithKeyword(SyntaxToken keyword)
    {
        return Update(keyword, Identifier, Register, OpenBraceToken, Members, CloseBraceToken, SemicolonToken);
    }

    public ConstantBufferDeclarationSyntax WithIdentifier(SyntaxToken identifier)
    {
        return Update(Keyword, identifier, Register, OpenBraceToken, Members, CloseBraceToken, SemicolonToken);
    }

    public ConstantBufferDeclarationSyntax WithRegister(RegisterSyntax? register)
    {
        return Update(Keyword, Identifier, register, OpenBraceToken, Members, CloseBraceToken, SemicolonToken);
    }

    public ConstantBufferDeclarationSyntax WithOpenBraceToken(SyntaxToken openBraceToken)
    {
        return Update(Keyword, Identifier, Register, openBraceToken, Members, CloseBraceToken, SemicolonToken);
    }

    public ConstantBufferDeclarationSyntax WithMembers(SyntaxList<FieldDeclarationSyntax> members)
    {
        return Update(Keyword, Identifier, Register, OpenBraceToken, members, CloseBraceToken, SemicolonToken);
    }

    private ConstantBufferDeclarationSyntax WithMembers(SyntaxList<MemberDeclarationSyntax> members)
    {
        return Update(Keyword, Identifier, Register, OpenBraceToken, members, CloseBraceToken, SemicolonToken);
    }

    public ConstantBufferDeclarationSyntax WithCloseBraceToken(SyntaxToken closeBraceToken)
    {
        return Update(Keyword, Identifier, Register, OpenBraceToken, Members, closeBraceToken, SemicolonToken);
    }

    public ConstantBufferDeclarationSyntax WithSemicolonToken(SyntaxToken semicolonToken)
    {
        return Update(Keyword, Identifier, Register, OpenBraceToken, Members, CloseBraceToken, semicolonToken);
    }

    public ConstantBufferDeclarationSyntax AddMembers(params FieldDeclarationSyntax[] items)
    {
        return WithMembers(Members.AddRange(items));
    }

    public override TResult? Accept<TResult>(HlslSyntaxVisitor<TResult> visitor) where TResult : default
    {
        return visitor.VisitConstantBufferDeclaration(this);
    }
}