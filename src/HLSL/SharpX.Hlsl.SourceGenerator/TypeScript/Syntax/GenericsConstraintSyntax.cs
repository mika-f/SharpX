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
internal class GenericsConstraintSyntax : Syntax
{
    public List<TypeSyntax> Constraints { get; }

    public GenericsConstraintSyntax(List<TypeSyntax> constraints)
    {
        Constraints = constraints;
    }

    public string GetDebuggerDisplay()
    {
        return ToFullString();
    }

    public override void Write(TextWriter writer)
    {
        writer.Write(" extends ");

        foreach (var (constraint, i) in Constraints.Select((w, i) => (w, i)))
        {
            constraint.Write(writer);

            if (i < Constraints.Count - 1)
                writer.Write(" | ");
        }
    }
}