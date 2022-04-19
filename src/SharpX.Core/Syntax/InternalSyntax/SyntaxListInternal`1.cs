// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

namespace SharpX.Core.Syntax.InternalSyntax;

public readonly struct SyntaxListInternal<TNode> where TNode : GreenNode
{
    public SyntaxListInternal(GreenNode? node)
    {
        Node = node;
    }

    public GreenNode? Node { get; }

    public int Count => Node == null ? 0 : Node.IsList ? Node.SlotCount : 1;

    public TNode? this[int index]
    {
        get
        {
            if (Node == null)
                return null;

            if (Node.IsList)
                return (TNode?)Node.GetSlot(index);

            if (index == 0)
                return (TNode?)Node;

            throw new NotImplementedException();
        }
    }

    public TNode GetRequiredItem(int index)
    {
        var node = this[index];
        Contract.AssertNotNull(node);

        return node;
    }

    public static implicit operator SyntaxListInternal<TNode>(TNode node)
    {
        return new SyntaxListInternal<TNode>(node);
    }

    public static implicit operator SyntaxListInternal<TNode>(SyntaxListInternal<GreenNode> nodes)
    {
        return new SyntaxListInternal<TNode>(nodes.Node);
    }

    public static implicit operator SyntaxListInternal<GreenNode>(SyntaxListInternal<TNode> nodes)
    {
        return new SyntaxListInternal<GreenNode>(nodes.Node);
    }
}