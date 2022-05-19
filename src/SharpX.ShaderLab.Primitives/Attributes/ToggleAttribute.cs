// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.ShaderLab.Primitives.Attributes.Compiler;

namespace SharpX.ShaderLab.Primitives.Attributes;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public sealed class ToggleAttribute : PropertyAttribute
{
    public ToggleAttribute() { }

    public ToggleAttribute(string @ref) { }

    public ToggleAttribute(Type t) { }
}