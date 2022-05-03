// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Hlsl.Primitives.Attributes.Compiler;

namespace SharpX.Hlsl.Primitives.Types;

[Component("PointStream<&T>")]
[ExternalComponent]
public interface IPointStream<in T> : IVertexStream<T> { }