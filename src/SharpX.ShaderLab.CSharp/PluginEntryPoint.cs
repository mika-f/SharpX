// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Composition.Attributes;
using SharpX.Composition.Interfaces;

namespace SharpX.ShaderLab.CSharp;

[Backend]
public class PluginEntryPoint : IBackend
{
    public void EntryPoint(IBackendRegistry registry)
    {
        registry.RegisterBackendVisitor("ShaderLab", typeof(ShaderLabNodeVisitor), typeof(ShaderLabSyntaxNode), 0);
        registry.RegisterReferences("ShaderLab", "./SharpX.ShaderLab.Primitives.dll", "./SharpX.Hlsl.Primitives.dll");
        registry.RegisterExtensions("ShaderLab", _ => "shader");
    }
}