// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.Hlsl.Syntax.InternalSyntax;

namespace SharpX.Hlsl.Syntax;

public class BreakStatementSyntax : StatementSyntax
{
    private SyntaxNode? _attributeLists;

    public override SyntaxList<AttributeListSyntax> AttributeLists => new(GetRedAtZero(ref _attributeLists));

    public SyntaxToken BreakKeyword => new(this, ((BreakStatementSyntaxInternal)Green).BreakKeyword, GetChildPosition(1), GetChildIndex(1));

    public SyntaxToken SemicolonToken => new(this, ((BreakStatementSyntaxInternal)Green).SemicolonToken, GetChildPosition(2), GetChildIndex(2));


    internal BreakStatementSyntax(HlslSyntaxNodeInternal node, SyntaxNode? parent, int position) : base(node, parent, position) { }

    public override SyntaxNode? GetNodeSlot(int index)
    {
        return index == 0 ? GetRedAtZero(ref _attributeLists) : null;
    }

    public override SyntaxNode? GetCachedSlot(int index)
    {
        return index == 0 ? _attributeLists : null;
    }

    public BreakStatementSyntax Update(SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken breakKeyword, SyntaxToken semicolonToken)
    {
        if (attributeLists != AttributeLists || breakKeyword != BreakKeyword || semicolonToken != SemicolonToken)
            return SyntaxFactory.BreakStatement(attributeLists, breakKeyword, semicolonToken);
        return this;
    }

    public new BreakStatementSyntax WithAttributeLists(SyntaxList<AttributeListSyntax> attributeLists)
    {
        return Update(attributeLists, BreakKeyword, SemicolonToken);
    }

    internal override StatementSyntax WithAttributeListsCore(SyntaxList<AttributeListSyntax> attributeLists)
    {
        return WithAttributeLists(attributeLists);
    }

    public BreakStatementSyntax WithBreakKeyword(SyntaxToken breakKeyword)
    {
        return Update(AttributeLists, breakKeyword, SemicolonToken);
    }

    public BreakStatementSyntax WithSemicolonToken(SyntaxToken semicolonToken)
    {
        return Update(AttributeLists, BreakKeyword, semicolonToken);
    }

    public new BreakStatementSyntax AddAttributeLists(params AttributeListSyntax[] items)
    {
        return WithAttributeLists(AttributeLists.AddRange(items));
    }

    internal override StatementSyntax AddAttributeListsCore(params AttributeListSyntax[] items)
    {
        return AddAttributeLists(items);
    }
}