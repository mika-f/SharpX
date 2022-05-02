// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;

namespace SharpX.Hlsl.SourceGenerator.TypeScript.Syntax;

internal class ExportStatementSyntax : MemberDeclarationSyntax
{
    public List<MemberDeclarationSyntax> Members { get; }

    public ExportStatementSyntax(List<MemberDeclarationSyntax> members)
    {
        Members = members;
    }

    public override void Write(TextWriter writer)
    {
        throw new NotImplementedException();
    }
}