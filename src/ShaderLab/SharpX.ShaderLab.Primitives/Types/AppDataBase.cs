// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Hlsl.Primitives.Attributes.Compiler;
using SharpX.Hlsl.Primitives.Types;

namespace SharpX.ShaderLab.Primitives.Types;

[Component("appdata_base")]
[ExternalComponent]
public struct AppDataBase
{
    [Name("vertex")]
    public Vector4<float> Vertex { get; }

    [Name("normal")]
    public Vector3<float> Normal { get; }

    [Name("texcoord")]
    public Vector4<float> TexCoord { get; }
}