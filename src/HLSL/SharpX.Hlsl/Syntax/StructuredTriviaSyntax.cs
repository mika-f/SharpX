// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;

namespace SharpX.Hlsl.Syntax;

public abstract class StructuredTriviaSyntax : HlslSyntaxNode, IStructuredTriviaSyntax
{
    protected StructuredTriviaSyntax(GreenNode node, SyntaxNode? parent, int position) : base(node, parent, position) { }

    public SyntaxTrivia ParentTrivia { get; private set; }

    internal static StructuredTriviaSyntax Create(SyntaxTrivia trivia)
    {
        var node = trivia.RequiredUnderlyingNode;
        var parent = trivia.Token.Parent;
        var position = trivia.Position;

        var red = (StructuredTriviaSyntax)node.CreateRed(parent, position);
        red.ParentTrivia = trivia;

        return red;
    }
}