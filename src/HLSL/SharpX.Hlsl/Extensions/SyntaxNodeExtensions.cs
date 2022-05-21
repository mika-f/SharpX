// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using Microsoft.CodeAnalysis;

namespace SharpX.Hlsl.Extensions;

public static class SyntaxNodeExtensions
{
    public static string ToTrimmedString(this SyntaxNode obj)
    {
        return obj.ToFullString().Trim();
    }
}