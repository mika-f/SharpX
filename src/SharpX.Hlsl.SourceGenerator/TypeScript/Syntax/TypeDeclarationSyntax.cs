// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;

namespace SharpX.Hlsl.SourceGenerator.TypeScript.Syntax;

internal class TypeDeclarationSyntax : MemberDeclarationSyntax
{
    public TypeSyntax Type { get; }

    public List<FunctionDeclarationSyntax> Functions { get; }

    public TypeDeclarationSyntax(TypeSyntax type, List<FunctionDeclarationSyntax> functions)
    {
        Type = type;
        Functions = functions;
    }

    public override void Write(TextWriter writer)
    {
        throw new NotImplementedException();
    }
}