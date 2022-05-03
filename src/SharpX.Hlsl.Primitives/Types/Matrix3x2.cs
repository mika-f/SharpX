// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Hlsl.Primitives.Attributes.Compiler;
using SharpX.Hlsl.SourceGenerator.Attributes;

namespace SharpX.Hlsl.Primitives.Types;

[Component("&T3x2")]
[ExternalComponent]
[Swizzle("_00", "_01", "_10", "_11", "_20", "_21")]
// ReSharper disable once InconsistentNaming
public partial class Matrix3x2<T>
{
    public Matrix3x2(Vector1<T> _) { }

    public Matrix3x2(Vector1<T> _00, Vector1<T> _01, Vector1<T> _10, Vector1<T> _11, Vector1<T> _20, Vector1<T> _21) { }

    public Matrix3x2(Vector3<T> _0, Vector3<T> _1) { }
}