// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.Hlsl.Syntax.InternalSyntax;

namespace SharpX.Hlsl.Syntax;

public class VariableDeclaratorSyntax : HlslSyntaxNode
{
    private EqualsValueClauseSyntax? _initializer;

    public SyntaxToken Identifier => new(this, ((VariableDeclaratorSyntaxInternal)Green).Identifier, Position, 0);

    public EqualsValueClauseSyntax? Initializer => GetRed(ref _initializer, 1);

    internal VariableDeclaratorSyntax(HlslSyntaxNodeInternal node, SyntaxNode? parent, int position) : base(node, parent, position) { }

    public override SyntaxNode? GetNodeSlot(int index)
    {
        return index == 1 ? GetRed(ref _initializer, 1) : null;
    }

    public override SyntaxNode? GetCachedSlot(int index)
    {
        return index == 1 ? _initializer : null;
    }

    public VariableDeclaratorSyntax Update(SyntaxToken identifier, EqualsValueClauseSyntax? initializer)
    {
        if (identifier != Identifier || initializer != Initializer)
            return SyntaxFactory.VariableDeclarator(identifier, initializer);
        return this;
    }

    public VariableDeclaratorSyntax WithIdentifier(SyntaxToken identifier)
    {
        return Update(identifier, Initializer);
    }

    public VariableDeclaratorSyntax WithInitializer(EqualsValueClauseSyntax? initializer)
    {
        return Update(Identifier, initializer);
    }

    public override TResult? Accept<TResult>(HlslSyntaxVisitor<TResult> visitor) where TResult : default
    {
        return visitor.VisitVariableDeclarator(this);
    }
}