// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using System.Collections;

using SharpX.Core.Syntax;

namespace SharpX.Core;

/// <summary>
///     represents <see cref="Microsoft.CodeAnalysis.SyntaxList{T}" /> for SharpX
/// </summary>
/// <typeparam name="TNode"></typeparam>
public readonly partial struct SyntaxList<TNode> : IReadOnlyList<TNode>, IEquatable<SyntaxList<TNode>> where TNode : SyntaxNode
{
    public override bool Equals(object? obj)
    {
        return obj is SyntaxList<TNode> other && Equals(other);
    }

    public override int GetHashCode()
    {
        return Node != null ? Node.GetHashCode() : 0;
    }

    public SyntaxList(SyntaxNode? node)
    {
        Node = node;
    }

    public SyntaxList(TNode? node) : this((SyntaxNode?)node) { }

    public SyntaxList(IEnumerable<TNode>? nodes) : this(CreateListNode(nodes)) { }

    private static SyntaxNode? CreateListNode(IEnumerable<TNode>? nodes)
    {
        if (nodes == null)
            return null;

        var builder = nodes is ICollection<TNode> collection ? new SyntaxListBuilder<TNode>(collection.Count) : SyntaxListBuilder<TNode>.Create();
        foreach (var node in nodes)
            builder.Add(node);

        return builder.ToList().Node;
    }

    public SyntaxNode? Node { get; }

    public IEnumerator<TNode> GetEnumerator()
    {
        return new Enumerator(this);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public int Count => Node == null ? 0 : Node.IsList ? Node.SlotCount : 1;

    public TNode this[int index]
    {
        get
        {
            if (Node != null)
            {
                if (Node.IsList)
                {
                    if (index < Node.SlotCount)
                        return (TNode)Node.GetRequiredNodeSlot(index);
                }
                else if (index == 0)
                {
                    return (TNode)Node;
                }
            }

            throw new ArgumentOutOfRangeException(nameof(index));
        }
    }

    public SyntaxList<TNode> Add(TNode node)
    {
        return Insert(Count, node);
    }

    public SyntaxList<TNode> AddRange(IEnumerable<TNode> nodes)
    {
        return InsertRange(Count, nodes);
    }

    public SyntaxList<TNode> Insert(int index, TNode node)
    {
        return InsertRange(index, new[] { node });
    }

    public SyntaxList<TNode> InsertRange(int index, IEnumerable<TNode> nodes)
    {
        if (index < 0 || index > Count)
            throw new ArgumentOutOfRangeException(nameof(index));

        var list = this.ToList();
        list.InsertRange(index, nodes);

        if (list.Count == 0)
            return this;
        return CreateList(list);
    }

    private static SyntaxList<TNode> CreateList(List<TNode> items)
    {
        if (items.Count == 0)
            return default;

        var green = GreenNode.CreateList(items, static w => w.Green);
        return new SyntaxList<TNode>(green!.CreateRed());
    }

    public static bool operator ==(SyntaxList<TNode> left, SyntaxList<TNode> right)
    {
        return left.Node == right.Node;
    }

    public static bool operator !=(SyntaxList<TNode> left, SyntaxList<TNode> right)
    {
        return left.Node != right.Node;
    }

    public bool Equals(SyntaxList<TNode> other)
    {
        return Node == other.Node;
    }

    public override string ToString()
    {
        return Node?.ToString() ?? string.Empty;
    }

    public string ToFullString()
    {
        return Node?.ToFullString() ?? string.Empty;
    }

    public static implicit operator SyntaxList<TNode>(SyntaxList<SyntaxNode> nodes)
    {
        return new SyntaxList<TNode>(nodes.Node);
    }

    public static implicit operator SyntaxList<SyntaxNode>(SyntaxList<TNode> nodes)
    {
        return new SyntaxList<SyntaxNode>(nodes.Node);
    }
}