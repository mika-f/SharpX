// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core.Syntax.InternalSyntax;

namespace SharpX.Core.Syntax;

/// <summary>
///     represents <see cref="Microsoft.CodeAnalysis.Syntax.SyntaxTokenListBuilder" />
/// </summary>
public class SyntaxTokenListBuilder
{
    private int _count;
    private GreenNode?[] _nodes;

    public SyntaxTokenListBuilder(int size)
    {
        _nodes = new GreenNode[size];
        _count = 0;
    }

    public static SyntaxTokenListBuilder Create()
    {
        return new SyntaxTokenListBuilder(8);
    }

    public void Add(SyntaxToken item)
    {
        Contract.AssertNotNull(item.Node);
        Add(item.Node);
    }

    public void Add(GreenNode item)
    {
        CheckSpace(1);
        _nodes[_count++] = item;
    }

    public void Add(SyntaxTokenList items)
    {
        Add(items, 0, items.Count);
    }

    public void Add(SyntaxTokenList items, int offset, int length)
    {
        CheckSpace(length);
        items.CopyTo(offset, _nodes, _count, length);
        _count += length;
    }

    public void Add(SyntaxToken[] items)
    {
        Add(items, 0, items.Length);
    }

    public void Add(SyntaxToken[] items, int offset, int length)
    {
        CheckSpace(length);

        for (var i = 0; i < length; i++)
            _nodes[_count + i] = items[offset + i].Node;

        _count += length;
    }

    private void CheckSpace(int delta)
    {
        if (_count + delta > _nodes.Length)
            Array.Resize(ref _nodes, _count + delta);
    }

    public SyntaxTokenList ToList()
    {
        if (_count == 0)
            return default;

        switch (_count)
        {
            case 1:
                return new SyntaxTokenList(null, _nodes[0], 0, 0);

            case 2:
                return new SyntaxTokenList(null, SyntaxListInternal.List(_nodes[0]!, _nodes[1]!), 0, 0);

            case 3:
                return new SyntaxTokenList(null, SyntaxListInternal.List(_nodes[0]!, _nodes[1]!, _nodes[2]!), 0, 0);

            default:
                return new SyntaxTokenList(null, SyntaxListInternal.List(_nodes!, _count), 0, 0);
        }
    }

    public static implicit operator SyntaxTokenList(SyntaxTokenListBuilder builder)
    {
        return builder.ToList();
    }
}