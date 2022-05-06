// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using Microsoft.CodeAnalysis;

namespace SharpX.Hlsl.Extensions;

public static class SyntaxTokenExtensions
{
    public static string ToTrimmedString(this SyntaxToken token)
    {
        return token.ToFullString().Trim();
    }
}