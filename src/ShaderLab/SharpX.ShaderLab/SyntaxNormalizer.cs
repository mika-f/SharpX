// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using System.Text;

using SharpX.Core;
using SharpX.ShaderLab.Syntax;

namespace SharpX.ShaderLab;

internal class SyntaxNormalizer : ShaderLabSyntaxRewriter
{
    private readonly TextSpan _consideredSpan;
    private readonly SyntaxTrivia _eolTrivia;
    private readonly string _indentWhitespace;
    private readonly int _initialDepth;
    private readonly bool _useElasticTrivia;

    private bool _afterIndentation;
    private bool _afterLineBreak;
    private bool _isInStructuredTrivia;

    private SyntaxToken _previousToken;

    private SyntaxNormalizer(TextSpan consideredSpan, int initialDepth, string indentWhitespace, string eolWhitespace, bool useElasticTrivia)
    {
        _consideredSpan = consideredSpan;
        _initialDepth = initialDepth;
        _indentWhitespace = indentWhitespace;
        _useElasticTrivia = useElasticTrivia;
        _eolTrivia = useElasticTrivia ? SyntaxFactory.ElasticEndOfLine(eolWhitespace) : SyntaxFactory.EndOfLine(eolWhitespace);
        _afterLineBreak = true;
        _afterIndentation = false;
    }

    internal static TNode Normalize<TNode>(TNode node, string indentWhitespace, string eolWhitespace, bool useElasticTrivia = false) where TNode : ShaderLabSyntaxNode
    {
        var normalizer = new SyntaxNormalizer(node.FullSpan, GetDeclarationDepth(node), indentWhitespace, eolWhitespace, useElasticTrivia);
        return (TNode)normalizer.Visit(node)!;
    }

    public override SyntaxToken VisitToken(SyntaxToken token)
    {
        if (token.RawKind == (int)SyntaxKind.None || token.FullWidth == 0)
            return token;

        try
        {
            var t = token;
            var depth = GetDeclarationDepth(t);

            t = t.WithLeadingTrivia(RewriteTrivia(token.LeadingTrivia, depth, false, NeedsIndentAfterLineBreak(token), false, 0));

            var nextToken = GetNextRelevantToken(token);

            _afterLineBreak = false;
            _afterIndentation = false;


            t = t.WithTrailingTrivia(RewriteTrivia(token.TrailingTrivia, depth, true, false, NeedsSeparator(token, nextToken), LineBreaksAfter(token, nextToken)));

            return t;
        }
        finally
        {
            _previousToken = token;
        }
    }

    private SyntaxToken GetNextRelevantToken(SyntaxToken token)
    {
        // maybe buggy
        var nextToken = token.GetNextToken(t => SyntaxToken.NonZeroWidth(t), _ => false);
        if (_consideredSpan.Contains(nextToken.FullSpan))
            return nextToken;
        return default;
    }

    private SyntaxTrivia GetIndentation(int count)
    {
        count = Math.Max(count - _initialDepth, 0);

        // should I use caches?
        var sb = new StringBuilder();
        for (var i = 0; i < count; i++)
            sb.Append(_indentWhitespace);

        return _useElasticTrivia ? SyntaxFactory.ElasticWhitespace(sb.ToString()) : SyntaxFactory.Whitespace(sb.ToString());
    }

