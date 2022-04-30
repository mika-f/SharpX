// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using Microsoft.CodeAnalysis;

using SharpX.Core;

using SyntaxNode = SharpX.Core.SyntaxNode;
using SyntaxTrivia = SharpX.Core.SyntaxTrivia;

namespace SharpX.Hlsl.Syntax.InternalSyntax;

internal class SyntaxTriviaInternal : HlslSyntaxNodeInternal
{
    private readonly string _text;

    public override bool IsTrivia => true;

    public SyntaxTriviaInternal(SyntaxKind kind, string text, DiagnosticInfo[]? diagnostics = null, SyntaxAnnotation[]? annotations = null) : base(kind, text.Length, diagnostics, annotations)
    {
        _text = text;
    }

    public override GreenNode SetAnnotations(SyntaxAnnotation[]? annotations)
    {
        return new SyntaxTriviaInternal(Kind, _text, GetDiagnostics(), annotations);
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new SyntaxTriviaInternal(Kind, _text, diagnostics, GetAnnotations());
    }

    public override GreenNode? GetSlot(int index)
    {
        throw Exceptions.Unreachable;
    }

    public override SyntaxNode CreateRed(SyntaxNode? parent, int position)
    {
        throw Exceptions.Unreachable;
    }

    public override string ToFullString()
    {
        return _text;
    }

    public override string ToString()
    {
        return _text;
    }

    protected override void WriteTriviaTo(TextWriter writer)
    {
        writer.Write(_text);
    }

    public static implicit operator SyntaxTrivia(SyntaxTriviaInternal trivia)
    {
        return new SyntaxTrivia(default, trivia, 0, 0);
    }
}