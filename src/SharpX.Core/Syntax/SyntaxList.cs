// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

namespace SharpX.Core.Syntax;

/// <summary>
///     represents <see cref="Microsoft.CodeAnalysis.Syntax.SyntaxList" />
/// </summary>
public abstract partial class SyntaxList : SyntaxNode
{
    public override string Language => throw Exceptions.Unreachable;

    public SyntaxList(GreenNode node, SyntaxNode? parent, int position) : base(node, parent, position) { }

    protected internal override SyntaxNode NormalizeWhitespaceCore(string indentation, string eol, bool elasticTrivia)
    {
        throw Exceptions.Unreachable;
    }
}