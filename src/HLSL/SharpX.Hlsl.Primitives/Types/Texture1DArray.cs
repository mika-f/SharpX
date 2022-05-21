// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Hlsl.Primitives.Attributes.Compiler;

namespace SharpX.Hlsl.Primitives.Types;

#pragma warning disable CS0626 // Method, operator, or accessor is marked external and has no attributes on it

// ReSharper disable once InconsistentNaming
[ExternalComponent]
public class Texture1DArray
{
    public extern void GetDimensions(uint mipLevel, out uint width, out uint elements, out uint numberOfLevels);

    public extern T Load<T>(int location, int offset, out uint status);

    public extern float Sample(SamplerState state, Vector2<float> location);

    public extern float Sample(SamplerState state, Vector2<float> location, int offset);

    public extern float Sample(SamplerState state, Vector2<float> location, int offset, float clamp);

    public extern float Sample(SamplerState state, Vector2<float> location, int offset, float clamp, out uint status);

    public extern float SampleBias(SamplerState state, Vector2<float> location, float bias, int offset, float clamp);

    public extern float SampleBias(SamplerState state, Vector2<float> location, float bias, int offset, float clamp, out uint status);

    public extern float SampleCmp(SamplerState state, Vector2<float> location, float compareValue, int offset, float clamp);

    public extern float SampleCmp(SamplerState state, Vector2<float> location, float compareValue, int offset, float clamp, out uint status);

    public extern float SampleCmpLevelZero(SamplerState state, Vector2<float> location, float compareValue, int offset);

    public extern float SampleCmpLevelZero(SamplerState state, Vector2<float> location, float compareValue, int offset, out uint status);

    public extern float SampleGrad(SamplerState state, Vector2<float> location, float ddx, float ddy, int offset, float clamp);

    public extern float SampleGrad(SamplerState state, Vector2<float> location, float ddx, float ddy, int offset, float clamp, out uint status);

    public extern float CalculateLevelOfDetail(SamplerState state, float x);

    public extern float CalculateLevelOfDetailUnclamped(SamplerState state, float x);
}