// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.ShaderLab.Syntax.InternalSyntax;

namespace SharpX.ShaderLab.Syntax;

public class TextureLiteralExpressionSyntax : ExpressionSyntax
{
    private LiteralExpressionSyntax? _value;

    public LiteralExpressionSyntax Value => GetRedAtZero(ref _value)!;

    public SyntaxToken OpenBraceToken => new(this, ((TextureLiteralExpressionSyntaxInternal)Green).OpenBraceToken, GetChildPosition(1), GetChildIndex(1));

    public SyntaxToken CloseBraceToken => new(this, ((TextureLiteralExpressionSyntaxInternal)Green).CloseBraceToken, GetChildPosition(2), GetChildIndex(2));

    internal TextureLiteralExpressionSyntax(ShaderLabSyntaxNodeInternal node, SyntaxNode? parent, int position) : base(node, parent, position) { }

    public override SyntaxNode? GetNodeSlot(int index)
    {
        return index == 0 ? GetRedAtZero(ref _value) : null;
    }

    public override SyntaxNode? GetCachedSlot(int index)
    {
        return index == 0 ? _value : null;
    }

    public TextureLiteralExpressionSyntax Update(LiteralExpressionSyntax value, SyntaxToken openBraceToken, SyntaxToken closeBraceToken)
    {
        if (value != Value || openBraceToken != OpenBraceToken || closeBraceToken != CloseBraceToken)
            return SyntaxFactory.TextureLiteralExpression(value, openBraceToken, closeBraceToken);
        return this;
    }

    public TextureLiteralExpressionSyntax WithValue(LiteralExpressionSyntax value)
    {
        return Update(value, OpenBraceToken, CloseBraceToken);
    }

    public TextureLiteralExpressionSyntax WithOpenBraceToken(SyntaxToken openBraceToken)
    {
        return Update(Value, openBraceToken, CloseBraceToken);
    }

    public TextureLiteralExpressionSyntax WithCloseBraceToken(SyntaxToken closeBraceToken)
    {
        return Update(Value, OpenBraceToken, closeBraceToken);
    }

    public override TResult? Accept<TResult>(ShaderLabSyntaxVisitor<TResult> visitor) where TResult : default
    {
        return visitor.VisitTextureLiteralExpression(this);
    }
}