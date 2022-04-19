// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.Core.Syntax;
using SharpX.Hlsl.Syntax;

using SyntaxFactoryInternal = SharpX.Hlsl.Syntax.InternalSyntax.SyntaxFactory;

namespace SharpX.Hlsl;

public static partial class SyntaxFactory
{
    public static SyntaxTrivia CarriageReturnLineFeed => SyntaxFactoryInternal.CarriageReturnLineFeed;

    public static SyntaxTrivia LineFeed => SyntaxFactoryInternal.LineFeed;

    public static SyntaxTrivia CarriageReturn => SyntaxFactoryInternal.CarriageReturn;

    public static SyntaxTrivia Space => SyntaxFactoryInternal.Space;

    public static SyntaxTrivia Tab => SyntaxFactoryInternal.Tab;

    public static SyntaxTrivia EndOfLine(string text)
    {
        return SyntaxFactoryInternal.EndOfLine(text);
    }

    public static SyntaxTrivia Whitespace(string text)
    {
        return SyntaxFactoryInternal.Whitespace(text);
    }

    public static SyntaxTrivia Comment(string text)
    {
        return SyntaxFactoryInternal.Comment(text);
    }

    public static SyntaxToken Token(SyntaxKind kind)
    {
        return new SyntaxToken(SyntaxFactoryInternal.Token(null, kind, null));
    }

    public static SyntaxToken Token(SyntaxTriviaList leading, SyntaxKind kind, SyntaxTriviaList trailing)
    {
        return new SyntaxToken(SyntaxFactoryInternal.Token(leading.Node, kind, trailing.Node));
    }

    public static SyntaxToken Token(SyntaxTriviaList leading, SyntaxKind kind, string text, string value, SyntaxTriviaList trailing)
    {
        switch (kind)
        {
            case SyntaxKind.IdentifierToken:
            case SyntaxKind.CharacterLiteralToken:
            case SyntaxKind.NumericLiteralToken:
                throw new ArgumentException(nameof(kind));
        }

        return new SyntaxToken(SyntaxFactoryInternal.Token(leading.Node, kind, text, value, trailing.Node));
    }

    public static SyntaxToken Identifier(SyntaxTriviaList leading, string text, SyntaxTriviaList trailing)
    {
        return new SyntaxToken(SyntaxFactoryInternal.Identifier(leading.Node, text, trailing.Node));
    }

    public static SyntaxToken Identifier(SyntaxTriviaList leading, SyntaxKind contextualKind, string text, string valueText, SyntaxTriviaList trailing)
    {
        return new SyntaxToken(SyntaxFactoryInternal.Identifier(contextualKind, leading.Node, text, valueText, trailing.Node));
    }

    public static SyntaxToken Literal(int value)
    {
        return new SyntaxToken(SyntaxFactoryInternal.Literal(null, value, null));
    }

    public static SyntaxToken Literal(string text, int value)
    {
        return new SyntaxToken(SyntaxFactoryInternal.Literal(null, text, value, null));
    }

    public static SyntaxToken Literal(float value)
    {
        return new SyntaxToken(SyntaxFactoryInternal.Literal(null, value, null));
    }

    public static SyntaxToken Literal(string text, float value)
    {
        return new SyntaxToken(SyntaxFactoryInternal.Literal(null, text, value, null));
    }

    public static SyntaxToken Literal(double value)
    {
        return new SyntaxToken(SyntaxFactoryInternal.Literal(null, value, null));
    }

    public static SyntaxToken Literal(string text, double value)
    {
        return new SyntaxToken(SyntaxFactoryInternal.Literal(null, text, value, null));
    }

    public static SyntaxToken Literal(char value)
    {
        return new SyntaxToken(SyntaxFactoryInternal.Literal(null, value, null));
    }

    public static SyntaxToken Literal(string text, char value)
    {
        return new SyntaxToken(SyntaxFactoryInternal.Literal(null, text, value, null));
    }

    public static SyntaxToken Literal(string value)
    {
        return new SyntaxToken(SyntaxFactoryInternal.Literal(null, value, null));
    }

    public static SyntaxList<TNode> List<TNode>() where TNode : SyntaxNode
    {
        return default;
    }

    public static SyntaxList<TNode> SingletonList<TNode>(TNode node) where TNode : SyntaxNode
    {
        return new SyntaxList<TNode>(node);
    }

