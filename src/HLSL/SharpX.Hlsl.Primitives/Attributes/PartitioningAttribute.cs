// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Hlsl.Primitives.Enums;

namespace SharpX.Hlsl.Primitives.Attributes;

[AttributeUsage(AttributeTargets.Method)]
public class PartitioningAttribute : Attribute
{
    public PartitioningAttribute(PartitioningParameters parameter) { }
}