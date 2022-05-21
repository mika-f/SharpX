// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Hlsl.Primitives.Attributes.Compiler;

namespace SharpX.Hlsl.Primitives.Types;

#pragma warning disable CS0626 // Method, operator, or accessor is marked external and has no attributes on it

[ExternalComponent]
[Component("ConsumeStructuredBuffer<&T>")]
public class ConsumeStructuredBuffer<T>
{
    public extern T Consume();

    public extern void GetDimensions(out uint numStructs, out uint stride);
}