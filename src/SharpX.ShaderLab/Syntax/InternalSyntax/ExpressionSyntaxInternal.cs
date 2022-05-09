﻿// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using Microsoft.CodeAnalysis;

using SharpX.Core;

namespace SharpX.ShaderLab.Syntax.InternalSyntax;

internal abstract class ExpressionSyntaxInternal : ShaderLabSyntaxNodeInternal
{
    protected ExpressionSyntaxInternal(SyntaxKind kind) : base(kind) { }

    protected ExpressionSyntaxInternal(SyntaxKind kind, DiagnosticInfo[]? diagnostics, SyntaxAnnotation[]? annotations) : base(kind, diagnostics, annotations) { }
}