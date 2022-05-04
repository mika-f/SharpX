// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using System.Collections;

using SharpX.Core.Syntax;
using SharpX.Core.Syntax.InternalSyntax;

namespace SharpX.Core;

/// <summary>
///     represents <see cref="Microsoft.CodeAnalysis.SyntaxNodeOrTokenList" />
/// </summary>
public readonly partial struct SyntaxNodeOrTokenList : IEquatable<SyntaxNodeOrTokenList>, IReadOnlyCollection<SyntaxNodeOrToken>
{
    internal readonly int Index;

    public SyntaxNodeOrTokenList(SyntaxNode? node, int index) : this()
    {
        if (node != null)
        {
            Node = node;
            Index = index;
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
                        return node.IsToken
                            ? new SyntaxToken(Parent, node, Node.GetChildPosition(index), Index + index)
                            : Node.GetRequiredNodeSlot(index);
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

    internal void CopyTo(int offset, GreenNode?[] array, int arrayOffset, int count)
    {
        for (var i = 0; i < count; i++)
            array[arrayOffset + i] = this[i + offset].UnderlyingNode;
    }

    public SyntaxNodeOrTokenList Add(SyntaxNodeOrToken node)
    {
        return Insert(Count, node);
    }

    public SyntaxNodeOrTokenList AddRange(IEnumerable<SyntaxNodeOrToken> nodes)
    {
        return InsertRange(Count, nodes);
    }

    public SyntaxNodeOrTokenList Insert(int index, SyntaxNodeOrToken node)
    {
        if (node == default)
            throw new ArgumentOutOfRangeException();
        return InsertRange(index, new List<SyntaxNodeOrToken> { node });
    }

    public SyntaxNodeOrTokenList InsertRange(int index, IEnumerable<SyntaxNodeOrToken> nodes)
    {
        if (index < 0 || index > Count)
            throw new ArgumentOutOfRangeException();

        var syntaxNodeOrTokens = nodes as SyntaxNodeOrToken[] ?? nodes.ToArray();
        if (!syntaxNodeOrTokens.Any())
            return this;

        var items = this.ToList();
        items.InsertRange(index, syntaxNodeOrTokens);
        return CreateList(items);
    }

    private static SyntaxNodeOrTokenList CreateList(List<SyntaxNodeOrToken> items)
    {
        if (items.Count == 0)
            return default;

        var green = GreenNode.CreateList(items, static w => w.RequiredUnderlyingNode)!;
        if (green.IsToken) green = SyntaxListInternal.List(new[] { green });

        return new SyntaxNodeOrTokenList(green.CreateRed(), 0);
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

    public static bool operator ==(SyntaxNodeOrTokenList left, SyntaxNodeOrTokenList right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(SyntaxNodeOrTokenList left, SyntaxNodeOrTokenList right)
    {
        return left.Equals(right);
    }

    public bool Equals(SyntaxNodeOrTokenList other)
    {
        return Node == other.Node;
    }

    public override bool Equals(object? obj)
    {
        return obj is SyntaxNodeOrTokenList other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Index, Node);
    }
}