// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using System.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpX.Core;

using SyntaxNode = SharpX.Core.SyntaxNode;
using SyntaxToken = SharpX.Core.SyntaxToken;

namespace SharpX.Hlsl.Syntax.InternalSyntax;

[DebuggerDisplay("{GetDebuggerDisplay(), nq}")]
internal abstract class HlslSyntaxNodeInternal : GreenNode
{
    public override string Language => "HLSL";

    public SyntaxKind Kind => (SyntaxKind)RawKind;

    public override string KindText => Kind.ToString();

    protected HlslSyntaxNodeInternal(SyntaxKind kind) : base((int)kind) { }

    protected HlslSyntaxNodeInternal(SyntaxKind kind, int fullWidth) : base((int)kind, fullWidth) { }

    protected HlslSyntaxNodeInternal(SyntaxKind kind, int fullWidth, DiagnosticInfo[]? diagnostics) : base((int)kind, fullWidth, diagnostics) { }

    protected HlslSyntaxNodeInternal(SyntaxKind kind, int fullWidth, DiagnosticInfo[]? diagnostics, SyntaxAnnotation[]? annotations) : base((int)kind, fullWidth, diagnostics, annotations) { }

    protected HlslSyntaxNodeInternal(SyntaxKind kind, DiagnosticInfo[]? diagnostics) : base((int)kind, diagnostics) { }

    protected HlslSyntaxNodeInternal(SyntaxKind kind, DiagnosticInfo[]? diagnostics, SyntaxAnnotation[]? annotations) : base((int)kind, diagnostics, annotations) { }

    public override SyntaxToken CreateSeparator<TNode>(SyntaxNode element)
    {
        return Hlsl.SyntaxFactory.Token(SyntaxKind.CommaToken);
    }

    public override bool IsTriviaWithEndOfLine()
    {
        return Kind is SyntaxKind.EndOfLineTrivia or SyntaxKind.SingleLineCommentTrivia;
    }
}