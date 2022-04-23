// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.Hlsl.Syntax.InternalSyntax;

namespace SharpX.Hlsl.Syntax;

public class InitializerExpressionSyntax : ExpressionSyntax
{
    private SyntaxNode? _expressions;

    public SyntaxToken OpenBraceToken => new(this, ((InitializerExpressionSyntaxInternal)Green).OpenBraceToken, Position, 0);

    public SeparatedSyntaxList<ExpressionSyntax> Expressions
    {
        get
        {
            var red = GetRed(ref _expressions, 1);
            return red != null ? new SeparatedSyntaxList<ExpressionSyntax>(red, GetChildIndex(1)) : default;
        }
    }

    public SyntaxToken CloseBraceToken => new(this, ((InitializerExpressionSyntaxInternal)Green).CloseBraceToken, GetChildPosition(2), GetChildIndex(2));

    internal InitializerExpressionSyntax(HlslSyntaxNodeInternal node, SyntaxNode? parent, int position) : base(node, parent, position) { }

    public override SyntaxNode? GetNodeSlot(int index)
    {
        return index == 1 ? GetRed(ref _expressions, 1)! : null;
    }

    public override SyntaxNode? GetCachedSlot(int index)
    {
        return index == 1 ? _expressions : null;
    }

    public InitializerExpressionSyntax Update(SyntaxToken openBraceToken, SeparatedSyntaxList<ExpressionSyntax> expressions, SyntaxToken closeBraceToken)
    {
        if (openBraceToken != OpenBraceToken || expressions != Expressions || closeBraceToken != CloseBraceToken)
            return SyntaxFactory.InitializerExpression(Kind, openBraceToken, expressions, CloseBraceToken);
        return this;
    }

    public InitializerExpressionSyntax WithOpenBraceToken(SyntaxToken openBraceToken)
    {
        return Update(openBraceToken, Expressions, CloseBraceToken);
    }

    public InitializerExpressionSyntax WithExpressions(SeparatedSyntaxList<ExpressionSyntax> expressions)
    {
        return Update(OpenBraceToken, expressions, CloseBraceToken);
    }

    public InitializerExpressionSyntax WithCloseBraceToken(SyntaxToken closeBraceToken)
    {
        return Update(OpenBraceToken, Expressions, closeBraceToken);
    }

    public InitializerExpressionSyntax AddExpressions(params ExpressionSyntax[] items)
    {
        return WithExpressions(Expressions.AddRange(items));
    }
}