// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Hlsl.Primitives.Attributes.Compiler;
using SharpX.Hlsl.SourceGenerator.Attributes;

namespace SharpX.Hlsl.Primitives.Types;

[Component("&T4x3")]
[ExternalComponent]
[Swizzle("_00", "_01", "_02", "_10", "_11", "_12", "_20", "_21", "_22", "_30", "_31", "_32")]
// ReSharper disable once InconsistentNaming
public class Matrix4x3<T>
{
    public Matrix4x3(Vector1<T> _) { }

    public Matrix4x3(Vector1<T> _00, Vector1<T> _01, Vector1<T> _02, Vector1<T> _10, Vector1<T> _11, Vector1<T> _12, Vector1<T> _20, Vector1<T> _21, Vector1<T> _22, Vector1<T> _30, Vector1<T> _31, Vector1<T> _32) { }

    public Matrix4x3(Vector4<T> _0, Vector4<T> _1, Vector4<T> _2) { }
}