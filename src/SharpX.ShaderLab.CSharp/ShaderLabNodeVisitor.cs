// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpX.Composition.CSharp;
using SharpX.Composition.Interfaces;
using SharpX.ShaderLab.Primitives.Attributes;
using SharpX.ShaderLab.Primitives.Enum;
using SharpX.ShaderLab.Syntax;

using CompilationUnitSyntax = Microsoft.CodeAnalysis.CSharp.Syntax.CompilationUnitSyntax;

namespace SharpX.ShaderLab.CSharp;

public class ShaderLabNodeVisitor : CompositeCSharpSyntaxVisitor<ShaderLabSyntaxNode>
{
    private readonly IBackendVisitorArgs<ShaderLabSyntaxNode> _args;
    private readonly HashSet<INamedTypeSymbol> _subShaders;
    private readonly HashSet<INamedTypeSymbol> _passes;

    public ShaderLabNodeVisitor(IBackendVisitorArgs<ShaderLabSyntaxNode> args) : base(args)
    {
        _args = args;
        _subShaders = new HashSet<INamedTypeSymbol>(SymbolEqualityComparer.Default);
        _passes = new HashSet<INamedTypeSymbol>(SymbolEqualityComparer.Default);
    }

    public override ShaderLabSyntaxNode? DefaultVisit(SyntaxNode node)
    {
        return null;
    }

    public override ShaderLabSyntaxNode? VisitCompilationUnit(CompilationUnitSyntax node)
    {
        var members = node.Members.Select(Visit)
                          .Where(w => w != null)
                          .ToArray();
        if (members.Length == 0)
            return null;

        var decl = members[0];
        if (decl is ShaderDeclarationSyntax shader)
            return SyntaxFactory.CompilationUnit(shader);

        return null; // returns HLSL source code that wrapped by ShaderLabSyntaxNode
    }

    public override ShaderLabSyntaxNode? VisitNamespaceDeclaration(NamespaceDeclarationSyntax node)
    {
        var members = node.Members.Select(Visit)
                          .Where(w => w != null)
                          .ToArray();

        if (members.Length != 1)
            return null;

        return members[0];
    }

    public override ShaderLabSyntaxNode? VisitFileScopedNamespaceDeclaration(FileScopedNamespaceDeclarationSyntax node)
    {
        var members = node.Members.Select(Visit)
                          .Where(w => w != null)
                          .ToArray();

        if (members.Length != 1)
            return null;

        return members[0];
    }
}