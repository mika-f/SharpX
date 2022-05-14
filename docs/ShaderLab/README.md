# ShaderLab Compiler

C# to ShaderLab (`*.shader` and `*.cginc`).  

## Requirements

* SharpX.ShaderLab.dll
* (internal-reference) SharpX.Hlsl.dll
* (auto-reference) SharpX.ShaderLab.Primitives.dll
* (auto-reference) SharpX.Hlsl.Primitives.dll

## Example

```bash
$ sharpx.exe --target=shaderlab
```

Input Source Code (C#):

```csharp
// SomeShader.cs
[ShaderName("NatsunekoLaboratory/SomeShader/Opacity")]
[SubShader(typeof(SomeShader.SubShader))]
[CustomEditor(typeof(SomeShaderEditor))]
public class SomeShader
{
    // if you declare properties, fields, this transpile into shader properties.
    [Toggle]
    [DefaultValue(false)]
    public bool IsEnableSomeFeature { get; }

    [Enum(typeof(CullMode))]
    [DefaultValue(CullMode.Back)]
    public CullMode Culling { get; }

    // you declare sub-shader in another class or self.
    [ShaderTag(ShaderTag.PreviewType, PreviewType.Plane)]
    [ShaderTag(ShaderTag.ForceNoShadowCaster, false)]
    [ShaderPass(typeof(SomeShader.SubShader.Pass1), typeof(SomeShader.SubShader.Pass2))]
    public class SubShader
    {
        [GrabPass("SomeGrabPass")]
        [ShaderTags(ShaderTag.LightMode, LightMode.ForwardBase)]
        public class Pass1
        {

        }

        [RenderPass]
        [Blend(BlendMode.SrcAlpha, BlendMode.OneMinusSrcAlpha)]
        [Culling(nameof(SomeShader.Culling))]
        [ShaderPragma("vertex", nameof(SomeVertexShader.VertexMain))]
        [ShaderPragma("fragment", nameof(SomeFragmentShader.FragmentMain))]
        [ShaderFeature(ShaderFeatures.Geometry)]
        // if you want to separate shader program into some file, you can use `[ShaderProgram(params Type[] refs)]`. otherwise;  you can write program into this class.
        [ShaderProgram(typeof(SomeCoreShader), typeof(SomeVertexShader), typeof(SomeFragmentShader))]
        public class Pass2
        {

        }
    }
}
```

Output ShaderLab Code (Shader):

```shaderlab
Shader "NatsunekoLaboratory/SomeShader/Opacity"
{
    Properties
    {
        [Toggle(_)]
        IsEnableSomeFeature ("IsEnableSomeFeature", Int) = 0
        [Enum(UnityEngine.Rendering.CullMode)]
        Culling ("Culling", Float) = 0
    }

    SubShader
    {
        Tags
        {
            "PreviewType" = "Plane"
            "ForceNoShadowCaster" = "False"
        }

        GrabPass
        {
            "SomeGrabPass"

            Tags
            {
                "LightMode" = "ForwardBase"
            }
        }

        Pass
        {
            Name = "Pass2"
            Blend SrcAlpha OneMinusSrcAlpha
            Cull [Culling]

            CGPROGRAM

            #pragma vertex VertexMain
            #pragma fragment FragmentMain
            #pragma target 4.5

            #include "path/to/some-core-shader.cginc"
            #include "path/to/some-vertex-shader.cginc"
            #include "path/to/some-fragment-shader.cginc"

            ENDCG
        }
    }

    CustomEditor "SomeShaderEditor"
}
```
