// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Hlsl.Primitives.Attributes.Compiler;
using SharpX.Hlsl.SourceGenerator.Attributes;

namespace SharpX.Hlsl.Primitives.Types;

[Component("&T2x2")]
[ExternalComponent]
[Swizzle("_00", "_01", "_10", "_11")]
// ReSharper disable once InconsistentNaming
public partial class Matrix2x2<T>
{
    public Matrix2x2(Vector1<T> _) { }

    public Matrix2x2(Vector1<T> _1, Vector1<T> _2, Vector1<T> _3, Vector1<T> _4) { }

    public Matrix2x2(Vector2<T> _1, Vector2<T> _2) { }
}