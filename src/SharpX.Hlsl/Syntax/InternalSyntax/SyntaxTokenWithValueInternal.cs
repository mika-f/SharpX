// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using Microsoft.CodeAnalysis;

using SharpX.Core;

namespace SharpX.Hlsl.Syntax.InternalSyntax;

internal class SyntaxTokenWithValueInternal<T> : SyntaxTokenInternal
{
    private readonly string _text;

    protected T RawValue { get; }

    public override string Text => _text;

    public override object Value => RawValue!;

    public override string ValueText => Convert.ToString(RawValue!)!;

    public SyntaxTokenWithValueInternal(SyntaxKind kind, string text, T value) : base(kind, text.Length)
    {
        _text = text;
        RawValue = value;
    }

    public SyntaxTokenWithValueInternal(SyntaxKind kind, string text, T value, DiagnosticInfo[]? diagnostics, SyntaxAnnotation[]? annotations) : base(kind, text.Length, diagnostics, annotations)
    {
        _text = text;
        RawValue = value;
    }

    public override GreenNode SetAnnotations(SyntaxAnnotation[]? annotations)
    {
        return new SyntaxTokenWithValueInternal<T>(Kind, _text, RawValue, GetDiagnostics(), annotations);
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new SyntaxTokenWithValueInternal<T>(Kind, _text, RawValue, diagnostics, GetAnnotations());
    }
}