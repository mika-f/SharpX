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

    public BackendVisitorArgs(SemanticModel model, Func<Microsoft.CodeAnalysis.SyntaxNode?, TResult> @delegate)
    {
        SemanticModel = model;
        _delegate = @delegate;
    }

    Func<Microsoft.CodeAnalysis.SyntaxNode?, TResult> IBackendVisitorArgs<TResult>.Delegate => _delegate;

    public SemanticModel SemanticModel { get; }
}