// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;

namespace SharpX.Hlsl.Syntax.InternalSyntax;

internal abstract class BranchingDirectiveTriviaSyntaxInternal : DirectiveTriviaSyntaxInternal
{
    protected BranchingDirectiveTriviaSyntaxInternal(SyntaxKind kind) : base(kind) { }

    protected BranchingDirectiveTriviaSyntaxInternal(SyntaxKind kind, DiagnosticInfo[]? diagnostics) : base(kind, diagnostics) { }
}