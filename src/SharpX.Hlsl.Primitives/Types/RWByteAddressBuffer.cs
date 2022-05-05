// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Hlsl.Primitives.Attributes.Compiler;

namespace SharpX.Hlsl.Primitives.Types;

#pragma warning disable CS0626 // Method, operator, or accessor is marked external and has no attributes on it

[ExternalComponent]
// ReSharper disable once InconsistentNaming
public class RWByteAddressBuffer
{
    public extern void GetDimensions(out uint dim);

    public extern void InterlockedAdd(uint dest, uint value, out uint originalValue);

    public extern void InterlockedAnd(uint dest, uint value, out uint originalValue);

    public extern void InterlockedCompareExchange(uint dest, uint compareValue, uint value, out uint originalValue);

    public extern void InterlockedCompareStore(uint dest, uint compareValue, uint value);

    public extern void InterlockedExchange(uint dest, uint value, out uint originalValue);

    public extern void InterlockedMax(uint dest, uint value, out uint originalValue);

    public extern void InterlockedMin(uint dest, uint value, out uint originalValue);

    public extern void InterlockedOr(uint dest, uint value, out uint originalValue);

    public extern void InterlockedXor(uint dest, uint value, out uint originalValue);

    public extern uint Load(int location);

    public extern uint Load(int location, out uint status);

    public extern Vector2<uint> Load2(uint address);

    public extern Vector2<uint> Load2(uint address, out uint status);

    public extern Vector3<uint> Load3(uint address);

    public extern Vector3<uint> Load3(uint address, out uint status);

    public extern Vector4<uint> Load4(uint address);

    public extern Vector4<uint> Load4(uint address, out uint status);

    public extern void Store(uint address, uint value);

    public extern void Store2(uint address, Vector2<uint> value);

    public extern void Store3(uint address, Vector3<uint> value);

    public extern void Store4(uint address, Vector4<uint> value);
}