// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Hlsl.Primitives.Attributes;
using SharpX.Hlsl.Primitives.Types;

namespace SharpX.ShaderLab.Example;

public struct Vertex2Geometry
{
    [Semantic("SV_POSITION")]
    public Vector4<float> Vertex { get; set; }

    [Semantic("NORMAL")]
    public Vector3<float> Normal { get; set; }

    [Semantic("TEXCOORD")]
    public Vector2<float> TexCoord { get; set; }

    [Semantic("TEXCOORD1")]
    public Vector4<float> WorldPos { get; set; }

    [Semantic("TEXCOORD2")]
    public Vector3<float> LocalPos { get; set; }
}