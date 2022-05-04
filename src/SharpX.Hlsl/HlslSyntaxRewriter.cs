// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.Core.Syntax;

namespace SharpX.Hlsl;

/// <summary>
///     based on <see cref="Microsoft.CodeAnalysis.CSharp.CSharpSyntaxRewriter" />
/// </summary>
public class HlslSyntaxRewriter : HlslSyntaxVisitor<SyntaxNode?>
{
    public virtual SyntaxToken VisitToken(SyntaxToken token)
    {
        var node = token.Node;
        if (node == null)
            return token;

        var leadingTrivia = node.GetLeadingTrivia();
        var trailingTrivia = node.GetTrailingTrivia();

        if (leadingTrivia != null)
        {
            var leading = VisitList(new SyntaxTriviaList(token, leadingTrivia));

            if (trailingTrivia != null)
            {
                var index = leadingTrivia.IsList ? leadingTrivia.SlotCount : 1;
                var trailing = VisitList(new SyntaxTriviaList(token, trailingTrivia, token.Position + node.FullWidth - trailingTrivia.FullWidth, index));

                if (leading.Node != leadingTrivia)
                    token = token.WithLeadingTrivia(leading);

                return trailing.Node != trailingTrivia ? token.WithTrailingTrivia(trailing) : token;
            }

            return leading.Node != leadingTrivia ? token.WithLeadingTrivia(leading) : token;
        }

        if (trailingTrivia != null)
        {
            var trailing = VisitList(new SyntaxTriviaList(token, trailingTrivia, token.Position + node.FullWidth - trailingTrivia.FullWidth));
            return trailing.Node != trailingTrivia ? token.WithTrailingTrivia(trailing) : token;
        }

        return token;
    }

    public virtual SyntaxTrivia VisitTrivia(SyntaxTrivia trivia)
    {
        /*
        if (this.VisitIntoStructuredTrivia && trivia.HasStructure)
        {
            var structure = (HlslSyntaxNode)trivia.GetStructure()!;
            var newStructure = (StructuredTriviaSyntax?)this.Visit(structure);
            if (newStructure != structure)
            {
                if (newStructure != null)
                    return SyntaxFactory.Trivia(newStructure);
                return default;
            }
        }
        */

        return trivia;
    }

    public virtual SyntaxList<TNode> VisitList<TNode>(SyntaxList<TNode> list) where TNode : SyntaxNode
    {
        SyntaxListBuilder? alternate = null;
        for (int i = 0, n = list.Count; i < n; i++)
        {
            var item = list[i];
            var visited = VisitListElement(item);
            if (item != visited && alternate == null)
            {
                alternate = new SyntaxListBuilder(n);
                alternate.AddRange(list, 0, i);
            }

            if (alternate != null && visited != null && visited.RawKind != (int)SyntaxKind.None)
                alternate.Add(visited);
        }

        if (alternate != null) return alternate.ToList();

        return list;
    }

    public virtual TNode? VisitListElement<TNode>(TNode? node) where TNode : SyntaxNode
    {
        return (TNode?)Visit(node);
    }

    public virtual SeparatedSyntaxList<TNode> VisitList<TNode>(SeparatedSyntaxList<TNode> list) where TNode : SyntaxNode
    {
        var count = list.Count;
        var sepCount = list.SeparatedCount;

        SeparatedSyntaxListBuilder<TNode> alternate = default;

        var i = 0;
        for (; i < sepCount; i++)
        {
            var node = list[i];
            var visitedNode = VisitListElement(node);

            var separator = list.GetSeparator(i);
            var visitedSeparator = VisitListSeparator(separator);

            if (alternate.IsNull)
                if (node != visitedNode || separator != visitedSeparator)
                {
                    alternate = new SeparatedSyntaxListBuilder<TNode>(count);
                    alternate.AddRange(list, i);
                }

            if (!alternate.IsNull)
            {
                if (visitedNode != null)
                {
                    alternate.Add(visitedNode);

                    if (visitedSeparator.RawKind == (int)SyntaxKind.None) throw new InvalidOperationException();
                    alternate.AddSeparator(visitedSeparator);
                }
                else
                {
                    if (visitedNode == null) throw new InvalidOperationException();
                }
            }
        }

        if (i < count)
        {
            var node = list[i];
            var visitedNode = VisitListElement(node);

            if (alternate.IsNull)
                if (node != visitedNode)
                {
                    alternate = new SeparatedSyntaxListBuilder<TNode>(count);
                    alternate.AddRange(list, i);
                }

            if (!alternate.IsNull && visitedNode != null) alternate.Add(visitedNode);
        }

        if (!alternate.IsNull)
            return alternate.ToList();

        return list;
    }

    public virtual SyntaxToken VisitListSeparator(SyntaxToken separator)
    {
        return VisitToken(separator);
    }

    public virtual SyntaxTokenList VisitList(SyntaxTokenList list)
    {
        SyntaxTokenListBuilder? alternate = null;
        var count = list.Count;
        var index = -1;

        foreach (var item in list)
        {
            index++;
            var visited = VisitToken(item);
            if (item != visited && alternate == null)
            {
                alternate = new SyntaxTokenListBuilder(count);
                alternate.Add(list, 0, index);
            }

            if (alternate != null && visited.RawKind != (int)SyntaxKind.None) //skip the null check since SyntaxToken is a value type
                alternate.Add(visited);
        }

        if (alternate != null) return alternate.ToList();

        return list;
    }

    public virtual SyntaxTriviaList VisitList(SyntaxTriviaList list)
    {
        var count = list.Count;
        if (count != 0)
        {
            SyntaxTriviaListBuilder? alternate = null;
            var index = -1;

            foreach (var item in list)
            {
                index++;
                var visited = VisitListElement(item);

                //skip the null check since SyntaxTrivia is a value type
                if (visited != item && alternate == null)
                {
                    alternate = new SyntaxTriviaListBuilder(count);
                    alternate.Add(list, 0, index);
                }

                if (alternate != null && visited.RawKind != (int)SyntaxKind.None) alternate.Add(visited);
            }

            if (alternate != null) return alternate.ToList();
        }

        return list;
    }

    public virtual SyntaxTrivia VisitListElement(SyntaxTrivia element)
    {
        return VisitTrivia(element);
    }
}