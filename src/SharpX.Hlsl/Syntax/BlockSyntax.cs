// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.Hlsl.Syntax.InternalSyntax;

namespace SharpX.Hlsl.Syntax;

public class BlockSyntax : StatementSyntax
{
    private SyntaxNode? _attributeLists;
    private SyntaxNode? _statements;

    public override SyntaxList<AttributeListSyntax> AttributeLists => new(GetRedAtZero(ref _attributeLists));

    public SyntaxToken OpenBraceToken => new(this, ((BlockSyntaxInternal)Green).OpenBraceToken, GetChildPosition(1), GetChildIndex(1));

    public SyntaxList<StatementSyntax> Statements => new(GetRed(ref _statements, 2));

    public SyntaxToken CloseBraceToken => new(this, ((BlockSyntaxInternal)Green).CloseBraceToken, GetChildPosition(3), GetChildIndex(3));

    internal BlockSyntax(HlslSyntaxNodeInternal node, SyntaxNode? parent, int position) : base(node, parent, position) { }

    public override SyntaxNode? GetNodeSlot(int index)
    {
        return index switch
        {
            0 => GetRedAtZero(ref _attributeLists)!,
            2 => GetRed(ref _statements, 2),
            _ => null
        };
    }

    public override SyntaxNode? GetCachedSlot(int index)
    {
        return index switch
        {
            0 => _attributeLists,
            2 => _statements,
            _ => null
        };
    }

    public BlockSyntax Update(SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken openBraceToken, SyntaxList<StatementSyntax> statements, SyntaxToken closeBraceToken)
    {
        if (attributeLists != AttributeLists || openBraceToken != OpenBraceToken || statements != Statements || closeBraceToken != CloseBraceToken)
            return SyntaxFactory.Block(attributeLists, openBraceToken, statements, closeBraceToken);
        return this;
    }

    public new BlockSyntax WithAttributeLists(SyntaxList<AttributeListSyntax> attributeLists)
    {
        return Update(attributeLists, OpenBraceToken, Statements, CloseBraceToken);
    }

    internal override StatementSyntax WithAttributeListsCore(SyntaxList<AttributeListSyntax> attributeLists)
    {
        return WithAttributeLists(attributeLists);
    }

    public BlockSyntax WithOpenBraceToken(SyntaxToken openBraceToken)
    {
        return Update(AttributeLists, openBraceToken, Statements, CloseBraceToken);
    }

    public BlockSyntax WithStatements(SyntaxList<StatementSyntax> statements)
    {
        return Update(AttributeLists, OpenBraceToken, statements, CloseBraceToken);
    }

    public BlockSyntax WithCloseBraceToken(SyntaxToken closeBraceToken)
    {
        return Update(AttributeLists, OpenBraceToken, Statements, closeBraceToken);
    }

    public new BlockSyntax AddAttributeLists(params AttributeListSyntax[] items)
    {
        return WithAttributeLists(AttributeLists.AddRange(items));
    }


    internal override StatementSyntax AddAttributeListsCore(params AttributeListSyntax[] items)
    {
        return AddAttributeLists(items);
    }

    public BlockSyntax AddStatements(params StatementSyntax[] items)
    {
        return WithStatements(Statements.AddRange(items));
    }

    public override TResult? Accept<TResult>(HlslSyntaxVisitor<TResult> visitor) where TResult : default
    {
        return visitor.VisitBlock(this);
    }
}