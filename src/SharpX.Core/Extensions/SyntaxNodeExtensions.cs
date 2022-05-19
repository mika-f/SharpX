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

    public static TNode WithLeadingTrivia<TNode>(this TNode node, params SyntaxTrivia[]? trivia) where TNode : SyntaxNode
    {
        return node.WithLeadingTrivia((IEnumerable<SyntaxTrivia>?)trivia);
    }

    public static TNode WithLeadingTrivia<TNode>(this TNode node, IEnumerable<SyntaxTrivia>? trivia) where TNode : SyntaxNode
    {
        var first = node.GetFirstToken(includeZeroWidth: true);
        var newFirst = first.WithLeadingTrivia(trivia);
        return node.ReplaceToken(first, newFirst);
    }

    public static TNode WithTrailingTrivia<TNode>(this TNode node, params SyntaxTrivia[]? trivia) where TNode : SyntaxNode
    {
        return node.WithTrailingTrivia((IEnumerable<SyntaxTrivia>?)trivia);
    }

    public static TNode WithTrailingTrivia<TNode>(this TNode node, IEnumerable<SyntaxTrivia>? trivia) where TNode : SyntaxNode
    {
        var last = node.GetLastToken(true);
        var newLast = last.WithTrailingTrivia(trivia);
        return node.ReplaceToken(last, newLast);
    }

    public static TRoot ReplaceToken<TRoot>(this TRoot root, SyntaxToken oldToken, SyntaxToken newToken) where TRoot : SyntaxNode
    {
        return (TRoot)root.ReplaceCore<SyntaxNode>(tokens: new[] { oldToken }, computeReplacementToken: (o, r) => newToken);
    }
}