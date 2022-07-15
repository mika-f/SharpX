// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

namespace SharpX.Hlsl.CSharp.ObjectInitializer.Extensions;

#if !NET5_0_OR_GREATER
internal static class StackExtensions
{
    public static bool TryPeek<T>(this Stack<T> obj, out T? @out) where T : class
    {
        if (obj.Count != 0)
        {
            @out = obj.Peek();
            return true;
        }

        @out = null;
        return false;
    }
}
#endif