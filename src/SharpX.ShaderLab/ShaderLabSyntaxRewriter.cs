// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.Core.Syntax;
using SharpX.ShaderLab.Syntax;
using SharpX.ShaderLab.Syntax.InternalSyntax;

namespace SharpX.ShaderLab;

public class ShaderLabSyntaxRewriter : ShaderLabSyntaxVisitor<SyntaxNode?>
{
    public override SyntaxNode? VisitIdentifierName(IdentifierNameSyntax node)
    {
        return node.Update(VisitToken(node.Identifier));
    }

    public override SyntaxNode? VisitQualifiedName(QualifiedNameSyntax node)
    {
        return node.Update(
            (NameSyntax?)Visit(node.Left) ?? throw new ArgumentNullException(),
            VisitToken(node.DotToken),
            (SimpleNameSyntax?)Visit(node.Right) ?? throw new ArgumentNullException()
        );
    }

    public override SyntaxNode? VisitEqualsValueClause(EqualsValueClauseSyntax node)
    {
        return node.Update(
            VisitToken(node.EqualsToken),
            (ExpressionSyntax?)Visit(node.Value) ?? throw new ArgumentException()
        );
    }

    public override SyntaxNode? VisitArgumentList(ArgumentListSyntax node)
    {
        return node.Update(
            VisitToken(node.OpenParenToken),
            VisitList(node.Arguments),
            VisitToken(node.CloseParenToken)
        );
    }

    public override SyntaxNode? VisitArgument(ArgumentSyntax node)
    {
        return node.Update(
            (ExpressionSyntax?)Visit(node.Expression) ?? throw new ArgumentNullException()
        );
    }

    public override SyntaxNode? VisitAttributeList(AttributeListSyntax node)
    {
        return node.Update(
            VisitToken(node.OpenBracketToken),
            VisitList(node.Attributes),
            VisitToken(node.CloseBracketToken)
        );
    }

    public override SyntaxNode? VisitAttribute(AttributeSyntax node)
    {
        return node.Update(
            (NameSyntax?)Visit(node.Name) ?? throw new ArgumentNullException(),
            (ArgumentListSyntax?)Visit(node.ArgumentList) ?? throw new ArgumentNullException()
        );
    }

    public override SyntaxNode? VisitLiteralExpression(LiteralExpressionSyntax node)
    {
        return node.Update(VisitToken(node.Token));
    }

    public override SyntaxNode? VisitTextureLiteralExpression(TextureLiteralExpressionSyntax node)
    {
        return node.Update(
            (LiteralExpressionSyntax?)Visit(node.Value) ?? throw new ArgumentNullException(),
            VisitToken(node.OpenBraceToken),
            VisitToken(node.CloseBraceToken)
        );
    }

    public override SyntaxNode? VisitPropertiesDeclaration(PropertiesDeclarationSyntax node)
    {
        return node.Update(
            VisitToken(node.PropertiesKeyword),
            VisitToken(node.OpenBraceToken),
            VisitList(node.Properties),
            VisitToken(node.CloseBraceToken)
        );
    }

    public override SyntaxNode? VisitPropertyDeclaration(PropertyDeclarationSyntax node)
    {
        return node.Update(
            VisitToken(node.Identifier),
            VisitToken(node.OpenParenToken),
            VisitToken(node.DisplayName),
            VisitToken(node.CommaToken),
            (SimpleNameSyntax?)Visit(node.Type) ?? throw new ArgumentNullException(),
            (ArgumentListSyntax?)Visit(node.ArgumentList),
            VisitToken(node.CloseParenToken),
            (EqualsValueClauseSyntax?)Visit(node.Default) ?? throw new ArgumentNullException()
        );
    }

    public override SyntaxNode? VisitCgIncludeDeclaration(CgIncludeDeclarationSyntax node)
    {
        return node.Update(
            VisitToken(node.CgIncludeKeyword),
            node.Source, // skipped because this node is already normalized, rewritten, and visited (visited by other visitor)
            VisitToken(node.EndCgKeyword)
        );
    }

    public override SyntaxNode? VisitCgProgramDeclaration(CgProgramDeclarationSyntax node)
    {
        return node.Update(
            VisitToken(node.CgProgramKeyword),
            node.Source, // skipped because this node is already normalized, rewritten, and visited (visited by other visitor)
            VisitToken(node.EndCgKeyword)
        );
    }

