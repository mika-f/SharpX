// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Hlsl.Primitives.Attributes.Compiler;
#if INCLUDE_MATRIX_INTELLISENSE
using SharpX.Hlsl.SourceGenerator.Attributes;
#endif

namespace SharpX.Hlsl.Primitives.Types;

[Component("&T3x4")]
[ExternalComponent]
#if INCLUDE_MATRIX_INTELLISENSE
[Swizzle("_00", "_01", "_02", "_03", "_10", "_11", "_12", "_13", "_20", "_21", "_22", "_23")]
#endif
// ReSharper disable once InconsistentNaming
public
#if INCLUDE_MATRIX_INTELLISENSE
    partial
#endif
    class Matrix3x4<T>
{
    public Matrix3x4(Vector1<T> _) { }

    public Matrix3x4(Vector1<T> _00, Vector1<T> _01, Vector1<T> _02, Vector1<T> _03, Vector1<T> _10, Vector1<T> _11, Vector1<T> _12, Vector1<T> _13, Vector1<T> _20, Vector1<T> _21, Vector1<T> _22, Vector1<T> _23) { }

    public Matrix3x4(Vector3<T> _0, Vector3<T> _1, Vector3<T> _2, Vector3<T> _3) { }
}