    private SyntaxTriviaList RewriteTrivia(SyntaxTriviaList triviaList, int depth, bool isTrailing, bool indentAfterLineBreak, bool mustHaveSeparator, int lineBreaksAfter)
    {
        var currentTriviaList = new List<SyntaxTrivia>();

        foreach (var trivia in triviaList)
        {
            if (trivia.RawKind == (int)SyntaxKind.WhitespaceTrivia || trivia.RawKind == (int)SyntaxKind.EndOfLineTrivia || trivia.FullWidth == 0)
                continue;

            var needsSeparator = (currentTriviaList.Any() && NeedsSeparatorBetween(currentTriviaList.Last())) || (currentTriviaList.Count == 0 && isTrailing);
            var needsLineBreak = NeedsLineBreakBefore(trivia, isTrailing) || (currentTriviaList.Any() && NeedsLineBreakBetween(currentTriviaList.Last(), trivia, isTrailing));

            if (needsLineBreak && !_afterLineBreak)
            {
                currentTriviaList.Add(GetEndOfLine());
                _afterLineBreak = true;
                _afterIndentation = false;
            }

            if (_afterLineBreak)
            {
                if (!_afterIndentation && NeedsIndentAfterLineBreak(trivia))
                {
                    currentTriviaList.Add(GetIndentation(GetDeclarationDepth(trivia)));
                    _afterIndentation = true;
                }
            }
            else if (needsSeparator)
            {
                currentTriviaList.Add(GetSpace());
                _afterLineBreak = false;
                _afterIndentation = false;
            }

            if (trivia.HasStructure)
            {
                var tr = VisitStructuredTrivia(trivia);
                currentTriviaList.Add(tr);
            }
            else
            {
                currentTriviaList.Add(trivia);
            }

            if (NeedsLineBreakAfter(trivia, isTrailing) && (currentTriviaList.Any() || !EndsInLineBreak(currentTriviaList.Last())))
            {
                currentTriviaList.Add(GetEndOfLine());
                _afterLineBreak = true;
                _afterIndentation = false;
            }
        }

        if (lineBreaksAfter > 0)
        {
            if (currentTriviaList.Any() && EndsInLineBreak(currentTriviaList.Last()))
                lineBreaksAfter--;

            for (var i = 0; i < lineBreaksAfter; i++)
            {
                currentTriviaList.Add(GetEndOfLine());
                _afterLineBreak = true;
                _afterIndentation = false;
            }
        }
        else if (indentAfterLineBreak && _afterLineBreak && !_afterIndentation)
        {
            currentTriviaList.Add(GetIndentation(depth));
            _afterIndentation = true;
        }
        else if (mustHaveSeparator)
        {
            currentTriviaList.Add(GetSpace());
            _afterLineBreak = false;
            _afterIndentation = false;
        }

        if (currentTriviaList.Count == 0)
            return default;
        if (currentTriviaList.Count == 1)
            return SyntaxFactory.TriviaList(currentTriviaList.First());
        return SyntaxFactory.TriviaList(currentTriviaList.ToArray());
    }

    private SyntaxTrivia GetSpace()
    {
        return _useElasticTrivia ? SyntaxFactory.ElasticSpace : SyntaxFactory.Space;
    }

    private SyntaxTrivia GetEndOfLine()
    {
        return _eolTrivia;
    }

    private SyntaxTrivia VisitStructuredTrivia(SyntaxTrivia trivia)
    {
        var oldIsInStructuredTrivia = _isInStructuredTrivia;
        _isInStructuredTrivia = true;

        var oldPreviousToken = _previousToken;

        var result = VisitTrivia(trivia);

        _isInStructuredTrivia = oldIsInStructuredTrivia;
        _previousToken = oldPreviousToken;

        return result;
    }

    #region Checking

    private static bool IsKeyword(SyntaxKind kind)
    {
        return SyntaxFacts.IsKeywordKind(kind);
    }

    private static bool IsWord(SyntaxKind kind)
    {
        return kind == SyntaxKind.IdentifierToken || IsKeyword(kind);
    }

    private static bool EndsInLineBreak(SyntaxTrivia trivia)
    {
        var kind = (SyntaxKind)trivia.RawKind;
        if (kind == SyntaxKind.EndOfLineTrivia)
            return true;

        if (trivia.HasStructure)
        {
            var node = trivia.GetStructure();
            var trailing = node!.GetTrailingTrivia();
            if (trailing.Count > 0)
                return EndsInLineBreak(trailing.Last());
            return false;
        }

        return false;
    }

