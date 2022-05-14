// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Hlsl.Primitives.Attributes;
using SharpX.Hlsl.Primitives.Types;

using static SharpX.Hlsl.Primitives.Functions.Builtin;

[Inline]
public struct Globals
{
    [Register("t0")]
    public static Texture2D ShadowMap { get; }

    [Register("t1")]
    public static Texture2D DiffuseMap { get; }

    [Register("t2")]
    public static Texture2D NormalMap { get; }

    [Register("s0")]
    public static SamplerState SampleWrap { get; }

    [Register("s1")]
    public static SamplerState SampleClamp { get; }

    public const int NUM_LIGHTS = 3;
    public const float SHADOW_DEPTH_BIAS = 0.000005f;
}

public struct LightState
{
    public Vector3<float> Position { get; set; }

    public Vector3<float> Direction { get; set; }

    public Vector4<float> Color { get; set; }

    public Vector4<float> Falloff { get; set; }

    public Matrix4x4<float> View { get; set; }

    public Matrix4x4<float> Projection { get; set; }
}

[CBuffer]
[Register("b0")]
public struct SceneConstantBuffer
{
    public static Matrix4x4<float> Model { get; }

    public static Matrix4x4<float> View { get; }

    public static Matrix4x4<float> Projection { get; }

    public static Vector4<float> AmbientColor { get; }

    public static bool SampleShadowMap { get; }

    [ConstantArray(Globals.NUM_LIGHTS)]
    public static LightState[] lights { get; }
}

public struct PSInput
{
    [Semantic("SV_POSITION")]
    public Vector4<float> Position { get; set; }

    [Semantic("POSITION")]
    public Vector4<float> WorldPos { get; set; }

    [Semantic("TEXCOORD")]
    public Vector2<float> UV { get; set; }

    [Semantic("NORMAL")]
    public Vector3<float> Normal { get; set; }

    [Semantic("TANGENT")]
    public Vector3<float> Tangent { get; set; }
}

[Inline]
public class Shader
{
    //--------------------------------------------------------------------------------------
    // Sample normal map, convert to signed, apply tangent-to-world space transform.
    //--------------------------------------------------------------------------------------
    public Vector3<float> CalcPerPixelNormal(Vector2<float> vTexcoord, Vector3<float> vVertNormal, Vector3<float> vVertTangent)
    {
        vVertNormal = Normalize(vVertNormal);
        vVertTangent = Normalize(vVertTangent);

        var vVertBinormal = Normalize(Cross(vVertTangent, vVertNormal));
        var mTangentSpaceToWorldSpace = new Matrix3x3<float>(vVertTangent, vVertBinormal, vVertNormal);

        var vBumpNormal = (Vector3<float>)Globals.NormalMap.Sample(Globals.SampleWrap, vTexcoord);
        vBumpNormal = 2.0f * vBumpNormal - 1.0f;

        return Mul<Vector3<float>>(vBumpNormal, mTangentSpaceToWorldSpace);
    }

    //--------------------------------------------------------------------------------------
    // Diffuse lighting calculation, with angle and distance falloff.
    //--------------------------------------------------------------------------------------
    public Vector4<float> CalcLightingColor(Vector3<float> vLightPos, Vector3<float> vLightDir, Vector4<float> vLightColor, Vector4<float> vFalloffs, Vector3<float> vPosWorld, Vector3<float> vPerPixelNormal)
    {
        var vLightToPixelUnNormalized = vPosWorld - vLightPos;

        var fDist = Length(vLightToPixelUnNormalized);
        var fDistFalloff = Saturate((vFalloffs.X - fDist) / vFalloffs.Y);

        var vLightToPixelNormalized = vLightToPixelUnNormalized / fDist;

        var fCosAngle = Dot(vLightToPixelNormalized, vLightDir / Length(vLightDir));
        var fAngleFalloff = Saturate((fCosAngle - vFalloffs.Z) / vFalloffs.Z);

        var fNDotL = Saturate(-Dot(vLightToPixelNormalized, vPerPixelNormal));

        return vLightColor * fNDotL * fDistFalloff * fAngleFalloff;
    }

