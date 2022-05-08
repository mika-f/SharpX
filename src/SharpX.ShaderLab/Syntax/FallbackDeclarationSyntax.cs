// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.ShaderLab.Syntax.InternalSyntax;

namespace SharpX.ShaderLab.Syntax;

public class FallbackDeclarationSyntax : ShaderLabSyntaxNode
{
    public SyntaxToken FallbackKeyword => new(this, ((FallbackDeclarationSyntaxInternal)Green).FallbackKeyword, Position, 0);

    public SyntaxToken ShaderNameOrOffKeyword => new(this, ((FallbackDeclarationSyntaxInternal)Green).ShaderNameOrOffKeyword, GetChildPosition(1), GetChildIndex(1));

    internal FallbackDeclarationSyntax(ShaderLabSyntaxNodeInternal node, SyntaxNode? parent, int position) : base(node, parent, position) { }

    public override SyntaxNode? GetNodeSlot(int index)
    {
        return null;
    }

    public override SyntaxNode? GetCachedSlot(int index)
    {
        return null;
    }

    public FallbackDeclarationSyntax Update(SyntaxToken fallbackKeyword, SyntaxToken shaderNameOrOffKeyword)
    {
        if (fallbackKeyword != FallbackKeyword || shaderNameOrOffKeyword != ShaderNameOrOffKeyword)
            return SyntaxFactory.FallbackDeclaration(fallbackKeyword, shaderNameOrOffKeyword);
        return this;
    }

    public FallbackDeclarationSyntax WithFallbackKeyword(SyntaxToken fallbackKeyword)
    {
        return Update(fallbackKeyword, ShaderNameOrOffKeyword);
    }

    public FallbackDeclarationSyntax WithShaderNameOrOffKeyword(SyntaxToken shaderNameOrOffKeyword)
    {
        return Update(FallbackKeyword, shaderNameOrOffKeyword);
    }

    public override TResult? Accept<TResult>(ShaderLabSyntaxVisitor<TResult> visitor) where TResult : default
    {
        return visitor.VisitFallbackDeclaration(this);
    }
}