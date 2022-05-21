// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using System.Diagnostics;
using System.IO;

namespace SharpX.Hlsl.SourceGenerator.TypeScript.Syntax;

[DebuggerDisplay("{GetDebuggerDisplay(), nq}")]
internal class ParameterDeclarationSyntax : Syntax
{
    public IdentifierSyntax Name { get; }

    public TypeSyntax Type { get; }

    public ParameterDeclarationSyntax(IdentifierSyntax name, TypeSyntax type)
    {
        Name = name;
        Type = type;
    }

    public string GetDebuggerDisplay()
    {
        return ToFullString();
    }

    public override void Write(TextWriter writer)
    {
        Name.Write(writer);
        writer.Write(" : ");
        Type.Write(writer);
    }
}