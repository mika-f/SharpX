// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using System;
using System.Linq;

namespace SharpX.Hlsl.SourceGenerator.Extensions;

public static class StringExtensions
{
    public static string ToUpperCamelCase(this string str)
    {
        if (string.IsNullOrWhiteSpace(str) || str.Length == 0)
            return str;

        var words = str.Split(new[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
        return string.Concat(words.Select(w => char.ToUpperInvariant(w[0]) + w.Substring(1)));
    }

    public static string ToLowerCamelCase(this string str)
    {
        if (string.IsNullOrWhiteSpace(str) || str.Length == 0)
            return str;

        var words = str.Split(new[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
        var firstWord = words[0];
        return char.ToLowerInvariant(firstWord[0]) + firstWord.Substring(1) + string.Concat(words.Skip(1).Select(w => char.ToUpperInvariant(w[0]) + w.Substring(1)));
    }
}