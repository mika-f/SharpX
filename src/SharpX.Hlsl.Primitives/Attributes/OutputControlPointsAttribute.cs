// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

namespace SharpX.Hlsl.Primitives.Attributes;

[AttributeUsage(AttributeTargets.Method)]
public class OutputControlPointsAttribute : Attribute
{
    public OutputControlPointsAttribute(uint num) { }
}