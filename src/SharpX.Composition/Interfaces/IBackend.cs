// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

namespace SharpX.Composition.Interfaces;

public interface IBackend
{
    void EntryPoint(IBackendRegistry registry);
}