// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Hlsl.Primitives.Attributes;
using SharpX.Hlsl.Primitives.Attributes.Compiler;
using SharpX.Hlsl.Primitives.Types;
using SharpX.ShaderLab.Primitives.Attributes;
using SharpX.ShaderLab.Primitives.Attributes.Compiler;
using SharpX.ShaderLab.Primitives.Enum;

namespace SharpX.ShaderLab.Example;

[ShaderName("NatsunekoLaboratory/SakuraShader/Avatars/Effects")]
[SubShader(typeof(SubShader0))]
[Inline]
public class VoxelShader
{
    [ShaderTag(ShaderTags.RenderType, RenderType.Opaque)]
    [ShaderTag(ShaderTags.Queue, RenderQueue.Transparent)]
    [ShaderTag(ShaderTags.DisableBatching, true)]
    [ShaderPass(typeof(PassVoxel), typeof(PassHolograph))]
    public class SubShader0
    {
        [RenderPass]
        [Culling(Culling.Off)]
        [ZWrite(ZWrite.On)]
        [Stencil(RefS = nameof(StencilRef), CompS = nameof(StencilCmp), PassS = nameof(StencilPass), FailS = nameof(StencilFail), ZFailS = nameof(StencilZFail), ReadMaskS = nameof(StencilReadMask), WriteMaskS = nameof(StencilWriteMask))]
        [ShaderFeature(ShaderFeatures.Geometry)]
        [ShaderVertex("")]
        [ShaderFragment("")]
        public class PassVoxel { }

        [RenderPass]
        [Blend(BlendFunc.SrcAlpha, BlendFunc.OneMinusSrcAlpha)]
        [ZWrite(ZWrite.On)]
        [Stencil(RefS = nameof(StencilRef), CompS = nameof(StencilCmp), PassS = nameof(StencilPass), FailS = nameof(StencilFail), ZFailS = nameof(StencilZFail), ReadMaskS = nameof(StencilReadMask), WriteMaskS = nameof(StencilWriteMask))]
        [ShaderFeature(ShaderFeatures.Geometry)]
        [ShaderVertex("")]
        [ShaderFragment("")]
        [ShaderProgram]
        public class PassHolograph { }
    }

    #region Coloring

    [MainTexture]
    [DefaultValue("white")]
    public static Sampler2D MainTexture { get; }

    [Name("MainTexture_ST")]
    [DefaultValue("(1, 1, 1, 1)")]
    public static Vector4<float> MainTextureST { get; }

    [MainColor]
    [Color]
    [DefaultValue("(1, 1, 1, 1)")]
    public static Vector4<float> Color { get; }

    #endregion

    #region Rim Lighting

    [Toggle]
    [DefaultValue(false)]
    public static bool IsEnableRimLighting { get; }

    [Color]
    [DefaultValue("(1, 1, 1, 1)")]
    public static Vector4<float> RimLightingColor { get; }

    [Range(0, 1)]
    [DefaultValue(1)]
    public static float RimLightingPower { get; }

    #endregion

    #region Voxelization

    [Toggle]
    [DefaultValue(false)]
    public static bool IsEnableVoxelization { get; }

    [Enum(typeof(VoxelSource))]
    [DefaultValue(VoxelSource.ShaderProperty)]
    public static VoxelSource VoxelSource { get; }

    [DefaultValue(0.00125)]
    public static float VoxelSize { get; }

    [DefaultValue("(1, 1, 1, 1)")]
    public static Vector4<float> VoxelSizeRatio { get; }

    [Enum(typeof(UvSamplingSource))]
    [DefaultValue(UvSamplingSource.Center)]
    public static UvSamplingSource VoxelUvSamplingSource { get; }

    [Color]
    [DefaultValue("(0, 0, 0, 1)")]
    public static Vector4<float> VoxelColor { get; }

    [Toggle]
    [DefaultValue(false)]
    public static bool IsEnableVoxelBoundary { get; }


    [Range(-0.5f, 1.5f)]
    [DefaultValue(1)]
    public static float VoxelBoundaryX { get; }

    [Range(-0.5f, 1.5f)]
    [DefaultValue(1)]
    public static float VoxelBoundaryY { get; }

    [Range(-0.5f, 1.5f)]
    [DefaultValue(1)]
    public static float VoxelBoundaryZ { get; }

    [Range(0, 1)]
    [DefaultValue(0.0025)]
    public static float VoxelBoundaryRange { get; }

    [DefaultValue(7.5)]
    [Range(0, 10)]
    public static float VoxelBoundaryFactor { get; }

    [Enum(typeof(BoundaryOperator))]
    [DefaultValue(BoundaryOperator.LessThan)]
    public static BoundaryOperator VoxelBoundaryOperator { get; }

    [DefaultValue("(0, 0, 0, 0)")]
    public static Vector4<float> VoxelOffset { get; }

    #endregion

    #region Holograph

    [Toggle]
    [DefaultValue(false)]
    public static bool IsEnableTriangleHolograph { get; }

    [Range(0, 1)]
    [DefaultValue(1)]
    public static float HolographAlphaTransparency { get; }

    [Range(0, 1)]
    [DefaultValue(0.0125)]
    public static float HolographHeight { get; }

    #endregion

    #region Stencil

    [DefaultValue(0)]
    public static int StencilRef { get; }

    [Enum(typeof(CompareFunction))]
    [DefaultValue(CompareFunction.Disabled)]
    public static CompareFunction StencilCmp { get; }

    [Enum(typeof(StencilOp))]
    [DefaultValue(StencilOp.Keep)]
    public static StencilOp StencilPass { get; }

    [Enum(typeof(StencilOp))]
    [DefaultValue(StencilOp.Keep)]
    public static StencilOp StencilFail { get; }

    [Enum(typeof(StencilOp))]
    [DefaultValue(StencilOp.Keep)]
    public static StencilOp StencilZFail { get; }

    [DefaultValue(0)]
    public static int StencilReadMask { get; }

    [DefaultValue(0)]
    public static int StencilWriteMask { get; }

    #endregion

    #region Meta

    [HideInInspector]
    public static int FoldoutStatus1 { get; }

    [HideInInspector]
    public static int FoldoutStatus2 { get; }

    #endregion
}