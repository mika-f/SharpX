// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Hlsl.Primitives.Attributes.Compiler;

#pragma warning disable CS0626 // Method, operator, or accessor is marked external and has no attributes on it

namespace SharpX.Hlsl.Primitives.Types;

[ExternalComponent]
[Component("InputPatch<&T>")]
public class InputPatch<T>
{
    public extern T this[int index] { get; set; }
}