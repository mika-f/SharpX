// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Hlsl.Primitives.Attributes.Compiler;
using SharpX.Hlsl.SourceGenerator.Attributes;

namespace SharpX.Hlsl.Primitives.Types;

[Component("&T4x4")]
[ExternalComponent]
[Swizzle("_00", "_01", "_02", "_03", "_10", "_11", "_12", "_13", "_20", "_21", "_22", "_23", "_30", "_31", "_32", "_33")]

// ReSharper disable once InconsistentNaming
public partial class Matrix4x4<T>
{
    public Matrix4x4(Vector1<T> _) { }

    public Matrix4x4(Vector1<T> _00, Vector1<T> _01, Vector1<T> _02, Vector1<T> _03, Vector1<T> _10, Vector1<T> _11, Vector1<T> _12, Vector1<T> _13, Vector1<T> _20, Vector1<T> _21, Vector1<T> _22, Vector1<T> _23, Vector1<T> _30, Vector1<T> _31, Vector1<T> _32, Vector1<T> _33) { }

    public Matrix4x4(Vector4<T> _0, Vector4<T> _1, Vector4<T> _2, Vector4<T> _3) { }
}