// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using Microsoft.CodeAnalysis.CSharp;

using SharpX.Composition.Interfaces;
using SharpX.Core;

namespace SharpX.Composition.CSharp;

public abstract class CompositeCSharpSyntaxVisitor<TResult> : CSharpSyntaxVisitor<TResult> where TResult : SyntaxNode
{
    // it is injected from compiler
    private readonly Func<Microsoft.CodeAnalysis.SyntaxNode?, TResult?> _delegate;

    public CompositeCSharpSyntaxVisitor(IBackendVisitorArgs<TResult> args)
    {
        _delegate = args.Delegate;
    }

    public override TResult? Visit(Microsoft.CodeAnalysis.SyntaxNode? node)
    {
        if (node != null)
        {
            var v = ((CSharpSyntaxNode)node).Accept(this);
            if (v != null)
                return v;
            return _delegate.Invoke(node);
        }

        return default;
    }
}