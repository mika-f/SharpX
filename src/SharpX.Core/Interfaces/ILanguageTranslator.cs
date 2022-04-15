// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace SharpX.Core.Interfaces;

public interface ILanguageTranslator
{
    CSharpSyntaxVisitor<SyntaxNode> GetVisitor(CSharpCompilation compilation, SemanticModel semanticModel);
}