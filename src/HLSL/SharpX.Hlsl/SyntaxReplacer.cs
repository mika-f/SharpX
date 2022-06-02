// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using System.Diagnostics.CodeAnalysis;

using SharpX.Core;

namespace SharpX.Hlsl;

internal static class SyntaxReplacer
{
    public static SyntaxNode Replace<TNode>(
        SyntaxNode root,
        IEnumerable<TNode>? nodes = null, Func<TNode, TNode, SyntaxNode>? computeReplacementNode = null,
        IEnumerable<SyntaxToken>? tokens = null, Func<SyntaxToken, SyntaxToken, SyntaxToken>? computeReplacementToken = null,
        IEnumerable<SyntaxTrivia>? trivia = null, Func<SyntaxTrivia, SyntaxTrivia, SyntaxTrivia>? computeReplacementTrivia = null
    ) where TNode : SyntaxNode
    {
        var replacer = new Replacer<TNode>(nodes, computeReplacementNode, tokens, computeReplacementToken, trivia, computeReplacementTrivia);
        if (replacer.HasWork)
            return replacer.Visit(root);
        return root;
    }

    private class Replacer<TNode> : HlslSyntaxRewriter where TNode : SyntaxNode
    {
        private static readonly HashSet<SyntaxNode> EmptyNodes = new();
        private static readonly HashSet<SyntaxToken> EmptyTokens = new();
        private static readonly HashSet<SyntaxTrivia> EmptyTrivia = new();

        private readonly Func<TNode, TNode, SyntaxNode>? _computeReplacementNode;
        private readonly Func<SyntaxToken, SyntaxToken, SyntaxToken>? _computeReplacementToken;
        private readonly Func<SyntaxTrivia, SyntaxTrivia, SyntaxTrivia>? _computeReplacementTrivia;


        private readonly HashSet<SyntaxNode> _nodeSet;
        private readonly bool _shouldVisitTrivia;
        private readonly HashSet<TextSpan> _spanSet;
        private readonly HashSet<SyntaxToken> _tokenSet;

        private readonly TextSpan _totalSpan;
        private readonly HashSet<SyntaxTrivia> _triviaSet;

        public bool HasWork => _nodeSet.Count + _tokenSet.Count + _triviaSet.Count > 0;

        public Replacer(
            IEnumerable<TNode>? nodes,
            Func<TNode, TNode, SyntaxNode>? computeReplacementNode,
            IEnumerable<SyntaxToken>? tokens,
            Func<SyntaxToken, SyntaxToken, SyntaxToken>? computeReplacementToken,
            IEnumerable<SyntaxTrivia>? trivia,
            Func<SyntaxTrivia, SyntaxTrivia, SyntaxTrivia>? computeReplacementTrivia)
        {
            _computeReplacementNode = computeReplacementNode;
            _computeReplacementToken = computeReplacementToken;
            _computeReplacementTrivia = computeReplacementTrivia;

            _nodeSet = nodes != null ? new HashSet<SyntaxNode>(nodes) : EmptyNodes;
            _tokenSet = tokens != null ? new HashSet<SyntaxToken>(tokens) : EmptyTokens;
            _triviaSet = trivia != null ? new HashSet<SyntaxTrivia>(trivia) : EmptyTrivia;

            _spanSet = new HashSet<TextSpan>(
                _nodeSet.Select(n => n.FullSpan).Concat(
                    _tokenSet.Select(t => t.FullSpan).Concat(
                        _triviaSet.Select(t => t.FullSpan))));


            _totalSpan = ComputeTotalSpan(_spanSet);
            _shouldVisitTrivia = _triviaSet.Count > 0;
        }

        private static TextSpan ComputeTotalSpan(IEnumerable<TextSpan> spans)
        {
            var first = true;
            var start = 0;
            var end = 0;

            foreach (var span in spans)
                if (first)
                {
                    start = span.Start;
                    end = span.End;
                    first = false;
                }
                else
                {
                    start = Math.Min(start, span.Start);
                    end = Math.Max(end, span.End);
                }

            return new TextSpan(start, end - start);
        }

        private bool ShouldVisit(TextSpan span)
        {
            if (!span.IntersectsWith(_totalSpan)) return false;

            foreach (var s in _spanSet)
                if (span.IntersectsWith(s))
                    return true;

            return false;
        }

        [return: NotNullIfNotNull("node")]
        public override SyntaxNode? Visit(SyntaxNode? node)
        {
            var rewritten = node;
            if (node != null)
            {
                if (ShouldVisit(node.FullSpan))
                    rewritten = base.Visit(node);

                if (_nodeSet.Contains(node) && _computeReplacementNode != null)
                    rewritten = _computeReplacementNode((TNode)node, (TNode)rewritten!);
            }

            return rewritten;
        }

        public override SyntaxToken VisitToken(SyntaxToken token)
        {
            var rewritten = token;
            if (_shouldVisitTrivia && ShouldVisit(token.FullSpan))
                rewritten = base.VisitToken(token);

            if (_tokenSet.Contains(token) && _computeReplacementToken != null)
                rewritten = _computeReplacementToken(token, rewritten);

            return rewritten;
        }

        public override SyntaxTrivia VisitListElement(SyntaxTrivia element)
        {
            var rewritten = element;
            if (element.HasStructure && ShouldVisit(element.FullSpan))
                rewritten = VisitTrivia(element);

            if (_triviaSet.Contains(element) && _computeReplacementTrivia != null)
                rewritten = _computeReplacementTrivia(element, rewritten);
            return rewritten;
        }
    }
}