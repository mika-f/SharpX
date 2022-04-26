// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.Core.Syntax.InternalSyntax;

namespace SharpX.Hlsl.Syntax.InternalSyntax;

internal class ParameterSyntaxInternal : HlslSyntaxNodeInternal
{
    private readonly GreenNode? _attributeLists;
    private readonly GreenNode? _modifiers;

    public SyntaxListInternal<AttributeListSyntaxInternal> AttributeLists => new(_attributeLists);

    public SyntaxListInternal<SyntaxTokenInternal> Modifiers => new(_modifiers);

    public TypeSyntaxInternal Type { get; }

    public SyntaxTokenInternal Identifier { get; }

    public EqualsValueClauseSyntaxInternal? Default { get; }

    public SemanticSyntaxInternal? Semantics { get; }

    public ParameterSyntaxInternal(SyntaxKind kind, GreenNode? attributeLists, GreenNode? modifiers, TypeSyntaxInternal type, SyntaxTokenInternal identifier, EqualsValueClauseSyntaxInternal? @default, SemanticSyntaxInternal? semantics) : base(kind)
    {
        SlotCount = 6;

        if (attributeLists != null)
        {
            AdjustWidth(attributeLists);
            _attributeLists = attributeLists;
        }

        if (modifiers != null)
        {
            AdjustWidth(modifiers);
            _modifiers = modifiers;
        }

        AdjustWidth(type);
        Type = type;

        AdjustWidth(identifier);
        Identifier = identifier;

        if (@default != null)
        {
            AdjustWidth(@default);
            Default = @default;
        }

        if (semantics != null)
        {
            AdjustWidth(semantics);
            Semantics = semantics;
        }
    }

    public ParameterSyntaxInternal(SyntaxKind kind, GreenNode? attributeLists, GreenNode? modifiers, TypeSyntaxInternal type, SyntaxTokenInternal identifier, EqualsValueClauseSyntaxInternal? @default, SemanticSyntaxInternal? semantics, DiagnosticInfo[]? diagnostics) : base(kind, diagnostics)
    {
        SlotCount = 6;

        if (attributeLists != null)
        {
            AdjustWidth(attributeLists);
            _attributeLists = attributeLists;
        }

        if (modifiers != null)
        {
            AdjustWidth(modifiers);
            _modifiers = modifiers;
        }

        AdjustWidth(type);
        Type = type;

        AdjustWidth(identifier);
        Identifier = identifier;

        if (@default != null)
        {
            AdjustWidth(@default);
            Default = @default;
        }

        if (semantics != null)
        {
            AdjustWidth(semantics);
            Semantics = semantics;
        }
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new ParameterSyntaxInternal(Kind, _attributeLists, _modifiers, Type, Identifier, Default, Semantics, diagnostics);
    }

    public override GreenNode? GetSlot(int index)
    {
        return index switch
        {
            0 => _attributeLists,
            1 => _modifiers,
            2 => Type,
            3 => Identifier,
            4 => Default,
            5 => Semantics,
            _ => null
        };
    }

    public override SyntaxNode CreateRed(SyntaxNode? parent, int position)
    {
        return new ParameterSyntax(this, parent, position);
    }
}