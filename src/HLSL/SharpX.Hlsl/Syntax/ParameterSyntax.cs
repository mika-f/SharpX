// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.Hlsl.Syntax.InternalSyntax;

namespace SharpX.Hlsl.Syntax;

public class ParameterSyntax : HlslSyntaxNode
{
    private SyntaxNode? _attributeLists;
    private EqualsValueClauseSyntax? _default;
    private SemanticSyntax? _semantics;
    private TypeSyntax? _type;

    public SyntaxList<AttributeListSyntax> AttributeLists => new(GetRedAtZero(ref _attributeLists));

    public SyntaxTokenList Modifiers
    {
        get
        {
            var slot = Green.GetSlot(1);
            return slot != null ? new SyntaxTokenList(this, slot, GetChildPosition(1), GetChildIndex(1)) : default;
        }
    }

    public TypeSyntax Type => GetRed(ref _type, 2)!;

    public SyntaxToken Identifier => new(this, ((ParameterSyntaxInternal)Green).Identifier, GetChildPosition(3), GetChildIndex(3));

    public EqualsValueClauseSyntax? Default => GetRed(ref _default, 4);

    public SemanticSyntax? Semantics => GetRed(ref _semantics, 5);


    internal ParameterSyntax(HlslSyntaxNodeInternal node, SyntaxNode? parent, int position) : base(node, parent, position) { }

    public override SyntaxNode? GetNodeSlot(int index)
    {
        return index switch
        {
            0 => GetRedAtZero(ref _attributeLists),
            2 => GetRed(ref _type, 2),
            4 => GetRed(ref _default, 4),
            5 => GetRed(ref _semantics, 5),
            _ => null
        };
    }

    public override SyntaxNode? GetCachedSlot(int index)
    {
        return index switch
        {
            0 => _attributeLists,
            2 => _type,
            4 => _default,
            5 => _semantics,
            _ => null
        };
    }

    public ParameterSyntax Update(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, TypeSyntax type, SyntaxToken identifier, EqualsValueClauseSyntax? @default, SemanticSyntax? semantics)
    {
        if (attributeLists != AttributeLists || modifiers != Modifiers || type != Type || identifier != Identifier || @default != Default || semantics != Semantics)
            return SyntaxFactory.Parameter(attributeLists, modifiers, type, identifier, @default, semantics);
        return this;
    }

    public ParameterSyntax WithAttributeLists(SyntaxList<AttributeListSyntax> attributeLists)
    {
        return Update(attributeLists, Modifiers, Type, Identifier, Default, Semantics);
    }

    public ParameterSyntax WithModifiers(SyntaxTokenList modifiers)
    {
        return Update(AttributeLists, modifiers, Type, Identifier, Default, Semantics);
    }

    public ParameterSyntax WithType(TypeSyntax type)
    {
        return Update(AttributeLists, Modifiers, type, Identifier, Default, Semantics);
    }

    public ParameterSyntax WithIdentifier(SyntaxToken identifier)
    {
        return Update(AttributeLists, Modifiers, Type, identifier, Default, Semantics);
    }

    public ParameterSyntax WithDefault(EqualsValueClauseSyntax? @default)
    {
        return Update(AttributeLists, Modifiers, Type, Identifier, @default, Semantics);
    }

    public ParameterSyntax WithSemantics(SemanticSyntax? semantics)
    {
        return Update(AttributeLists, Modifiers, Type, Identifier, Default, semantics);
    }

    public ParameterSyntax AddAttributeLists(params AttributeListSyntax[] items)
    {
        return WithAttributeLists(AttributeLists.AddRange(items));
    }

    public ParameterSyntax AddModifiers(params SyntaxToken[] items)
    {
        return WithModifiers(Modifiers.AddRange(items));
    }

    public override TResult? Accept<TResult>(HlslSyntaxVisitor<TResult> visitor) where TResult : default
    {
        return visitor.VisitParameter(this);
    }
}