// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.ShaderLab.Primitives.Enum;

namespace SharpX.ShaderLab.Primitives.Attributes.Compiler;

[AttributeUsage(AttributeTargets.Class)]
public class StencilAttribute : Attribute
{
    public int? Ref { get; init; }

    public string? RefS { get; init; }

    public CompareFunction? Comp { get; init; }

    public string? CompS { get; init; }

    public StencilOp? Pass { get; init; }

    public string? PassS { get; init; }

    public StencilOp? Fail { get; init; }

    public string? FailS { get; init; }

    public StencilOp? ZFail { get; init; }

    public string? ZFailS { get; init; }

    public int? ReadMask { get; init; }

    public string? ReadMaskS { get; init; }

    public int? WriteMask { get; init; }

    public string? WriteMaskS { get; init; }
}