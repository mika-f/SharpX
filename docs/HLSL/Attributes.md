# Attributes

## CBuffer

`CBufferAttribute` is specified when you want to compiled as Constant Buffer for a `struct` declaration.

### Example

Source:

```csharp
[CBuffer]
[Register("b0")]
public struct SceneConstantBuffer
{
    public Vector4<float> Projection { get; }
}
```

Output:

```hlsl
cbuffer SceneConstantBuffer : register(b0)
{
    float4 Projection;
};
```


## Inline

`InlineAttribute` is specified when you want to compiled as inline (extract to higher level).

### Example

Source:

```csharp
[Inline]
public class Shader
{
    public void Hello() {}
}
```

Output:

```hlsl
void Hello()
{
}
```