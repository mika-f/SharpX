// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Hlsl.Primitives.Attributes.Compiler;

namespace SharpX.Hlsl.Primitives.Types;

#pragma warning disable CS0626 // Method, operator, or accessor is marked external and has no attributes on it

// ReSharper disable once InconsistentNaming
[ExternalComponent]
public class Texture2D
{
    public extern Vector4<float> Gather(SamplerState state, Vector2<float> location, Vector2<int> offset);

    public extern Vector4<float> Gather(SamplerState state, Vector2<float> location, Vector2<int> offset, out uint status);

    public extern Vector4<float> GatherAlpha(SamplerState state, Vector2<float> location, Vector2<int> offset);

    public extern Vector4<float> GatherAlpha(SamplerState state, Vector2<float> location, Vector2<int> offset, out uint status);

    public extern Vector4<float> GatherAlpha(SamplerState state, Vector2<float> location, Vector2<int> offset1, Vector2<int> offset2, Vector2<int> offset3, Vector2<int> offset4);

    public extern Vector4<float> GatherAlpha(SamplerState state, Vector2<float> location, Vector2<int> offset1, Vector2<int> offset2, Vector2<int> offset3, Vector2<int> offset4, out uint status);

    public extern Vector4<float> GatherBlue(SamplerState state, Vector2<float> location, Vector2<int> offset);

    public extern Vector4<float> GatherBlue(SamplerState state, Vector2<float> location, Vector2<int> offset, out uint status);

    public extern Vector4<float> GatherBlue(SamplerState state, Vector2<float> location, Vector2<int> offset1, Vector2<int> offset2, Vector2<int> offset3, Vector2<int> offset4);

    public extern Vector4<float> GatherBlue(SamplerState state, Vector2<float> location, Vector2<int> offset1, Vector2<int> offset2, Vector2<int> offset3, Vector2<int> offset4, out uint status);

    public extern Vector4<float> GatherCmp(SamplerComparisonState state, Vector2<float> location, float compareValue, Vector1<int> offset);

    public extern Vector4<float> GatherCmp(SamplerComparisonState state, Vector2<float> location, float compareValue, Vector1<int> offset, out uint status);

    public extern Vector4<float> GatherCmpAlpha(SamplerComparisonState state, Vector2<float> location, float compareValue, Vector2<int> offset);

    public extern Vector4<float> GatherCmpAlpha(SamplerComparisonState state, Vector2<float> location, float compareValue, Vector2<int> offset, out uint status);

    public extern Vector4<float> GatherCmpAlpha(SamplerComparisonState state, Vector2<float> location, float compareValue, Vector2<int> offset1, Vector2<int> offset2, Vector2<int> offset3, Vector2<int> offset4);

    public extern Vector4<float> GatherCmpAlpha(SamplerComparisonState state, Vector2<float> location, float compareValue, Vector2<int> offset1, Vector2<int> offset2, Vector2<int> offset3, Vector2<int> offset4, out uint status);

    public extern Vector4<float> GatherCmpBlue(SamplerComparisonState state, Vector2<float> location, float compareValue, Vector2<int> offset);

    public extern Vector4<float> GatherCmpBlue(SamplerComparisonState state, Vector2<float> location, float compareValue, Vector2<int> offset, out uint status);

    public extern Vector4<float> GatherCmpBlue(SamplerComparisonState state, Vector2<float> location, float compareValue, Vector2<int> offset1, Vector2<int> offset2, Vector2<int> offset3, Vector2<int> offset4);

    public extern Vector4<float> GatherCmpBlue(SamplerComparisonState state, Vector2<float> location, float compareValue, Vector2<int> offset1, Vector2<int> offset2, Vector2<int> offset3, Vector2<int> offset4, out uint status);

    public extern Vector4<float> GatherCmpGreen(SamplerComparisonState state, Vector2<float> location, float compareValue, Vector2<int> offset);

    public extern Vector4<float> GatherCmpGreen(SamplerComparisonState state, Vector2<float> location, float compareValue, Vector2<int> offset, out uint status);

    public extern Vector4<float> GatherCmpGreen(SamplerComparisonState state, Vector2<float> location, float compareValue, Vector2<int> offset1, Vector2<int> offset2, Vector2<int> offset3, Vector2<int> offset4);

    public extern Vector4<float> GatherCmpGreen(SamplerComparisonState state, Vector2<float> location, float compareValue, Vector2<int> offset1, Vector2<int> offset2, Vector2<int> offset3, Vector2<int> offset4, out uint status);

