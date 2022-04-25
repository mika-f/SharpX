// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.Hlsl.Syntax.InternalSyntax;

namespace SharpX.Hlsl.Syntax;

public class IfStatementSyntax : StatementSyntax
{
    private SyntaxNode? _attributeLists;
    private ExpressionSyntax? _condition;
    private ElseClauseSyntax? _else;
    private StatementSyntax? _statement;

    public override SyntaxList<AttributeListSyntax> AttributeLists => new(GetRedAtZero(ref _attributeLists));

    public SyntaxToken IfKeyword => new(this, ((IfStatementSyntaxInternal)Green).IfKeyword, GetChildPosition(1), GetChildIndex(1));

    public SyntaxToken OpenParenToken => new(this, ((IfStatementSyntaxInternal)Green).OpenParenToken, GetChildPosition(2), GetChildIndex(2));

    public ExpressionSyntax Condition => GetRed(ref _condition, 3)!;

    public SyntaxToken CloseParenToken => new(this, ((IfStatementSyntaxInternal)Green).CloseParenToken, GetChildPosition(4), GetChildIndex(4));

    public StatementSyntax Statement => GetRed(ref _statement, 5)!;

    public ElseClauseSyntax? Else => GetRed(ref _else, 6);

    internal IfStatementSyntax(HlslSyntaxNodeInternal node, SyntaxNode? parent, int position) : base(node, parent, position) { }

    public override SyntaxNode? GetNodeSlot(int index)
    {
        return index switch
        {
            0 => GetRedAtZero(ref _attributeLists)!,
            3 => GetRed(ref _condition, 3)!,
            5 => GetRed(ref _statement, 5)!,
            6 => GetRed(ref _else, 6),
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
            6 => _else,
            _ => null
        };
    }

    public IfStatementSyntax Update(SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken ifKeyword, SyntaxToken openParenToken, ExpressionSyntax condition, SyntaxToken closeParenToken, StatementSyntax statement, ElseClauseSyntax? @else)
    {
        if (attributeLists != AttributeLists || ifKeyword != IfKeyword || openParenToken != OpenParenToken || condition != Condition || closeParenToken != CloseParenToken || statement != Statement || @else != Else)
            return SyntaxFactory.IfStatement(attributeLists, ifKeyword, openParenToken, condition, closeParenToken, statement, @else);
        return this;
    }

    public new IfStatementSyntax WithAttributeLists(SyntaxList<AttributeListSyntax> attributeLists)
    {
        return Update(attributeLists, IfKeyword, OpenParenToken, Condition, CloseParenToken, Statement, Else);
    }

    internal override StatementSyntax WithAttributeListsCore(SyntaxList<AttributeListSyntax> attributeLists)
    {
        return WithAttributeLists(attributeLists);
    }

    public IfStatementSyntax WithIfKeyword(SyntaxToken ifKeyword)
    {
        return Update(AttributeLists, ifKeyword, OpenParenToken, Condition, CloseParenToken, Statement, Else);
    }

    public IfStatementSyntax WithOpenParenToken(SyntaxToken openParenToken)
    {
        return Update(AttributeLists, IfKeyword, openParenToken, Condition, CloseParenToken, Statement, Else);
    }

    public IfStatementSyntax WithCondition(ExpressionSyntax condition)
    {
        return Update(AttributeLists, IfKeyword, OpenParenToken, condition, CloseParenToken, Statement, Else);
    }

    public IfStatementSyntax WithCloseParenToken(SyntaxToken closeParenToken)
    {
        return Update(AttributeLists, IfKeyword, OpenParenToken, Condition, closeParenToken, Statement, Else);
    }

    public IfStatementSyntax WithStatement(StatementSyntax statement)
    {
        return Update(AttributeLists, IfKeyword, OpenParenToken, Condition, CloseParenToken, statement, Else);
    }

    public IfStatementSyntax WithElse(ElseClauseSyntax @else)
    {
        return Update(AttributeLists, IfKeyword, OpenParenToken, Condition, CloseParenToken, Statement, @else);
    }

    public new IfStatementSyntax AddAttributeLists(params AttributeListSyntax[] items)
    {
        return WithAttributeLists(AttributeLists.AddRange(items));
    }

    internal override StatementSyntax AddAttributeListsCore(params AttributeListSyntax[] items)
    {
        return AddAttributeLists(items);
    }
}