﻿// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

namespace SharpX.Hlsl.Primitives.Attributes;

[AttributeUsage(AttributeTargets.Method)]
public class NumThreadsAttribute : Attribute
{
    public NumThreadsAttribute(uint x, uint y, uint z) { }
}