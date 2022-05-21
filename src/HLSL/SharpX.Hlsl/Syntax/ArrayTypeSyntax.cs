// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.Hlsl.Syntax.InternalSyntax;

namespace SharpX.Hlsl.Syntax;

public class ArrayTypeSyntax : TypeSyntax
{
    private TypeSyntax? _elementType;
    private SyntaxNode? _rankSpecifiers;

    public TypeSyntax ElementType => GetRedAtZero(ref _elementType)!;

    public SyntaxList<ArrayRankSpecifierSyntax> RankSpecifiers => new(GetRed(ref _rankSpecifiers, 1));

    internal ArrayTypeSyntax(HlslSyntaxNodeInternal node, SyntaxNode? parent, int position) : base(node, parent, position) { }

    public override SyntaxNode? GetNodeSlot(int index)
    {
        return index switch
        {
            0 => GetRedAtZero(ref _elementType)!,
            1 => GetRed(ref _rankSpecifiers, 1)!,
            _ => null
        };
    }

    public override SyntaxNode? GetCachedSlot(int index)
    {
        return index switch
        {
            0 => _elementType,
            1 => _rankSpecifiers,
            _ => null
        };
    }

    public ArrayTypeSyntax Update(TypeSyntax elementType, SyntaxList<ArrayRankSpecifierSyntax> rankSpecifiers)
    {
        if (elementType != ElementType || rankSpecifiers != RankSpecifiers)
            return SyntaxFactory.ArrayType(elementType, rankSpecifiers);
        return this;
    }

    public ArrayTypeSyntax WithElementType(TypeSyntax elementType)
    {
        return Update(elementType, RankSpecifiers);
    }

    public ArrayTypeSyntax WithRankSpecifiers(SyntaxList<ArrayRankSpecifierSyntax> rankSpecifiers)
    {
        return Update(ElementType, rankSpecifiers);
    }

    public ArrayTypeSyntax AddRankSpecifiers(params ArrayRankSpecifierSyntax[] items)
    {
        return WithRankSpecifiers(RankSpecifiers.AddRange(items));
    }

    public override TResult? Accept<TResult>(HlslSyntaxVisitor<TResult> visitor) where TResult : default
    {
        return visitor.VisitArrayType(this);
    }
}