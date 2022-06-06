// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using Microsoft.CodeAnalysis;

using SharpX.Composition.CSharp;
using SharpX.Composition.Interfaces;
using SharpX.Hlsl.Syntax;

using MemberAccessExpressionSyntax = Microsoft.CodeAnalysis.CSharp.Syntax.MemberAccessExpressionSyntax;

namespace SharpX.Hlsl.CSharp.Enum;

internal class HlslNodeVisitor : CompositeCSharpSyntaxVisitor<HlslSyntaxNode>
{
    private static readonly Core.SyntaxList<AttributeListSyntax> EmptyAttributes = SyntaxFactory.List<AttributeListSyntax>();
    private readonly IBackendVisitorArgs<HlslSyntaxNode> _args;

    public HlslNodeVisitor(IBackendVisitorArgs<HlslSyntaxNode> args) : base(args)
    {
        _args = args;
    }

    public override HlslSyntaxNode? VisitMemberAccessExpression(MemberAccessExpressionSyntax oldNode, HlslSyntaxNode? newNode)
    {
        var m = _args.SemanticModel.GetSymbolInfo(oldNode);
        if (m.Symbol is not IFieldSymbol symbol)
            return newNode;

        if (symbol.IsConst)
        {
            var val = _args.SemanticModel.GetConstantValue(oldNode).Value!.ToString()!;
            return SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, SyntaxFactory.Literal(val));
        }

        return newNode;
    }
}