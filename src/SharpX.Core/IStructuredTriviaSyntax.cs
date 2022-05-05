// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

namespace SharpX.Core;

/// <summary>
///     represents <see cref="Microsoft.CodeAnalysis.IStructuredTriviaSyntax" />
/// </summary>
public interface IStructuredTriviaSyntax
{
    SyntaxTrivia ParentTrivia { get; }
}