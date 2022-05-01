// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using System.Collections.Generic;

namespace SharpX.Hlsl.SourceGenerator;

// ReSharper disable once InconsistentNaming
internal sealed class MSBuildOptionsComparer : IEqualityComparer<MSBuildOptions>
{
    public bool Equals(MSBuildOptions x, MSBuildOptions y)
    {
        return x.Equals(y);
    }

    public int GetHashCode(MSBuildOptions obj)
    {
        return obj.GetHashCode();
    }
}