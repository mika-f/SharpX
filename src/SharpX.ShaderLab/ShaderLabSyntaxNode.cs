// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;

namespace SharpX.ShaderLab;

public abstract class ShaderLabSyntaxNode : SyntaxNode
{
    public override string Language => "ShaderLab";

    public SyntaxKind Kind => (SyntaxKind)Green.RawKind;

    internal new ShaderLabSyntaxNode? Parent => (ShaderLabSyntaxNode?)base.Parent;

    protected ShaderLabSyntaxNode(GreenNode node, SyntaxNode? parent, int position) : base(node, parent, position) { }

    protected override SyntaxNode NormalizeWhitespaceCore(string indentation, string eol, bool elasticTrivia)
    {
        throw new NotImplementedException();
    }

    public abstract TResult? Accept<TResult>(ShaderLabSyntaxVisitor<TResult> visitor);
}