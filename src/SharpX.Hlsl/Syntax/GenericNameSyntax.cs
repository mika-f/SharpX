// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.Hlsl.Syntax.InternalSyntax;

namespace SharpX.Hlsl.Syntax;

public sealed class GenericNameSyntax : SimpleNameSyntax
{
    private TypeArgumentListSyntax? _typeArgumentList;

    public override SyntaxToken Identifier => new(this, ((GenericNameSyntaxInternal)Green).Identifier, Position, 0);

    public TypeArgumentListSyntax TypeArgumentList => GetRed(ref _typeArgumentList, 1)!;

    internal GenericNameSyntax(HlslSyntaxNodeInternal node, SyntaxNode? parent, int position) : base(node, parent, position) { }

    public override SyntaxNode? GetNodeSlot(int index)
    {
        return index == 1 ? GetRed(ref _typeArgumentList, 1) : null;
    }

    public override SyntaxNode? GetCachedSlot(int index)
    {
        return index == 1 ? _typeArgumentList : null;
    }

    public GenericNameSyntax Update(SyntaxToken identifier, TypeArgumentListSyntax typeArgumentList)
    {
        if (identifier != Identifier || typeArgumentList != TypeArgumentList)
            return SyntaxFactory.GenericName(identifier, typeArgumentList);

        return this;
    }

    protected override SimpleNameSyntax WithIdentifierInternal(SyntaxToken identifier)
    {
        return Update(identifier, TypeArgumentList);
    }

    public GenericNameSyntax WithTypeArgumentList(TypeArgumentListSyntax typeArgumentList)
    {
        return Update(Identifier, typeArgumentList);
    }

    public GenericNameSyntax AddTypeArgumentListArguments(params TypeSyntax[] items)
    {
        return WithTypeArgumentList(TypeArgumentList.WithArguments(TypeArgumentList.Arguments.AddRange(items)));
    }

    public override TResult? Accept<TResult>(HlslSyntaxVisitor<TResult> visitor) where TResult : default
    {
        return visitor.VisitGenericName(this);
    }
}