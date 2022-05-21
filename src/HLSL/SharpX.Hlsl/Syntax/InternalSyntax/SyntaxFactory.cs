// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using System.Globalization;

using Microsoft.CodeAnalysis;

using SharpX.Core;
using SharpX.Core.Extensions;
using SharpX.Core.Syntax.InternalSyntax;

namespace SharpX.Hlsl.Syntax.InternalSyntax;

internal static partial class SyntaxFactory
{
    private const string CrLf = "\r\n";

    internal static readonly SyntaxTriviaInternal CarriageReturnLineFeed = EndOfLine(CrLf);
    internal static readonly SyntaxTriviaInternal LineFeed = EndOfLine("\n");
    internal static readonly SyntaxTriviaInternal CarriageReturn = EndOfLine("\r");
    internal static readonly SyntaxTriviaInternal Space = Whitespace(" ");
    internal static readonly SyntaxTriviaInternal Tab = Whitespace("\t");

    internal static readonly SyntaxTriviaInternal ElasticCarriageReturnLineFeed = EndOfLine(CrLf, true);
    internal static readonly SyntaxTriviaInternal ElasticLineFeed = EndOfLine("\n", true);
    internal static readonly SyntaxTriviaInternal ElasticCarriageReturn = EndOfLine("\r", true);
    internal static readonly SyntaxTriviaInternal ElasticSpace = Whitespace(" ", true);
    internal static readonly SyntaxTriviaInternal ElasticTab = Whitespace("\t", true);

    internal static readonly SyntaxTriviaInternal ElasticZeroSpace = Whitespace(string.Empty, true);


    public static SyntaxTriviaInternal EndOfLine(string text, bool elastic = false)
    {
        var trivia = text switch
        {
            "\r" => elastic ? ElasticCarriageReturn : CarriageReturn,
            "\n" => elastic ? ElasticLineFeed : LineFeed,
            "\r\n" => elastic ? ElasticCarriageReturnLineFeed : CarriageReturnLineFeed,
            _ => null
        };

        if (trivia != null)
            return trivia;

        trivia = new SyntaxTriviaInternal(SyntaxKind.EndOfLineTrivia, text);
        if (!elastic)
            return trivia;

        return trivia.WithAnnotationsGreen(new[] { SyntaxAnnotation.ElasticAnnotation });
    }

    public static SyntaxTriviaInternal Whitespace(string text, bool elastic = false)
    {
        var trivia = new SyntaxTriviaInternal(SyntaxKind.WhitespaceTrivia, text);
        if (!elastic)
            return trivia;

        return trivia.WithAnnotationsGreen(new[] { SyntaxAnnotation.ElasticAnnotation });
    }

    public static SyntaxTriviaInternal Comment(string text)
    {
        if (text.StartsWith("/*"))
            return new SyntaxTriviaInternal(SyntaxKind.MultilineCommentTrivia, text);
        return new SyntaxTriviaInternal(SyntaxKind.SingleLineCommentTrivia, text);
    }

    public static SyntaxTokenInternal Token(SyntaxKind kind)
    {
        return new SyntaxTokenInternal(kind);
    }

    public static SyntaxTokenInternal Token(GreenNode? leading, SyntaxKind kind, GreenNode? trailing)
    {
        return new SyntaxTokenWithTriviaInternal(kind, leading, trailing);
    }

    public static SyntaxTokenInternal Token(GreenNode? leading, SyntaxKind kind, string text, string valueText, GreenNode? trailing)
    {
        var @default = SyntaxFacts.GetText(kind);
        if (kind is >= SyntaxTokenInternal.FirstTokenWithWellKnownText and <= SyntaxTokenInternal.LastTokenWithWellKnownText && text == @default && valueText == @default)
            return Token(leading, kind, trailing);
        return SyntaxTokenInternal.WithValue(kind, leading, text, valueText, trailing);
    }


    public static SyntaxTokenInternal Identifier(string text)
    {
        return Identifier(SyntaxKind.IdentifierToken, null, text, text, null);
    }

    public static SyntaxTokenInternal Identifier(GreenNode? leading, string text, GreenNode? trailing)
    {
        return Identifier(SyntaxKind.IdentifierToken, leading, text, text, trailing);
    }

    public static SyntaxTokenInternal Identifier(SyntaxKind kind, GreenNode? leading, string text, string valueText, GreenNode? trailing)
    {
        return SyntaxTokenInternal.Identifier(kind, leading, text, valueText, trailing);
    }

    public static SyntaxTokenInternal Literal(GreenNode? leading, int value, GreenNode? trailing)
    {
        return Literal(leading, Convert.ToString(value), value, trailing);
    }

