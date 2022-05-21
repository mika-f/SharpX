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
internal class TypeAliasSyntax : TypeDeclarationSyntax
{
    public TypeSyntax Left { get; }

    public List<TypeSyntax> Right { get; }

    public TypeAliasSyntax(TypeSyntax left, List<TypeSyntax> right) : base(left, new List<FunctionDeclarationSyntax>())
    {
        Left = left;
        Right = right;
    }

    public string GetDebuggerDisplay()
    {
        return ToFullString();
    }

    public override void Write(TextWriter writer)
    {
        Left.Write(writer);

        writer.Write(" = ");

        foreach (var (t, i) in Right.Select((w, i) => (w, i)))
        {
            t.Write(writer);

            if (i < Right.Count - 1)
                writer.Write(" | ");
        }
    }
}