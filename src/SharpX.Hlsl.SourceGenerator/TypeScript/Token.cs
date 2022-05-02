// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

namespace SharpX.Hlsl.SourceGenerator.TypeScript;

internal class Token
{
    public SyntaxKind Kind { get; }

    public string Value { get; }

    public Token(SyntaxKind kind, string value)
    {
        Kind = kind;
        Value = value;
    }
}