// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using Microsoft.CodeAnalysis;

using SharpX.Composition.Interfaces;

namespace SharpX.Compiler.Models.Abstractions;

internal class BackendVisitorArgs : IBackendVisitorArgs
{
    public SemanticModel SemanticModel { get; init; }
}