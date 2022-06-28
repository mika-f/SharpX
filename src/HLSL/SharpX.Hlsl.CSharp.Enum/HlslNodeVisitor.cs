// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpX.Composition.CSharp;
using SharpX.Composition.Interfaces;

using AttributeListSyntax = SharpX.Hlsl.Syntax.AttributeListSyntax;
using FieldDeclarationSyntax = SharpX.Hlsl.Syntax.FieldDeclarationSyntax;

namespace SharpX.Hlsl.CSharp.Enum;

internal class HlslNodeVisitor : CompositeCSharpSyntaxVisitor<HlslSyntaxNode>
{
    private static readonly Core.SyntaxList<AttributeListSyntax> EmptyAttributes = SyntaxFactory.List<AttributeListSyntax>();
    private readonly IBackendVisitorArgs<HlslSyntaxNode> _args;

    public HlslNodeVisitor(IBackendVisitorArgs<HlslSyntaxNode> args) : base(args)
    {
        _args = args;
    }

    public override HlslSyntaxNode? VisitPropertyDeclaration(PropertyDeclarationSyntax oldNode, HlslSyntaxNode? newNode)
    {
        var m = _args.SemanticModel.GetSymbolInfo(oldNode.Type!);
        if (m.Symbol is not INamedTypeSymbol symbol)
            return newNode;

        if (symbol.TypeKind != TypeKind.Enum)
            return newNode;

        if (newNode is not FieldDeclarationSyntax field)
            return newNode;

        return field.WithType(SyntaxFactory.IdentifierName("int"));
    }

    public override HlslSyntaxNode? VisitParameter(ParameterSyntax oldNode, HlslSyntaxNode? newNode)
    {
        var m = _args.SemanticModel.GetSymbolInfo(oldNode.Type!);
        if (m.Symbol is not INamedTypeSymbol symbol)
            return newNode;

        if (symbol.TypeKind != TypeKind.Enum)
            return newNode;

        if (newNode is not Syntax.ParameterSyntax parameter)
            return newNode;

        return parameter.WithType(SyntaxFactory.IdentifierName("int"));
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