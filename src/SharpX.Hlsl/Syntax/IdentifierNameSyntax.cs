// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.Hlsl.Syntax.InternalSyntax;

namespace SharpX.Hlsl.Syntax;

public sealed class IdentifierNameSyntax : SimpleNameSyntax
{
    public override SyntaxToken Identifier => new(this, ((IdentifierNameSyntaxInternal)Green).Identifier, Position, 0);

    internal IdentifierNameSyntax(HlslSyntaxNodeInternal node, SyntaxNode? parent, int position) : base(node, parent, position) { }

    public override SyntaxNode? GetCachedSlot(int index)
    {
        return null;
    }

    public override SyntaxNode? GetNodeSlot(int index)
    {
        return null;
    }

    public IdentifierNameSyntax Update(SyntaxToken identifier)
    {
        if (identifier != Identifier)
            return SyntaxFactory.IdentifierName(identifier);

        return this;
    }

    protected override SimpleNameSyntax WithIdentifierInternal(SyntaxToken identifier)
    {
        return Update(identifier);
    }
}