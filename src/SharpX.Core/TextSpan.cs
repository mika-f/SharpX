// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

namespace SharpX.Core;

/// <summary>
///     re-implements <see cref="TextSpan" />
/// </summary>
public readonly struct TextSpan : IEquatable<TextSpan>, IComparable<TextSpan>
{
    public int Start { get; }

    public int Length { get; }

    public int End => Start + Length;

    public bool IsEmpty => Length == 0;

    public TextSpan(int start, int length)
    {
        if (start < 0)
            throw new ArgumentOutOfRangeException(nameof(start));
        if (start + length < start)
            throw new ArgumentOutOfRangeException(nameof(length));

        Start = start;
        Length = length;
    }

    public bool Contains(TextSpan span)
    {
        return span.Start >= Start && span.End <= End;
    }

    public bool IntersectsWith(TextSpan span)
    {
        return span.Start <= End && span.End >= Start;
    }

    public bool Equals(TextSpan other)
    {
        return Start == other.Start && Length == other.Length;
    }

    public int CompareTo(TextSpan other)
    {
        var diff = Start - other.Start;
        if (diff != 0) return diff;

        return Length - other.Length;
    }
}