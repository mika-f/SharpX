// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Composition.CSharp;
using SharpX.Composition.Interfaces;

namespace SharpX.ShaderLab.CSharp;

public class ShaderLabNodeVisitor : CompositeCSharpSyntaxVisitor<ShaderLabSyntaxNode>
{
    public ShaderLabNodeVisitor(IBackendVisitorArgs<ShaderLabSyntaxNode> args) : base(args) { }
}