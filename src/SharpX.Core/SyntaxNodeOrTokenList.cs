// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using System.Collections;

using SharpX.Core.Syntax;

namespace SharpX.Core;

/// <summary>
///     represents <see cref="Microsoft.CodeAnalysis.SyntaxNodeOrTokenList" />
/// </summary>
public readonly partial struct SyntaxNodeOrTokenList : IReadOnlyCollection<SyntaxNodeOrToken>
{
    private readonly int _index;

    public SyntaxNodeOrTokenList(SyntaxNode? node, int index) : this()
    {
        if (node != null)
        {
            Node = node;
            _index = index;
        }
    }

    public SyntaxNodeOrTokenList(IEnumerable<SyntaxNodeOrToken> nodes) : this(CreateNode(nodes), 0) { }

    public SyntaxNodeOrTokenList(params SyntaxNodeOrToken[] nodes) : this(CreateNode(nodes), 0) { }


    private static SyntaxNode? CreateNode(IEnumerable<SyntaxNodeOrToken> items)
    {
        var builder = new SyntaxNodeOrTokenListBuilder(8);
        builder.AddRange(items.ToList());

        return builder.ToList().Node;
    }

    public SyntaxNode? Node { get; }

    public int Position => Node?.Position ?? 0;

    public SyntaxNode? Parent => Node?.Parent;

    public int Count => Node == null ? 0 : Node.IsList ? Node.SlotCount : 1;

    public SyntaxNodeOrToken this[int index]
    {
        get
        {
            if (Node != null)
            {
                if (Node.IsList)
                {
                    if (index < Node.SlotCount)
                    {
                        var node = Node.Green.GetRequiredSlot(index);
                        return node.IsToken ? new SyntaxToken(Parent, node, Node.GetChildPosition(index), _index + index) : Node.GetRequiredNodeSlot(index);
                    }
                }
                else if (index == 0)
                {
                    return Node;
                }
            }

            throw new ArgumentOutOfRangeException(nameof(index));
        }
    }

    internal void CopyTo(int offset, GreenNode?[] array, int arrayOffset, int count)
    {
        for (var i = 0; i < count; i++)
            array[arrayOffset + i] = this[i + offset].UnderlyingNode;
    }

    public override string ToString()
    {
        return Node?.ToString() ?? string.Empty;
    }

    public string ToFullString()
    {
        return Node?.ToFullString() ?? string.Empty;
    }

    public IEnumerator<SyntaxNodeOrToken> GetEnumerator()
    {
        return new Enumerator(this);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return Node != null ? new List<SyntaxNodeOrToken>.Enumerator() : GetEnumerator();
    }
}