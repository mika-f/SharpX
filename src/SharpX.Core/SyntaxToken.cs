// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

namespace SharpX.Core;

/// <summary>
///     represents <see cref="Microsoft.CodeAnalysis.SyntaxToken" />
/// </summary>
public readonly struct SyntaxToken : IEquatable<SyntaxToken>
{
    public SyntaxToken(SyntaxNode? parent, GreenNode? token, int position, int index)
    {
        Parent = parent;
        Node = token;
        Index = index;
        Position = position;
    }

    public SyntaxToken(GreenNode? token) : this()
    {
        Node = token;
    }

    public int RawKind => Node?.RawKind ?? 0;

    public string Language => Node?.Language ?? string.Empty;

    public SyntaxNode? Parent { get; }

    public GreenNode? Node { get; }

    public GreenNode RequiredNode
    {
        get
        {
            var node = Node;
            Contract.AssertNotNull(node);

            return node;
        }
    }

    public int Index { get; }

    public int Position { get; }

    public int Width => Node?.Width ?? 0;

    public int FullWidth => Node?.FullWidth ?? 0;

    public SyntaxTriviaList LeadingTrivia => Node != null ? new SyntaxTriviaList(this, Node.GetLeadingTrivia(), Position) : default;

    public SyntaxTriviaList TrailingTrivia
    {
        get
        {
            if (Node == null)
                return default;

            var leading = Node.GetLeadingTrivia();
            var index = 0;
            if (leading != null)
                index = leading.IsList ? leading.SlotCount : 1;

            var trailing = Node.GetTrailingTrivia();
            var trailingPosition = Position + this.FullWidth;
            if (trailing != null)
                trailingPosition -= trailing.FullWidth;

            return new SyntaxTriviaList(this, trailing, trailingPosition, index);
        }
    }

    public static bool operator ==(SyntaxToken left, SyntaxToken right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(SyntaxToken left, SyntaxToken right)
    {
        return !left.Equals(right);
    }

    public bool Equals(SyntaxToken other)
    {
        return Parent == other.Parent && Node == other.Node && Position == other.Position && Index == other.Index;
    }

    public override bool Equals(object? obj)
    {
        return obj is SyntaxToken other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Parent, Node, Index, Position);
    }
}