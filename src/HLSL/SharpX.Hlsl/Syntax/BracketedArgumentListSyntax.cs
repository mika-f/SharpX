// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.Hlsl.Syntax.InternalSyntax;

namespace SharpX.Hlsl.Syntax;

public class BracketedArgumentListSyntax : BaseArgumentListSyntax
{
    private SyntaxNode? _arguments;

    public SyntaxToken OpenBracketToken => new(this, ((BracketedArgumentListSyntaxInternal)Green).OpenBracketToken, Position, 0);

    public override SeparatedSyntaxList<ArgumentSyntax> Arguments
    {
        get
        {
            var red = GetRed(ref _arguments, 1);
            return red != null ? new SeparatedSyntaxList<ArgumentSyntax>(red, GetChildIndex(1)) : default;
        }
    }

    public SyntaxToken CloseBracketToken => new(this, ((BracketedArgumentListSyntaxInternal)Green).CloseBracketToken, GetChildPosition(2), GetChildIndex(2));

    internal BracketedArgumentListSyntax(HlslSyntaxNodeInternal node, SyntaxNode? parent, int position) : base(node, parent, position) { }

    public override SyntaxNode? GetNodeSlot(int index)
    {
        return index == 1 ? GetRed(ref _arguments, 1)! : null;
    }

    public override SyntaxNode? GetCachedSlot(int index)
    {
        return index == 1 ? _arguments : null;
    }

    public BracketedArgumentListSyntax Update(SyntaxToken openBracketToken, SeparatedSyntaxList<ArgumentSyntax> arguments, SyntaxToken closeBracketToken)
    {
        if (openBracketToken != OpenBracketToken || arguments != Arguments || closeBracketToken != CloseBracketToken)
            return SyntaxFactory.BracketedArgumentList(openBracketToken, arguments, closeBracketToken);
        return this;
    }

    public BracketedArgumentListSyntax WithOpenBracketToken(SyntaxToken openBracketToken)
    {
        return Update(openBracketToken, Arguments, CloseBracketToken);
    }

    public new BracketedArgumentListSyntax WithArguments(SeparatedSyntaxList<ArgumentSyntax> arguments)
    {
        return Update(OpenBracketToken, arguments, CloseBracketToken);
    }

    public BracketedArgumentListSyntax WithCloseBracketToken(SyntaxToken closeBracketToken)
    {
        return Update(OpenBracketToken, Arguments, closeBracketToken);
    }

    internal override BaseArgumentListSyntax WithArgumentsCore(SeparatedSyntaxList<ArgumentSyntax> arguments)
    {
        return WithArguments(arguments);
    }

    internal override BaseArgumentListSyntax AddArgumentsCore(ArgumentSyntax[] items)
    {
        return AddArguments(items);
    }

    public new BracketedArgumentListSyntax AddArguments(params ArgumentSyntax[] items)
    {
        return WithArguments(Arguments.AddRange(items));
    }

    public override TResult? Accept<TResult>(HlslSyntaxVisitor<TResult> visitor) where TResult : default
    {
        return visitor.VisitBracketedArgumentList(this);
    }
}