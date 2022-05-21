// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Hlsl.Primitives.Attributes.Compiler;

namespace SharpX.Hlsl.Primitives.Types;

#pragma warning disable CS0626 // Method, operator, or accessor is marked external and has no attributes on it

// ReSharper disable once InconsistentNaming
[ExternalComponent]
[Component("StructuredBuffer<&T>")]
public class StructuredBuffer<T>
{
    public extern T this[uint index] { get; set; }

    public extern void GetDimensions(out uint numStructs, out uint stride);

    public extern T Load(int location);

    public extern T Load(int location, out uint status);
}