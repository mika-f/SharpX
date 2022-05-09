// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.ShaderLab.Syntax.InternalSyntax;

namespace SharpX.ShaderLab.Syntax;

public class IdentifierNameSyntax : SimpleNameSyntax
{
    public override SyntaxToken Identifier => new(this, ((IdentifierNameSyntaxInternal)Green).Identifier, Position, 0);

    internal IdentifierNameSyntax(ShaderLabSyntaxNodeInternal node, SyntaxNode? parent, int position) : base(node, parent, position) { }

    public override SyntaxNode? GetNodeSlot(int index)
    {
        return null;
    }

    public override SyntaxNode? GetCachedSlot(int index)
    {
        return null;
    }

    public IdentifierNameSyntax Update(SyntaxToken identifier)
    {
        if (identifier != Identifier)
            return SyntaxFactory.IdentifierName(identifier);
        return this;
    }

    public IdentifierNameSyntax WithIdentifier(SyntaxToken identifier)
    {
        return Update(identifier);
    }

    public override TResult? Accept<TResult>(ShaderLabSyntaxVisitor<TResult> visitor) where TResult : default
    {
        return visitor.VisitIdentifierName(this);
    }
}