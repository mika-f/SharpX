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

    [Name("unity_ObjectToWorld")]
    public static Matrix4x4<float> ObjectToWorld { get; }

    [Name("unity_WorldToObject")]
    public static Matrix4x4<float> WorldToObject { get; }

    [Name("_WorldSpaceCameraPos")]
    public static Vector3<float> WorldSpaceCameraPos { get; }

    [Name("_ProjectionParams")]
    public static Vector4<float> ProjectionParams { get; }

    [Name("_ScreenParams")]
    public static Vector4<float> ScreenParams { get; }

    [Name("_ZBufferParams")]
    public static Vector4<float> ZBufferParams { get; }

    [Name("unity_OrthoParams")]
    public static Vector4<float> OrthtoParams { get; }

    [Name("unity_CameraProjection")]
    public static Matrix4x4<float> CameraProjection { get; }

    [Name("unity_CameraInvProjection")]
    public static Matrix4x4<float> CameraInvProjection { get; }

    [Name("unity_CameraWorldClipPlanes")]
    public static Vector4<float> CameraWorldClipPanes { get; }

    [Name("_Time")]
    public static Vector4<float> Time { get; }

    [Name("_SinTime")]
    public static Vector4<float> SinTime { get; }

    [Name("_CosTime")]
    public static Vector4<float> CosTime { get; }

    [Name("unity_DeltaTime")]
    public static Vector4<float> DeltaTime { get; }
}