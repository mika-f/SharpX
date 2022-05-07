# Primitives

## Vector&lt;T&gt;, Vector1&lt;T&gt;, Vector2&lt;T&gt;, Vector3&lt;T&gt;

If you want to use `int1`, `int2`, `int3`, and `int4` and other vector components, you can use it instead of `intX`.  
For Example: If you want to use `float3`, you use `Vector3<float>` instead.

## Matrix2x2&lt;T&gt;, ...

If you want to use `int2x2` and other matrix components, you can use it instead of `intXxX`.

### INCLUDE_MATRIX_INTELLISENSE

By default, Matrix types are not support to property accesses for reduce library binary size.  
If you want to access properties of matrix, you should use full packages of `SharpX.Hlsl.Primitives.dll`.