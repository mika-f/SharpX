// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;

namespace SharpX.Hlsl.Syntax.InternalSyntax;

internal abstract class ConditionalDirectiveTriviaSyntaxInternal : BranchingDirectiveTriviaSyntaxInternal
{
    public abstract ExpressionSyntaxInternal Condition { get; }

    protected ConditionalDirectiveTriviaSyntaxInternal(SyntaxKind kind) : base(kind) { }

    protected ConditionalDirectiveTriviaSyntaxInternal(SyntaxKind kind, DiagnosticInfo[]? diagnostics) : base(kind, diagnostics) { }
}