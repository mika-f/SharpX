// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using System.Collections.Generic;

namespace SharpX.Hlsl.SourceGenerator.TypeScript.Syntax;

internal sealed class CompilationUnitSyntax
{
    public List<MemberDeclarationSyntax> Members { get; }

    public CompilationUnitSyntax(List<MemberDeclarationSyntax> members)
    {
        Members = members;
    }
}