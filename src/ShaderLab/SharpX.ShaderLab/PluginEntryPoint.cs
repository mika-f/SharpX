// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Composition.Attributes;
using SharpX.Composition.Interfaces;

namespace SharpX.ShaderLab;

[Backend]
public class PluginEntryPoint : IBackend
{
    public string ContextId => "HLSL";
    {
        // nop
    }
}