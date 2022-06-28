// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using Microsoft.CodeAnalysis;

using SyntaxNode = SharpX.Core.SyntaxNode;

namespace SharpX.Composition.Interfaces;

public interface IBackendVisitorArgs<TResult> where TResult : SyntaxNode
{
    protected internal Func<Microsoft.CodeAnalysis.SyntaxNode?, TResult> Delegate1 { get; }

    protected internal Func<Microsoft.CodeAnalysis.SyntaxNode?, TResult?, TResult> Delegate2 { get; }

    SemanticModel SemanticModel { get; }

    SyntaxNode? Invoke(string language, Microsoft.CodeAnalysis.SyntaxNode? syntaxNode);

    string GetOutputFilePath(INamedTypeSymbol t);
}