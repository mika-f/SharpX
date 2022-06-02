// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;

namespace SharpX.ShaderLab.Syntax.InternalSyntax;

internal abstract class TypeSyntaxInternal : ShaderLabSyntaxNodeInternal
{
    protected TypeSyntaxInternal(SyntaxKind kind) : base(kind) { }
    protected TypeSyntaxInternal(SyntaxKind kind, DiagnosticInfo[]? diagnostics) : base(kind, diagnostics) { }
}