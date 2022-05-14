// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

namespace SharpX.ShaderLab.Primitives.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class StencilCompareAttribute : Attribute
{
    public StencilCompareAttribute(StencilCompareAttribute val) { }

    public StencilCompareAttribute(string val) { }
}