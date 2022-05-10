// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using Microsoft.CodeAnalysis;

using SharpX.Core;

using SyntaxNode = SharpX.Core.SyntaxNode;

namespace SharpX.ShaderLab.Syntax.InternalSyntax;

internal class PropertyDeclarationSyntaxInternal : ShaderLabSyntaxNodeInternal
{
    public SyntaxTokenInternal Identifier { get; }

    public SyntaxTokenInternal OpenParenToken { get; }

    public SyntaxTokenInternal DisplayName { get; }

    public SyntaxTokenInternal CommaToken { get; }

    public SimpleNameSyntaxInternal Type { get; }

    public ArgumentListSyntaxInternal? ArgumentList { get; }

    public SyntaxTokenInternal CloseParenToken { get; }

    public EqualsValueClauseSyntaxInternal DefaultValue { get; }

    public PropertyDeclarationSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal identifier, SyntaxTokenInternal openParenToken, SyntaxTokenInternal displayName, SyntaxTokenInternal commaToken, SimpleNameSyntaxInternal type, ArgumentListSyntaxInternal? argumentList,
                                             SyntaxTokenInternal closeParenToken, EqualsValueClauseSyntaxInternal @default) : base(kind)
    {
        SlotCount = 8;

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

    public PropertyDeclarationSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal identifier, SyntaxTokenInternal openParenToken, SyntaxTokenInternal displayName, SyntaxTokenInternal commaToken, SimpleNameSyntaxInternal type, ArgumentListSyntaxInternal? argumentList,
                                             SyntaxTokenInternal closeParenToken, EqualsValueClauseSyntaxInternal @default, DiagnosticInfo[]? diagnostics, SyntaxAnnotation[]? annotations) : base(kind, diagnostics, annotations)
    {
        SlotCount = 8;

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

    public override GreenNode SetAnnotations(SyntaxAnnotation[]? annotations)
    {
        return new PropertyDeclarationSyntaxInternal(Kind, Identifier, OpenParenToken, DisplayName, CommaToken, Type, ArgumentList, CloseParenToken, DefaultValue, GetDiagnostics(), annotations);
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new PropertyDeclarationSyntaxInternal(Kind, Identifier, OpenParenToken, DisplayName, CommaToken, Type, ArgumentList, CloseParenToken, DefaultValue, diagnostics, GetAnnotations());
    }

    public override GreenNode? GetSlot(int index)
    {
        return index switch
        {
            0 => Identifier,
            1 => OpenParenToken,
            2 => DisplayName,
            3 => CommaToken,
            4 => Type,
            5 => ArgumentList,
            6 => CloseParenToken,
            7 => DefaultValue,
            _ => null
        };
    }

    public override SyntaxNode CreateRed(SyntaxNode? parent, int position)
    {
        return new PropertyDeclarationSyntax(this, parent, position);
    }
}