// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.Hlsl.Syntax.InternalSyntax;

namespace SharpX.Hlsl.Syntax;

public class DoStatementSyntax : StatementSyntax
{
    private SyntaxNode? _attributeLists;
    private ExpressionSyntax? _condition;
    private StatementSyntax? _statement;

    public override SyntaxList<AttributeListSyntax> AttributeLists => new(GetRedAtZero(ref _attributeLists));

    public SyntaxToken DoKeyword => new(this, ((DoStatementSyntaxInternal)Green).DoKeyword, GetChildPosition(1), GetChildIndex(1));

    public StatementSyntax Statement => GetRed(ref _statement, 2)!;

    public SyntaxToken WhileKeyword => new(this, ((DoStatementSyntaxInternal)Green).WhileKeyword, GetChildPosition(3), GetChildIndex(3));

    public SyntaxToken OpenParenToken => new(this, ((DoStatementSyntaxInternal)Green).OpenParenToken, GetChildPosition(4), GetChildIndex(4));

    public ExpressionSyntax Condition => GetRed(ref _condition, 5)!;

    public SyntaxToken CloseParenToken => new(this, ((DoStatementSyntaxInternal)Green).CloseParenToken, GetChildPosition(6), GetChildIndex(6));

    public SyntaxToken SemicolonToken => new(this, ((DoStatementSyntaxInternal)Green).SemicolonToken, GetChildPosition(7), GetChildIndex(7));

    internal DoStatementSyntax(HlslSyntaxNodeInternal node, SyntaxNode? parent, int position) : base(node, parent, position) { }

    public override SyntaxNode? GetNodeSlot(int index)
    {
        return index switch
        {
            0 => GetRedAtZero(ref _attributeLists)!,
            2 => GetRed(ref _statement, 2)!,
            5 => GetRed(ref _condition, 5),
            _ => null
        };
    }

    public override SyntaxNode? GetCachedSlot(int index)
    {
        return index switch
        {
            0 => _attributeLists,
            2 => _statement,
            5 => _condition,
            _ => null
        };
    }

    public DoStatementSyntax Update(SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken doKeyword, StatementSyntax statement, SyntaxToken whileKeyword, SyntaxToken openParenToken, ExpressionSyntax condition, SyntaxToken closeParenToken, SyntaxToken semicolonToken)
    {
        if (attributeLists != AttributeLists || doKeyword != DoKeyword || statement != Statement || whileKeyword != WhileKeyword || openParenToken != OpenParenToken || condition != Condition || closeParenToken != CloseParenToken || semicolonToken != SemicolonToken)
            return SyntaxFactory.DoStatement(attributeLists, doKeyword, statement, whileKeyword, openParenToken, condition, closeParenToken, semicolonToken);
        return this;
    }

    public new DoStatementSyntax WithAttributeLists(SyntaxList<AttributeListSyntax> attributeLists)
    {
        return Update(attributeLists, DoKeyword, Statement, WhileKeyword, OpenParenToken, Condition, CloseParenToken, SemicolonToken);
    }

    internal override StatementSyntax WithAttributeListsCore(SyntaxList<AttributeListSyntax> attributeLists)
    {
        return WithAttributeLists(attributeLists);
    }

    public DoStatementSyntax WithDoKeyword(SyntaxToken doKeyword)
    {
        return Update(AttributeLists, doKeyword, Statement, WhileKeyword, OpenParenToken, Condition, CloseParenToken, SemicolonToken);
    }

    public DoStatementSyntax WithStatement(StatementSyntax statement)
    {
        return Update(AttributeLists, DoKeyword, statement, WhileKeyword, OpenParenToken, Condition, CloseParenToken, SemicolonToken);
    }

    public DoStatementSyntax WithWhileKeyword(SyntaxToken whileKeyword)
    {
        return Update(AttributeLists, DoKeyword, Statement, whileKeyword, OpenParenToken, Condition, CloseParenToken, SemicolonToken);
    }

    public DoStatementSyntax WithOpenParenToken(SyntaxToken openParenToken)
    {
        return Update(AttributeLists, DoKeyword, Statement, WhileKeyword, openParenToken, Condition, CloseParenToken, SemicolonToken);
    }

    public DoStatementSyntax WithCondition(ExpressionSyntax condition)
    {
        return Update(AttributeLists, DoKeyword, Statement, WhileKeyword, OpenParenToken, condition, CloseParenToken, SemicolonToken);
    }

    public DoStatementSyntax WithCloseParenToken(SyntaxToken closeParenToken)
    {
        return Update(AttributeLists, DoKeyword, Statement, WhileKeyword, OpenParenToken, Condition, closeParenToken, SemicolonToken);
    }

    public DoStatementSyntax WithSemicolonToken(SyntaxToken semicolonToken)
    {
        return Update(AttributeLists, DoKeyword, Statement, WhileKeyword, OpenParenToken, Condition, CloseParenToken, semicolonToken);
    }

    public new DoStatementSyntax AddAttributeLists(params AttributeListSyntax[] items)
    {
        return WithAttributeLists(AttributeLists.AddRange(items));
    }

    internal override StatementSyntax AddAttributeListsCore(params AttributeListSyntax[] items)
    {
        return AddAttributeLists(items);
    }

    public override TResult? Accept<TResult>(HlslSyntaxVisitor<TResult> visitor) where TResult : default
    {
        return visitor.VisitDoStatement(this);
    }
}