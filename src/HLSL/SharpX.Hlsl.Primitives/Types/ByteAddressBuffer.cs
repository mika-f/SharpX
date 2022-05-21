// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Hlsl.Primitives.Attributes.Compiler;

#pragma warning disable CS0626 // Method, operator, or accessor is marked external and has no attributes on it

namespace SharpX.Hlsl.Primitives.Types;

[ExternalComponent]
public class ByteAddressBuffer
{
    public extern void GetDimensions(out uint dim);

    public extern uint Load(int address);

    public extern uint Load(int location, out uint status);

    public extern Vector2<uint> Load2(uint address);

    public extern Vector2<uint> Load2(uint location, out uint status);

    public extern Vector3<uint> Load3(uint address);

    public extern Vector3<uint> Load3(uint location, out uint status);

    public extern Vector4<uint> Load4(uint address);

    public extern Vector4<uint> Load4(uint location, out uint status);
}