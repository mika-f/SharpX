// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using MemberDeclarationSyntax = SharpX.Hlsl.Syntax.MemberDeclarationSyntax;

namespace SharpX.Hlsl.CSharp;

internal class NodeVisitor : CSharpSyntaxVisitor<HlslSyntaxNode>
{
    private readonly SemanticModel _semanticModel;

    public NodeVisitor(SemanticModel semanticModel)
    {
        _semanticModel = semanticModel;
    }

    public override HlslSyntaxNode VisitCompilationUnit(CompilationUnitSyntax node)
    {
        var members = SyntaxFactory.List(node.Members.Select(w => (MemberDeclarationSyntax)w.Accept(this)!));
        return SyntaxFactory.CompilationUnit(members);
    }
}