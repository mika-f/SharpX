// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Hlsl.Primitives.Attributes.Compiler;
using SharpX.Hlsl.SourceGenerator.Attributes;

#pragma warning disable CS0626, CS0660, CS0661

namespace SharpX.Hlsl.Primitives.Types;

[Component("&T4")]
[ExternalComponent]
[Swizzle("X", "Y", "Z", "W")]
[Swizzle("R", "G", "B", "A")]
public sealed partial class Vector4<T>
{
    public Vector4(Vector1<T> _) { }

    public Vector4(Vector1<T> _1, Vector1<T> _2, Vector1<T> _3) { }

    public Vector4(Vector2<T> _1, Vector2<T> _2) { }

    public Vector4(Vector2<T> _1, Vector1<T> _2, Vector1<T> _3) { }

    public Vector4(Vector1<T> _1, Vector2<T> _2, Vector1<T> _3) { }

    public Vector4(Vector1<T> _1, Vector1<T> _2, Vector2<T> _3) { }

    public Vector4(Vector3<T> _1, Vector1<T> _2) { }

    public Vector4(Vector1<T> _1, Vector3<T> _2) { }

    public Vector4(Vector1<T> _1, Vector1<T> _2, Vector1<T> _3, Vector1<T> _4) { }

    [ImplicitCastInCompiler]
    public static explicit operator Vector4<T>(T _)
    {
        throw new NotImplementedException();
    }

    [ImplicitCastInCompiler]
    public static explicit operator Vector4<T>(Vector1<T> _)
    {
        throw new NotImplementedException();
    }

    [ImplicitCastInCompiler]
    public static explicit operator Vector4<T>(Vector2<T> _)
    {
        throw new NotImplementedException();
    }

    [ImplicitCastInCompiler]
    public static explicit operator Vector4<T>(Vector3<T> _)
    {
        throw new NotImplementedException();
    }

    public static bool operator >(Vector4<T> a, Vector4<T> b)
    {
        throw new NotImplementedException();
    }

    public static bool operator >=(Vector4<T> a, Vector4<T> b)
    {
        throw new NotImplementedException();
    }

    public static bool operator <(Vector4<T> a, Vector4<T> b)
    {
        throw new NotImplementedException();
    }

    public static bool operator <=(Vector4<T> a, Vector4<T> b)
    {
        throw new NotImplementedException();
    }

    public static bool operator !=(Vector4<T> a, Vector4<T> b)
    {
        throw new NotImplementedException();
    }


    public static bool operator ==(Vector4<T> a, Vector4<T> b)
    {
        throw new NotImplementedException();
    }


    public static Vector4<T> operator +(Vector4<T> a, Vector4<T> b)
    {
        throw new NotImplementedException();
    }


    public static Vector4<T> operator -(Vector4<T> a, Vector4<T> b)
    {
        throw new NotImplementedException();
    }

    public static Vector4<T> operator *(Vector4<T> a, Vector4<T> b)
    {
        throw new NotImplementedException();
    }

    public static Vector4<T> operator /(Vector4<T> a, Vector4<T> b)
    {
        throw new NotImplementedException();
    }

    public static Vector4<T> operator %(Vector4<T> a, Vector4<T> b)
    {
        throw new NotImplementedException();
    }

    public static Vector4<T> operator +(Vector4<T> a, T b)
    {
        throw new NotImplementedException();
    }

    public static Vector4<T> operator -(Vector4<T> a, T b)
    {
        throw new NotImplementedException();
    }

    public static Vector4<T> operator *(Vector4<T> a, T b)
    {
        throw new NotImplementedException();
    }

    public static Vector4<T> operator /(Vector4<T> a, T b)
    {
        throw new NotImplementedException();
    }

    public static Vector4<T> operator %(Vector4<T> a, T b)
    {
        throw new NotImplementedException();
    }

    public static Vector4<T> operator +(T a, Vector4<T> b)
    {
        throw new NotImplementedException();
    }

    public static Vector4<T> operator -(T a, Vector4<T> b)
    {
        throw new NotImplementedException();
    }

    public static Vector4<T> operator *(T a, Vector4<T> b)
    {
        throw new NotImplementedException();
    }

    public static Vector4<T> operator /(T a, Vector4<T> b)
    {
        throw new NotImplementedException();
    }

    public static Vector4<T> operator %(T a, Vector4<T> b)
    {
        throw new NotImplementedException();
    }

    public static Vector4<T> operator +(Vector4<T> a, Vector1<T> b)
    {
        throw new NotImplementedException();
    }

    public static Vector4<T> operator -(Vector4<T> a, Vector1<T> b)
    {
        throw new NotImplementedException();
    }

    public static Vector4<T> operator *(Vector4<T> a, Vector1<T> b)
    {
        throw new NotImplementedException();
    }

    public static Vector4<T> operator /(Vector4<T> a, Vector1<T> b)
    {
        throw new NotImplementedException();
    }

    public static Vector4<T> operator %(Vector4<T> a, Vector1<T> b)
    {
        throw new NotImplementedException();
    }

    public static Vector4<T> operator +(Vector4<T> a, Vector2<T> b)
    {
        throw new NotImplementedException();
    }

    public static Vector4<T> operator -(Vector4<T> a, Vector2<T> b)
    {
        throw new NotImplementedException();
    }

    public static Vector4<T> operator *(Vector4<T> a, Vector2<T> b)
    {
        throw new NotImplementedException();
    }

    public static Vector4<T> operator /(Vector4<T> a, Vector2<T> b)
    {
        throw new NotImplementedException();
    }

    public static Vector4<T> operator %(Vector4<T> a, Vector2<T> b)
    {
        throw new NotImplementedException();
    }

    public static Vector4<T> operator +(Vector4<T> a, Vector3<T> b)
    {
        throw new NotImplementedException();
    }

    public static Vector4<T> operator -(Vector4<T> a, Vector3<T> b)
    {
        throw new NotImplementedException();
    }

    public static Vector4<T> operator *(Vector4<T> a, Vector3<T> b)
    {
        throw new NotImplementedException();
    }

    public static Vector4<T> operator /(Vector4<T> a, Vector3<T> b)
    {
        throw new NotImplementedException();
    }

    public static Vector4<T> operator %(Vector4<T> a, Vector3<T> b)
    {
        throw new NotImplementedException();
    }
}