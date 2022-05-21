// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Hlsl.Primitives.Attributes.Compiler;

namespace SharpX.Hlsl.Primitives.Types;

#pragma warning disable CS0626 // Method, operator, or accessor is marked external and has no attributes on it

// ReSharper disable once InconsistentNaming
[ExternalComponent]
public class SamplerState
{
    public extern object AddressU { private get; set; }

    public extern object AddressV { private get; set; }

    public extern object AddressW { private get; set; }

    public extern object BorderColor { private get; set; }

    public extern object Filter { private get; set; }

    public extern object MaxAnisotropy { private set; get; }

    public extern object MaxLOD { private set; get; }

    public extern object MinLOD { private set; get; }

    public extern object MipLODBias { private set; get; }
}