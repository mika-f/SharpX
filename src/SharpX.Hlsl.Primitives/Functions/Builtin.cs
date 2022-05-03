// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Hlsl.Primitives.Attributes.Compiler;
using SharpX.Hlsl.SourceGenerator.Attributes;

namespace SharpX.Hlsl.Primitives.Functions;

[FunctionSource("Functions/builtin.d.ts")]
[ExternalComponent]
public partial class Builtin
{
    [Name("mul")]
    public static extern T Mul<T>(object a, object b);

    [Name("transpose")]
    public static extern T Transpose<T>(object a);
}
