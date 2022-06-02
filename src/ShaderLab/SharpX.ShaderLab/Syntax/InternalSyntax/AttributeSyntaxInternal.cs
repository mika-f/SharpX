// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;

namespace SharpX.ShaderLab.Syntax.InternalSyntax;

internal class AttributeSyntaxInternal : ShaderLabSyntaxNodeInternal
{
    public NameSyntaxInternal Name { get; }

    public ArgumentListSyntaxInternal? ArgumentList { get; }

    public AttributeSyntaxInternal(SyntaxKind kind, NameSyntaxInternal name, ArgumentListSyntaxInternal? argumentList) : base(kind)
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

    public AttributeSyntaxInternal(SyntaxKind kind, NameSyntaxInternal name, ArgumentListSyntaxInternal? argumentList, DiagnosticInfo[]? diagnostics) : base(kind, diagnostics)
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
}