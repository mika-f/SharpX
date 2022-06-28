// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;

namespace SharpX.Hlsl.Syntax;

public abstract class DirectiveTriviaSyntax : StructuredTriviaSyntax
{
    public abstract SyntaxToken HashToken { get; }

    public abstract SyntaxToken EndOfDirectiveToken { get; }

    protected DirectiveTriviaSyntax(GreenNode node, SyntaxNode? parent, int position) : base(node, parent, position) { }
}