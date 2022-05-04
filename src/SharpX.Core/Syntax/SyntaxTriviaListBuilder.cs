// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core.Syntax.InternalSyntax;

namespace SharpX.Core.Syntax;

/// <summary>
///     represents <see cref="Microsoft.CodeAnalysis.Syntax.SyntaxTriviaListBuilder" />
/// </summary>
public class SyntaxTriviaListBuilder
{
    private SyntaxTrivia[] _nodes;

    public int Count { get; private set; }

    public SyntaxTrivia this[int index]
    {
        get
        {
            if (index < 0 || index >= Count)
                throw new IndexOutOfRangeException();

            return _nodes[index];
        }
    }

    public SyntaxTriviaListBuilder(int size)
    {
        _nodes = new SyntaxTrivia[size];
    }

    public static SyntaxTriviaListBuilder Create()
    {
        return new SyntaxTriviaListBuilder(4);
    }

    public static SyntaxTriviaList Create(IEnumerable<SyntaxTrivia>? trivia)
    {
        if (trivia == null)
            return new SyntaxTriviaList();

        var builder = Create();
        builder.AddRange(trivia);

        return builder.ToList();
    }

    public void Clear()
    {
        Count = 0;
    }

    public void AddRange(IEnumerable<SyntaxTrivia>? items)
    {
        if (items == null)
            return;

        foreach (var item in items)
            Add(item);
    }

    public SyntaxTriviaListBuilder Add(SyntaxTrivia item)
    {
        if (Count >= _nodes.Length)
            Array.Resize(ref _nodes, Count == 0 ? 8 : _nodes.Length * 2);

        _nodes[Count++] = item;
        return this;
    }

    public void AddRange(SyntaxTrivia[] items)
    {
        AddRange(items, 0, items.Length);
    }

    public void AddRange(SyntaxTrivia[] items, int offset, int length)
    {
        if (Count + length >= _nodes.Length)
            Array.Resize(ref _nodes, Count + length);

        Array.Copy(items, offset, _nodes, Count, length);
        Count += length;
    }

    public void Add(SyntaxTriviaList list)
    {
        Add(list, 0, list.Count);
    }

    public void Add(SyntaxTriviaList list, int offset, int length)
    {
        if (Count + length >= _nodes.Length)
            Array.Resize(ref _nodes, Count + length);

        list.CopyTo(offset, _nodes, Count, length);
        Count += length;
    }

    public static implicit operator SyntaxTriviaList(SyntaxTriviaListBuilder builder)
    {
        return builder.ToList();
    }

    public SyntaxTriviaList ToList()
    {
        if (Count == 0)
            return default;

        return Count switch
        {
            1 => new SyntaxTriviaList(default, _nodes[0].UnderlyingNode, 0),
            2 => new SyntaxTriviaList(default, SyntaxListInternal.List(_nodes[0].UnderlyingNode!, _nodes[1].UnderlyingNode!), 0),
            3 => new SyntaxTriviaList(default, SyntaxListInternal.List(_nodes[0].UnderlyingNode!, _nodes[1].UnderlyingNode!, _nodes[2].UnderlyingNode!), 0),
            _ => new SyntaxTriviaList(default, SyntaxListInternal.List(_nodes.Select(w => w.UnderlyingNode!).ToArray()), 0)
        };
    }
}