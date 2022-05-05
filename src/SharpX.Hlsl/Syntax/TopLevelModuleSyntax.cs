// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.Hlsl.Syntax.InternalSyntax;

namespace SharpX.Hlsl.Syntax;

public class TopLevelModuleSyntax : MemberDeclarationSyntax
{
    private SyntaxNode? _members;

    public SyntaxList<MemberDeclarationSyntax> Members => new(GetRedAtZero(ref _members));

    internal TopLevelModuleSyntax(HlslSyntaxNodeInternal node, SyntaxNode? parent, int position) : base(node, parent, position) { }

    public override SyntaxNode? GetNodeSlot(int index)
    {
        return index == 0 ? GetRedAtZero(ref _members) : null;
    }

    public override SyntaxNode? GetCachedSlot(int index)
    {
        return index == 0 ? _members : null;
    }

    public TopLevelModuleSyntax Update(SyntaxList<MemberDeclarationSyntax> members)
    {
        if (Members != members)
            return SyntaxFactory.TopLevelModule(members);
        return this;
    }

    public TopLevelModuleSyntax WithMembers(SyntaxList<MemberDeclarationSyntax> members)
    {
        return Update(members);
    }

    public TopLevelModuleSyntax AddMembers(params MemberDeclarationSyntax[] members)
    {
        return WithMembers(Members.AddRange(members));
    }

    public override TResult? Accept<TResult>(HlslSyntaxVisitor<TResult> visitor) where TResult : default
    {
        return visitor.VisitTopLevelModule(this);
    }
}