// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.Core.Syntax.InternalSyntax;

namespace SharpX.Hlsl.Syntax.InternalSyntax;

internal abstract class StatementSyntaxInternal : HlslSyntaxNodeInternal
{
    public abstract SyntaxListInternal<AttributeListSyntaxInternal> AttributeLists { get; }

    protected StatementSyntaxInternal(SyntaxKind kind) : base(kind) { }

    protected StatementSyntaxInternal(SyntaxKind kind, DiagnosticInfo[]? diagnostics) : base(kind, diagnostics) { }
}