    private static bool TokenCharacterCanBeDoubled(char c)
    {
        switch (c)
        {
            case '+':
            case '-':
            case '<':
            case '=':
                return true;
            default:
                return false;
        }
    }

    #endregion

    #region Indent

    private static bool NeedsIndentAfterLineBreak(SyntaxTrivia trivia)
    {
        var kind = (SyntaxKind)trivia.RawKind;
        switch (kind)
        {
            default:
                return false;
        }
    }

    private static bool NeedsIndentAfterLineBreak(SyntaxToken token)
    {
        return token.RawKind != (int)SyntaxKind.EndOfFileToken;
    }

    #endregion

    #region Separators

    private static bool NeedsSeparatorBetween(SyntaxTrivia trivia)
    {
        switch ((SyntaxKind)trivia.RawKind)
        {
            case SyntaxKind.None:
            case SyntaxKind.WhitespaceTrivia:
                return false;

            default:
                return true;
        }
    }

    private static bool NeedsSeparator(SyntaxToken currentToken, SyntaxToken nextToken)
    {
        if (currentToken.Parent == null || nextToken.Parent == null)
            return false;

        var currentKind = (SyntaxKind)currentToken.RawKind;
        var nextKind = (SyntaxKind)nextToken.RawKind;

        // ,[ ]
        if (currentKind == SyntaxKind.CommaToken && nextKind != SyntaxKind.CommaToken)
            return true;

        // ][ ]...
        if (currentKind == SyntaxKind.CloseBracketToken && IsWord(nextKind))
            return true;

        // =[ ]
        if (currentKind == SyntaxKind.EqualsToken)
            return true;

        // [ ]=
        if (nextKind == SyntaxKind.EqualsToken)
            return true;

        // Am I checking next tokens?
        if (IsKeyword(currentKind))
            return true;

        // (some-word)[ ](some-word)
        if (IsWord(currentKind) && IsWord(nextKind))
            return true;

        if (currentKind == SyntaxKind.IdentifierToken && nextToken.Parent is null or PropertyDeclarationSyntax)
            return true;

        if (currentToken.Width > 1 && nextToken.Width > 1)
        {
            var tokenLastChar = currentToken.Text.Last();
            var nextFirstChar = nextToken.Text.First();
            if (tokenLastChar == nextFirstChar && TokenCharacterCanBeDoubled(tokenLastChar))
                return true;
        }

        return false;
    }

    private static bool AssignmentTokenNeedsSeparator(SyntaxKind kind)
    {
        return false;
    }

    private static bool BinaryTokenNeedsSeparator(SyntaxKind kind)
    {
        return kind switch
        {
            SyntaxKind.DotToken => false,
            _ => false
        };
    }

    #endregion

    #region LineBreaks

    private int LineBreaksAfter(SyntaxToken currentToken, SyntaxToken nextToken)
    {
        if (currentToken.RawKind == (int)SyntaxKind.None)
            return 0;

        if (_isInStructuredTrivia)
            return 0;

        var currentKind = (SyntaxKind)currentToken.RawKind;
        var nextKind = (SyntaxKind)nextToken.RawKind;

        switch (currentKind)
        {
            case SyntaxKind.None:
                return 0;

            case SyntaxKind.OpenBraceToken:
                return LineBreaksAfterOpenBrace(currentToken, nextToken);

            case SyntaxKind.CloseBraceToken:
                return LineBreaksAfterCloseBrace(currentToken, nextToken);

            case SyntaxKind.CloseParenToken:
            {
                return 0;
            }

            case SyntaxKind.CloseBracketToken:
            {
                if (currentToken.Parent is AttributeListSyntax)
                    return 1;
                break;
            }

            case SyntaxKind.CommaToken:
                return 0;

            case SyntaxKind.CgProgramKeyword:
                return 1;

            case SyntaxKind.CgIncludeKeyword:
                return 1;

            case SyntaxKind.EndCgKeyword:
                return 2;

            case SyntaxKind.IdentifierToken:
                if (currentToken.Parent?.Parent is CommandDeclarationSyntax command && command.Arguments.Last() == currentToken.Parent)
                    return 1;
                break;

            case SyntaxKind.StringLiteralToken:
                if (currentToken.Parent is TagDeclarationSyntax tag && tag.Value == currentToken)
                    return 1;
                break;
        }

        switch (nextKind)
        {
            case SyntaxKind.OpenBraceToken:
                return LineBreaksBeforeOpenBrace(nextToken);

            case SyntaxKind.CloseBraceToken:
                return LineBreaksBeforeCloseBrace(nextToken);

            case SyntaxKind.OpenBracketToken:
                return nextToken.Parent is AttributeListSyntax ? 1 : 0;
        }

        if (currentToken.Parent is ExpressionSyntax && currentToken.Parent.Parent is EqualsValueClauseSyntax)
            return 1;

        return 0;
    }

