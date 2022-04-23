// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.Hlsl.Syntax.InternalSyntax;

namespace SharpX.Hlsl.Syntax;

public class CastExpressionSyntax : ExpressionSyntax
{
    private ExpressionSyntax? _expression;
    private TypeSyntax? _type;

    public SyntaxToken OpenParenToken => new(this, ((CastExpressionSyntaxInternal)Green).OpenParenToken, Position, 0);

    public TypeSyntax Type => GetRed(ref _type, 1)!;

    public SyntaxToken CloseParenToken => new(this, ((CastExpressionSyntaxInternal)Green).CloseParenToken, GetChildPosition(2), GetChildIndex(2));

    public ExpressionSyntax Expression => GetRed(ref _expression, 3)!;


    internal CastExpressionSyntax(HlslSyntaxNodeInternal node, SyntaxNode? parent, int position) : base(node, parent, position) { }

    public override SyntaxNode? GetNodeSlot(int index)
    {
        return index switch
        {
            1 => GetRed(ref _type, 1)!,
            3 => GetRed(ref _expression, 3),
            _ => null
        };
    }

    public override SyntaxNode? GetCachedSlot(int index)
    {
        return index switch
        {
            1 => _type,
            3 => _expression,
            _ => null
        };
    }

    public CastExpressionSyntax Update(SyntaxToken openParenToken, TypeSyntax type, SyntaxToken closeParenToken, ExpressionSyntax expression)
    {
        if (openParenToken != OpenParenToken || type != Type || closeParenToken != CloseParenToken || expression != Expression)
            return SyntaxFactory.CastExpression(openParenToken, type, closeParenToken, expression);
        return this;
    }

    public CastExpressionSyntax WithOpenParenToken(SyntaxToken openParenToken)
    {
        return Update(openParenToken, Type, CloseParenToken, Expression);
    }

    public CastExpressionSyntax WithType(TypeSyntax type)
    {
        return Update(OpenParenToken, type, CloseParenToken, Expression);
    }

    public CastExpressionSyntax WithCloseParenToken(SyntaxToken closeParenToken)
    {
        return Update(OpenParenToken, Type, closeParenToken, Expression);
    }

    public CastExpressionSyntax WithExpression(ExpressionSyntax expression)
    {
        return Update(OpenParenToken, Type, CloseParenToken, expression);
    }
}