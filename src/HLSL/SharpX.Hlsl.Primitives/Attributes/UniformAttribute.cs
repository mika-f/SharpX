// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

namespace SharpX.Hlsl.Primitives.Attributes;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class UniformAttribute : Attribute
{
    public UniformAttribute(uint? num = null) { }
}