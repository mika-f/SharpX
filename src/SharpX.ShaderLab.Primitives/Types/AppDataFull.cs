// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Hlsl.Primitives.Attributes.Compiler;
using SharpX.Hlsl.Primitives.Types;

namespace SharpX.ShaderLab.Primitives.Types;

[Component("appdata_full")]
[ExternalComponent]
public struct AppDataFull
{
    [Name("vertex")]
    public Vector4<float> Vertex { get; }

    [Name("tangent")]
    public Vector4<float> Tangent { get; }

    [Name("normal")]
    public Vector3<float> Normal { get; }

    [Name("texcoord")]
    public Vector4<float> TexCoord { get; }

    [Name("texcoord1")]
    public Vector4<float> TexCoord1 { get; }

    [Name("texcoord2")]
    public Vector4<float> TexCoord2 { get; }

    [Name("texcoord3")]
    public Vector4<float> TexCoord3 { get; }

    [Name("color")]
    public Vector4<float> Color { get; }
}