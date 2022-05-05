// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpX.Composition.CSharp;
using SharpX.Composition.Interfaces;

using FieldDeclarationSyntax = SharpX.Hlsl.Syntax.FieldDeclarationSyntax;
using MemberDeclarationSyntax = SharpX.Hlsl.Syntax.MemberDeclarationSyntax;

namespace SharpX.Hlsl.CSharp;

internal class NodeVisitor : CompositeCSharpSyntaxVisitor<HlslSyntaxNode>
{
    private readonly SemanticModel _semanticModel;

    public NodeVisitor(IBackendVisitorArgs<HlslSyntaxNode> args) : base(args)
    {
        _semanticModel = args.SemanticModel;
    }

    public override HlslSyntaxNode VisitCompilationUnit(CompilationUnitSyntax node)
    {
        var members = node.Members.Select(w => (MemberDeclarationSyntax?)Visit(w)).Where(w => w != null).Select(w => w!);
        return SyntaxFactory.CompilationUnit(SyntaxFactory.List(members));
    }

    public override HlslSyntaxNode? VisitClassDeclaration(ClassDeclarationSyntax node)
    {
        return base.VisitClassDeclaration(node);
    }

    public override HlslSyntaxNode? VisitStructDeclaration(StructDeclarationSyntax node)
    {
        var identifier = SyntaxFactory.Identifier(node.Identifier.ValueText);
        var members = node.Members.Select(w => (MemberDeclarationSyntax?)Visit(w))
                          .Where(w => w != null)
                          .OfType<FieldDeclarationSyntax>();

        return SyntaxFactory.StructDeclaration(identifier, SyntaxFactory.List(members));
    }
}