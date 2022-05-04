// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using Microsoft.CodeAnalysis;

using SharpX.Core;

using SyntaxNode = SharpX.Core.SyntaxNode;

namespace SharpX.Hlsl.Syntax.InternalSyntax;

internal class SyntaxTokenInternal : HlslSyntaxNodeInternal
{
    public const SyntaxKind FirstTokenWithWellKnownText = SyntaxKind.TildeToken;

    public const SyntaxKind LastTokenWithWellKnownText = SyntaxKind.EndOfFileToken;

    public virtual string Text => SyntaxFacts.GetText(Kind);

    public virtual object Value
    {
        get
        {
            switch (Kind)
            {
                case SyntaxKind.TrueKeyword:
                    return true;

                case SyntaxKind.FalseKeyword:
                    return false;

                case SyntaxKind.NullKeyword:
                    return null;

                default:
                    return Text;
            }
        }
    }

    public virtual string ValueText => Text;

    public override int Width => Text.Length;

    public virtual SyntaxKind ContextualKind => Kind;

    public override int RawContextualKind => (int)ContextualKind;

    public override bool IsToken => true;


    public SyntaxTokenInternal(SyntaxKind kind) : base(kind)
    {
        FullWidth = Text.Length;
    }

    public SyntaxTokenInternal(SyntaxKind kind, int fullWidth) : base(kind, fullWidth) { }

    public SyntaxTokenInternal(SyntaxKind kind, int fullWidth, DiagnosticInfo[]? diagnostics, SyntaxAnnotation[]? annotations) : base(kind, fullWidth, diagnostics, annotations) { }

    public SyntaxTokenInternal(SyntaxKind kind, DiagnosticInfo[]? diagnostics, SyntaxAnnotation[]? annotations) : base(kind, diagnostics, annotations)
    {
        FullWidth = Text.Length;
    }

    public override GreenNode SetAnnotations(SyntaxAnnotation[]? annotations)
    {
        return new SyntaxTokenInternal(Kind, FullWidth, GetDiagnostics(), annotations);
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new SyntaxTokenInternal(Kind, FullWidth, diagnostics, GetAnnotations());
    }

    public override GreenNode? GetSlot(int index)
    {
        throw Exceptions.Unreachable;
    }

    public override SyntaxNode CreateRed(SyntaxNode? parent, int position)
    {
        throw Exceptions.Unreachable;
    }

    public override object? GetValue()
    {
        return Value;
    }

    public override string GetValueText()
    {
        return Text;
    }

    public override int GetLeadingTriviaWidth()
    {
        var trivia = GetLeadingTrivia();
        return trivia?.FullWidth ?? 0;
    }

    public override int GetTrailingTriviaWidth()
    {
        var trivia = GetTrailingTrivia();
        return trivia?.FullWidth ?? 0;
    }

    protected override void WriteTokenTo(TextWriter writer, bool leading, bool trailing)
    {
        if (leading)
        {
            var trivia = GetLeadingTrivia();
            trivia?.WriteTo(writer);
        }

        writer.Write(Text);

        if (trailing)
        {
            var trivia = GetTrailingTrivia();
            trivia?.WriteTo(writer);
        }
    }

    public override GreenNode WithTrailingTrivia(GreenNode? trivia)
    {
        return TokenWitTrailingTrivia(trivia);
    }

    public override GreenNode WithLeadingTrivia(GreenNode? trivia)
    {
        return TokenWithLeadingTrivia(trivia);
    }

    public virtual SyntaxTokenInternal TokenWitTrailingTrivia(GreenNode? trivia)
    {
        return new SyntaxTokenWithTriviaInternal(Kind, null, trivia);
    }


    public virtual SyntaxTokenInternal TokenWithLeadingTrivia(GreenNode? trivia)
    {
        return new SyntaxTokenWithTriviaInternal(Kind, trivia, null);
    }


    #region Factories

    public static SyntaxTokenInternal Identifier(string text)
    {
        return new SyntaxIdentifierInternal(text);
    }

    public static SyntaxTokenInternal Identifier(GreenNode? leading, string text, GreenNode? trailing)
    {
        if (leading == null && trailing == null)
            return Identifier(text);
        return new SyntaxIdentifierWithTriviaInternal(SyntaxKind.IdentifierToken, text, text, leading, trailing);
    }

    public static SyntaxTokenInternal Identifier(SyntaxKind kind, GreenNode? leading, string text, string valueText, GreenNode? trailing)
    {
        if (kind == SyntaxKind.IdentifierToken && text == valueText)
            return Identifier(leading, text, trailing);
        return new SyntaxIdentifierWithTriviaInternal(kind, text, valueText, leading, trailing);
    }

    public static SyntaxTokenInternal WithValue<T>(SyntaxKind kind, string text, T value)
    {
        return new SyntaxTokenWithValueInternal<T>(kind, text, value);
    }

    public static SyntaxTokenInternal WithValue<T>(SyntaxKind kind, GreenNode? leading, string text, T value, GreenNode? trailing)
    {
        return new SyntaxTokenWithValueAndTriviaInternal<T>(kind, text, value, leading, trailing);
    }

    public static SyntaxTokenInternal StringLiteral(string text)
    {
        return new SyntaxTokenWithValueInternal<string>(SyntaxKind.StringLiteralToken, text, text);
    }

    public static SyntaxTokenInternal StringLiteral(GreenNode? leading, string text, GreenNode? trailing)
    {
        return new SyntaxTokenWithValueAndTriviaInternal<string>(SyntaxKind.StringLiteralToken, text, text, leading, trailing);
    }

    #endregion

    public override TResult? Accept<TResult>(HlslSyntaxVisitorInternal<TResult> visitor) where TResult : default
    {
        return visitor.VisitToken(this);
    }
}