// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Hlsl.Primitives.Attributes.Compiler;

#pragma warning disable CS0626 // Method, operator, or accessor is marked external and has no attributes on it

namespace SharpX.Hlsl.Primitives.Types;

[ExternalComponent]
[Component("AppendStructuredBuffer<&T>")]
public class AppendStructuredBuffer<T>
{
    public extern void Append(T value);

    public extern void GetDimensions(out uint numStructs, out uint stride);
}