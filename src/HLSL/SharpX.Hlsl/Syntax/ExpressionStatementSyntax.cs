// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.Hlsl.Syntax.InternalSyntax;

namespace SharpX.Hlsl.Syntax;

public class ExpressionStatementSyntax : StatementSyntax
{
    private SyntaxNode? _attributeLists;
    private ExpressionSyntax? _expression;

    public override SyntaxList<AttributeListSyntax> AttributeLists => new(GetRedAtZero(ref _attributeLists));

    public ExpressionSyntax Expression => GetRed(ref _expression, 1)!;

    public SyntaxToken SemicolonToken => new(this, ((ExpressionStatementSyntaxInternal)Green).SemicolonToken, GetChildPosition(2), GetChildIndex(2));

    internal ExpressionStatementSyntax(HlslSyntaxNodeInternal node, SyntaxNode? parent, int position) : base(node, parent, position) { }

    public override SyntaxNode? GetNodeSlot(int index)
    {
        return index switch
        {
            0 => GetRedAtZero(ref _attributeLists)!,
            1 => GetRed(ref _expression, 1)!,
            _ => null
        };
    }

    public override SyntaxNode? GetCachedSlot(int index)
    {
        return index switch
        {
            0 => _attributeLists,
            1 => _expression,
            _ => null
        };
    }

    public ExpressionStatementSyntax Update(SyntaxList<AttributeListSyntax> attributeLists, ExpressionSyntax expression, SyntaxToken semicolonToken)
    {
        if (attributeLists != AttributeLists || expression != Expression || semicolonToken != SemicolonToken)
            return SyntaxFactory.ExpressionStatement(attributeLists, expression, semicolonToken);
        return this;
    }

    public new ExpressionStatementSyntax WithAttributeLists(SyntaxList<AttributeListSyntax> attributeLists)
    {
        return Update(attributeLists, Expression, SemicolonToken);
    }

    internal override StatementSyntax WithAttributeListsCore(SyntaxList<AttributeListSyntax> attributeLists)
    {
        return WithAttributeLists(attributeLists);
    }

    public ExpressionStatementSyntax WithExpression(ExpressionSyntax expression)
    {
        return Update(AttributeLists, expression, SemicolonToken);
    }

    public ExpressionStatementSyntax WithSemicolonToken(SyntaxToken semicolonToken)
    {
        return Update(AttributeLists, Expression, semicolonToken);
    }

    public new ExpressionStatementSyntax AddAttributeLists(params AttributeListSyntax[] items)
    {
        return WithAttributeLists(AttributeLists.AddRange(items));
    }

    internal override StatementSyntax AddAttributeListsCore(params AttributeListSyntax[] items)
    {
        return AddAttributeLists(items);
    }

    public override TResult? Accept<TResult>(HlslSyntaxVisitor<TResult> visitor) where TResult : default
    {
        return visitor.VisitExpressionStatement(this);
    }
}