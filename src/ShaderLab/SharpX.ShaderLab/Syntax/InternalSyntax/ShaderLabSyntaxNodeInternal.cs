// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

using Microsoft.CodeAnalysis;

using SharpX.Core;

using SyntaxNode = SharpX.Core.SyntaxNode;
using SyntaxToken = SharpX.Core.SyntaxToken;
using SyntaxTrivia = SharpX.Core.SyntaxTrivia;

namespace SharpX.ShaderLab.Syntax.InternalSyntax;

[DebuggerDisplay("{GetDebuggerDisplay(), nq}")]
internal abstract class ShaderLabSyntaxNodeInternal : GreenNode
{
    public override string Language => "ShaderLab";

    public SyntaxKind Kind => (SyntaxKind)RawKind;

    public override string KindText => Kind.ToString();

    public override int RawContextualKind => RawKind;

    protected ShaderLabSyntaxNodeInternal(SyntaxKind kind) : base((int)kind) { }
    protected ShaderLabSyntaxNodeInternal(SyntaxKind kind, int fullWidth) : base((int)kind, fullWidth) { }
    protected ShaderLabSyntaxNodeInternal(SyntaxKind kind, int fullWidth, DiagnosticInfo[]? diagnostics) : base((int)kind, fullWidth, diagnostics) { }
    protected ShaderLabSyntaxNodeInternal(SyntaxKind kind, int fullWidth, DiagnosticInfo[]? diagnostics, SyntaxAnnotation[]? annotations) : base((int)kind, fullWidth, diagnostics, annotations) { }
    protected ShaderLabSyntaxNodeInternal(SyntaxKind kind, DiagnosticInfo[]? diagnostics) : base((int)kind, diagnostics) { }
    protected ShaderLabSyntaxNodeInternal(SyntaxKind kind, DiagnosticInfo[]? diagnostics, SyntaxAnnotation[]? annotations) : base((int)kind, diagnostics, annotations) { }

    public override SyntaxNode? GetStructure(SyntaxTrivia parentTrivia)
    {
        return null;
    }

    public override SyntaxToken CreateSeparator<TNode>(SyntaxNode element)
    {
        throw new NotImplementedException();
    }

    public override bool IsTriviaWithEndOfLine()
    {
        throw new NotImplementedException();
    }

    public override bool TryCreateRed(SyntaxNode? parent, int position, [NotNullWhen(true)] out SyntaxNode? node)
    {
        node = CreateRed(parent, position);
        return true;
    }
}