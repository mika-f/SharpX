// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;

namespace SharpX.Hlsl.Syntax.InternalSyntax;

internal abstract class SimpleNameSyntaxInternal : NameSyntaxInternal
{
    public abstract SyntaxTokenInternal Identifier { get; }

    protected SimpleNameSyntaxInternal(SyntaxKind kind) : base(kind) { }

    protected SimpleNameSyntaxInternal(SyntaxKind kind, DiagnosticInfo[]? diagnostics) : base(kind, diagnostics) { }
}