    public extern Vector4<float> GatherCmpRed(SamplerComparisonState state, Vector2<float> location, float compareValue, Vector2<int> offset);

    public extern Vector4<float> GatherCmpRed(SamplerComparisonState state, Vector2<float> location, float compareValue, Vector2<int> offset, out uint status);

    public extern Vector4<float> GatherCmpRed(SamplerComparisonState state, Vector2<float> location, float compareValue, Vector2<int> offset1, Vector2<int> offset2, Vector2<int> offset3, Vector2<int> offset4);

    public extern Vector4<float> GatherCmpRed(SamplerComparisonState state, Vector2<float> location, float compareValue, Vector2<int> offset1, Vector2<int> offset2, Vector2<int> offset3, Vector2<int> offset4, out uint status);

    public extern Vector4<float> GatherGreen(SamplerState state, Vector2<float> location, Vector2<int> offset);

    public extern Vector4<float> GatherGreen(SamplerState state, Vector2<float> location, Vector2<int> offset, out uint status);

    public extern Vector4<float> GatherGreen(SamplerState state, Vector2<float> location, Vector2<int> offset1, Vector2<int> offset2, Vector2<int> offset3, Vector2<int> offset4);

    public extern Vector4<float> GatherGreen(SamplerState state, Vector2<float> location, Vector2<int> offset1, Vector2<int> offset2, Vector2<int> offset3, Vector2<int> offset4, out uint status);

    public extern Vector4<float> GatherRed(SamplerState state, Vector2<float> location, Vector2<int> offset);

    public extern Vector4<float> GatherRed(SamplerState state, Vector2<float> location, Vector2<int> offset, out uint status);

    public extern Vector4<float> GatherRed(SamplerState state, Vector2<float> location, Vector2<int> offset1, Vector2<int> offset2, Vector2<int> offset3, Vector2<int> offset4);

    public extern Vector4<float> GatherRed(SamplerState state, Vector2<float> location, Vector2<int> offset1, Vector2<int> offset2, Vector2<int> offset3, Vector2<int> offset4, out uint status);

    public extern void GetDimensions(uint mipLevel, out uint width, out uint height, out uint numberOfLevels);

    public extern float Sample(SamplerState state, Vector2<float> location);

    public extern float Sample(SamplerState state, Vector2<float> location, Vector2<int> offset);

    public extern float Sample(SamplerState state, Vector2<float> location, Vector2<int> offset, float clamp);

    public extern float Sample(SamplerState state, Vector2<float> location, Vector2<int> offset, float clamp, out uint status);

    public extern float SampleBias(SamplerState state, Vector2<float> location, float bias);

    public extern float SampleBias(SamplerState state, Vector2<float> location, float bias, Vector2<int> offset);

    public extern float SampleBias(SamplerState state, Vector2<float> location, float bias, Vector2<int> offset, float clamp);

    public extern float SampleBias(SamplerState state, Vector2<float> location, float bias, Vector2<int> offset, float clamp, out uint status);

    public extern float SampleCmp(SamplerState state, Vector2<float> location, float compareValue);

    public extern float SampleCmp(SamplerState state, Vector2<float> location, float compareValue, Vector2<int> offset);

    public extern float SampleCmp(SamplerState state, Vector2<float> location, float compareValue, Vector2<int> offset, float clamp);

    public extern float SampleCmp(SamplerState state, Vector2<float> location, float compareValue, Vector2<int> offset, float clamp, out uint status);

    public extern float SampleGrad(SamplerState state, Vector2<float> location, Vector2<float> ddx, Vector2<float> ddy);

    public extern float SampleGrad(SamplerState state, Vector2<float> location, Vector2<float> ddx, Vector2<float> ddy, Vector2<int> offset);

    public extern float SampleGrad(SamplerState state, Vector2<float> location, Vector2<float> ddx, Vector2<float> ddy, Vector2<int> offset, float clamp);

    public extern float SampleGrad(SamplerState state, Vector2<float> location, Vector2<float> ddx, Vector2<float> ddy, Vector2<int> offset, float clamp, out uint status);

    public extern float SampleLevel(SamplerState state, Vector2<float> location, float lod);

    public extern float SampleLevel(SamplerState state, Vector2<float> location, float lod, Vector2<int> offset);

    public extern float SampleLevel(SamplerState state, Vector2<float> location, float lod, Vector2<int> offset, float clamp);

    public extern float SampleLevel(SamplerState state, Vector2<float> location, float lod, Vector2<int> offset, float clamp, out uint status);
}