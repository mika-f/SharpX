// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using System;

namespace SharpX.Hlsl.SourceGenerator;

internal sealed class CSharpSignature : IEquatable<CSharpSignature>
{
    public string Signature { get; }

    public string Name { get; }

    public CSharpSignature(string name, string signature)
    {
        Name = name;
        Signature = signature;
    }

    public bool Equals(CSharpSignature? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Signature == other.Signature && Name == other.Name;
    }

    public override bool Equals(object? obj)
    {
        return ReferenceEquals(this, obj) || (obj is CSharpSignature other && Equals(other));
    }

    public override int GetHashCode()
    {
        return Signature.GetHashCode();
    }
}