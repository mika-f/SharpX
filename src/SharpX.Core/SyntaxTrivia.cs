// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using System.Diagnostics.CodeAnalysis;

using Microsoft.CodeAnalysis.Text;

namespace SharpX.Core;

/// <summary>
///     represents <see cref="Microsoft.CodeAnalysis.SyntaxTrivia" />
/// </summary>
public readonly struct SyntaxTrivia : IEquatable<SyntaxTrivia>
{
    public SyntaxTrivia(SyntaxToken token, GreenNode? trivia, int position, int index)
    {
        Token = token;
        UnderlyingNode = trivia;
        Position = position;
        Index = index;
    }

    public int RawKind => UnderlyingNode?.RawKind ?? 0;

    public string Language => UnderlyingNode?.Language ?? string.Empty;

    public SyntaxToken Token { get; }

    public GreenNode? UnderlyingNode { get; }

    public GreenNode RequiredUnderlyingNode
    {
        get
        {
            Contract.AssertNotNull(UnderlyingNode);
            return UnderlyingNode;
        }
    }

    public int Position { get; }

    public int Index { get; }

    public int Width => UnderlyingNode?.Width ?? 0;

    public int FullWidth => UnderlyingNode?.FullWidth ?? 0;

    public TextSpan FullSpan => UnderlyingNode != null ? new TextSpan(Position, UnderlyingNode.FullWidth) : default;

    public bool ContainsDiagnostics => UnderlyingNode?.ContainsDiagnostics ?? false;

    public bool HasStructure => UnderlyingNode?.IsStructuredTrivia ?? false;

    public bool IsDirective => UnderlyingNode?.IsDirective ?? false;

    public SyntaxNode? GetStructure()
    {
        return HasStructure ? UnderlyingNode!.GetStructure(this) : null;
    }


    public bool TryGetStructure([NotNullWhen(true)] out SyntaxNode? structure)
    {
        structure = GetStructure();
        return structure != null;
    }

    public override string ToString()
    {
        return UnderlyingNode?.ToFullString() ?? string.Empty;
    }

    public string ToFullString()
    {
        return UnderlyingNode?.ToFullString() ?? string.Empty;
    }

    public void WriteTo(TextWriter writer)
    {
        UnderlyingNode?.WriteTo(writer);
    }

    public static bool operator ==(SyntaxTrivia left, SyntaxTrivia right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(SyntaxTrivia left, SyntaxTrivia right)
    {
        return !left.Equals(right);
    }

    public bool Equals(SyntaxTrivia other)
    {
        return Token == other.Token && UnderlyingNode == other.UnderlyingNode && Position == other.Position && Index == other.Index;
    }

    public override bool Equals(object? obj)
    {
        return obj is SyntaxTrivia other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Token, UnderlyingNode, Position, Index);
    }
}