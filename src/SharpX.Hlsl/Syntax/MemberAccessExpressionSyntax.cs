// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.Hlsl.Syntax.InternalSyntax;

namespace SharpX.Hlsl.Syntax;

public class MemberAccessExpressionSyntax : ExpressionSyntax
{
    private ExpressionSyntax? _expression;
    private SimpleNameSyntax? _name;

    public ExpressionSyntax Expression => GetRedAtZero(ref _expression)!;

    public SyntaxToken OperatorToken => new(this, ((MemberAccessExpressionSyntaxInternal)Green).OperatorToken, GetChildPosition(1), GetChildIndex(1));

    public SimpleNameSyntax Name => GetRed(ref _name, 2)!;

    internal MemberAccessExpressionSyntax(HlslSyntaxNodeInternal node, SyntaxNode? parent, int position) : base(node, parent, position) { }

    public override SyntaxNode? GetNodeSlot(int index)
    {
        return index switch
        {
            0 => GetRedAtZero(ref _expression)!,
            2 => GetRed(ref _name, 2)!,
            _ => null
        };
    }

    public override SyntaxNode? GetCachedSlot(int index)
    {
        return index switch
        {
            0 => _expression,
            2 => _name,
            _ => null
        };
    }

    public MemberAccessExpressionSyntax Update(ExpressionSyntax expression, SyntaxToken operatorToken, SimpleNameSyntax name)
    {
        if (expression != Expression || operatorToken != OperatorToken || name != Name)
            return SyntaxFactory.MemberAccessExpression(expression, operatorToken, name);
        return this;
    }

    public MemberAccessExpressionSyntax WithExpression(ExpressionSyntax expression)
    {
        return Update(expression, OperatorToken, Name);
    }

    public MemberAccessExpressionSyntax WithOperatorToken(SyntaxToken operatorToken)
    {
        return Update(Expression, operatorToken, Name);
    }

    public MemberAccessExpressionSyntax WithName(SimpleNameSyntax name)
    {
        return Update(Expression, OperatorToken, name);
    }
}