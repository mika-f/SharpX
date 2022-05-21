// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Hlsl.Primitives.Attributes.Compiler;
#if INCLUDE_MATRIX_INTELLISENSE
using SharpX.Hlsl.SourceGenerator.Attributes;
#endif

namespace SharpX.Hlsl.Primitives.Types;

[Component("&T4x2")]
[ExternalComponent]
#if INCLUDE_MATRIX_INTELLISENSE
[Swizzle("_00", "_01", "_10", "_11", "_20", "_21", "_30", "_31")]
#endif
// ReSharper disable once InconsistentNaming
public
#if INCLUDE_MATRIX_INTELLISENSE
    partial
#endif
    class Matrix4x2<T>
{
    public Matrix4x2(Vector1<T> _) { }

    public Matrix4x2(Vector1<T> _00, Vector1<T> _01, Vector1<T> _10, Vector1<T> _11, Vector1<T> _20, Vector1<T> _21, Vector1<T> _30, Vector1<T> _31) { }

    public Matrix4x2(Vector4<T> _0, Vector4<T> _1) { }
}