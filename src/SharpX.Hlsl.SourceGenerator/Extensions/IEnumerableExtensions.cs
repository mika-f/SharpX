// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;

namespace SharpX.Hlsl.SourceGenerator.Extensions;

// ReSharper disable once InconsistentNaming
internal static class IEnumerableExtensions
{
    public static bool None<T>(this IEnumerable<T> obj)
    {
        return !obj.Any();
    }
}