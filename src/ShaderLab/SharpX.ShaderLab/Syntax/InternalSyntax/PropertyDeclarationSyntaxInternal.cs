// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;

namespace SharpX.ShaderLab.Syntax.InternalSyntax;

internal class PropertyDeclarationSyntaxInternal : ShaderLabSyntaxNodeInternal
{
    public AttributeListSyntaxInternal? AttributeList { get; }

    public SyntaxTokenInternal Identifier { get; }

    public SyntaxTokenInternal OpenParenToken { get; }

    public SyntaxTokenInternal DisplayName { get; }

    public SyntaxTokenInternal CommaToken { get; }

    public SimpleNameSyntaxInternal Type { get; }

    public ArgumentListSyntaxInternal? ArgumentList { get; }

    public SyntaxTokenInternal CloseParenToken { get; }

    public EqualsValueClauseSyntaxInternal DefaultValue { get; }

    public PropertyDeclarationSyntaxInternal(SyntaxKind kind, AttributeListSyntaxInternal? attributeList, SyntaxTokenInternal identifier, SyntaxTokenInternal openParenToken, SyntaxTokenInternal displayName, SyntaxTokenInternal commaToken, SimpleNameSyntaxInternal type,
                                             ArgumentListSyntaxInternal? argumentList, SyntaxTokenInternal closeParenToken, EqualsValueClauseSyntaxInternal @default) : base(kind)
    {
        SlotCount = 9;

        if (attributeList != null)
        {
            AdjustWidth(attributeList);
            AttributeList = attributeList;
        }

        AdjustWidth(identifier);
        Identifier = identifier;

        AdjustWidth(openParenToken);
        OpenParenToken = openParenToken;

        AdjustWidth(displayName);
        DisplayName = displayName;

        AdjustWidth(commaToken);
        CommaToken = commaToken;

        AdjustWidth(type);
        Type = type;

        if (argumentList != null)
        {
            AdjustWidth(argumentList);
            ArgumentList = argumentList;
        }

        AdjustWidth(closeParenToken);
        CloseParenToken = closeParenToken;

        AdjustWidth(@default);
        DefaultValue = @default;
    }

    public PropertyDeclarationSyntaxInternal(SyntaxKind kind, AttributeListSyntaxInternal? attributeList, SyntaxTokenInternal identifier, SyntaxTokenInternal openParenToken, SyntaxTokenInternal displayName, SyntaxTokenInternal commaToken, SimpleNameSyntaxInternal type,
                                             ArgumentListSyntaxInternal? argumentList, SyntaxTokenInternal closeParenToken, EqualsValueClauseSyntaxInternal @default, DiagnosticInfo[]? diagnostics) : base(kind, diagnostics)
    {
        SlotCount = 9;

        if (attributeList != null)
        {
            AdjustWidth(attributeList);
            AttributeList = attributeList;
        }

        AdjustWidth(identifier);
        Identifier = identifier;

        AdjustWidth(openParenToken);
        OpenParenToken = openParenToken;

        AdjustWidth(displayName);
        DisplayName = displayName;

        AdjustWidth(commaToken);
        CommaToken = commaToken;

        AdjustWidth(type);
        Type = type;

        if (argumentList != null)
        {
            AdjustWidth(argumentList);
            ArgumentList = argumentList;
        }

        AdjustWidth(closeParenToken);
        CloseParenToken = closeParenToken;

        AdjustWidth(@default);
        DefaultValue = @default;
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new PropertyDeclarationSyntaxInternal(Kind, AttributeList, Identifier, OpenParenToken, DisplayName, CommaToken, Type, ArgumentList, CloseParenToken, DefaultValue, diagnostics);
    }

    public override GreenNode? GetSlot(int index)
    {
        return index switch
        {
            0 => AttributeList,
            1 => Identifier,
            2 => OpenParenToken,
            3 => DisplayName,
            4 => CommaToken,
            5 => Type,
            6 => ArgumentList,
            7 => CloseParenToken,
            8 => DefaultValue,
            _ => null
        };
    }

    public override SyntaxNode CreateRed(SyntaxNode? parent, int position)
    {
        return new PropertyDeclarationSyntax(this, parent, position);
    }
}