// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Hlsl.Primitives.Attributes.Compiler;
using SharpX.Hlsl.Primitives.Types;

namespace SharpX.ShaderLab.Primitives.Types;

[Component("appdata_img")]
[ExternalComponent]
public struct AppDataImg
{
    [Name("vertex")]
    public Vector4<float> Vertex { get; }

    [Name("texcoord")]
    public Vector2<float> TexCoord { get; }
}