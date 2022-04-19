// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core.Syntax.InternalSyntax;

namespace SharpX.Core.Syntax;

/// <summary>
///     represents <see cref="Microsoft.CodeAnalysis.Syntax.SyntaxListBuilder" />
/// </summary>
internal class SyntaxListBuilder
{
    private GreenNode?[] _nodes;

    public int Count { get; private set; }

    public SyntaxListBuilder(int size)
    {
        _nodes = new GreenNode[size];
    }

    public void Clear()
    {
        Count = 0;
    }

    public void Add(SyntaxNode item)
    {
        Add(item.Green);
    }

    internal void Add(GreenNode? item)
    {
        if (item == null)
            throw new ArgumentNullException();

        if (Count > _nodes.Length)
            Array.Resize(ref _nodes, _nodes.Length * 2);

        _nodes[Count++] = item;
    }

    public void AddRange(SyntaxNode[] items)
    {
        AddRange(items, 0, items.Length);
    }

    public void AddRange(SyntaxNode[] items, int offset, int length)
    {
        if (Count + length > _nodes.Length)
            Array.Resize(ref _nodes, Count + length);

        for (int i = offset, j = Count; i < offset + length; i++, j++)
            _nodes[j] = items[i].Green;

        Count += length;
    }

    public void AddRange(SyntaxList<SyntaxNode> list)
    {
        AddRange(list, 0, list.Count);
    }

    public void AddRange(SyntaxList<SyntaxNode> list, int offset, int count)
    {
        if (Count + count > _nodes.Length)
            Array.Resize(ref _nodes, Count + count);

        var counter = Count;
        for (int i = offset, limit = offset + count; i < limit; i++)
            _nodes[counter++] = list[i].Green;

        Count += count;
    }

    public void AddRange<TNode>(SyntaxList<TNode> list) where TNode : SyntaxNode
    {
        AddRange(list, 0, list.Count);
    }

    public void AddRange<TNode>(SyntaxList<TNode> list, int offset, int count) where TNode : SyntaxNode
    {
        AddRange(new SyntaxList<SyntaxNode>(list.Node), offset, count);
    }

    public void AddRange(SyntaxNodeOrTokenList list)
    {
        AddRange(list, 0, list.Count);
    }

    public void AddRange(SyntaxNodeOrTokenList list, int offset, int count)
    {
        if (Count + count > _nodes.Length)
            Array.Resize(ref _nodes, Count + count);

        var counter = Count;
        for (int i = offset, limit = offset + count; i < limit; i++)
            _nodes[counter++] = list[i].UnderlyingNode;

        Count += count;
    }

    public static implicit operator SyntaxList<SyntaxNode>(SyntaxListBuilder? builder)
    {
        if (builder == null)
            return default;
        return builder.ToList();
    }

    public GreenNode? ToListNode()
    {
        switch (Count)
        {
            case 0:
                return null;

            case 1:
                return _nodes[0];

            case 2:
                return SyntaxListInternal.List(_nodes[0]!, _nodes[1]!);

            case 3:
                return SyntaxListInternal.List(_nodes[0]!, _nodes[1]!, _nodes[2]!);

            default:
                return SyntaxListInternal.List(_nodes!);
        }
    }

    public SyntaxTokenList ToTokenList()
    {
        if (Count == 0)
            return default;
        return new SyntaxTokenList(null, ToListNode(), 0, 0);
    }

    public SyntaxList<SyntaxNode> ToList()
    {
        var node = ToListNode();
        if (node == null)
            return default;
        return new SyntaxList<SyntaxNode>(node.CreateRed());
    }

    public SyntaxList<TNode> ToList<TNode>() where TNode : SyntaxNode
    {
        var node = ToListNode();
        if (node == null)
            return default;
        return new SyntaxList<TNode>(node.CreateRed());
    }

    public SeparatedSyntaxList<TNode> ToSeparatedList<TNode>() where TNode : SyntaxNode
    {
        var node = ToListNode();
        if (node == null)
            return default;
        return new SeparatedSyntaxList<TNode>(new SyntaxNodeOrTokenList(node.CreateRed(), 0));
    }
}