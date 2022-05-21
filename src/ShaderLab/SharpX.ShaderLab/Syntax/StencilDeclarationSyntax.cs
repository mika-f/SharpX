// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.ShaderLab.Syntax.InternalSyntax;

namespace SharpX.ShaderLab.Syntax;

public class StencilDeclarationSyntax : BaseCommandDeclarationSyntax
{
    private SyntaxNode? _commands;

    public override SyntaxToken Keyword => new(this, ((StencilDeclarationSyntaxInternal)Green).Keyword, Position, 0);

    public SyntaxToken OpenBraceToken => new(this, ((StencilDeclarationSyntaxInternal)Green).OpenBraceToken, GetChildPosition(1), GetChildIndex(1));

    public SyntaxList<CommandDeclarationSyntax> Commands
    {
        get
        {
            var red = GetRed(ref _commands, 2);
            return red != null ? new SyntaxList<CommandDeclarationSyntax>(red) : default;
        }
    }

    public SyntaxToken CloseBraceToken => new(this, ((StencilDeclarationSyntaxInternal)Green).CloseBraceToken, GetChildPosition(3), GetChildIndex(3));

    internal StencilDeclarationSyntax(ShaderLabSyntaxNodeInternal node, SyntaxNode? parent, int position) : base(node, parent, position) { }

    public override SyntaxNode? GetNodeSlot(int index)
    {
        return index == 2 ? GetRed(ref _commands, 2) : null;
    }

    public override SyntaxNode? GetCachedSlot(int index)
    {
        return index == 2 ? _commands : null;
    }

    public StencilDeclarationSyntax Update(SyntaxToken keyword, SyntaxToken openBraceToken, SyntaxList<CommandDeclarationSyntax> commands, SyntaxToken closeBraceToken)
    {
        if (keyword != Keyword || openBraceToken != OpenBraceToken || commands != Commands || closeBraceToken != CloseBraceToken)
            return SyntaxFactory.StencilDeclaration(keyword, openBraceToken, commands, closeBraceToken);
        return this;
    }

    public StencilDeclarationSyntax WithKeyword(SyntaxToken keyword)
    {
        return Update(keyword, OpenBraceToken, Commands, CloseBraceToken);
    }

    public StencilDeclarationSyntax WithOpenBraceToken(SyntaxToken openBraceToken)
    {
        return Update(Keyword, openBraceToken, Commands, CloseBraceToken);
    }

    public StencilDeclarationSyntax WithCommands(SyntaxList<CommandDeclarationSyntax> commands)
    {
        return Update(Keyword, OpenBraceToken, commands, CloseBraceToken);
    }

    public StencilDeclarationSyntax WithCloseBraceToken(SyntaxToken closeBraceToken)
    {
        return Update(Keyword, OpenBraceToken, Commands, closeBraceToken);
    }

    public override TResult? Accept<TResult>(ShaderLabSyntaxVisitor<TResult> visitor) where TResult : default
    {
        return visitor.VisitStencilDeclaration(this);
    }
}