    public override SyntaxNode? VisitPassDeclaration(PassDeclarationSyntax node)
    {
        return node.Update(
            VisitToken(node.Keyword),
            VisitToken(node.OpenBraceToken),
            (TagsDeclarationSyntax?)Visit(node.Tags),
            VisitList(node.Commands),
            (CgProgramDeclarationSyntax?)Visit(node.CgProgram) ?? throw new ArgumentNullException(),
            VisitToken(node.CloseBraceToken)
        );
    }

    public override SyntaxNode? VisitGrabPassDeclaration(GrabPassDeclarationSyntax node)
    {
        return node.Update(
            VisitToken(node.Keyword),
            VisitToken(node.OpenBraceToken),
            VisitToken(node.Identifier),
            (TagsDeclarationSyntax?)Visit(node.Tags),
            (NameDeclarationSyntax?)Visit(node.Name),
            VisitToken(node.CloseBraceToken)
        );
    }

    public override SyntaxNode? VisitUsePassDeclaration(UsePassDeclarationSyntax node)
    {
        return node.Update(
            VisitToken(node.Keyword),
            VisitToken(node.PassReference)
        );
    }

    public override SyntaxNode? VisitCommandDeclaration(CommandDeclarationSyntax node)
    {
        return node.Update(
            VisitToken(node.Keyword),
            VisitList(node.Arguments)
        );
    }

    public override SyntaxNode? VisitStencilDeclaration(StencilDeclarationSyntax node)
    {
        return node.Update(
            VisitToken(node.Keyword),
            VisitToken(node.OpenBraceToken),
            VisitList(node.Commands),
            VisitToken(node.CloseBraceToken)
        );
    }

    public override SyntaxNode? VisitNameDeclaration(NameDeclarationSyntax node)
    {
        return node.Update(
            VisitToken(node.Keyword),
            VisitToken(node.Name)
        );
    }


    public override SyntaxNode? VisitTagsDeclaration(TagsDeclarationSyntax node)
    {
        return node.Update(
            VisitToken(node.TagsKeyword),
            VisitToken(node.OpenBraceToken),
            VisitList(node.Tags),
            VisitToken(node.CloseBraceToken)
        );
    }

    public override SyntaxNode? VisitTagDeclaration(TagDeclarationSyntax node)
    {
        return node.Update(
            VisitToken(node.Key),
            VisitToken(node.EqualsToken),
            VisitToken(node.Value)
        );
    }

    public override SyntaxNode? VisitFallbackDeclaration(FallbackDeclarationSyntax node)
    {
        return node.Update(
            VisitToken(node.FallbackKeyword),
            VisitToken(node.ShaderNameOrOffKeyword)
        );
    }

    public override SyntaxNode? VisitCustomEditorDeclaration(CustomEditorDeclarationSyntax node)
    {
        return node.Update(
            VisitToken(node.CustomEditorKeyword),
            VisitToken(node.FullyQualifiedInspectorName)
        );
    }

    public virtual SyntaxToken? VisitToken(SyntaxToken? token)
    {
        if (token == null)
            return null;
        return VisitToken(token.Value);
    }

    public virtual SyntaxToken VisitToken(SyntaxToken token)
    {
        var node = token.Node;
        if (node == null)
            return token;

        var leadingTrivia = node.GetLeadingTrivia();
        var trailingTrivia = node.GetTrailingTrivia();

        if (leadingTrivia != null)
        {
            var leading = VisitList(new SyntaxTriviaList(token, leadingTrivia));

            if (trailingTrivia != null)
            {
                var index = leadingTrivia.IsList ? leadingTrivia.SlotCount : 1;
                var trailing = VisitList(new SyntaxTriviaList(token, trailingTrivia, token.Position + node.FullWidth - trailingTrivia.FullWidth, index));

                if (leading.Node != leadingTrivia)
                    token = token.WithLeadingTrivia(leading);

                return trailing.Node != trailingTrivia ? token.WithTrailingTrivia(trailing) : token;
            }

            return leading.Node != leadingTrivia ? token.WithLeadingTrivia(leading) : token;
        }

        if (trailingTrivia != null)
        {
            var trailing = VisitList(new SyntaxTriviaList(token, trailingTrivia, token.Position + node.FullWidth - trailingTrivia.FullWidth));
            return trailing.Node != trailingTrivia ? token.WithTrailingTrivia(trailing) : token;
        }

        return token;
    }

