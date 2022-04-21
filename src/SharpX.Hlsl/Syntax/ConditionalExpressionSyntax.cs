// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.Hlsl.Syntax.InternalSyntax;

namespace SharpX.Hlsl.Syntax;

public class ConditionalExpressionSyntax : ExpressionSyntax
{
    private ExpressionSyntax? _condition;
    private ExpressionSyntax? _whenFalse;
    private ExpressionSyntax? _whenTrue;

    public ExpressionSyntax Condition => GetRedAtZero(ref _condition)!;

    public SyntaxToken QuestionToken => new(this, ((ConditionalExpressionSyntaxInternal)Green).QuestionToken, GetChildPosition(1), GetChildIndex(1));

    public ExpressionSyntax WhenTrue => GetRed(ref _whenTrue, 2)!;

    public SyntaxToken ColonToken => new(this, ((ConditionalExpressionSyntaxInternal)Green).ColonToken, GetChildPosition(3), GetChildIndex(3));

    public ExpressionSyntax WhenFalse => GetRed(ref _whenFalse, 4)!;

    internal ConditionalExpressionSyntax(HlslSyntaxNodeInternal node, SyntaxNode? parent, int position) : base(node, parent, position) { }

    public override SyntaxNode? GetNodeSlot(int index)
    {
        return index switch
        {
            0 => GetRedAtZero(ref _condition),
            2 => GetRed(ref _whenTrue, 2),
            4 => GetRed(ref _whenFalse, 4),
            _ => null
        };
    }

    public override SyntaxNode? GetCachedSlot(int index)
    {
        return index switch
        {
            0 => _condition,
            2 => _whenTrue,
            4 => _whenFalse,
            _ => null
        };
    }

    public ConditionalExpressionSyntax Update(ExpressionSyntax condition, SyntaxToken questionToken, ExpressionSyntax whenTrue, SyntaxToken colonToken, ExpressionSyntax whenFalse)
    {
        if (condition != Condition || questionToken != QuestionToken || whenTrue != WhenTrue || colonToken != ColonToken || whenFalse != WhenFalse)
            return SyntaxFactory.ConditionalExpression(condition, questionToken, whenTrue, colonToken, whenFalse);
        return this;
    }

    public ConditionalExpressionSyntax WithCondition(ExpressionSyntax condition)
    {
        return Update(condition, QuestionToken, WhenTrue, ColonToken, WhenFalse);
    }

    public ConditionalExpressionSyntax WithQuestionToken(SyntaxToken questionToken)
    {
        return Update(Condition, questionToken, WhenTrue, ColonToken, WhenFalse);
    }

    public ConditionalExpressionSyntax WithWhenTrue(ExpressionSyntax whenTrue)
    {
        return Update(Condition, QuestionToken, whenTrue, ColonToken, WhenFalse);
    }

    public ConditionalExpressionSyntax WithColonToken(SyntaxToken colonToken)
    {
        return Update(Condition, QuestionToken, WhenTrue, colonToken, WhenFalse);
    }

    public ConditionalExpressionSyntax WithWhenFalse(ExpressionSyntax whenFalse)
    {
        return Update(Condition, QuestionToken, WhenTrue, ColonToken, whenFalse);
    }
}