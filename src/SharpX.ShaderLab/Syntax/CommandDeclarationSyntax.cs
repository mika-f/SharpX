// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.ShaderLab.Syntax.InternalSyntax;

namespace SharpX.ShaderLab.Syntax;

public class CommandDeclarationSyntax : BaseCommandDeclarationSyntax
{
    private SyntaxNode? _arguments;

    public override SyntaxToken Keyword => new(this, ((CommandDeclarationSyntaxInternal)Green).Keyword, Position, 0);

    public SeparatedSyntaxList<IdentifierNameSyntax> Arguments
    {
        get
        {
            var red = GetRed(ref _arguments, 1);
            return red != null ? new SeparatedSyntaxList<IdentifierNameSyntax>(red, GetChildIndex(1)) : default;
        }
    }

    internal CommandDeclarationSyntax(ShaderLabSyntaxNodeInternal node, SyntaxNode? parent, int position) : base(node, parent, position) { }

    public override SyntaxNode? GetNodeSlot(int index)
    {
        return index == 1 ? GetRed(ref _arguments, 1) : null;
    }

    public override SyntaxNode? GetCachedSlot(int index)
    {
        return index == 1 ? _arguments : null;
    }

    public CommandDeclarationSyntax Update(SyntaxToken keyword, SeparatedSyntaxList<IdentifierNameSyntax> arguments)
    {
        if (keyword != Keyword || arguments != Arguments)
            return SyntaxFactory.CommandDeclaration(keyword, arguments);
        return this;
    }

    public CommandDeclarationSyntax WithKeyword(SyntaxToken keyword)
    {
        return Update(keyword, Arguments);
    }

    public CommandDeclarationSyntax WithArguments(SeparatedSyntaxList<IdentifierNameSyntax> arguments)
    {
        return Update(Keyword, arguments);
    }

    public CommandDeclarationSyntax AddArguments(params IdentifierNameSyntax[] items)
    {
        return WithArguments(Arguments.AddRange(items));
    }

    public override TResult? Accept<TResult>(ShaderLabSyntaxVisitor<TResult> visitor) where TResult : default
    {
        return visitor.VisitCommandDeclaration(this);
    }
}