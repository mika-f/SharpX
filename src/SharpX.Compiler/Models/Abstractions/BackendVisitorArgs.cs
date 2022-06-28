// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using Microsoft.CodeAnalysis;

using SharpX.Composition.Interfaces;

using SyntaxNode = SharpX.Core.SyntaxNode;

#pragma warning disable CS8616

namespace SharpX.Compiler.Models.Abstractions;

internal class BackendVisitorArgs<TResult> : IBackendVisitorArgs<TResult> where TResult : SyntaxNode
{
    private readonly Func<Microsoft.CodeAnalysis.SyntaxNode?, TResult> _delegate1;
    private readonly Func<Microsoft.CodeAnalysis.SyntaxNode?, TResult, TResult> _delegate2;
    private readonly Dictionary<INamedTypeSymbol, string> _fileMappings;
    private readonly Func<string, Microsoft.CodeAnalysis.SyntaxNode?, SyntaxNode?> _invoker;

    public BackendVisitorArgs(SemanticModel model, Dictionary<INamedTypeSymbol, string> fileMappings, Func<Microsoft.CodeAnalysis.SyntaxNode?, TResult> delegate1, Func<Microsoft.CodeAnalysis.SyntaxNode?, TResult, TResult> delegate2,
                              Func<string, Microsoft.CodeAnalysis.SyntaxNode?, SyntaxNode?> invoker)
    {
        SemanticModel = model;
        _fileMappings = fileMappings;
        _delegate1 = delegate1;
        _delegate2 = delegate2;
        _invoker = invoker;
    }

    Func<Microsoft.CodeAnalysis.SyntaxNode?, TResult> IBackendVisitorArgs<TResult>.Delegate1 => _delegate1;

    Func<Microsoft.CodeAnalysis.SyntaxNode?, TResult, TResult> IBackendVisitorArgs<TResult>.Delegate2 => _delegate2;

    public SemanticModel SemanticModel { get; }

    public SyntaxNode? Invoke(string language, Microsoft.CodeAnalysis.SyntaxNode? node)
    {
        return _invoker.Invoke(language, node);
    }

    public string GetOutputFilePath(INamedTypeSymbol t)
    {
        return _fileMappings.TryGetValue(t, out var val) ? $"<#ref {val}>" : "<#ref unknown>";
    }
}