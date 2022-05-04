// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Hlsl.Syntax;

namespace SharpX.Hlsl;

internal class SyntaxNormalizer : HlslSyntaxRewriter
{
    private SyntaxNormalizer(int initialDepth, string indentWhitespace, string eolWhitespace, bool useElasticTrivia) { }

    internal static TNode Normalize<TNode>(TNode node, string indentWhitespace, string eolWhitespace, bool useElasticTrivia = false) where TNode : HlslSyntaxNode
    {
        var normalizer = new SyntaxNormalizer(GetDeclarationDepth(node), indentWhitespace, eolWhitespace, useElasticTrivia);
        var result = (TNode)normalizer.Visit(node)!;

        return result;
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
}