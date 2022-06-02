// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;

namespace SharpX.Hlsl.Syntax.InternalSyntax;

internal abstract class BaseTypeDeclarationSyntaxInternal : MemberDeclarationSyntaxInternal
{
    public abstract SyntaxTokenInternal Identifier { get; }

    public abstract SyntaxTokenInternal OpenBraceToken { get; }

    public abstract SyntaxTokenInternal CloseBraceToken { get; }

    protected BaseTypeDeclarationSyntaxInternal(SyntaxKind kind) : base(kind) { }

    protected BaseTypeDeclarationSyntaxInternal(SyntaxKind kind, DiagnosticInfo[]? diagnostics) : base(kind, diagnostics) { }
}