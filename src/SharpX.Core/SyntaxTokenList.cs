// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using System.Collections;

using SharpX.Core.Syntax;

namespace SharpX.Core;

/// <summary>
///     represents <see cref="Microsoft.CodeAnalysis.SyntaxTokenList" />
/// </summary>
public readonly partial struct SyntaxTokenList : IEquatable<SyntaxTokenList>, IReadOnlyList<SyntaxToken>
{
    private readonly SyntaxNode? _parent;
    private readonly int _index;

    public SyntaxTokenList(SyntaxToken token)
    {
        _parent = token.Parent;
        Node = token.Node;
        Position = token.Position;
        _index = 0;
    }

    public SyntaxTokenList(SyntaxNode? parent, GreenNode? tokenOrList, int position, int index)
    {
        _parent = parent;
        Node = tokenOrList;
        Position = position;
        _index = index;
    }

    public SyntaxTokenList(params SyntaxToken[] tokens) : this(null, CreateNode(tokens), 0, 0) { }

    private static GreenNode? CreateNode(SyntaxToken[]? tokens)
    {
        if (tokens == null)
            return null;

        var builder = new SyntaxTokenListBuilder(tokens.Length);
        foreach (var t in tokens)
        {
            var node = t.Node;
            Contract.AssertNotNull(node);

            builder.Add(node);
        }

        return builder.ToList().Node;
    }

    public GreenNode? Node { get; }

    public int Position { get; }

    public int Count => Node == null ? 0 : Node.IsList ? Node.SlotCount : 1;

    internal void CopyTo(int offset, GreenNode?[] array, int arrayOffset, int count)
    {
        for (var i = 0; i < count; i++)
            array[arrayOffset + i] = GetGreenNodeAt(offset + i);
    }

    public SyntaxTokenList Add(SyntaxToken token)
    {
        return Insert(Count, token);
    }

    public SyntaxTokenList AddRange(IEnumerable<SyntaxToken> tokens)
    {
        return InsertRange(Count, tokens);
    }

    public SyntaxTokenList Insert(int index, SyntaxToken token)
    {
        return InsertRange(index, new[] { token });
    }

    public SyntaxTokenList InsertRange(int index, IEnumerable<SyntaxToken> tokens)
    {
        if (index < 0 || index > Count)
            throw new ArgumentOutOfRangeException(nameof(index));

        var items = tokens.ToList();
        if (items.Count == 0)
            return this;

        var list = this.ToList();
        list.InsertRange(index, items);

        if (list.Count == 0)
            return this;

        return new SyntaxTokenList(null, GreenNode.CreateList(list, static w => w.RequiredNode), 0, 0);
    }

    public IEnumerator<SyntaxToken> GetEnumerator()
    {
        return new Enumerator(this);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public SyntaxToken this[int index]
    {
        get
        {
            if (Node != null)
            {
                if (Node.IsList)
                {
                    if (index < Node.SlotCount)
                        return new SyntaxToken(_parent, Node.GetSlot(index), Position + Node.GetSlotOffset(index), _index + index);
                }
                else if (index == 0)
                {
                    return new SyntaxToken(_parent, Node, Position, index);
                }
            }

            throw new ArgumentOutOfRangeException(nameof(index));
        }
    }

    public override string ToString()
    {
        return Node?.ToString() ?? string.Empty;
    }

    public string ToFullString()
    {
        return Node?.ToFullString() ?? string.Empty;
    }

    private GreenNode? GetGreenNodeAt(int i)
    {
        Contract.AssertNotNull(Node);
        return GetGreenNodeAt(Node, i);
    }

    private static GreenNode? GetGreenNodeAt(GreenNode node, int index)
    {
        Contract.Assert(node.IsList || (index == 0 && !node.IsList), null);
        return node.IsList ? node.GetSlot(index) : node;
    }

    public static bool operator ==(SyntaxTokenList left, SyntaxTokenList right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(SyntaxTokenList left, SyntaxTokenList right)
    {
        return !left.Equals(right);
    }

    public bool Equals(SyntaxTokenList other)
    {
        return Node == other.Node && _parent == other._parent && _index == other._index;
    }

    public override bool Equals(object? obj)
    {
        return obj is SyntaxTokenList other && Equals(other);
    }

    public override int GetHashCode()
    {
#if NET5_0_OR_GREATER
        return HashCode.Combine(_parent, _index, Node, Position);
#else
        unchecked
        {
            var hashCode = _parent != null ? _parent.GetHashCode() : 0;
            hashCode = (hashCode * 397) ^ _index;
            hashCode = (hashCode * 397) ^ (Node != null ? Node.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ Position;
            return hashCode;
        }
#endif
    }
}