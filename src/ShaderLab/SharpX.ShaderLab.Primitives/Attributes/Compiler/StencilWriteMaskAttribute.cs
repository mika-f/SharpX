// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

namespace SharpX.ShaderLab.Primitives.Attributes.Compiler;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public sealed class StencilWriteMaskAttribute : Attribute
{
    public StencilWriteMaskAttribute(int val) { }

    public StencilWriteMaskAttribute(string val) { }
}