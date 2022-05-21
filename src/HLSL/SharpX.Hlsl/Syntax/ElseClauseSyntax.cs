// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.Hlsl.Syntax.InternalSyntax;

namespace SharpX.Hlsl.Syntax;

public class ElseClauseSyntax : HlslSyntaxNode
{
    private StatementSyntax? _statement;

    public SyntaxToken ElseKeyword => new(this, ((ElseClauseSyntaxInternal)Green).ElseKeyword, Position, 0);

    public StatementSyntax Statement => GetRed(ref _statement, 1)!;

    internal ElseClauseSyntax(HlslSyntaxNodeInternal node, SyntaxNode? parent, int position) : base(node, parent, position) { }

    public override SyntaxNode? GetNodeSlot(int index)
    {
        return index == 1 ? GetRed(ref _statement, 1) : null;
    }

    public override SyntaxNode? GetCachedSlot(int index)
    {
        return index == 1 ? _statement : null;
    }

    public ElseClauseSyntax Update(SyntaxToken elseKeyword, StatementSyntax statement)
    {
        if (elseKeyword != ElseKeyword || statement != Statement)
            return SyntaxFactory.ElseClause(elseKeyword, statement);
        return this;
    }

    public ElseClauseSyntax WithElseKeyword(SyntaxToken elseKeyword)
    {
        return Update(elseKeyword, Statement);
    }

    public ElseClauseSyntax WithStatement(StatementSyntax statement)
    {
        return Update(ElseKeyword, statement);
    }

    public override TResult? Accept<TResult>(HlslSyntaxVisitor<TResult> visitor) where TResult : default
    {
        return visitor.VisitElseClause(this);
    }
}