    public static SyntaxTokenInternal Literal(GreenNode? leading, string text, int value, GreenNode? trailing)
    {
        return SyntaxTokenInternal.WithValue(SyntaxKind.NumericLiteralToken, leading, text, value, trailing);
    }

    public static SyntaxTokenInternal Literal(GreenNode? leading, uint value, GreenNode? trailing)
    {
        return Literal(leading, Convert.ToString(value), value, trailing);
    }

    public static SyntaxTokenInternal Literal(GreenNode? leading, string text, uint value, GreenNode? trailing)
    {
        return SyntaxTokenInternal.WithValue(SyntaxKind.NumericLiteralToken, leading, text, value, trailing);
    }

    public static SyntaxTokenInternal Literal(GreenNode? leading, float value, GreenNode? trailing)
    {
        return Literal(leading, Convert.ToString(value, CultureInfo.InvariantCulture), value, trailing);
    }

    public static SyntaxTokenInternal Literal(GreenNode? leading, string text, float value, GreenNode? trailing)
    {
        return SyntaxTokenInternal.WithValue(SyntaxKind.NumericLiteralToken, leading, text, value, trailing);
    }

    public static SyntaxTokenInternal Literal(GreenNode? leading, double value, GreenNode? trailing)
    {
        return Literal(leading, Convert.ToString(value, CultureInfo.InvariantCulture), value, trailing);
    }

    public static SyntaxTokenInternal Literal(GreenNode? leading, string text, double value, GreenNode? trailing)
    {
        return SyntaxTokenInternal.WithValue(SyntaxKind.NumericLiteralToken, leading, text, value, trailing);
    }

    public static SyntaxTokenInternal Literal(GreenNode? leading, char value, GreenNode? trailing)
    {
        return Literal(leading, Convert.ToString(value), value, trailing);
    }

    public static SyntaxTokenInternal Literal(GreenNode? leading, string text, char value, GreenNode? trailing)
    {
        return SyntaxTokenInternal.WithValue(SyntaxKind.CharacterLiteralToken, leading, text, value, trailing);
    }

    public static SyntaxTokenInternal Literal(GreenNode? leading, string value, GreenNode? trailing)
    {
        return Literal(leading, value, value, trailing);
    }

    public static SyntaxTokenInternal Literal(GreenNode? leading, string text, string value, GreenNode? trailing)
    {
        return SyntaxTokenInternal.WithValue(SyntaxKind.StringLiteralToken, leading, text, value, trailing);
    }

    public static SyntaxListInternal<TNode> List<TNode>() where TNode : HlslSyntaxNodeInternal
    {
        return default;
    }

    public static SyntaxListInternal<TNode> List<TNode>(TNode node1, TNode node2) where TNode : HlslSyntaxNodeInternal
    {
        return new SyntaxListInternal<TNode>(SyntaxListInternal.List(node1, node2));
    }

    public static SyntaxListInternal<TNode> List<TNode>(params TNode[]? nodes) where TNode : HlslSyntaxNodeInternal
    {
        if (nodes == null)
            return default;
        return new SyntaxListInternal<TNode>(SyntaxListInternal.List(nodes.Cast<GreenNode>().ToArray()));
    }

    public static SeparatedSyntaxListInternal<TNode> SeparatedList<TNode>(TNode node) where TNode : HlslSyntaxNodeInternal
    {
        return new SeparatedSyntaxListInternal<TNode>(new SyntaxListInternal<HlslSyntaxNodeInternal>(node));
    }

    public static SeparatedSyntaxListInternal<TNode> SeparatedList<TNode>(SyntaxTokenInternal token) where TNode : HlslSyntaxNodeInternal
    {
        return new SeparatedSyntaxListInternal<TNode>(new SyntaxListInternal<HlslSyntaxNodeInternal>(token));
    }

    public static SeparatedSyntaxListInternal<TNode> SeparatedList<TNode>(TNode node1, SyntaxTokenInternal token, TNode node2) where TNode : HlslSyntaxNodeInternal
    {
        return new SeparatedSyntaxListInternal<TNode>(SyntaxListInternal.List(node1, token, node2));
    }

    public static SeparatedSyntaxListInternal<TNode> SeparatedList<TNode>(params TNode[]? nodes) where TNode : HlslSyntaxNodeInternal
    {
        if (nodes == null)
            return default;
        return new SeparatedSyntaxListInternal<TNode>(SyntaxListInternal.List(nodes.Cast<GreenNode>().ToArray()));
    }
}