    public virtual SyntaxTrivia VisitTrivia(SyntaxTrivia trivia)
    {
        return trivia;
    }

    public virtual SyntaxList<TNode> VisitList<TNode>(SyntaxList<TNode> list) where TNode : SyntaxNode
    {
        SyntaxListBuilder? alternate = null;
        for (int i = 0, n = list.Count; i < n; i++)
        {
            var item = list[i];
            var visited = VisitListElement(item);
            if (item != visited && alternate == null)
            {
                alternate = new SyntaxListBuilder(n);
                alternate.AddRange(list, 0, i);
            }

            if (alternate != null && visited != null && visited.RawKind != (int)SyntaxKind.None)
                alternate.Add(visited);
        }

        if (alternate != null) return alternate.ToList();

        return list;
    }

    public virtual TNode? VisitListElement<TNode>(TNode? node) where TNode : SyntaxNode
    {
        return (TNode?)Visit(node);
    }

    public virtual SeparatedSyntaxList<TNode> VisitList<TNode>(SeparatedSyntaxList<TNode> list) where TNode : SyntaxNode
    {
        var count = list.Count;
        var sepCount = list.SeparatedCount;

        SeparatedSyntaxListBuilder<TNode> alternate = default;

        var i = 0;
        for (; i < sepCount; i++)
        {
            var node = list[i];
            var visitedNode = VisitListElement(node);

            var separator = list.GetSeparator(i);
            var visitedSeparator = VisitListSeparator(separator);

            if (alternate.IsNull)
                if (node != visitedNode || separator != visitedSeparator)
                {
                    alternate = new SeparatedSyntaxListBuilder<TNode>(count);
                    alternate.AddRange(list, i);
                }

            if (!alternate.IsNull)
            {
                if (visitedNode != null)
                {
                    alternate.Add(visitedNode);

                    if (visitedSeparator.RawKind == (int)SyntaxKind.None) throw new InvalidOperationException();
                    alternate.AddSeparator(visitedSeparator);
                }
                else
                {
                    if (visitedNode == null) throw new InvalidOperationException();
                }
            }
        }

        if (i < count)
        {
            var node = list[i];
            var visitedNode = VisitListElement(node);

            if (alternate.IsNull)
                if (node != visitedNode)
                {
                    alternate = new SeparatedSyntaxListBuilder<TNode>(count);
                    alternate.AddRange(list, i);
                }

            if (!alternate.IsNull && visitedNode != null) alternate.Add(visitedNode);
        }

        if (!alternate.IsNull)
            return alternate.ToList();

        return list;
    }

    public virtual SyntaxToken VisitListSeparator(SyntaxToken separator)
    {
        return VisitToken(separator);
    }

    public virtual SyntaxTokenList VisitList(SyntaxTokenList list)
    {
        SyntaxTokenListBuilder? alternate = null;
        var count = list.Count;
        var index = -1;

        foreach (var item in list)
        {
            index++;
            var visited = VisitToken(item);
            if (item != visited && alternate == null)
            {
                alternate = new SyntaxTokenListBuilder(count);
                alternate.Add(list, 0, index);
            }

            if (alternate != null && visited.RawKind != (int)SyntaxKind.None) //skip the null check since SyntaxToken is a value type
                alternate.Add(visited);
        }

        if (alternate != null) return alternate.ToList();

        return list;
    }

    public virtual SyntaxTriviaList VisitList(SyntaxTriviaList list)
    {
        var count = list.Count;
        if (count != 0)
        {
            SyntaxTriviaListBuilder? alternate = null;
            var index = -1;

            foreach (var item in list)
            {
                index++;
                var visited = VisitListElement(item);

                //skip the null check since SyntaxTrivia is a value type
                if (visited != item && alternate == null)
                {
                    alternate = new SyntaxTriviaListBuilder(count);
                    alternate.Add(list, 0, index);
                }

                if (alternate != null && visited.RawKind != (int)SyntaxKind.None) alternate.Add(visited);
            }

            if (alternate != null) return alternate.ToList();
        }

        return list;
    }

    public virtual SyntaxTrivia VisitListElement(SyntaxTrivia element)
    {
        return VisitTrivia(element);
    }
}