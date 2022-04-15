// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace SharpX.Core.Utilities;

public static class SharpXAssert
{
    [Conditional("DEBUG")]
    public static void Assert([DoesNotReturnIf(false)] bool cond, string? message)
    {
        Debug.Assert(cond, message ?? string.Empty);
    }

    [Conditional("DEBUG")]
    public static void AssertNotNull<T>([NotNull] T value) where T : class?
    {
        Assert(value != null, "Unexpected null reference");
    }
}