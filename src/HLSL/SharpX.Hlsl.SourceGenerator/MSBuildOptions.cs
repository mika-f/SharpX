// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using System;

namespace SharpX.Hlsl.SourceGenerator;

// ReSharper disable once InconsistentNaming
internal sealed class MSBuildOptions : IEquatable<MSBuildOptions>
{
    public string ProjectDirectory { get; }

    public bool IsDesignTimeBuild { get; }

    public MSBuildOptions(string projectDirectory, bool isDesignTimeBuild)
    {
        ProjectDirectory = projectDirectory;
        IsDesignTimeBuild = isDesignTimeBuild;
    }

    public bool Equals(MSBuildOptions? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return ProjectDirectory == other.ProjectDirectory && IsDesignTimeBuild == other.IsDesignTimeBuild;
    }

    public override bool Equals(object? obj)
    {
        return ReferenceEquals(this, obj) || (obj is MSBuildOptions other && Equals(other));
    }

    public override int GetHashCode()
    {
        return ProjectDirectory.GetHashCode() * (IsDesignTimeBuild ? -1 : 1);
    }
}