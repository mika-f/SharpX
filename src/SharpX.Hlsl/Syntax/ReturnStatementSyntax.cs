// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.Hlsl.Syntax.InternalSyntax;

namespace SharpX.Hlsl.Syntax;

public class ReturnStatementSyntax : StatementSyntax
{
    private SyntaxNode? _attributeLists;
    private ExpressionSyntax? _expression;

    public override SyntaxList<AttributeListSyntax> AttributeLists => new(GetRedAtZero(ref _attributeLists));

    public SyntaxToken ReturnKeyword => new(this, ((ReturnStatementSyntaxInternal)Green).ReturnKeyword, GetChildPosition(1), GetChildIndex(1));

    public ExpressionSyntax? Expression => GetRed(ref _expression, 2);

    public SyntaxToken SemicolonToken => new(this, ((ReturnStatementSyntaxInternal)Green).SemicolonToken, GetChildPosition(3), GetChildIndex(3));

    internal ReturnStatementSyntax(HlslSyntaxNodeInternal node, SyntaxNode? parent, int position) : base(node, parent, position) { }

    public override SyntaxNode? GetNodeSlot(int index)
    {
        return index switch
        {
            0 => GetRedAtZero(ref _attributeLists)!,
            2 => GetRed(ref _expression, 2),
            _ => null
        };
    }

    public override SyntaxNode? GetCachedSlot(int index)
    {
        return index switch
        {
            0 => _attributeLists,
            2 => _expression,
            _ => null
        };
    }

    public ReturnStatementSyntax Update(SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken returnKeyword, ExpressionSyntax? expression, SyntaxToken semicolonToken)
    {
        if (attributeLists != AttributeLists || returnKeyword != ReturnKeyword || expression != Expression || semicolonToken != SemicolonToken)
            return SyntaxFactory.ReturnStatement(attributeLists, returnKeyword, expression, semicolonToken);
        return this;
    }

    public new ReturnStatementSyntax WithAttributeLists(SyntaxList<AttributeListSyntax> attributeLists)
    {
        return Update(attributeLists, ReturnKeyword, Expression, SemicolonToken);
    }

    internal override StatementSyntax WithAttributeListsCore(SyntaxList<AttributeListSyntax> attributeLists)
    {
        return WithAttributeLists(attributeLists);
    }


    public ReturnStatementSyntax WithReturnKeyword(SyntaxToken returnKeyword)
    {
        return Update(AttributeLists, returnKeyword, Expression, SemicolonToken);
    }

    public ReturnStatementSyntax WithExpression(ExpressionSyntax? expression)
    {
        return Update(AttributeLists, ReturnKeyword, expression, SemicolonToken);
    }

    public ReturnStatementSyntax WithSemicolonToken(SyntaxToken semicolonToken)
    {
        return Update(AttributeLists, ReturnKeyword, Expression, semicolonToken);
    }

    public new ReturnStatementSyntax AddAttributeLists(params AttributeListSyntax[] items)
    {
        return WithAttributeLists(AttributeLists.AddRange(items));
    }

    internal override StatementSyntax AddAttributeListsCore(params AttributeListSyntax[] items)
    {
        return AddAttributeLists(items);
    }
}