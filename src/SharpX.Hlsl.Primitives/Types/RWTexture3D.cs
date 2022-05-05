// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Hlsl.Primitives.Attributes.Compiler;

namespace SharpX.Hlsl.Primitives.Types;

#pragma warning disable CS0626 // Method, operator, or accessor is marked external and has no attributes on it

// ReSharper disable once InconsistentNaming
[ExternalComponent]
[Component("RWTexture3D<&T>")]
public class RWTexture3D<T>
{
    public extern T this[Vector3<uint> index] { get; set; }

    public extern void GetDimensions(out uint width, out uint height, out uint depth);

    public extern T Load(int location);

    public extern T Load(int location, out uint status);
}