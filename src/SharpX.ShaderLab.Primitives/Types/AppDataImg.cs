using SharpX.Hlsl.Primitives.Attributes.Compiler;
using SharpX.Hlsl.Primitives.Types;

namespace SharpX.ShaderLab.Primitives.Types
{
    [Component("appdata_img")]
    [ExternalComponent]
    public struct AppDataImg
    {
        [Name("vertex")]
        public Vector4<float> Vertex { get; }

        [Name("texcoord")]
        public Vector2<Half> TexCoord { get; }
    }
}
