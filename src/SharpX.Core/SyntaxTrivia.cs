// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

namespace SharpX.Core;

/// <summary>
///     represents <see cref="Microsoft.CodeAnalysis.SyntaxTrivia" />
/// </summary>
public readonly struct SyntaxTrivia
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

    public bool ContainsDiagnostics => UnderlyingNode?.ContainsDiagnostics ?? false;

    public bool IsDirective => UnderlyingNode?.IsDirective ?? false;

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
}