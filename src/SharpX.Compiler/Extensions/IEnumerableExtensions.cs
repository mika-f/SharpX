// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

namespace SharpX.Compiler.Extensions;

// ReSharper disable once InconsistentNaming
internal static class IEnumerableExtensions
{
    public static IEnumerable<T> NonNullable<T>(this IEnumerable<T?> obj)
    {
        return obj.Where(w => w != null).Select(w => w!);
    }
}