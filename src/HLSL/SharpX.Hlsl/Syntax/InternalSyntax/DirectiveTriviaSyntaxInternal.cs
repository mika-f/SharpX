// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;

namespace SharpX.Hlsl.Syntax.InternalSyntax;

internal abstract class DirectiveTriviaSyntaxInternal : StructuredTriviaSyntaxInternal
{
    public abstract SyntaxTokenInternal HashToken { get; }

    public abstract SyntaxTokenInternal EndOfDirectiveToken { get; }

    protected DirectiveTriviaSyntaxInternal(SyntaxKind kind) : base(kind)
    {
        ContainsDirectives = true;
    }

    protected DirectiveTriviaSyntaxInternal(SyntaxKind kind, DiagnosticInfo[]? diagnostics) : base(kind, diagnostics)
    {
        ContainsDirectives = true;
    }
}