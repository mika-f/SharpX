// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.ShaderLab.Primitives.Attributes.Compiler;

namespace SharpX.ShaderLab.Primitives.Enum;

[UnityName("UnityEngine.Rendering.CompareFunction")]
public enum ZTestFunc
{
    Less,

    Greater,

    LEqual,

    GEqual,

    Equal,

    NotEqual,

    Always
}