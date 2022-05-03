// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Hlsl.Primitives.Attributes.Compiler;
using SharpX.Hlsl.SourceGenerator.Attributes;

#pragma warning disable CS0626, CS0660, CS0661

namespace SharpX.Hlsl.Primitives.Types;

[Component("&T3")]
[ExternalComponent]
[Swizzle("X", "Y", "Z")]
[Swizzle("R", "G", "B")]
public sealed partial class Vector3<T>
{
    public Vector3(Vector1<T> _) { }

    public Vector3(Vector1<T> _1, Vector1<T> _2) { }

    public Vector3(Vector1<T> _1, Vector2<T> _2) { }

    public Vector3(Vector2<T> _1, Vector1<T> _2) { }

    public Vector3(Vector1<T> _1, Vector1<T> _2, Vector1<T> _3) { }

    [ImplicitCastInCompiler]
    public static extern explicit operator Vector1<T>(Vector3<T> _);

    [ImplicitCastInCompiler]
    public static extern explicit operator Vector3<T>(Vector2<T> _);

    [ImplicitCastInCompiler]
    public static extern explicit operator Vector4<T>(Vector3<T> _);

    public static extern bool operator >(Vector3<T> a, Vector3<T> b);

    public static extern bool operator >=(Vector3<T> a, Vector3<T> b);

    public static extern bool operator <(Vector3<T> a, Vector3<T> b);

    public static extern bool operator <=(Vector3<T> a, Vector3<T> b);

    public static extern bool operator !=(Vector3<T> a, Vector3<T> b);

    public static extern bool operator ==(Vector3<T> a, Vector3<T> b);

    public static extern Vector3<T> operator +(Vector3<T> a, Vector3<T> b);

    public static extern Vector3<T> operator -(Vector3<T> a, Vector3<T> b);

    public static extern Vector3<T> operator *(Vector3<T> a, Vector3<T> b);

    public static extern Vector3<T> operator /(Vector3<T> a, Vector3<T> b);

    public static extern Vector3<T> operator %(Vector3<T> a, Vector3<T> b);

    public static extern Vector3<T> operator +(Vector3<T> a, Vector3<int> b);

    public static extern Vector3<T> operator -(Vector3<T> a, Vector3<int> b);

    public static extern Vector3<T> operator *(Vector3<T> a, Vector3<int> b);

    public static extern Vector3<T> operator /(Vector3<T> a, Vector3<int> b);

    public static extern Vector3<T> operator %(Vector3<T> a, Vector3<int> b);

    public static extern Vector3<T> operator +(Vector3<T> a, Vector3<float> b);

    public static extern Vector3<T> operator -(Vector3<T> a, Vector3<float> b);

    public static extern Vector3<T> operator *(Vector3<T> a, Vector3<float> b);

    public static extern Vector3<T> operator /(Vector3<T> a, Vector3<float> b);

    public static extern Vector3<T> operator %(Vector3<T> a, Vector3<float> b);

    public static extern Vector3<T> operator +(Vector3<int> a, Vector3<T> b);

    public static extern Vector3<T> operator -(Vector3<int> a, Vector3<T> b);

    public static extern Vector3<T> operator *(Vector3<int> a, Vector3<T> b);

    public static extern Vector3<T> operator /(Vector3<int> a, Vector3<T> b);

    public static extern Vector3<T> operator %(Vector3<int> a, Vector3<T> b);

    public static extern Vector3<T> operator +(Vector3<float> a, Vector3<T> b);

    public static extern Vector3<T> operator -(Vector3<float> a, Vector3<T> b);

    public static extern Vector3<T> operator *(Vector3<float> a, Vector3<T> b);

    public static extern Vector3<T> operator /(Vector3<float> a, Vector3<T> b);

    public static extern Vector3<T> operator %(Vector3<float> a, Vector3<T> b);

    public static extern Vector3<T> operator +(Vector3<T> a, T b);

    public static extern Vector3<T> operator -(Vector3<T> a, T b);

    public static extern Vector3<T> operator *(Vector3<T> a, T b);

    public static extern Vector3<T> operator /(Vector3<T> a, T b);

    public static extern Vector3<T> operator %(Vector3<T> a, T b);

    public static extern Vector3<T> operator +(Vector3<T> a, Vector1<T> b);

    public static extern Vector3<T> operator -(Vector3<T> a, Vector1<T> b);

    public static extern Vector3<T> operator *(Vector3<T> a, Vector1<T> b);

    public static extern Vector3<T> operator /(Vector3<T> a, Vector1<T> b);

    public static extern Vector3<T> operator %(Vector3<T> a, Vector1<T> b);

    public static extern Vector3<T> operator +(Vector3<T> a, Vector2<T> b);

    public static extern Vector3<T> operator -(Vector3<T> a, Vector2<T> b);

    public static extern Vector3<T> operator *(Vector3<T> a, Vector2<T> b);

    public static extern Vector3<T> operator /(Vector3<T> a, Vector2<T> b);

    public static extern Vector3<T> operator %(Vector3<T> a, Vector2<T> b);

    public static extern Vector3<T> operator +(Vector3<T> a, Vector4<T> b);

    public static extern Vector3<T> operator -(Vector3<T> a, Vector4<T> b);

    public static extern Vector3<T> operator *(Vector3<T> a, Vector4<T> b);

    public static extern Vector3<T> operator /(Vector3<T> a, Vector4<T> b);

    public static extern Vector3<T> operator %(Vector3<T> a, Vector4<T> b);
}