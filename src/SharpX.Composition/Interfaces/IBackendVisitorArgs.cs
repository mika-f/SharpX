// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using Microsoft.CodeAnalysis;

using SyntaxNode = SharpX.Core.SyntaxNode;

namespace SharpX.Composition.Interfaces;

public interface IBackendVisitorArgs<out TResult> where TResult : SyntaxNode
{
    protected internal Func<Microsoft.CodeAnalysis.SyntaxNode?, TResult> Delegate { get; }

    SemanticModel SemanticModel { get; }
}