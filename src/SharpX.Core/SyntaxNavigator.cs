// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

namespace SharpX.Core;

/// <summary>
///     represents <see cref="Microsoft.CodeAnalysis.SyntaxNavigator" />
/// </summary>
internal class SyntaxNavigator
{
    private const int None = 0;

    public static readonly SyntaxNavigator Instance = new();

    private static readonly Func<SyntaxTrivia, bool>?[] StepIntoFunctions =
    {
        /* 00 */ null,
        /* 01 */ t => t.IsDirective,
        /* 10 */ t => false,
        /* 11 */ t => false || t.IsDirective
    };

    private SyntaxNavigator() { }

    private static Func<SyntaxToken, bool> GetPredicateFunction(bool includeZeroWidth)
    {
        return includeZeroWidth ? SyntaxToken.Any : SyntaxToken.NonZeroWidth;
    }

    private static Func<SyntaxTrivia, bool>? GetStepIntoFunction(bool skipped, bool directive)
    {
        var i = (skipped ? 2 : 0) | (directive ? 1 : 0);
        return StepIntoFunctions[i];
    }

    private static bool Matches(Func<SyntaxToken, bool> predicate, SyntaxToken token)
    {
        return predicate == SyntaxToken.Any || predicate(token);
    }

    public SyntaxToken GetNextToken(SyntaxToken current, Func<SyntaxToken, bool> predicate, Func<SyntaxTrivia, bool>? stepInto)
    {
        return GetNextToken(current, predicate, stepInto != null, stepInto);
    }

    public SyntaxToken GetFirstToken(SyntaxNode current, bool includeZeroWidth, bool includeSkipped, bool includeDirectives)
    {
        return GetFirstToken(current, GetPredicateFunction(includeZeroWidth), GetStepIntoFunction(includeSkipped, includeDirectives));
    }

    public SyntaxToken GetLastToken(SyntaxNode current, bool includeZeroWidth, bool includeSkipped, bool includeDirectives)
    {
        return GetLastToken(current, GetPredicateFunction(includeZeroWidth), GetStepIntoFunction(includeSkipped, includeDirectives));
    }

    #region GetFirstToken

    private SyntaxToken GetFirstToken(SyntaxNode current, Func<SyntaxToken, bool> predicate, Func<SyntaxTrivia, bool>? stepInto)
    {
        var stack = new Stack<ChildSyntaxList.Enumerator>();
        try
        {
            stack.Push(current.ChildNodesAndTokens().GetEnumerator());

            while (stack.Count > 0)
            {
                var en = stack.Pop();
                if (en.MoveNext())
                {
                    var child = en.Current;
                    if (child.IsToken)
                    {
                        var token = GetFirstToken(child.AsToken(), predicate, stepInto);
                        if (token.RawKind != None)
                            return token;
                    }

                    stack.Push(en);
                    if (child.IsNode)
                        stack.Push(child.AsNode()!.ChildNodesAndTokens().GetEnumerator());
                }
            }

            return default;
        }
        finally
        {
            stack.Clear();
        }
    }

    private SyntaxToken GetFirstToken(SyntaxToken token, Func<SyntaxToken, bool> predicate, Func<SyntaxTrivia, bool>? stepInto)
    {
        if (stepInto != null)
        {
            var firstToken = GetFirstToken(token.LeadingTrivia, predicate, stepInto);
            if (firstToken.RawKind != None)
                return firstToken;
        }

        if (Matches(predicate, token))
            return token;

        if (stepInto != null)
        {
            var firstToken = GetFirstToken(token.TrailingTrivia, predicate, stepInto);
            if (firstToken.RawKind != None)
                return firstToken;
        }

        return default;
    }

    private SyntaxToken GetFirstToken(SyntaxTriviaList triviaList, Func<SyntaxToken, bool> predicate, Func<SyntaxTrivia, bool> stepInto)
    {
        foreach (var trivia in triviaList)
            if (trivia.TryGetStructure(out var structure) && stepInto(trivia))
            {
                var token = GetFirstToken(structure, predicate, stepInto);
                if (token.RawKind != None)
                    return token;
            }

        return default;
    }

    #endregion

    #region GetNextToken

    private SyntaxToken GetNextToken(SyntaxToken current, Func<SyntaxToken, bool> predicate, bool searchInsideCurrentTokenTrailingTrivia, Func<SyntaxTrivia, bool>? stepInto)
    {
        if (current.Parent != null)
        {
            if (searchInsideCurrentTokenTrailingTrivia)
            {
                var firstToken = GetFirstToken(current.TrailingTrivia, predicate, stepInto!);
                if (firstToken.RawKind != None)
                    return firstToken;
            }

            var returnNext = false;
            foreach (var child in current.Parent.ChildNodesAndTokens())
                if (returnNext)
                {
                    if (child.IsToken)
                    {
                        var token = GetFirstToken(child.AsToken(), predicate, stepInto);
                        if (token.RawKind != None)
                            return token;
                    }
                    else
                    {
                        var token = GetFirstToken(child.AsNode()!, predicate, stepInto);
                        if (token.RawKind != None)
                            return token;
                    }
                }
                else if (child.IsToken && child.AsToken() == current)
                {
                    returnNext = true;
                }

            return GetNextToken(current.Parent, predicate, stepInto);
        }

        return default;
    }

