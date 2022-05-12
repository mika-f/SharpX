// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.ShaderLab.Primitives.Enum;

namespace SharpX.ShaderLab.Primitives.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class BlendAttribute : Attribute
{
    public BlendAttribute(BlendFunc a, BlendFunc b) { }

    public BlendAttribute(BlendFunc a, BlendFunc b, BlendFunc c, BlendFunc d) { }
}