// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Composition.Attributes;
using SharpX.Composition.Interfaces;

namespace SharpX.ShaderLab.CSharp.ShaderTarget;

[Backend]
public class PluginEntryPoint : IBackend
{
    public void EntryPoint(IBackendRegistry registry)
    {
        var url = Path.GetDirectoryName(typeof(PluginEntryPoint).Assembly.Location);
        registry.RegisterBackendVisitor("ShaderLab", typeof(ShaderLabNodeVisitor), typeof(ShaderLabSyntaxNode), 1);
        registry.RegisterReferences("ShaderLab", Path.Combine(url, "SharpX.ShaderLab.Primitives.ShaderTarget.dll"));
    }
}