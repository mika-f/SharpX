// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Composition.Interfaces;
using SharpX.Core;

namespace SharpX.Compiler.Models.Abstractions;

internal class BackendContainer
{
    private readonly SortedList<uint, Type> _visitors;

    public string Language { get; }

    public BackendContainer(string language)
    {
        Language = language;
        _visitors = new SortedList<uint, Type>();
    }

    public void Register(uint priority, Type visitor)
    {
        _visitors.Add(priority, visitor);
    }

    public SyntaxNode RunAsync(IBackendVisitorArgs args)
    {
        return default;
    }
}