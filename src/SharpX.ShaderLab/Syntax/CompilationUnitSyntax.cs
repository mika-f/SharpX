// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.ShaderLab.Syntax.InternalSyntax;

namespace SharpX.ShaderLab.Syntax;

public class CompilationUnitSyntax : ShaderLabSyntaxNode
{
    private ShaderDeclarationSyntax? _shader;

    public ShaderDeclarationSyntax Shader => GetRedAtZero(ref _shader)!;

    public SyntaxToken EndOfFileToken => new(this, ((CompilationUnitSyntaxInternal)Green).EndOfFileToken, GetChildPosition(1), GetChildIndex(1));

    internal CompilationUnitSyntax(ShaderLabSyntaxNodeInternal node, SyntaxNode? parent, int position) : base(node, parent, position) { }

    public override SyntaxNode? GetNodeSlot(int index)
    {
        return index == 0 ? GetRedAtZero(ref _shader) : null;
    }

    public override SyntaxNode? GetCachedSlot(int index)
    {
        return index == 0 ? _shader : null;
    }

    public CompilationUnitSyntax Update(ShaderDeclarationSyntax shader, SyntaxToken endOfFileToken)
    {
        if (shader != Shader || endOfFileToken != EndOfFileToken)
            return SyntaxFactory.CompilationUnit(shader, endOfFileToken);
        return this;
    }

    public CompilationUnitSyntax WithShader(ShaderDeclarationSyntax shader)
    {
        return Update(shader, EndOfFileToken);
    }

    public CompilationUnitSyntax WithEndOfFileToken(SyntaxToken endOfFileToken)
    {
        return Update(Shader, endOfFileToken);
    }

    public override TResult? Accept<TResult>(ShaderLabSyntaxVisitor<TResult> visitor) where TResult : default
    {
        return visitor.VisitCompilationUnit(this);
    }
}