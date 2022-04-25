// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.Hlsl.Syntax.InternalSyntax;

namespace SharpX.Hlsl.Syntax;

public class CompilationUnitSyntax : HlslSyntaxNode
{
    private SyntaxNode? _members;

    public SyntaxList<MemberDeclarationSyntax> Members => new(GetRedAtZero(ref _members));

    public SyntaxToken EndOfFileToken => new(this, ((CompilationUnitSyntaxInternal)Green).EndOfFileToken, GetChildPosition(1), GetChildIndex(1));

    public CompilationUnitSyntax(GreenNode node, SyntaxNode? parent, int position) : base(node, parent, position) { }

    public override SyntaxNode? GetNodeSlot(int index)
    {
        return index switch
        {
            0 => GetRedAtZero(ref _members),
            _ => null
        };
    }

    public override SyntaxNode? GetCachedSlot(int index)
    {
        return index switch
        {
            0 => _members,
            _ => null
        };
    }

    public CompilationUnitSyntax Update(SyntaxList<MemberDeclarationSyntax> members, SyntaxToken endOfFileToken)
    {
        if (members != Members || endOfFileToken != EndOfFileToken)
            return SyntaxFactory.CompilationUnit(members, endOfFileToken);
        return this;
    }

    public CompilationUnitSyntax WithMembers(SyntaxList<MemberDeclarationSyntax> members)
    {
        return Update(members, EndOfFileToken);
    }

    public CompilationUnitSyntax WithEndOfFileToken(SyntaxToken endOfFileToken)
    {
        return Update(Members, endOfFileToken);
    }

    public CompilationUnitSyntax AddMembers(params MemberDeclarationSyntax[] items)
    {
        return WithMembers(Members.AddRange(items));
    }
}