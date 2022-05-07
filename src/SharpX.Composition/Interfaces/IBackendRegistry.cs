// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;

namespace SharpX.Composition.Interfaces;

public interface IBackendRegistry
{
    void RegisterBackendVisitor(string language, Type visitor, Type @return, uint priority);

    void RegisterReferences(string language, params string[] references);

    void RegisterExtensions(string language, Func<SyntaxNode, string> callback);
}