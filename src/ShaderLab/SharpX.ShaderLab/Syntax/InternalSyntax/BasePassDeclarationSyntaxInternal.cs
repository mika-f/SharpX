// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using Microsoft.CodeAnalysis;

using SharpX.Core;

namespace SharpX.ShaderLab.Syntax.InternalSyntax;

internal abstract class BasePassDeclarationSyntaxInternal : ShaderLabSyntaxNodeInternal
{
    public abstract SyntaxTokenInternal Keyword { get; }

    protected BasePassDeclarationSyntaxInternal(SyntaxKind kind) : base(kind) { }

    protected BasePassDeclarationSyntaxInternal(SyntaxKind kind, DiagnosticInfo[]? diagnostics, SyntaxAnnotation[]? annotations) : base(kind, diagnostics, annotations) { }
}