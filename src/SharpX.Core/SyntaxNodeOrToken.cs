// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

namespace SharpX.Core;

/// <summary>
///     represents <see cref="Microsoft.CodeAnalysis.SyntaxNodeOrToken" />
/// </summary>
public readonly struct SyntaxNodeOrToken : IEquatable<SyntaxNodeOrToken>
{
    private readonly SyntaxNode? _nodeOrParent;

    private readonly GreenNode? _token;

    private readonly int _tokenIndex;

    public SyntaxNodeOrToken(SyntaxNode node) : this()
    {
        _nodeOrParent = node;
        _tokenIndex = -1;
        Position = node.Position;
    }

    public SyntaxNodeOrToken(SyntaxNode? parent, GreenNode? token, int position, int index)
    {
        _tokenIndex = index;
        Position = position;
        _nodeOrParent = parent;
        _token = token;
    }

    public int RawKind => _token?.RawKind ?? _nodeOrParent?.RawKind ?? 0;

    public string Language => _token?.Language ?? _nodeOrParent?.Language ?? string.Empty;

    public SyntaxNode? Parent => _token != null ? _nodeOrParent : _nodeOrParent?.Parent;

    public GreenNode? UnderlyingNode => _token ?? _nodeOrParent?.Green;

    public GreenNode RequiredUnderlyingNode
    {
        get
        {
            Contract.AssertNotNull(UnderlyingNode);
            return UnderlyingNode;
        }
    }

    public int Position { get; }

    public bool IsToken => !IsNode;

    public bool IsNode => _tokenIndex < 0;

    public SyntaxToken AsToken()
    {
        return _token != null ? new SyntaxToken(_nodeOrParent, _token, Position, _tokenIndex) : default;
    }

    public SyntaxNode? AsNode()
    {
        return _token != null ? null : _nodeOrParent;
    }

    public override string ToString()
    {
        if (_token != null)
            return _token.ToString();
        if (_nodeOrParent != null)
            return _nodeOrParent.ToString();

        return string.Empty;
    }

    public string ToFullString()
    {
        if (_token != null)
            return _token.ToFullString();
        if (_nodeOrParent != null)
            return _nodeOrParent.ToFullString();

        return string.Empty;
    }

    public void WriteTo(TextWriter writer)
    {
        if (_token != null)
            _token.WriteTo(writer);
        else if (_nodeOrParent != null)
            _nodeOrParent.WriteTo(writer);
    }

    public static implicit operator SyntaxNodeOrToken(SyntaxToken token)
    {
        return new SyntaxNodeOrToken(token.Parent, token.Node, token.Position, token.Index);
    }

    public static explicit operator SyntaxToken(SyntaxNodeOrToken token)
    {
        return token.AsToken();
    }

    public static implicit operator SyntaxNodeOrToken(SyntaxNode? node)
    {
        return node != null ? new SyntaxNodeOrToken(node) : default;
    }

    public static explicit operator SyntaxNode?(SyntaxNodeOrToken token)
    {
        return token.AsNode();
    }

    public static bool operator ==(SyntaxNodeOrToken left, SyntaxNodeOrToken right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(SyntaxNodeOrToken left, SyntaxNodeOrToken right)
    {
        return !left.Equals(right);
    }

    public bool Equals(SyntaxNodeOrToken other)
    {
        return Equals(_nodeOrParent, other._nodeOrParent) && Equals(_token, other._token) && _tokenIndex == other._tokenIndex && Position == other.Position;
    }

    public override bool Equals(object? obj)
    {
        return obj is SyntaxNodeOrToken other && Equals(other);
    }

    public override int GetHashCode()
    {
#if NET5_0_OR_GREATER
        return HashCode.Combine(_nodeOrParent, _token, _tokenIndex, Position);
#else
        unchecked
        {
            var hashCode = _nodeOrParent != null ? _nodeOrParent.GetHashCode() : 0;
            hashCode = (hashCode * 397) ^ (_token != null ? _token.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ _tokenIndex;
            hashCode = (hashCode * 397) ^ Position;
            return hashCode;
        }
#endif
    }
}