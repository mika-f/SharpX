// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using System.Diagnostics;
using System.IO;

namespace SharpX.Hlsl.SourceGenerator.TypeScript.Syntax;

[DebuggerDisplay("{GetDebuggerDisplay(), nq}")]
internal class GenericTypeSyntax : TypeSyntax
{
    public IdentifierSyntax Identifier { get; }

    public GenericsDeclarationSyntax Generics { get; }

    public GenericTypeSyntax(IdentifierSyntax identifier, GenericsDeclarationSyntax generics)
    {
        Identifier = identifier;
        Generics = generics;
    }

    public string GetDebuggerDisplay()
    {
        return ToFullString();
    }

    public override void Write(TextWriter writer)
    {
        Identifier.Write(writer);
        writer.Write("<");
        Generics.Write(writer);
        writer.Write(">");
    }
}