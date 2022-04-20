// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using System.Collections;

namespace SharpX.Core;

public readonly partial struct SyntaxTriviaList : IReadOnlyList<SyntaxTrivia>
{
    public static SyntaxTriviaList Empty => default;

    public SyntaxTriviaList(SyntaxTrivia trivia)
    {
        Token = default;
        Node = trivia.UnderlyingNode;
        Position = 0;
        Index = 0;
    }

    public SyntaxTriviaList(SyntaxToken token, GreenNode? node)
    {
        Token = token;
        Node = node;
        Position = token.Position;
        Index = 0;
    }

    public SyntaxTriviaList(SyntaxToken token, GreenNode? node, int position, int index = 0)
    {
        Token = token;
        Node = node;
        Position = position;
        Index = index;
    }

    public SyntaxTriviaList(params SyntaxTrivia[] trivias) : this(default, CreateNode(trivias), 0, 0) { }

    private static GreenNode? CreateNode(SyntaxTrivia[]? trivias)
    {
        if (trivias == null)
            return null;

        return default;
    }

    public SyntaxToken Token { get; }

    public GreenNode? Node { get; }

    public int Position { get; }

    public int Index { get; }


    public static GreenNode? GetGreenNodeAt(GreenNode node, int i)
    {
        Contract.Assert(node.IsList || i == 0 && !node.IsList, null);
        return node.IsList ? node.GetSlot(i) : node;
    }

    public IEnumerator<SyntaxTrivia> GetEnumerator()
    {
        return new Enumerator(this);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public int Count => Node == null ? 0 : Node.IsList ? Node.SlotCount : 1;

    public SyntaxTrivia this[int index]
    {
        get
        {
            if (Node != null)
            {
                if (Node.IsList)
                {
                    if (index < Node.SlotCount)
                        return new SyntaxTrivia(Token, Node.GetSlot(index), Position + Node.GetSlotOffset(index), Index + index);
                }
                else if (index == 0)
                {
                    return new SyntaxTrivia(Token, Node, Position, Index);
                }
            }

            throw new ArgumentOutOfRangeException();
        }
    }
}