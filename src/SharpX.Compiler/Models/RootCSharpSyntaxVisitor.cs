// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using System.Collections;

using Microsoft.CodeAnalysis.CSharp;

using SharpX.Core;

namespace SharpX.Compiler.Models;

internal class RootCSharpSyntaxVisitor<T> : CSharpSyntaxVisitor<T> where T : SyntaxNode
{
    private readonly List<CSharpSyntaxVisitor<T>> _visitors;
    private readonly Hashtable _counter;

    public RootCSharpSyntaxVisitor()
    {
        _visitors = new List<CSharpSyntaxVisitor<T>>();
        _counter = new Hashtable();
    }

    public override T? Visit(Microsoft.CodeAnalysis.SyntaxNode? node)
    {
        if (node == null)
            return default;

        var counter = _counter[node] as int? ?? 0;
        foreach (var visitor in _visitors.Skip(counter))
        {
            _counter[node] = counter + 1;

            var v = visitor.Visit(node);
            if (v != null)
            {
                _counter.Remove(node);
                return v;
            }
        }

        _counter.Remove(node);
        return default;
    }

    public void AddVisitor(CSharpSyntaxVisitor<T> visitor)
    {
        _visitors.Add(visitor);
    }
}