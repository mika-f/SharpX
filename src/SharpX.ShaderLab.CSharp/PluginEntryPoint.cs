// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Composition.Interfaces;
using SharpX.Hlsl;

using HlslNodeVisitor = SharpX.Hlsl.CSharp.NodeVisitor;

namespace SharpX.ShaderLab.CSharp;

public class PluginEntryPoint : IBackend
{
    public void EntryPoint(IBackendRegistry registry)
    {
        registry.RegisterBackendVisitor("ShaderLab", typeof(ShaderLabNodeVisitor), typeof(ShaderLabSyntaxNode), 0);
        registry.RegisterBackendVisitor("ShaderLab", typeof(HlslNodeVisitor), typeof(HlslSyntaxNode), 1);

        registry.RegisterReferences("ShaderLab", "./SharpX.ShaderLab.Primitives.dll");
        registry.RegisterReferences("ShaderLab", "./SharpX.Hlsl.Primitives.dll");

        registry.RegisterExtensions("ShaderLab", node =>
        {
            if (node is HlslSyntaxNode)
                return "cginc";
            return "shader";
        });
    }
}