    //--------------------------------------------------------------------------------------
    // Test how much pixel is in shadow, using 2x2 percentage-closer filtering.
    //--------------------------------------------------------------------------------------
    public Vector4<float> CalcUnshadowedAmountPCF2x2(int lightIndex, Vector4<float> vPosWorld)
    {
        var vLightSpacePos = vPosWorld;
        vLightSpacePos = Mul<Vector4<float>>(vLightSpacePos, SceneConstantBuffer.lights[lightIndex].View);
        vLightSpacePos = Mul<Vector4<float>>(vLightSpacePos, SceneConstantBuffer.lights[lightIndex].Projection);

        vLightSpacePos.XYZ /= vLightSpacePos.W;

        var vShadowTexCoord = 0.5f * vLightSpacePos.XY + 0.5f;
        vShadowTexCoord.Y = 1.0f - vShadowTexCoord.Y;

        var vLightSpaceDepth = vLightSpacePos.Z - Globals.SHADOW_DEPTH_BIAS;

        var vShadowMapDims = new Vector2<float>(1280.0f, 720.0f);
        var vSubPixelCoords = new Vector4<float>(1.0f, 1.0f, 1.0f, 1.0f);
        vSubPixelCoords.XY = Frac(vShadowMapDims * vShadowTexCoord);
        vSubPixelCoords.ZW = 1.0f - vSubPixelCoords.XY;

        var vBilinearWeights = vSubPixelCoords.XZXZ * vSubPixelCoords.WWYY;

        var vTexelUnits = 1.0f / vShadowMapDims;
        var vShadowDepth = new Vector4<float>(0);
        vShadowDepth.X = Globals.ShadowMap.Sample(Globals.SampleClamp, vShadowTexCoord);
        vShadowDepth.Y = Globals.ShadowMap.Sample(Globals.SampleClamp, vShadowTexCoord + new Vector2<float>(vTexelUnits.X, 0.0f));
        vShadowDepth.Z = Globals.ShadowMap.Sample(Globals.SampleClamp, vShadowTexCoord + new Vector2<float>(0.0f, vTexelUnits.Y));
        vShadowDepth.W = Globals.ShadowMap.Sample(Globals.SampleClamp, vShadowTexCoord + vTexelUnits);

        var vShadowTests = vShadowDepth >= (Vector4<float>)vLightSpaceDepth ? 1.0f : 0.0f;
        return (Vector4<float>)Dot(vBilinearWeights, new Vector4<float>(vShadowTests));
    }

    public PSInput VSMain([Semantic("POSITION")] Vector3<float> position, [Semantic("NORMAL")] Vector3<float> normal, [Semantic("TEXCOORD")] Vector2<float> uv, [Semantic("TANGENT")] Vector3<float> tangent)
    {
        var result = new PSInput();

        var newPosition = new Vector4<float>(position, 1.0f);

        normal.Z *= -1.0f;
        newPosition = Mul<Vector4<float>>(newPosition, SceneConstantBuffer.Model);

        result.WorldPos = newPosition;

        newPosition = Mul<Vector4<float>>(newPosition, SceneConstantBuffer.View);
        newPosition = Mul<Vector4<float>>(newPosition, SceneConstantBuffer.Projection);

        result.Position = newPosition;
        result.UV = uv;
        result.Normal = normal;
        result.Tangent = tangent;

        return result;
    }

    [return: Semantic("SV_TARGET")]
    public Vector4<float> PSMain(PSInput input)
    {
        var diffuseColor = Globals.DiffuseMap.Sample(Globals.SampleWrap, input.UV);
        var pixelNormal = CalcPerPixelNormal(input.UV, input.Normal, input.Tangent);
        var totalLight = SceneConstantBuffer.AmbientColor;

        for (var i = 0; i < Globals.NUM_LIGHTS; i++)
        {
            var light = SceneConstantBuffer.lights[i];
            var lightPass = CalcLightingColor(light.Position, light.Direction, light.Color, light.Falloff, input.WorldPos.XYZ, pixelNormal);
            if (SceneConstantBuffer.SampleShadowMap && i == 0)
                lightPass *= CalcUnshadowedAmountPCF2x2(i, input.WorldPos);

            totalLight += lightPass;
        }

        return diffuseColor * Saturate(totalLight);
    }
}