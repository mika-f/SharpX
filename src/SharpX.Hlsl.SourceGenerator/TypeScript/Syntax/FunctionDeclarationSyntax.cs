// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace SharpX.Hlsl.SourceGenerator.TypeScript.Syntax;

[DebuggerDisplay("{GetDebuggerDisplay(),nq}")]
internal class FunctionDeclarationSyntax : Syntax
{
    public IdentifierSyntax Identifier { get; }

    public GenericsDeclarationSyntax? Generics { get; }

    public List<ParameterDeclarationSyntax> Parameters { get; }

    public TypeSyntax ReturnType { get; }

    public FunctionDeclarationSyntax(IdentifierSyntax identifier, GenericsDeclarationSyntax? generics, List<ParameterDeclarationSyntax> parameters, TypeSyntax returnType)
    {
        Identifier = identifier;
        Generics = generics;
        Parameters = parameters;
        ReturnType = returnType;
    }

    public string GetDebuggerDisplay()
    {
        return ToFullString();
    }

    public override void Write(TextWriter writer)
    {
        Identifier.Write(writer);

        if (Generics != null)
        {
            writer.Write("<");
            Generics.Write(writer);
            writer.Write(">");
        }

        writer.Write("(");

        foreach (var (parameter, i) in Parameters.Select((w, i) => (w, i)))
        {
            parameter.Write(writer);

            if (i < Parameters.Count - 1)
                writer.Write(", ");
        }

        writer.Write(") : ");
        ReturnType.Write(writer);
    }
}