    // ReSharper disable once UnusedParameter.Local
    private static int LineBreaksBeforeOpenBrace(SyntaxToken nextToken)
    {
        return 1;
    }

    // ReSharper disable once UnusedParameter.Local
    private static int LineBreaksAfterOpenBrace(SyntaxToken currentToken, SyntaxToken nextToken)
    {
        if (currentToken.Parent is TextureLiteralExpressionSyntax)
            return 0;
        return 1;
    }

    private static int LineBreaksBeforeCloseBrace(SyntaxToken nextToken)
    {
        return 1;
    }

    private static int LineBreaksAfterCloseBrace(SyntaxToken currentToken, SyntaxToken nextToken)
    {
        var kind = (SyntaxKind)nextToken.RawKind;
        switch (kind)
        {
            case SyntaxKind.EndOfFileToken:
            case SyntaxKind.CloseBraceToken:
                return 1;

            default:
                return currentToken.Parent is not TextureLiteralExpressionSyntax ? 2 : 1;
        }
    }

    // ReSharper disable once UnusedParameter.Local
    private static int LineBreaksAfterSemicolon(SyntaxToken currentToken, SyntaxToken nextToken)
    {
        return 1;
    }

    private static bool NeedsLineBreakBetween(SyntaxTrivia trivia, SyntaxTrivia next, bool isTrailingTrivia)
    {
        return NeedsLineBreakAfter(trivia, isTrailingTrivia) || NeedsLineBreakBefore(next, isTrailingTrivia);
    }

    // ReSharper disable once UnusedParameter.Local
    private static bool NeedsLineBreakBefore(SyntaxTrivia trivia, bool isTrailingTrivia)
    {
        var kind = (SyntaxKind)trivia.RawKind;
        return false;
    }

    // ReSharper disable once UnusedParameter.Local
    private static bool NeedsLineBreakAfter(SyntaxTrivia trivia, bool isTrailingTrivia)
    {
        var kind = (SyntaxKind)trivia.RawKind;
        switch (kind)
        {
            default:
                return false;
        }
    }

    #endregion

    #region Depth

    private static int GetDeclarationDepth(SyntaxToken token)
    {
        return GetDeclarationDepth(token.Parent as ShaderLabSyntaxNode);
    }

    private static int GetDeclarationDepth(SyntaxTrivia trivia)
    {
        return GetDeclarationDepth(trivia.Token);
    }

    private static int GetDeclarationDepth(ShaderLabSyntaxNode? node)
    {
        if (node == null)
            return 0;

        if (node.Parent != null)
        {
            if (node.Parent.Kind == SyntaxKind.CompilationUnit)
                return 0;

            var parentDepth = GetDeclarationDepth(node.Parent);
            if (node.Parent is ShaderDeclarationSyntax or SubShaderDeclarationSyntax or TagsDeclarationSyntax or BasePassDeclarationSyntax or StencilDeclarationSyntax or PropertiesDeclarationSyntax)
                return parentDepth + 1;
            return parentDepth;
        }

        return 0;
    }

    #endregion
}