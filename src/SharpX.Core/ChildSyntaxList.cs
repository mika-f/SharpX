// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using System.Collections;

namespace SharpX.Core;

/// <summary>
///     represents <see cref="Microsoft.CodeAnalysis.ChildSyntaxList" />
/// </summary>
public readonly partial struct ChildSyntaxList : IEquatable<ChildSyntaxList>, IReadOnlyList<SyntaxNodeOrToken>
{
    private readonly SyntaxNode? _node;

    public ChildSyntaxList(SyntaxNode node)
    {
        _node = node;
        Count = CountNodes(node.Green);
    }

    private static int CountNodes(GreenNode green)
    {
        var n = 0;

        for (int i = 0, s = green.SlotCount; i < s; i++)
        {
            var child = green.GetSlot(i);
            if (child != null)
            {
                if (child.IsList)
                    n += child.SlotCount;
                else
                    n++;
            }
        }

        return n;
    }

    public Reversed Reverse()
    {
        Contract.AssertNotNull(_node);
        return new Reversed(_node, Count);
    }

    public override bool Equals(object? obj)
    {
        return obj is ChildSyntaxList r && Equals(r);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_node, Count);
    }

    public bool Equals(ChildSyntaxList other)
    {
        return _node == other._node;
    }

    public Enumerator GetEnumerator()
    {
        Contract.AssertNotNull(_node);
        return new Enumerator(_node, Count);
    }

    IEnumerator<SyntaxNodeOrToken> IEnumerable<SyntaxNodeOrToken>.GetEnumerator()
    {
        return GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public int Count { get; }

    public SyntaxNodeOrToken this[int index]
    {
        get
        {
            if (unchecked((uint)index < (uint)Count))
                return ItemInternal(_node!, index);

            throw new ArgumentOutOfRangeException(nameof(index));
        }
    }

    private static SyntaxNodeOrToken ItemInternal(SyntaxNode node, int index)
    {
        GreenNode? greenChild;
        var green = node.Green;
        var idx = index;
        var slotIndex = 0;
        var position = node.Position;

        while (true)
        {
            greenChild = green.GetSlot(slotIndex);
            if (greenChild != null)
            {
                var currentOccupancy = Occupancy(greenChild);
                if (idx < currentOccupancy)
                    break;

                idx -= currentOccupancy;
                position += greenChild.FullWidth;
            }

            slotIndex++;
        }

        var red = node.GetNodeSlot(slotIndex);
        if (!greenChild.IsList)
        {
            if (red != null)
                return red;
        }
        else if (red != null)
        {
            var redChild = red.GetNodeSlot(idx);
            if (redChild != null)
                return redChild;

            greenChild = greenChild.GetSlot(idx);
            position = red.GetChildPosition(idx);
        }
        else
        {
            position += greenChild.GetSlotOffset(idx);
            greenChild = greenChild.GetSlot(idx);
        }

        return new SyntaxNodeOrToken(node, greenChild, position, index);
    }

    private static int Occupancy(GreenNode node)
    {
        return node.IsList ? node.SlotCount : 1;
    }
}