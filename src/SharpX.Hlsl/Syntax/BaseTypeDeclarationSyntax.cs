// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.Hlsl.Syntax.InternalSyntax;

namespace SharpX.Hlsl.Syntax;

public abstract class BaseTypeDeclarationSyntax : MemberDeclarationSyntax
{
    public abstract SyntaxToken Identifier { get; }

    public abstract SyntaxToken OpenBraceToken { get; }

    public abstract SyntaxToken CloseBraceToken { get; }

    internal BaseTypeDeclarationSyntax(HlslSyntaxNodeInternal node, SyntaxNode? parent, int position) : base(node, parent, position) { }
}