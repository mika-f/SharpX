// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Composition.Attributes;
using SharpX.Composition.Interfaces;

namespace SharpX.Hlsl.CSharp;

[Backend]
public class PluginEntryPoint : IBackend
{
    public void EntryPoint(IBackendRegistry registry)
    {
        var url = Path.GetDirectoryName(typeof(PluginEntryPoint).Assembly.Location);
        registry.RegisterBackendVisitor("HLSL", typeof(NodeVisitor), typeof(HlslSyntaxNode), 0);
        registry.RegisterReferences("HLSL", Path.Combine(url, "SharpX.Hlsl.Primitives.dll"));
        registry.RegisterExtensions("HLSL", _ => "hlsl");
    }
}