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

    public static IEnumerable<T[]> Combination<T>(this IEnumerable<T> items, int pick, bool withRepetition)
    {
        if (pick == 1)
        {
            foreach (var item in items)
                yield return new[] { item };

            yield break;
        }

        var array = items.ToList();
        foreach (var item in array)
        {
            var leftSide = new[] { item };
            var remaining = withRepetition ? array : array.SkipWhile(w => !w!.Equals(item)).Skip(1).ToList();

            foreach (var rightSide in Combination(remaining, pick - 1, withRepetition))
                yield return leftSide.Concat(rightSide).ToArray();
        }
    }
}