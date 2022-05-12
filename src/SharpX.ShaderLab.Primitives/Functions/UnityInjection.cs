// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Hlsl.Primitives.Attributes.Compiler;
using SharpX.Hlsl.Primitives.Types;

namespace SharpX.ShaderLab.Primitives.Functions;

[ExternalComponent]
public static class UnityInjection
{
    [Name("UNITY_NEAR_CLIP_VALUE")]
    public static float NearClipPoint { get; }

    [Name("UNITY_MATRIX_MVP")]
    public static Matrix4x4<float> MatrixMVP { get; }

    [Name("UNITY_MATRIX_MV")]
    public static Matrix4x4<float> MatrixMV { get; }

    [Name("UNITY_MATRIX_V")]
    public static Matrix4x4<float> MatrixV { get; }

    [Name("UNITY_MATRIX_P")]
    public static Matrix4x4<float> MatrixP { get; }

    [Name("UNITY_MATRIX_VP")]
    public static Matrix4x4<float> MatrixVP { get; }

    [Name("UNITY_MATRIX_T_MV")]
    public static Matrix4x4<float> MatrixTMV { get; }

    [Name("UNITY_MATRIX_IT_MV")]
    public static Matrix4x4<float> MatrixITMV { get; }
}