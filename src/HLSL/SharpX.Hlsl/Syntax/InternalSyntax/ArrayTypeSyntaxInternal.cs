// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.Core.Syntax.InternalSyntax;

namespace SharpX.Hlsl.Syntax.InternalSyntax;

internal class ArrayTypeSyntaxInternal : TypeSyntaxInternal
{
    private readonly GreenNode? _rankSpecifiers;

    public TypeSyntaxInternal ElementType { get; }

    public SyntaxListInternal<ArrayRankSpecifierSyntaxInternal> RankSpecifiers => new(_rankSpecifiers);

    public ArrayTypeSyntaxInternal(SyntaxKind kind, TypeSyntaxInternal elementType, GreenNode? rankSpecifiers) : base(kind)
    {
        SlotCount = 2;

        AdjustWidth(elementType);
        ElementType = elementType;

        if (rankSpecifiers != null)
        {
            AdjustWidth(rankSpecifiers);
            _rankSpecifiers = rankSpecifiers;
        }
    }

    public ArrayTypeSyntaxInternal(SyntaxKind kind, TypeSyntaxInternal elementType, GreenNode? rankSpecifiers, DiagnosticInfo[]? diagnostics) : base(kind, diagnostics)
    {
        SlotCount = 2;

        AdjustWidth(elementType);
        ElementType = elementType;

        if (rankSpecifiers != null)
        {
            AdjustWidth(rankSpecifiers);
            _rankSpecifiers = rankSpecifiers;
        }
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new ArrayTypeSyntaxInternal(Kind, ElementType, _rankSpecifiers, diagnostics);
    }

    public override GreenNode? GetSlot(int index)
    {
        return index switch
        {
            0 => ElementType,
            1 => _rankSpecifiers,
            _ => null
        };
    }

    public override SyntaxNode CreateRed(SyntaxNode? parent, int position)
    {
        return new ArrayTypeSyntax(this, parent, position);
    }

    public override TResult? Accept<TResult>(HlslSyntaxVisitorInternal<TResult> visitor) where TResult : default
    {
        return visitor.VisitArrayType(this);
    }
}