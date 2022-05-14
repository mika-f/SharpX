// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using Microsoft.CodeAnalysis;

using SharpX.Composition.Interfaces;

using SyntaxNode = SharpX.Core.SyntaxNode;

namespace SharpX.Compiler.Models.Abstractions;

internal class BackendVisitorArgs<TResult> : IBackendVisitorArgs<TResult> where TResult : SyntaxNode
{
    private readonly Func<Microsoft.CodeAnalysis.SyntaxNode?, TResult> _delegate;
    private readonly Func<string, Microsoft.CodeAnalysis.SyntaxNode?, SyntaxNode?> _invoker;

    public BackendVisitorArgs(SemanticModel model, Func<Microsoft.CodeAnalysis.SyntaxNode?, TResult> @delegate, Func<string, Microsoft.CodeAnalysis.SyntaxNode?, SyntaxNode?> invoker)
    {
        SemanticModel = model;
        _delegate = @delegate;
        _invoker = invoker;
    }

    Func<Microsoft.CodeAnalysis.SyntaxNode?, TResult> IBackendVisitorArgs<TResult>.Delegate => _delegate;

    public SemanticModel SemanticModel { get; }

    public SyntaxNode? Invoke(string language, Microsoft.CodeAnalysis.SyntaxNode? node)
    {
        return _invoker.Invoke(language, node);
    }
}