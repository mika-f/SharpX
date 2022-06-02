// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;

namespace SharpX.Hlsl.Syntax.InternalSyntax;

internal class AttributeSyntaxInternal : HlslSyntaxNodeInternal
{
    public NameSyntaxInternal Name { get; }

    public AttributeArgumentListSyntaxInternal? ArgumentList { get; }

    public AttributeSyntaxInternal(SyntaxKind kind, NameSyntaxInternal name, AttributeArgumentListSyntaxInternal? argumentList) : base(kind)
    {
        SlotCount = 2;

        AdjustWidth(name);
        Name = name;

        if (argumentList != null)
        {
            AdjustWidth(argumentList);
            ArgumentList = argumentList;
        }
    }

    public AttributeSyntaxInternal(SyntaxKind kind, NameSyntaxInternal name, AttributeArgumentListSyntaxInternal? argumentList, DiagnosticInfo[]? diagnostics) : base(kind, diagnostics)
    {
        SlotCount = 2;

        AdjustWidth(name);
        Name = name;

        if (argumentList != null)
        {
            AdjustWidth(argumentList);
            ArgumentList = argumentList;
        }
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new AttributeSyntaxInternal(Kind, Name, ArgumentList, diagnostics);
    }

    public override GreenNode? GetSlot(int index)
    {
        return index switch
        {
            0 => Name,
            1 => ArgumentList,
            _ => null
        };
    }

    public override SyntaxNode CreateRed(SyntaxNode? parent, int position)
    {
        return new AttributeSyntax(this, parent, position);
    }

    public override TResult? Accept<TResult>(HlslSyntaxVisitorInternal<TResult> visitor) where TResult : default
    {
        return visitor.VisitAttribute(this);
    }
}