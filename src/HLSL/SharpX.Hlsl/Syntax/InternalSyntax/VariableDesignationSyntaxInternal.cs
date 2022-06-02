// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;

namespace SharpX.Hlsl.Syntax.InternalSyntax;

internal abstract class VariableDesignationSyntaxInternal : HlslSyntaxNodeInternal
{
    protected VariableDesignationSyntaxInternal(SyntaxKind kind) : base(kind) { }

    protected VariableDesignationSyntaxInternal(SyntaxKind kind, DiagnosticInfo[]? diagnostics) : base(kind, diagnostics) { }
}