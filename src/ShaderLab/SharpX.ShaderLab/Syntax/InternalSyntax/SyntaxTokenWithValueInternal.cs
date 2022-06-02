// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;

namespace SharpX.ShaderLab.Syntax.InternalSyntax;

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

    public SyntaxTokenWithValueInternal(SyntaxKind kind, string text, T value, DiagnosticInfo[]? diagnostics) : base(kind, text.Length, diagnostics)
    {
        _text = text;
        RawValue = value;
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new SyntaxTokenWithValueInternal<T>(Kind, _text, RawValue, diagnostics);
    }

    public override SyntaxTokenInternal TokenWithLeadingTrivia(GreenNode? trivia)
    {
        return new SyntaxTokenWithValueAndTriviaInternal<T>(Kind, _text, RawValue, trivia, null, GetDiagnostics());
    }

    public override SyntaxTokenInternal TokenWitTrailingTrivia(GreenNode? trivia)
    {
        return new SyntaxTokenWithValueAndTriviaInternal<T>(Kind, _text, RawValue, null, trivia, GetDiagnostics());
    }
}