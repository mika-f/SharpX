// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.Hlsl.Syntax.InternalSyntax;

namespace SharpX.Hlsl.Syntax;

public class TypeArgumentListSyntax : HlslSyntaxNode
{
    private SyntaxNode? _arguments;

    public SyntaxToken LessThanToken => new(this, ((TypeArgumentListSyntaxInternal)Green).LessThanToken, Position, 0);

    public SeparatedSyntaxList<TypeSyntax> Arguments
    {
        get
        {
            var red = GetRed(ref _arguments, 1);
            return red != null ? new SeparatedSyntaxList<TypeSyntax>(red, GetChildPosition(1)) : default;
        }
    }

    public SyntaxToken GreaterThanToken => new(this, ((TypeArgumentListSyntaxInternal)Green).GreaterThanToken, Position, 0);

    internal TypeArgumentListSyntax(HlslSyntaxNodeInternal node, SyntaxNode? parent, int position) : base(node, parent, position) { }

    public override SyntaxNode? GetNodeSlot(int index)
    {
        return index == 1 ? GetRed(ref _arguments, 1)! : null;
    }

    public override SyntaxNode? GetCachedSlot(int index)
    {
        return index == 1 ? _arguments : null;
    }

    public TypeArgumentListSyntax Update(SyntaxToken lessThanToken, SeparatedSyntaxList<TypeSyntax> arguments, SyntaxToken greaterThanToken)
    {
        if (lessThanToken != LessThanToken || arguments != Arguments || greaterThanToken != GreaterThanToken)
            return SyntaxFactory.TypeArgumentList(lessThanToken, arguments, greaterThanToken);

        return this;
    }

    public TypeArgumentListSyntax WithLessThanToken(SyntaxToken lessThanToken)
    {
        return Update(lessThanToken, Arguments, GreaterThanToken);
    }

    public TypeArgumentListSyntax WithArguments(SeparatedSyntaxList<TypeSyntax> arguments)
    {
        return Update(LessThanToken, arguments, GreaterThanToken);
    }

    public TypeArgumentListSyntax WithGreaterThanToken(SyntaxToken greaterThanToken)
    {
        return Update(LessThanToken, Arguments, greaterThanToken);
    }

    public TypeArgumentListSyntax AddArguments(params TypeSyntax[] items)
    {
        return WithArguments(Arguments.AddRange(items));
    }
}