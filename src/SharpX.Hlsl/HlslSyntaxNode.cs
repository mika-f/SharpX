// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;

namespace SharpX.Hlsl;

public abstract class HlslSyntaxNode : SyntaxNode
{
    public override string Language => "HLSL";

    public SyntaxKind Kind => (SyntaxKind)Green.RawKind;

    internal new HlslSyntaxNode? Parent => (HlslSyntaxNode?)base.Parent;

    internal HlslSyntaxNode(GreenNode node, SyntaxNode? parent, int position) : base(node, parent, position) { }

    protected override SyntaxNode NormalizeWhitespaceCore(string indentation, string eol, bool elasticTrivia)
    {
        return SyntaxNormalizer.Normalize(this, indentation, eol, elasticTrivia);
    }

    public abstract TResult? Accept<TResult>(HlslSyntaxVisitor<TResult> visitor);

    protected override SyntaxNode ReplaceCore<TNode>(IEnumerable<TNode>? nodes = null, Func<TNode, TNode, SyntaxNode>? computeReplacementNode = null, IEnumerable<SyntaxToken>? tokens = null, Func<SyntaxToken, SyntaxToken, SyntaxToken>? computeReplacementToken = null,
                                                     IEnumerable<SyntaxTrivia>? trivia = null, Func<SyntaxTrivia, SyntaxTrivia, SyntaxTrivia>? computeReplacementTrivia = null)
    {
        return SyntaxReplacer.Replace(this, nodes, computeReplacementNode, tokens, computeReplacementToken, trivia, computeReplacementTrivia);
    }
}