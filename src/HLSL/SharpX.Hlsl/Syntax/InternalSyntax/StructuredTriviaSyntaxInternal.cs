// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using Microsoft.CodeAnalysis;

using SharpX.Core;

namespace SharpX.Hlsl.Syntax.InternalSyntax;

internal abstract class StructuredTriviaSyntaxInternal : HlslSyntaxNodeInternal
{
    protected StructuredTriviaSyntaxInternal(SyntaxKind kind, DiagnosticInfo[]? diagnostics = null, SyntaxAnnotation[]? annotations = null) : base(kind, diagnostics, annotations)
    {
        ContainsStructuredTrivia = true;
    }
}