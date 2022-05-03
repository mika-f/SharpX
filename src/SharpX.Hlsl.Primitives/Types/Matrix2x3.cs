// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Hlsl.Primitives.Attributes.Compiler;
using SharpX.Hlsl.SourceGenerator.Attributes;

namespace SharpX.Hlsl.Primitives.Types;

[Component("&T2x3")]
[ExternalComponent]
[Swizzle("_00", "_01", "_02", "_10", "_11", "_12")]
// ReSharper disable once InconsistentNaming
public partial class Matrix2x3<T>
{
    public Matrix2x3(Vector1<T> _) { }

    public Matrix2x3(Vector1<T> _00, Vector1<T> _01, Vector1<T> _02, Vector1<T> _10, Vector1<T> _11, Vector1<T> _12) { }

    public Matrix2x3(Vector2<T> _0, Vector2<T> _1, Vector2<T> _2) { }
}