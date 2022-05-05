// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using System.Diagnostics;
using System.Text;

using Microsoft.CodeAnalysis.Text;

using SharpX.Core;
using SharpX.Hlsl.Syntax;

namespace SharpX.Hlsl;

internal class SyntaxNormalizer : HlslSyntaxRewriter
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

    internal static TNode Normalize<TNode>(TNode node, string indentWhitespace, string eolWhitespace, bool useElasticTrivia = false) where TNode : HlslSyntaxNode
    {
        var normalizer = new SyntaxNormalizer(node.FullSpan, GetDeclarationDepth(node), indentWhitespace, eolWhitespace, useElasticTrivia);
        var result = (TNode)normalizer.Visit(node)!;

        try
        {
            return result;
        }
        finally
        {
            // for debugging
            if (Debugger.IsAttached)
            {
                var _ = result.ToFullString();
                Debugger.Break();
            }
        }
    }

    public override SyntaxToken VisitToken(SyntaxToken token)
    {
        if (token.RawKind == (int)SyntaxKind.None || token.FullWidth == 0)
            return token;

        var t = token;
        var depth = GetDeclarationDepth(t);

        t = t.WithLeadingTrivia(RewriteTrivia(token.LeadingTrivia, depth, false, NeedsIndentAfterLineBreak(token), false, 0));

        var nextToken = GetNextRelevantToken(token);

        _afterLineBreak = false;
        _afterIndentation = false;


        t = t.WithTrailingTrivia(RewriteTrivia(token.TrailingTrivia, depth, true, false, NeedsSeparator(token, nextToken), LineBreaksAfter(token, nextToken)));

        return t;
    }

    private SyntaxToken GetNextRelevantToken(SyntaxToken token)
    {
        var nextToken = token.GetNextToken(t => SyntaxToken.NonZeroWidth(t) || t.RawKind == (int)SyntaxKind.EndOfDirectiveToken, _ => false);
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
        return SyntaxFacts.IsKeywordKind(kind) || SyntaxFacts.IsPreprocessorKeyword(kind);
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
            case SyntaxKind.SingleLineCommentTrivia:
                return true;

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
                return !SyntaxFacts.IsPreprocessorDirective((SyntaxKind)trivia.RawKind);
        }
    }

    private static bool NeedsSeparator(SyntaxToken currentToken, SyntaxToken nextToken)
    {
        if (currentToken.Parent == null || nextToken.Parent == null)
        {
            if (currentToken.Parent is IdentifierNameSyntax)
                return true;
            return false;
        }

        if (nextToken.RawKind == (int)SyntaxKind.EndOfDirectiveToken)
            return IsKeyword((SyntaxKind)currentToken.RawKind) && nextToken.LeadingWidth > 0;

        var currentKind = (SyntaxKind)currentToken.RawKind;
        var nextKind = (SyntaxKind)nextToken.RawKind;

        // expressions [ ](=operator)[ ] and/or [ ](operator)[ ]
        if (currentToken.Parent is AssignmentExpressionSyntax && AssignmentTokenNeedsSeparator(currentKind))
            return true;
        if (nextToken.Parent is AssignmentExpressionSyntax && AssignmentTokenNeedsSeparator(nextKind))
            return true;
        if (currentToken.Parent is BinaryExpressionSyntax && BinaryTokenNeedsSeparator(currentKind))
            return true;
        if (nextToken.Parent is BinaryExpressionSyntax && BinaryTokenNeedsSeparator(nextKind))
            return true;

        // generics
        if (currentKind == SyntaxKind.GreaterThanToken && currentToken.Parent.RawKind == (int)SyntaxKind.TypeArgumentList)
            if (!SyntaxFacts.IsPunctuation(nextKind))
                return true;

        // ,[ ]
        if (currentKind == SyntaxKind.CommaToken && nextKind != SyntaxKind.CommaToken)
            return true;

        // ;[ ]) in for
        if (currentKind == SyntaxKind.SemicolonToken && nextKind is not SyntaxKind.SemicolonToken or SyntaxKind.CloseParenToken)
            return true;

        // ?[ ]...:[ ]... 
        if (currentKind == SyntaxKind.QuestionToken && (currentToken.Parent.RawKind == (int)SyntaxKind.ConditionalExpression || (currentToken.Parent is TypeSyntax && currentToken.Parent?.Parent?.RawKind != (int)SyntaxKind.TypeArgumentList)))
            return true;

        // ][ ]...
        if (currentKind == SyntaxKind.CloseBracketToken && IsWord(nextKind))
            return true;

        // [ ]?...[ ]: for conditional expression
        if (nextKind is SyntaxKind.QuestionToken or SyntaxKind.ColonToken && nextToken.Parent.RawKind == (int)SyntaxKind.ConditionalExpression)
            return true;

        // =[ ]
        if (currentKind == SyntaxKind.EqualsToken)
            return true;

        // [ ]=
        if (nextKind == SyntaxKind.EqualsToken)
            return true;

        // directives
        if (SyntaxFacts.IsLiteral(currentKind) && SyntaxFacts.IsLiteral(nextKind))
            return true;

        // Am I checking next tokens?
        if (IsKeyword(currentKind))
            return true;

        // (some-word)[ ](some-word)
        if (IsWord(currentKind) && IsWord(nextKind))
            return true;

        if (currentKind == SyntaxKind.IdentifierToken && nextToken.Parent is null)
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
        return SyntaxFacts.GetAssignmentExpression(kind) != SyntaxKind.None;
    }

    private static bool BinaryTokenNeedsSeparator(SyntaxKind kind)
    {
        return kind switch
        {
            SyntaxKind.DotToken => false,
            _ => SyntaxFacts.GetBinaryExpression(kind) != SyntaxKind.None
        };
    }

    #endregion

    #region LineBreaks

    private int LineBreaksAfter(SyntaxToken currentToken, SyntaxToken nextToken)
    {
        if (currentToken.RawKind == (int)SyntaxKind.None)
            return 0;

        if (currentToken.RawKind == (int)SyntaxKind.EndOfDirectiveToken)
            return 1;

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
                return (currentToken.Parent is StatementSyntax && nextToken.Parent != currentToken.Parent) || nextKind == SyntaxKind.OpenBraceToken ? 1 : 0;

            case SyntaxKind.CloseBracketToken:
            {
                if (currentToken.Parent is AttributeListSyntax && currentToken.Parent.Parent is not ParameterSyntax)
                    return 1;
                break;
            }

            case SyntaxKind.SemicolonToken:
                return LineBreaksAfterSemicolon(currentToken, nextToken);

            case SyntaxKind.CommaToken:
                return 0;

            case SyntaxKind.ElseKeyword:
                return nextKind != SyntaxKind.IfKeyword ? 1 : 0;

            case SyntaxKind.ColonToken:
            {
                if (currentToken.Parent is SwitchLabelSyntax)
                    return 1;
                break;
            }
        }

        switch (nextKind)
        {
            case SyntaxKind.OpenBraceToken:
                return LineBreaksBeforeOpenBrace(nextToken);

            case SyntaxKind.CloseBraceToken:
                return LineBreaksBeforeCloseBrace(nextToken);

            case SyntaxKind.ElseKeyword:
                return 1;

            case SyntaxKind.OpenBracketToken:
                return nextToken.Parent is AttributeListSyntax && nextToken.Parent.Parent is not ParameterSyntax ? 1 : 0;
        }

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
        if (currentToken.Parent is InitializerExpressionSyntax)
            return 0;
        return 1;
    }

    private static int LineBreaksBeforeCloseBrace(SyntaxToken nextToken)
    {
        if (nextToken.Parent is InitializerExpressionSyntax)
            return 0;
        return 1;
    }

    private static int LineBreaksAfterCloseBrace(SyntaxToken currentToken, SyntaxToken nextToken)
    {
        if (currentToken.Parent is InitializerExpressionSyntax)
            return 0;

        var kind = (SyntaxKind)nextToken.RawKind;
        switch (kind)
        {
            case SyntaxKind.EndOfFileToken:
            case SyntaxKind.CloseBraceToken:
            case SyntaxKind.ElseKeyword:
                return 1;

            default:
                if (kind == SyntaxKind.WhileKeyword && nextToken.Parent?.RawKind == (int)SyntaxKind.DoStatement)
                    return 1;
                if (kind == SyntaxKind.SemicolonToken)
                    return 0;
                return 2;
        }
    }

    // ReSharper disable once UnusedParameter.Local
    private static int LineBreaksAfterSemicolon(SyntaxToken currentToken, SyntaxToken nextToken)
    {
        if (currentToken.Parent is ForStatementSyntax)
            return 0;

        if (currentToken.Parent is StructDeclarationSyntax)
            return 2;

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
        return SyntaxFacts.IsPreprocessorDirective(kind);
    }

    // ReSharper disable once UnusedParameter.Local
    private static bool NeedsLineBreakAfter(SyntaxTrivia trivia, bool isTrailingTrivia)
    {
        var kind = (SyntaxKind)trivia.RawKind;
        switch (kind)
        {
            case SyntaxKind.SingleLineCommentTrivia:
                return true;

            default:
                return SyntaxFacts.IsPreprocessorDirective(kind);
        }
    }

    #endregion

    #region Depth

    private static int GetDeclarationDepth(SyntaxToken token)
    {
        return GetDeclarationDepth(token.Parent as HlslSyntaxNode);
    }

    private static int GetDeclarationDepth(SyntaxTrivia trivia)
    {
        if (SyntaxFacts.IsPreprocessorDirective((SyntaxKind)trivia.RawKind))
            return 0;

        return GetDeclarationDepth(trivia.Token);
    }

    private static int GetDeclarationDepth(HlslSyntaxNode? node)
    {
        if (node == null)
            return 0;

        if (node.Parent != null)
        {
            if (node.Parent.Kind == SyntaxKind.CompilationUnit)
                return 0;

            var parentDepth = GetDeclarationDepth(node.Parent);
            if (node.Kind == SyntaxKind.IfStatement && node.Parent.Kind == SyntaxKind.ElseClause)
                return parentDepth;
            if (node.Parent is BlockSyntax || (node is StatementSyntax && !(node is BlockSyntax)))
                return parentDepth + 1;

            if (node is MemberDeclarationSyntax || node is SwitchSectionSyntax)
                return parentDepth + 1;
            return parentDepth;
        }

        return 0;
    }

    #endregion
}