// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

namespace SharpX.Compiler.Extensions;

// ReSharper disable once InconsistentNaming
public static class IAsyncEnumerableExtensions
{
    public static IAsyncEnumerable<T> NonNullable<T>(this IAsyncEnumerable<T?> obj)
    {
        return obj.Where(w => w != null).Select(w => w!);
    }
}