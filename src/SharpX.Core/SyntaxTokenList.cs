// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using System.Collections;

namespace SharpX.Core;

public readonly partial struct SyntaxTokenList : IReadOnlyList<SyntaxToken>
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

        throw new NotImplementedException();
    }

    public GreenNode? Node { get; }

    public int Position { get; }

    public int Count => Node == null ? 0 : Node.IsList ? Node.SlotCount : 1;

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

    private static GreenNode? GetGreenNodeAt(GreenNode node, int index)
    {
        Contract.Assert(node.IsList || index == 0 && !node.IsList, null);
        return node.IsList ? node.GetSlot(index) : node;
    }
}