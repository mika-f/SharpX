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
public readonly partial struct SeparatedSyntaxList<TNode> : IReadOnlyList<TNode> where TNode : SyntaxNode
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
}