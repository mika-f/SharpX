// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

namespace SharpX.Core.Syntax;

/// <summary>
///     represents <see cref="Microsoft.CodeAnalysis.Syntax.SyntaxList" />
/// </summary>
public abstract partial class SyntaxList : SyntaxNode
{
    public override string Language => throw Exceptions.Unreachable;

    public SyntaxList(GreenNode node, SyntaxNode? parent, int position) : base(node, parent, position) { }

    protected internal override SyntaxNode NormalizeWhitespaceCore(string indentation, string eol, bool elasticTrivia)
    {
        throw Exceptions.Unreachable;
    }

    protected internal override SyntaxNode ReplaceCore<TNode>(IEnumerable<TNode>? nodes = null, Func<TNode, TNode, SyntaxNode>? computeReplacementNode = null, IEnumerable<SyntaxToken>? tokens = null, Func<SyntaxToken, SyntaxToken, SyntaxToken>? computeReplacementToken = null,
                                                              IEnumerable<SyntaxTrivia>? trivia = null, Func<SyntaxTrivia, SyntaxTrivia, SyntaxTrivia>? computeReplacementTrivia = null)
    {
        throw Exceptions.Unreachable;
    }
}