// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace SharpX.Hlsl.SourceGenerator.TypeScript.Syntax;

[DebuggerDisplay("{GetDebuggerDisplay(), nq}")]
internal class GenericsDeclarationSyntax : Syntax
{
    public List<GenericsSyntax> Generics { get; }

    public GenericsDeclarationSyntax(List<GenericsSyntax> generics)
    {
        Generics = generics;
    }

    public string GetDebuggerDisplay()
    {
        return ToFullString();
    }

    public override void Write(TextWriter writer)
    {
        foreach (var (generic, i) in Generics.Select((w, i) => (w, i)))
        {
            generic.Write(writer);
            if (i != Generics.Count - 1)
                writer.Write(", ");
        }
    }
}