    private SyntaxToken GetNextToken(SyntaxNode node, Func<SyntaxToken, bool> predicate, Func<SyntaxTrivia, bool>? stepInto)
    {
        while (node.Parent != null)
        {
            var returnNext = false;
            foreach (var child in node.Parent.ChildNodesAndTokens())
                if (returnNext)
                {
                    if (child.IsToken)
                    {
                        var token = GetFirstToken(child.AsToken(), predicate, stepInto);
                        if (token.RawKind != None)
                            return token;
                    }
                    else
                    {
                        var token = GetFirstToken(child.AsNode()!, predicate, stepInto);
                        if (token.RawKind != None)
                            return token;
                    }
                }
                else if (child.IsToken && child.AsNode() == node)
                {
                    returnNext = true;
                }

            node = node.Parent;
        }

        if (node.IsStructuredTrivia)
            return GetNextToken(((IStructuredTriviaSyntax)node).ParentTrivia, predicate, stepInto);
        return default;
    }

    private SyntaxToken GetNextToken(SyntaxTrivia current, Func<SyntaxToken, bool> predicate, Func<SyntaxTrivia, bool>? stepInto)
    {
        var returnNext = false;

        var leadingToken = GetNextToken(current, current.Token.LeadingTrivia, predicate, stepInto, ref returnNext);
        if (leadingToken.RawKind != None)
            return leadingToken;

        if (returnNext && predicate(current.Token))
            return current.Token;

        var trailingToken = GetNextToken(current, current.Token.TrailingTrivia, predicate, stepInto, ref returnNext);
        if (trailingToken.RawKind != None)
            return trailingToken;

        return GetNextToken(current.Token, predicate, false, stepInto);
    }

    private SyntaxToken GetNextToken(SyntaxTrivia current, SyntaxTriviaList list, Func<SyntaxToken, bool> predicate, Func<SyntaxTrivia, bool>? stepInto, ref bool returnNext)
    {
        foreach (var trivia in list)
            if (returnNext)
            {
                if (trivia.TryGetStructure(out var structure) && stepInto != null && stepInto(trivia))
                {
                    var token = GetFirstToken(structure!, predicate, stepInto);
                    if (token.RawKind != None)
                        return token;
                }
            }
            else if (trivia == current)
            {
                returnNext = true;
            }

        return default;
    }

    #endregion

    #region GetLastToken

    private SyntaxToken GetLastToken(SyntaxNode current, Func<SyntaxToken, bool> predicate, Func<SyntaxTrivia, bool>? stepInto)
    {
        var stack = new Stack<ChildSyntaxList.Reversed.ReversedEnumerator>();
        stack.Push(current.ChildNodesAndTokens().Reverse().GetEnumerator());

        while (stack.Count > 0)
        {
            var en = stack.Pop();
            if (en.MoveNext())
            {
                var child = en.Current;
                if (child.IsToken)
                {
                    var token = GetLastToken(child.AsToken(), predicate, stepInto);
                    if (token.RawKind != None)
                        return token;
                }

                stack.Push(en);

                if (child.IsNode)
                    stack.Push(child.AsNode()!.ChildNodesAndTokens().Reverse().GetEnumerator());
            }
        }

        return default;
    }


    private SyntaxToken GetLastToken(SyntaxToken token, Func<SyntaxToken, bool> predicate, Func<SyntaxTrivia, bool>? stepInto)
    {
        if (stepInto != null)
        {
            var lastToken = GetLastToken(token.TrailingTrivia, predicate, stepInto);
            if (lastToken.RawKind != None)
                return lastToken;
        }

        if (Matches(predicate, token))
            return token;

        if (stepInto != null)
        {
            var lastToken = GetLastToken(token.LeadingTrivia, predicate, stepInto);
            if (lastToken.RawKind != None)
                return lastToken;
        }

        return default;
    }

    private SyntaxToken GetLastToken(SyntaxTriviaList list, Func<SyntaxToken, bool> predicate, Func<SyntaxTrivia, bool> stepInto)
    {
        foreach (var trivia in list.Reverse())
            if (TryGetLastTokenForStructuredTrivia(trivia, predicate, stepInto, out var token))
                return token;

        return default;
    }

    private bool TryGetLastTokenForStructuredTrivia(SyntaxTrivia trivia, Func<SyntaxToken, bool> predicate, Func<SyntaxTrivia, bool> stepInto, out SyntaxToken token)
    {
        token = default;

        if (!trivia.TryGetStructure(out var structure) || !stepInto(trivia))
            return false;

        token = GetLastToken(structure, predicate, stepInto);

        return token.RawKind != None;
    }

    #endregion
}