// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.Hlsl.Syntax.InternalSyntax;

namespace SharpX.Hlsl.Syntax;

public class SemanticSyntax : HlslSyntaxNode
{
    private IdentifierNameSyntax? _identifier;

    public SyntaxToken ColonToken => new(this, ((SemanticSyntaxInternal)Green).ColonToken, Position, 0);

    public IdentifierNameSyntax Identifier => GetRed(ref _identifier, 1)!;


    internal SemanticSyntax(HlslSyntaxNodeInternal node, SyntaxNode? parent, int position) : base(node, parent, position) { }

    public override SyntaxNode? GetNodeSlot(int index)
    {
        return index == 1 ? GetRed(ref _identifier, 1) : null;
    }

    public override SyntaxNode? GetCachedSlot(int index)
    {
        return index == 1 ? _identifier : null;
    }

    public SemanticSyntax Update(SyntaxToken colonToken, IdentifierNameSyntax identifier)
    {
        if (colonToken != ColonToken || identifier != Identifier)
            return SyntaxFactory.Semantics(colonToken, identifier);
        return this;
    }

    public SemanticSyntax WithColonToken(SyntaxToken colonToken)
    {
        return Update(colonToken, Identifier);
    }

    public SemanticSyntax WithIdentifier(IdentifierNameSyntax identifier)
    {
        return Update(ColonToken, identifier);
    }

    public override TResult? Accept<TResult>(HlslSyntaxVisitor<TResult> visitor) where TResult : default
    {
        return visitor.VisitSemantics(this);
    }
}