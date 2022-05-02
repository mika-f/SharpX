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
internal class GenericsSyntax : Syntax
{
    public TypeSyntax T { get; }

    public List<TypeSyntax> OrTypes { get; }

    public GenericsConstraintSyntax? Constraint { get; }

    public GenericsSyntax(TypeSyntax t, GenericsConstraintSyntax? constraint)
    {
        T = t;
        OrTypes = new List<TypeSyntax>();
        Constraint = constraint;
    }

    public GenericsSyntax(TypeSyntax t, List<TypeSyntax> ors, GenericsConstraintSyntax? constraint)
    {
        T = t;
        OrTypes = ors;
        Constraint = constraint;
    }

    public GenericsSyntax WithGenericsConstraint(GenericsConstraintSyntax constraint)
    {
        return new GenericsSyntax(T, constraint);
    }

    public GenericsSyntax AddOrTypes(TypeSyntax t)
    {
        var types = OrTypes;
        types.Add(t);

        return new GenericsSyntax(T, OrTypes, Constraint);
    }

    public string GetDebuggerDisplay()
    {
        return ToFullString();
    }

    public override void Write(TextWriter writer)
    {
        T.Write(writer);

        if (OrTypes.Any())
            foreach (var t in OrTypes)
            {
                writer.Write(" | ");
                t.Write(writer);
            }

        if (Constraint != null)
            Constraint.Write(writer);
    }
}