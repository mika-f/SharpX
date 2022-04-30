// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using Microsoft.CodeAnalysis;

using SharpX.Core;

namespace SharpX.Hlsl.Syntax.InternalSyntax;

internal class SyntaxIdentifierWithTriviaInternal : SyntaxIdentifierExtendedInternal
{
    private readonly GreenNode? _leading;
    private readonly GreenNode? _trailing;

    public SyntaxIdentifierWithTriviaInternal(SyntaxKind kind, string text, string value, GreenNode? leading, GreenNode? trailing) : base(kind, text, value)
    {
        if (leading != null)
        {
            AdjustWidth(leading);
            _leading = leading;
        }

        if (trailing != null)
        {
            AdjustWidth(trailing);
            _trailing = trailing;
        }
    }

    public SyntaxIdentifierWithTriviaInternal(SyntaxKind kind, string text, string value, GreenNode? leading, GreenNode? trailing, DiagnosticInfo[]? diagnostics, SyntaxAnnotation[]? annotations) : base(kind, text, value, diagnostics, annotations)
    {
        if (leading != null)
        {
            AdjustWidth(leading);
            _leading = leading;
        }

        if (trailing != null)
        {
            AdjustWidth(trailing);
            _trailing = trailing;
        }
    }

    public override GreenNode? GetLeadingTrivia()
    {
        return _leading;
    }

    public override GreenNode? GetTrailingTrivia()
    {
        return _trailing;
    }

    public override GreenNode SetAnnotations(SyntaxAnnotation[]? annotations)
    {
        return new SyntaxIdentifierWithTriviaInternal(Kind, Text, ValueText, _leading, _trailing, GetDiagnostics(), annotations);
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new SyntaxIdentifierWithTriviaInternal(Kind, Text, ValueText, _leading, _trailing, diagnostics, GetAnnotations());
    }

    public override SyntaxTokenInternal TokenWithLeadingTrivia(GreenNode? trivia)
    {
        return new SyntaxIdentifierWithTriviaInternal(Kind, Text, ValueText, trivia, _trailing, GetDiagnostics(), GetAnnotations());
    }

    public override SyntaxTokenInternal TokenWitTrailingTrivia(GreenNode? trivia)
    {
        return new SyntaxIdentifierWithTriviaInternal(Kind, Text, ValueText, _leading, trivia, GetDiagnostics(), GetAnnotations());
    }
}