    public static SyntaxList<TNode> List<TNode>(params TNode[] nodes) where TNode : SyntaxNode
    {
        return new SyntaxList<TNode>(nodes.ToList());
    }

    public static SyntaxTokenList TokenList()
    {
        return default;
    }

    public static SyntaxTokenList TokenList(SyntaxToken token)
    {
        return new SyntaxTokenList(token);
    }

    public static SyntaxTokenList TokenList(params SyntaxToken[] tokens)
    {
        return new SyntaxTokenList(tokens);
    }

    public static SyntaxTriviaList TriviaList()
    {
        return default;
    }

    public static SyntaxTriviaList TriviaList(SyntaxTrivia trivia)
    {
        return new SyntaxTriviaList(trivia);
    }

    public static SyntaxTriviaList TriviaList(params SyntaxTrivia[] trivias)
    {
        return new SyntaxTriviaList(trivias);
    }

    public static SeparatedSyntaxList<TNode> SeparatedList<TNode>() where TNode : SyntaxNode
    {
        return default;
    }

    public static SeparatedSyntaxList<TNode> SingletonSeparatedList<TNode>(TNode node) where TNode : SyntaxNode
    {
        return new SeparatedSyntaxList<TNode>(new SyntaxNodeOrTokenList(node, 0));
    }

    public static SeparatedSyntaxList<TNode> SeparatedList<TNode>(IEnumerable<TNode>? nodes) where TNode : SyntaxNode
    {
        if (nodes == null)
            return default;

        var collections = nodes as ICollection<TNode>;
        if (collections is { Count: 0 })
            return default;

        using var enumerator = nodes.GetEnumerator();
        if (!enumerator.MoveNext())
            return default;

        var node = enumerator.Current;
        if (!enumerator.MoveNext())
            return SingletonSeparatedList(node);

        var builder = new SeparatedSyntaxListBuilder<TNode>(collections?.Count ?? 3);
        builder.Add(node);

        var comma = Token(SyntaxKind.CommaToken);

        do
        {
            builder.AddSeparator(comma);
            builder.Add(enumerator.Current);
        } while (enumerator.MoveNext());

        return builder.ToList();
    }

    public static SeparatedSyntaxList<TNode> SeparatedList<TNode>(IEnumerable<TNode> nodes, IEnumerable<SyntaxToken>? separators) where TNode : SyntaxNode
    {
        using var enumerator = nodes.GetEnumerator();
        var builder = SeparatedSyntaxListBuilder<TNode>.Create();

        if (separators != null)
            foreach (var separator in separators)
            {
                if (!enumerator.MoveNext())
                    throw new ArgumentException();

                builder.Add(enumerator.Current);
                builder.AddSeparator(separator);
            }

        if (enumerator.MoveNext())
        {
            builder.Add(enumerator.Current);

            if (enumerator.MoveNext())
                throw new ArgumentException();
        }

        return builder.ToList();
    }

    public static SeparatedSyntaxList<TNode> SeparatedList<TNode>(IEnumerable<SyntaxNodeOrToken> nodes) where TNode : SyntaxNode
    {
        return SeparatedList<TNode>(NodeOrTokenList(nodes));
    }

    public static SeparatedSyntaxList<TNode> SeparatedList<TNode>(SyntaxNodeOrTokenList nodes) where TNode : SyntaxNode
    {
        bool HasSeparateNodeTokenPatterns(SyntaxNodeOrTokenList list)
        {
            for (int i = 0, n = list.Count; i < n; i++)
            {
                var element = list[i];
                if (element.IsToken == ((i & 1) == 0))
                    return false;
            }

            return true;
        }

        if (!HasSeparateNodeTokenPatterns(nodes))
            throw new ArgumentException();

        if (nodes.All(w => !w.IsNode || w.AsNode() is not TNode))
            throw new ArgumentException();

        return new SeparatedSyntaxList<TNode>(nodes);
    }

    public static SyntaxNodeOrTokenList NodeOrTokenList()
    {
        return default;
    }

    public static SyntaxNodeOrTokenList NodeOrTokenList(IEnumerable<SyntaxNodeOrToken> nodes)
    {
        return new SyntaxNodeOrTokenList(nodes);
    }

    public static SyntaxNodeOrTokenList NodeOrTokenList(params SyntaxNodeOrToken[] nodes)
    {
        return new SyntaxNodeOrTokenList(nodes);
    }

    public static IdentifierNameSyntax IdentifierName(string name)
