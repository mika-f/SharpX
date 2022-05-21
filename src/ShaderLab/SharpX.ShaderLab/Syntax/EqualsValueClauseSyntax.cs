// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.ShaderLab.Syntax.InternalSyntax;

namespace SharpX.ShaderLab.Syntax;

public class EqualsValueClauseSyntax : ShaderLabSyntaxNode
{
    private ExpressionSyntax? _value;

    public SyntaxToken EqualsToken => new(this, ((EqualsValueClauseSyntaxInternal)Green).EqualsToken, Position, 0);

    public ExpressionSyntax Value => GetRed(ref _value, 1)!;

    internal EqualsValueClauseSyntax(ShaderLabSyntaxNodeInternal node, SyntaxNode? parent, int position) : base(node, parent, position) { }

    public override SyntaxNode? GetNodeSlot(int index)
    {
        return index == 1 ? GetRed(ref _value, 1) : null;
    }

    public override SyntaxNode? GetCachedSlot(int index)
    {
        return index == 1 ? _value : null;
    }

    public EqualsValueClauseSyntax Update(SyntaxToken equalsToken, ExpressionSyntax value)
    {
        if (equalsToken != EqualsToken || value != Value)
            return SyntaxFactory.EqualsValueClause(equalsToken, value);
        return this;
    }

    public EqualsValueClauseSyntax WithEqualsToken(SyntaxToken equalsToken)
    {
        return Update(equalsToken, Value);
    }

    public EqualsValueClauseSyntax WithValue(ExpressionSyntax value)
    {
        return Update(EqualsToken, value);
    }

    public override TResult? Accept<TResult>(ShaderLabSyntaxVisitor<TResult> visitor) where TResult : default
    {
        return visitor.VisitEqualsValueClause(this);
    }
}