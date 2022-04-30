// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using Microsoft.CodeAnalysis;

using SharpX.Core;

using SyntaxNode = SharpX.Core.SyntaxNode;

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

    public AttributeSyntaxInternal(SyntaxKind kind, NameSyntaxInternal name, AttributeArgumentListSyntaxInternal? argumentList, DiagnosticInfo[]? diagnostics, SyntaxAnnotation[]? annotations) : base(kind, diagnostics, annotations)
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

    public override GreenNode SetAnnotations(SyntaxAnnotation[]? annotations)
    {
        return new AttributeSyntaxInternal(Kind, Name, ArgumentList, GetDiagnostics(), annotations);
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new AttributeSyntaxInternal(Kind, Name, ArgumentList, diagnostics, GetAnnotations());
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
}