// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.Hlsl.Syntax.InternalSyntax;

namespace SharpX.Hlsl.Syntax;

public abstract class SimpleNameSyntax : NameSyntax
{
    public abstract SyntaxToken Identifier { get; }

    internal SimpleNameSyntax(HlslSyntaxNodeInternal node, SyntaxNode? parent, int position) : base(node, parent, position) { }

    public SimpleNameSyntax WithIdentifier(SyntaxToken identifier)
    {
        return WithIdentifierInternal(identifier);
    }

    protected abstract SimpleNameSyntax WithIdentifierInternal(SyntaxToken identifier);
}