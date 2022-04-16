// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.Core.Syntax.InternalSyntax;

namespace SharpX.Hlsl.Syntax.InternalSyntax;

internal abstract class TypeDeclarationSyntaxInternal : BaseTypeDeclarationSyntaxInternal
{
    // struct, technique, pass
    public abstract SyntaxTokenInternal Keyword { get; }

    public abstract SyntaxListInternal<MemberDeclarationSyntaxInternal> Members { get; }

    protected TypeDeclarationSyntaxInternal(SyntaxKind kind) : base(kind) { }

    protected TypeDeclarationSyntaxInternal(SyntaxKind kind, DiagnosticInfo[]? diagnostics) : base(kind, diagnostics) { }
}