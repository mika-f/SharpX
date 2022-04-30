// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using Microsoft.CodeAnalysis.CSharp;

using SharpX.Core;

namespace SharpX.Compiler.Models;

internal class RootCSharpSyntaxVisitor<T> : CSharpSyntaxVisitor<T> where T : SyntaxNode
{
    private readonly List<CSharpSyntaxVisitor<T>> _visitors;

    public RootCSharpSyntaxVisitor()
    {
        _visitors = new List<CSharpSyntaxVisitor<T>>();
    }

    public override T? DefaultVisit(Microsoft.CodeAnalysis.SyntaxNode node)
    {
        return base.DefaultVisit(node);
    }

    public void AddVisitor(CSharpSyntaxVisitor<T> visitor)
    {
        _visitors.Add(visitor);
    }
}