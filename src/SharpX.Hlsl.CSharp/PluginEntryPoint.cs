// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using System.Diagnostics;

using SharpX.Composition.Attributes;
using SharpX.Composition.Interfaces;

namespace SharpX.Hlsl.CSharp;

[Backend]
public class PluginEntryPoint : IBackend
{
    public void EntryPoint(IBackendRegistry registry)
    {
        Debug.WriteLine("");
        registry.RegisterBackendVisitor("HLSL", typeof(NodeVisitor), 0);
    }
}