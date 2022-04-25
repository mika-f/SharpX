# Target: WebGL

C# to WebGL (`*.glsl`).  
CommandLine:

```bash
$ sharpx.exe --input=path/to/source.cs --target=webgl --output=/path/to/output/source.glsl
```


Input Source Code (C#):

```csharp
[Precision(Precision.Medium, "float")]
public class VertexShader
{
    [Attribute]
    public Vector2 _position;

    [Inline("gl_Position")]
    public Vector2 _glPosition;

    [VertexShader]
    public void Vertex()
    {
        _glPosition = new Vector4(_position, 0.0f, 1.0f);
    }
}

[Precision(Precision.Medium, "float")]
public class FragmentShader
{
    [Uniform]
    public float _time;

    [Uniform]
    public float _height;

    [Uniform]
    public float _width;

    [Define("COLOR_SAKURA")]
    public Vector4 Sakura = new Vector4(255.0f / 255.0f, 179.0f / 255.0f, 217.0f / 255.0f);

    [Inline("gl_FragColor")]
    public Vector4 _glFragColor;

    [FragmentShader]
    public void Fragment()
    {   
        _glFragColor = Sakura;
    }
}
```

Output WebGL Code:

```glsl
// vert.glsl
precision mediump float;
attribute vec2 _position;

void main()
{
    gl_Position = vec4(_position, 0.0, 1.0);
}

// frag.glsl
precision mediump float;
uniform float _time;
uniform float _height;
uniform float _width;

#define COLOR_SAKURA vec4(255.0 / 255.0, 179.0 / 255.0, 217.0 / 255.0)

void main()
{
    gl_FragColor = COLOR_SAKURA;
}
```