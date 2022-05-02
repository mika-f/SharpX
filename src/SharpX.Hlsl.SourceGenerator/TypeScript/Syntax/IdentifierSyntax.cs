// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using System.Diagnostics;
using System.IO;

namespace SharpX.Hlsl.SourceGenerator.TypeScript.Syntax;

[DebuggerDisplay("{GetDebuggerDisplay(), nq}")]
internal class IdentifierSyntax : Syntax
{
    public Token Identifier { get; }

    public IdentifierSyntax(Token identifier)
    {
        Identifier = identifier;
    }

    public string GetDebuggerDisplay()
    {
        return ToFullString();
    }

    public override void Write(TextWriter writer)
    {
        writer.Write(Identifier.Value);
    }
}