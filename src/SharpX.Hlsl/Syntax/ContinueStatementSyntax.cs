// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.Hlsl.Syntax.InternalSyntax;

namespace SharpX.Hlsl.Syntax;

public class ContinueStatementSyntax : StatementSyntax
{
    private SyntaxNode? _attributeLists;

    public override SyntaxList<AttributeListSyntax> AttributeLists => new(GetRedAtZero(ref _attributeLists));

    public SyntaxToken ContinueKeyword => new(this, ((ContinueStatementSyntaxInternal)Green).ContinueKeyword, GetChildPosition(1), GetChildIndex(1));

    public SyntaxToken SemicolonToken => new(this, ((ContinueStatementSyntaxInternal)Green).SemicolonToken, GetChildPosition(2), GetChildIndex(2));

    internal ContinueStatementSyntax(HlslSyntaxNodeInternal node, SyntaxNode? parent, int position) : base(node, parent, position) { }

    public override SyntaxNode? GetNodeSlot(int index)
    {
        return index == 0 ? GetRedAtZero(ref _attributeLists)! : null;
    }

    public override SyntaxNode? GetCachedSlot(int index)
    {
        return index == 0 ? _attributeLists : null;
    }

    public ContinueStatementSyntax Update(SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken continueKeyword, SyntaxToken semicolonToken)
    {
        if (attributeLists != AttributeLists || continueKeyword != ContinueKeyword || semicolonToken != SemicolonToken)
            return SyntaxFactory.ContinueStatement(attributeLists, continueKeyword, semicolonToken);
        return this;
    }

    public new ContinueStatementSyntax WithAttributeLists(SyntaxList<AttributeListSyntax> attributeLists)
    {
        return Update(attributeLists, ContinueKeyword, SemicolonToken);
    }

    internal override StatementSyntax WithAttributeListsCore(SyntaxList<AttributeListSyntax> attributeLists)
    {
        return WithAttributeLists(attributeLists);
    }

    public ContinueStatementSyntax WithContinueKeyword(SyntaxToken continueKeyword)
    {
        return Update(AttributeLists, continueKeyword, SemicolonToken);
    }

    public ContinueStatementSyntax WithSemicolonToken(SyntaxToken semicolonToken)
    {
        return Update(AttributeLists, ContinueKeyword, semicolonToken);
    }

    public new ContinueStatementSyntax AddAttributeLists(params AttributeListSyntax[] items)
    {
        return WithAttributeLists(AttributeLists.AddRange(items));
    }

    internal override StatementSyntax AddAttributeListsCore(params AttributeListSyntax[] items)
    {
        return AddAttributeLists(items);
    }
}