// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

namespace SharpX.ShaderLab.Primitives.Enum;

[Flags]
public enum ShaderFeatures
{
    Derivatives,

    Interpolators10,

    Interpolators15,

    Interpolators32,

    SampleLod,

    FragCoord,

    MultipleRenderTarget4,

    MultipleRenderTarget8,

    Integers,

    Array2D,

    ArrayCube,

    Instancing,

    Geometry,

    Compute,

    RandomWrite,

    TessellationHardware,

    Tessellation,

    MultiSamplingTextureAccess,

    SparseTexture,

    FrameBufferFetch
}