// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;

namespace SharpX.ShaderLab.Syntax.InternalSyntax;

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

    public SyntaxIdentifierWithTriviaInternal(SyntaxKind kind, string text, string value, GreenNode? leading, GreenNode? trailing, DiagnosticInfo[]? diagnostics) : base(kind, text, value, diagnostics)
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

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new SyntaxIdentifierWithTriviaInternal(Kind, Text, ValueText, _leading, _trailing, diagnostics);
    }

    public override SyntaxTokenInternal TokenWithLeadingTrivia(GreenNode? trivia)
    {
        return new SyntaxIdentifierWithTriviaInternal(Kind, Text, ValueText, trivia, _trailing, GetDiagnostics());
    }

    public override SyntaxTokenInternal TokenWitTrailingTrivia(GreenNode? trivia)
    {
        return new SyntaxIdentifierWithTriviaInternal(Kind, Text, ValueText, _leading, trivia, GetDiagnostics());
    }
}