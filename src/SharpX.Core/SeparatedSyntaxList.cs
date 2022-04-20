// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using System.Collections;

namespace SharpX.Core;

/// <summary>
///     represents <see cref="Microsoft.CodeAnalysis.SeparatedSyntaxList{T}" />
/// </summary>
/// <typeparam name="TNode"></typeparam>
public readonly partial struct SeparatedSyntaxList<TNode> : IEquatable<SeparatedSyntaxList<TNode>>, IReadOnlyList<TNode>
    where TNode : SyntaxNode
{
    private readonly SyntaxNodeOrTokenList _list;

    public SeparatedSyntaxList(SyntaxNodeOrTokenList list) : this()
    {
        var count = list.Count;

        _list = list;
        Count = (count + 1) >> 1;
        SeparatedCount = count >> 1;
    }

    public SeparatedSyntaxList(SyntaxNode node, int index) : this(new SyntaxNodeOrTokenList(node, index)) { }

    public SyntaxNode? Node => _list.Node;

    public int Count { get; }

    public int SeparatedCount { get; }

    public TNode this[int index]
    {
        get
        {
            var node = _list.Node;
            if (node != null)
            {
                if (node.IsList)
                {
                    if (index < Count)
                        return (TNode)node.GetRequiredNodeSlot(index << 1);
                }
                else if (index == 0)
                {
                    return (TNode)node;
                }
            }

            throw new ArgumentOutOfRangeException(nameof(index));
        }
    }

    public IEnumerator<TNode> GetEnumerator()
    {
        return new Enumerator(this);
    }

    public override string ToString()
    {
        return _list.ToString();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public string ToFullString()
    {
        return _list.ToFullString();
    }

    public SyntaxNodeOrTokenList GetWithSeparators()
    {
        return _list;
    }

    public SeparatedSyntaxList<TNode> Add(TNode node)
    {
        return Insert(Count, node);
    }

    public SeparatedSyntaxList<TNode> AddRange(IEnumerable<TNode> nodes)
    {
        return InsertRange(Count, nodes);
    }

    public SeparatedSyntaxList<TNode> Insert(int index, TNode node)
    {
        return InsertRange(index, new[] { node });
    }

    public SeparatedSyntaxList<TNode> InsertRange(int index, IEnumerable<TNode> nodes)
    {
        if (index < 0 || index > Count)
            throw new ArgumentOutOfRangeException(nameof(index));

        var nodesWithSeps = GetWithSeparators();
        var insertionPosition = index < Count ? nodesWithSeps.IndexOf(this[index]) : nodesWithSeps.Count;

        if (insertionPosition > 0 && insertionPosition < nodesWithSeps.Count)
        {
            var previous = nodesWithSeps[insertionPosition - 1];
            if (previous.IsToken && !KeepSeparatorWithPreviousNode(previous.AsToken())) insertionPosition--;
        }

        var nodesToInsetWithSeparators = new List<SyntaxNodeOrToken>();
        foreach (var node in nodes)
        {
            if (nodesToInsetWithSeparators.Count > 0 || (insertionPosition > 0 && nodesWithSeps[insertionPosition - 1].IsNode))
                nodesToInsetWithSeparators.Add(node.Green.CreateSeparator<TNode>(node));
            nodesToInsetWithSeparators.Add(node);
        }

        if (insertionPosition < nodesWithSeps.Count && nodesWithSeps[insertionPosition] is { IsNode: true })
        {
            var node = nodesWithSeps[insertionPosition].AsNode();
            Contract.AssertNotNull(node);

            nodesToInsetWithSeparators.Add(node.Green.CreateSeparator<TNode>(node));
        }

        return new SeparatedSyntaxList<TNode>(nodesWithSeps.InsertRange(insertionPosition, nodesToInsetWithSeparators));
    }

    private static bool KeepSeparatorWithPreviousNode(SyntaxToken separator)
    {
        foreach (var tr in separator.TrailingTrivia)
        {
            Contract.AssertNotNull(tr.UnderlyingNode);

            if (tr.UnderlyingNode.IsTriviaWithEndOfLine())
                return true;
        }

        return false;
    }

    public int IndexOf(SyntaxNodeOrToken node)
    {
        var i = 0;
        foreach (var item in this)
        {
            if (item == node)
                return i;

            i++;
        }

        return -1;
    }

    public static bool operator ==(SeparatedSyntaxList<TNode> left, SeparatedSyntaxList<TNode> right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(SeparatedSyntaxList<TNode> left, SeparatedSyntaxList<TNode> right)
    {
        return !left.Equals(right);
    }


    public bool Equals(SeparatedSyntaxList<TNode> other)
    {
        return _list == other._list;
    }

    public override bool Equals(object? obj)
    {
        return obj is SeparatedSyntaxList<TNode> other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_list, Count, SeparatedCount);
    }
}