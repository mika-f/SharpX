// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

namespace SharpX.ShaderLab.Primitives.ShaderTarget;

[Flags]
public enum ShaderFeatures
{
    Derivatives = 1,

    Interpolators10 = 1 << 1,

    Interpolators15 = 1 << 2,

    Interpolators32 = 1 << 3,

    SampleLod = 1 << 4,

    FragCoord = 1 << 5,

    MultipleRenderTarget4 = 1 << 6,

    MultipleRenderTarget8 = 1 << 7,

    Integers = 1 << 8,

    Array2D = 1 << 9,

    ArrayCube = 1 << 10,

    Instancing = 1 << 11,

    Geometry = 1 << 12,

    Compute = 1 << 13,

    RandomWrite = 1 << 14,

    TessellationHardware = 1 << 15,

    Tessellation = 1 << 16,

    MultiSamplingTextureAccess = 1 << 17,

    SparseTexture = 1 << 18,

    FrameBufferFetch = 1 << 19
}