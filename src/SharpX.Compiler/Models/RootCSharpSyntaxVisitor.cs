// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using System.Collections;

using Microsoft.CodeAnalysis.CSharp;

using SharpX.Composition.CSharp;
using SharpX.Core;

namespace SharpX.Compiler.Models;

internal class RootCSharpSyntaxVisitor<T> : CSharpSyntaxVisitor<T> where T : SyntaxNode
{
    private readonly List<CompositeCSharpSyntaxVisitor<T>> _visitors;
    private readonly Hashtable _counter;
    private int _initialCounter;

    public RootCSharpSyntaxVisitor()
    {
        _visitors = new List<CompositeCSharpSyntaxVisitor<T>>();
        _counter = new Hashtable();
        _initialCounter = 0;
    }

    public override T? Visit(Microsoft.CodeAnalysis.SyntaxNode? node)
    {
        if (node == null)
            return default;

        var counter = _counter[node] as int? ?? _initialCounter;

        if (_initialCounter == 0)
            _initialCounter++; // remove self if it is not root element

        T? rewritten = default;
        foreach (var visitor in _visitors.Skip(counter))
        {
            _counter[node] = counter + 1;

            if (rewritten == default)
                rewritten = visitor.Visit(node);
            rewritten = visitor.Visit(node, rewritten);
        }

        _counter.Remove(node);

        return rewritten;
    }

    public T? Visit(Microsoft.CodeAnalysis.SyntaxNode oldNode, T newNode)
    {
        var counter = _counter[oldNode] as int? ?? _initialCounter;
        var rewritten = newNode;
        foreach (var visitor in _visitors.Skip(counter))
        {
            _counter[oldNode] = counter + 1;
            rewritten = visitor.Visit(oldNode, rewritten);
        }

        return rewritten;
    }

    public void AddVisitor(CompositeCSharpSyntaxVisitor<T> visitor)
    {
        _visitors.Add(visitor);
    }
}