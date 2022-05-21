// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using Microsoft.CodeAnalysis;

using SharpX.Core;

namespace SharpX.ShaderLab.Syntax.InternalSyntax;

internal abstract class BaseCommandDeclarationSyntaxInternal : ShaderLabSyntaxNodeInternal
{
    public abstract SyntaxTokenInternal Keyword { get; }

    protected BaseCommandDeclarationSyntaxInternal(SyntaxKind kind) : base(kind) { }

    protected BaseCommandDeclarationSyntaxInternal(SyntaxKind kind, DiagnosticInfo[]? diagnostics, SyntaxAnnotation[]? annotations) : base(kind, diagnostics, annotations) { }
}