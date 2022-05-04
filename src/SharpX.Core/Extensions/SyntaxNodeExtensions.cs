// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

namespace SharpX.Core.Extensions;

public static class SyntaxNodeExtensions
{
    public static TNode NormalizeWhitespace<TNode>(this TNode node, string indentation, bool elasticTrivia) where TNode : SyntaxNode
    {
        return (TNode)node.NormalizeWhitespaceCore(indentation, "\r\n", elasticTrivia);
    }

    public static TNode NormalizeWhitespace<TNode>(this TNode node, string indentation = "    ", string eol = "\r\n", bool elasticTrivia = false) where TNode : SyntaxNode
    {
        return (TNode)node.NormalizeWhitespaceCore(indentation, eol, elasticTrivia);
    }
}