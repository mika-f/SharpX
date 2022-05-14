// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Hlsl.Primitives.Attributes;
using SharpX.Hlsl.Primitives.Functions;
using SharpX.Hlsl.Primitives.Types;
using SharpX.ShaderLab.Primitives.Functions;
using SharpX.ShaderLab.Primitives.Types;

namespace SharpX.ShaderLab.Example;

[Inline]
internal class VertexShader
{
    public static Vertex2Geometry VertexMain(AppDataFull i)
    {
        var obj = new Vertex2Geometry();
        obj.Vertex = UnityCg.UnityObjectToClipPos(i.Vertex);
        obj.Normal = UnityCg.UnityObjectToWorldNorma(i.Normal);
        obj.TexCoord = UnityCg.TransformTexture(i.TexCoord, VoxelShader.MainTexture);
        obj.WorldPos = Builtin.Mul<Vector4<float>>(UnityInjection.ObjectToWorld, i.Vertex);
        obj.LocalPos = i.Vertex.XYZ;

        return obj;
    }
}