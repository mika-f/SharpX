// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using System.Diagnostics;
using System.IO;

namespace SharpX.Hlsl.SourceGenerator.TypeScript.Syntax;

[DebuggerDisplay("{GetDebuggerDisplay(), nq}")]
internal class DefaultTypeSyntax : TypeSyntax
{
    public TypeSyntax T { get; }

    public TypeSyntax Default { get; }

    public DefaultTypeSyntax(TypeSyntax t, TypeSyntax @default)
    {
        T = t;
        Default = @default;
    }

    public string GetDebuggerDisplay()
    {
        return ToFullString();
    }

    public override void Write(TextWriter writer)
    {
        T.Write(writer);

        writer.Write(" = ");

        Default.Write(writer);
    }
}