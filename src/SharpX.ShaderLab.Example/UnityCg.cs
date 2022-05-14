// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Hlsl.Primitives.Attributes.Compiler;
using SharpX.Hlsl.Primitives.Types;

namespace SharpX.ShaderLab.Example;

[ExternalComponent]
internal static class UnityCg
{
    public static extern Vector4<float> UnityObjectToClipPos(Vector4<float> a);

    public static extern Vector3<float> UnityObjectToWorldNorma(Vector3<float> a);

    [Name("TRANSFORM_TEX")]
    public static extern Vector2<float> TransformTexture(Vector4<float> a, Sampler2D b);
}