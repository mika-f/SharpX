// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

namespace SharpX.ShaderLab.Primitives.ShaderTarget;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public sealed class ShaderFeatureAttribute : Attribute
{
    public ShaderFeatureAttribute(ShaderFeatures value) { }
}