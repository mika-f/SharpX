// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Hlsl.Primitives.Attributes.Compiler;

#pragma warning disable CS0626, CS0660, CS0661

namespace SharpX.Hlsl.Primitives.Types;

[Component("&T")]
[ExternalComponent]
public class Vector1<T>
{
    public static extern implicit operator T(Vector1<T> _);

    public static extern implicit operator Vector1<T>(T _);

    [ImplicitCastInCompiler]
    public static extern explicit operator Vector2<T>(Vector1<T> _);

    [ImplicitCastInCompiler]
    public static extern explicit operator Vector3<T>(Vector1<T> _);

    [ImplicitCastInCompiler]
    public static extern explicit operator Vector4<T>(Vector1<T> _);

    [ImplicitCastInCompiler]
    public static extern implicit operator bool(Vector1<T> _);

    public static extern bool operator true(Vector1<T> _);

    public static extern bool operator false(Vector1<T> _);

    public static extern bool operator >(Vector1<T> a, Vector1<T> b);

    public static extern bool operator >=(Vector1<T> a, Vector1<T> b);

    public static extern bool operator <(Vector1<T> a, Vector1<T> b);

    public static extern bool operator <=(Vector1<T> a, Vector1<T> b);

    public static extern bool operator !=(Vector1<T> a, Vector1<T> b);

    public static extern bool operator ==(Vector1<T> a, Vector1<T> b);

    public static extern Vector1<T> operator +(Vector1<T> a, Vector1<T> b);

    public static extern Vector1<T> operator -(Vector1<T> a, Vector1<T> b);

    public static extern Vector1<T> operator *(Vector1<T> a, Vector1<T> b);

    public static extern Vector1<T> operator /(Vector1<T> a, Vector1<T> b);

    public static extern Vector1<T> operator %(Vector1<T> a, Vector1<T> b);

    public static extern Vector1<T> operator +(Vector1<T> a, Vector1<int> b);

    public static extern Vector1<T> operator -(Vector1<T> a, Vector1<int> b);

    public static extern Vector1<T> operator *(Vector1<T> a, Vector1<int> b);

    public static extern Vector1<T> operator /(Vector1<T> a, Vector1<int> b);

    public static extern Vector1<T> operator %(Vector1<T> a, Vector1<int> b);

    public static extern Vector1<T> operator +(Vector1<T> a, Vector1<float> b);

    public static extern Vector1<T> operator -(Vector1<T> a, Vector1<float> b);

    public static extern Vector1<T> operator *(Vector1<T> a, Vector1<float> b);

    public static extern Vector1<T> operator /(Vector1<T> a, Vector1<float> b);

    public static extern Vector1<T> operator %(Vector1<T> a, Vector1<float> b);

    public static extern Vector1<T> operator +(Vector1<int> a, Vector1<T> b);

    public static extern Vector1<T> operator -(Vector1<int> a, Vector1<T> b);

    public static extern Vector1<T> operator *(Vector1<int> a, Vector1<T> b);

    public static extern Vector1<T> operator /(Vector1<int> a, Vector1<T> b);

    public static extern Vector1<T> operator %(Vector1<int> a, Vector1<T> b);

    public static extern Vector1<T> operator +(Vector1<float> a, Vector1<T> b);

    public static extern Vector1<T> operator -(Vector1<float> a, Vector1<T> b);

    public static extern Vector1<T> operator *(Vector1<float> a, Vector1<T> b);

    public static extern Vector1<T> operator /(Vector1<float> a, Vector1<T> b);

    public static extern Vector1<T> operator %(Vector1<float> a, Vector1<T> b);

    public static extern Vector1<T> operator +(Vector1<T> a, T b);

    public static extern Vector1<T> operator -(Vector1<T> a, T b);

    public static extern Vector1<T> operator *(Vector1<T> a, T b);

    public static extern Vector1<T> operator /(Vector1<T> a, T b);

    public static extern Vector1<T> operator %(Vector1<T> a, T b);

    public static extern Vector1<T> operator +(Vector1<T> a, Vector2<T> b);

    public static extern Vector1<T> operator -(Vector1<T> a, Vector2<T> b);

    public static extern Vector1<T> operator *(Vector1<T> a, Vector2<T> b);

    public static extern Vector1<T> operator /(Vector1<T> a, Vector2<T> b);

    public static extern Vector1<T> operator %(Vector1<T> a, Vector2<T> b);

    public static extern Vector1<T> operator +(Vector1<T> a, Vector3<T> b);

    public static extern Vector1<T> operator -(Vector1<T> a, Vector3<T> b);

    public static extern Vector1<T> operator *(Vector1<T> a, Vector3<T> b);

    public static extern Vector1<T> operator /(Vector1<T> a, Vector3<T> b);

    public static extern Vector1<T> operator %(Vector1<T> a, Vector3<T> b);

    public static extern Vector1<T> operator +(Vector1<T> a, Vector4<T> b);

    public static extern Vector1<T> operator -(Vector1<T> a, Vector4<T> b);

    public static extern Vector1<T> operator *(Vector1<T> a, Vector4<T> b);

    public static extern Vector1<T> operator /(Vector1<T> a, Vector4<T> b);

    public static extern Vector1<T> operator %(Vector1<T> a, Vector4<T> b);
}