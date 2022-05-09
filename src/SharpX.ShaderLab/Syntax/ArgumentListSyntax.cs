// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.ShaderLab.Syntax.InternalSyntax;

namespace SharpX.ShaderLab.Syntax;

public class ArgumentListSyntax : ShaderLabSyntaxNode
{
    private SyntaxNode? _arguments;

    public SyntaxToken OpenParenToken => new(this, ((ArgumentListSyntaxInternal)Green).OpenParenToken, Position, 0);

    public SeparatedSyntaxList<ArgumentSyntax> Arguments
    {
        get
        {
            var red = GetRed(ref _arguments, 1);
            return red != null ? new SeparatedSyntaxList<ArgumentSyntax>(red, GetChildIndex(1)) : default;
        }
    }

    public SyntaxToken CloseParenToken => new(this, ((ArgumentListSyntaxInternal)Green).CloseParenToken, GetChildPosition(2), GetChildIndex(2));

    internal ArgumentListSyntax(ShaderLabSyntaxNodeInternal node, SyntaxNode? parent, int position) : base(node, parent, position) { }

    public override SyntaxNode? GetNodeSlot(int index)
    {
        return index == 1 ? GetRed(ref _arguments, 1) : null;
    }

    public override SyntaxNode? GetCachedSlot(int index)
    {
        return index == 1 ? _arguments : null;
    }

    public ArgumentListSyntax Update(SyntaxToken openParenToken, SeparatedSyntaxList<ArgumentSyntax> arguments, SyntaxToken closeParenToken)
    {
        if (openParenToken != OpenParenToken || arguments != Arguments || closeParenToken != CloseParenToken)
            return SyntaxFactory.ArgumentList(openParenToken, arguments, closeParenToken);
        return this;
    }

    public ArgumentListSyntax WithOpenParenToken(SyntaxToken openParenToken)
    {
        return Update(openParenToken, Arguments, CloseParenToken);
    }

    public ArgumentListSyntax WithArguments(SeparatedSyntaxList<ArgumentSyntax> arguments)
    {
        return Update(OpenParenToken, arguments, CloseParenToken);
    }

    public ArgumentListSyntax WithCloseParenToken(SyntaxToken closeParenToken)
    {
        return Update(OpenParenToken, Arguments, closeParenToken);
    }

    public ArgumentListSyntax AddArguments(params ArgumentSyntax[] items)
    {
        return WithArguments(Arguments.AddRange(items));
    }

    public override TResult? Accept<TResult>(ShaderLabSyntaxVisitor<TResult> visitor) where TResult : default
    {
        return visitor.VisitArgumentList(this);
    }
}