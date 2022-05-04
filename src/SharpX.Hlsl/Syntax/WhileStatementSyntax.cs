// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.Hlsl.Syntax.InternalSyntax;

namespace SharpX.Hlsl.Syntax;

public class WhileStatementSyntax : StatementSyntax
{
    private SyntaxNode? _attributeLists;
    private ExpressionSyntax? _condition;
    private StatementSyntax? _statement;

    public override SyntaxList<AttributeListSyntax> AttributeLists => new(GetRedAtZero(ref _attributeLists));

    public SyntaxToken WhileKeyword => new(this, ((WhileStatementSyntaxInternal)Green).WhileKeyword, GetChildPosition(1), GetChildIndex(1));

    public SyntaxToken OpenParenToken => new(this, ((WhileStatementSyntaxInternal)Green).OpenParenToken, GetChildPosition(2), GetChildIndex(2));

    public ExpressionSyntax Condition => GetRed(ref _condition, 3)!;

    public SyntaxToken CloseParenToken => new(this, ((WhileStatementSyntaxInternal)Green).CloseParenToken, GetChildPosition(4), GetChildIndex(4));

    public StatementSyntax Statement => GetRed(ref _statement, 5)!;

    internal WhileStatementSyntax(HlslSyntaxNodeInternal node, SyntaxNode? parent, int position) : base(node, parent, position) { }

    public override SyntaxNode? GetNodeSlot(int index)
    {
        return index switch
        {
            0 => GetRedAtZero(ref _attributeLists)!,
            3 => GetRed(ref _condition, 3)!,
            5 => GetRed(ref _statement, 5)!,
            _ => null
        };
    }

    public override SyntaxNode? GetCachedSlot(int index)
    {
        return index switch
        {
            0 => _attributeLists,
            3 => _condition,
            5 => _statement,
            _ => null
        };
    }

    public WhileStatementSyntax Update(SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken whileKeyword, SyntaxToken openParenToken, ExpressionSyntax condition, SyntaxToken closeParenToken, StatementSyntax statement)
    {
        if (attributeLists != AttributeLists || whileKeyword != WhileKeyword || openParenToken != OpenParenToken || condition != Condition || closeParenToken != CloseParenToken)
            return SyntaxFactory.WhileStatement(attributeLists, whileKeyword, openParenToken, condition, closeParenToken, statement);
        return this;
    }

    public new WhileStatementSyntax WithAttributeLists(SyntaxList<AttributeListSyntax> attributeLists)
    {
        return Update(attributeLists, WhileKeyword, OpenParenToken, Condition, CloseParenToken, Statement);
    }

    internal override StatementSyntax WithAttributeListsCore(SyntaxList<AttributeListSyntax> attributeLists)
    {
        return WithAttributeLists(attributeLists);
    }

    public WhileStatementSyntax WithWhileKeyword(SyntaxToken whileKeyword)
    {
        return Update(AttributeLists, whileKeyword, OpenParenToken, Condition, CloseParenToken, Statement);
    }

    public WhileStatementSyntax WithOpenParenToken(SyntaxToken openParenToken)
    {
        return Update(AttributeLists, WhileKeyword, openParenToken, Condition, CloseParenToken, Statement);
    }

    public WhileStatementSyntax WithCondition(ExpressionSyntax condition)
    {
        return Update(AttributeLists, WhileKeyword, OpenParenToken, condition, CloseParenToken, Statement);
    }

    public WhileStatementSyntax WithCloseParenToken(SyntaxToken closeParenToken)
    {
        return Update(AttributeLists, WhileKeyword, OpenParenToken, Condition, closeParenToken, Statement);
    }

    public WhileStatementSyntax WithStatement(StatementSyntax statement)
    {
        return Update(AttributeLists, WhileKeyword, OpenParenToken, Condition, CloseParenToken, statement);
    }

    public new WhileStatementSyntax AddAttributeLists(params AttributeListSyntax[] items)
    {
        return WithAttributeLists(AttributeLists.AddRange(items));
    }

    internal override StatementSyntax AddAttributeListsCore(params AttributeListSyntax[] items)
    {
        return AddAttributeLists(items);
    }

    public override TResult? Accept<TResult>(HlslSyntaxVisitor<TResult> visitor) where TResult : default
    {
        return visitor.VisitWhileStatement(this);
    }
}