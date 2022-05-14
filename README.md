# SharpX - C# Transpiler

C# to **any language** Transpiler written in C#, powered by Roslyn.

## Supports

- ShaderLab [working]
- HLSL [working]
- GLSL [planned]
- WebGL [planned]

## Implementation

SharpX is a C# AST to X AST compiler.  
The X AST-worthy part is done with a minimal implementation of the Red-Green Trees used in the Roslyn Compiler.  
For more information, see the core implementation `SharpX.Core` or `SharpX.Hlsl`.

## Objective

The goals of SharpX is to output shader code for each platform (Unity, WebGL, ShaderToy, others) from a single C# source code.  
We should also like to use a new C# features, like Babel, a JavaScript Compiler, to reduce the amount of code in the shader itself.

## License

* MIT by [@6jz](https://twitter.com/6jz)  
* Some codes is taken from [dotnet/roslyn](https://github.com/dotnet/roslyn) - [MIT](https://github.com/dotnet/roslyn/blob/main/License.txt) by .NET Foundation and Contributors