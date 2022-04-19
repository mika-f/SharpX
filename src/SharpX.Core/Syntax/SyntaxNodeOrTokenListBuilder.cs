// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core.Syntax.InternalSyntax;

namespace SharpX.Core.Syntax;

/// <summary>
///     represents <see cref="Microsoft.CodeAnalysis.Syntax.SyntaxNodeOrTokenListBuilder" />
/// </summary>
internal class SyntaxNodeOrTokenListBuilder
{
    private GreenNode?[] _nodes;

    public int Count { get; private set; }

    public SyntaxNodeOrToken this[int index]
    {
        get
        {
            var node = _nodes[index];
            Contract.AssertNotNull(node);

            if (node.IsToken)
                return new SyntaxNodeOrToken(null, node, 0, 0);
            return node.CreateRed();
        }
        set => _nodes[index] = value.UnderlyingNode;
    }

    public SyntaxNodeOrTokenListBuilder(int size)
    {
        _nodes = new GreenNode[size];
        Count = 0;
    }

    public void Clear()
    {
        Count = 0;
    }

    public void Add(SyntaxNode item)
    {
        Add(item.Green);
    }

    public void Add(SyntaxToken item)
    {
        Contract.AssertNotNull(item.Node);
        Add(item.Node);
    }

    public void Add(SyntaxNodeOrToken item)
    {
        Contract.AssertNotNull(item.UnderlyingNode);
        Add(item.UnderlyingNode);
    }

    public void Add(GreenNode item)
    {
        if (Count > _nodes.Length)
            Array.Resize(ref _nodes, Count == 0 ? 8 : _nodes.Length * 2);
        _nodes[Count++] = item;
    }

    public void AddRange(SyntaxNodeOrTokenList list)
    {
        AddRange(list, 0, list.Count);
    }

    public void AddRange(SyntaxNodeOrTokenList list, int offset, int length)
    {
        if (Count + length > _nodes.Length)
            Array.Resize(ref _nodes, Count + length);

        list.CopyTo(offset, _nodes, Count, length);
        Count += length;
    }

    public void AddRange(IEnumerable<SyntaxNodeOrToken> items)
    {
        foreach (var item in items)
            Add(item);
    }

    public SyntaxNodeOrTokenList ToList()
    {
        switch (Count)
        {
            case 0:
                return default;

            case 1:
                if (_nodes[0]!.IsToken)
                    return new SyntaxNodeOrTokenList(SyntaxListInternal.List(_nodes[0]!).CreateRed(), 0);
                return new SyntaxNodeOrTokenList(_nodes[0]!.CreateRed(), 0);

            case 2:
                return new SyntaxNodeOrTokenList(SyntaxListInternal.List(_nodes[0]!, _nodes[1]!).CreateRed(), 0);

            case 3:
                return new SyntaxNodeOrTokenList(SyntaxListInternal.List(_nodes[0]!, _nodes[1]!, _nodes[2]!).CreateRed(), 0);

            default:
                return new SyntaxNodeOrTokenList(SyntaxListInternal.List(_nodes.Select(w => w!).ToArray()).CreateRed(), 0);
        }
    }
}