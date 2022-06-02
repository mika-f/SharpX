// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.Core.Syntax.InternalSyntax;

namespace SharpX.Hlsl.Syntax.InternalSyntax;

internal abstract class BaseArgumentListSyntaxInternal : HlslSyntaxNodeInternal
{
    public abstract SeparatedSyntaxListInternal<ArgumentSyntaxInternal> Arguments { get; }

    protected BaseArgumentListSyntaxInternal(SyntaxKind kind) : base(kind) { }

    protected BaseArgumentListSyntaxInternal(SyntaxKind kind, DiagnosticInfo[]? diagnostics) : base(kind, diagnostics) { }
}