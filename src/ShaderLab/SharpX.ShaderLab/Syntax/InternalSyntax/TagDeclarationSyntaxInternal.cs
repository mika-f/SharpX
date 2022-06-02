// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;

namespace SharpX.ShaderLab.Syntax.InternalSyntax;

internal class TagDeclarationSyntaxInternal : ShaderLabSyntaxNodeInternal
{
    public SyntaxTokenInternal Key { get; }

    public SyntaxTokenInternal EqualsToken { get; }

    public SyntaxTokenInternal Value { get; }

    public TagDeclarationSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal key, SyntaxTokenInternal equalsToken, SyntaxTokenInternal value) : base(kind)
    {
        SlotCount = 3;

        AdjustWidth(key);
        Key = key;

        AdjustWidth(equalsToken);
        EqualsToken = equalsToken;

        AdjustWidth(value);
        Value = value;
    }

    public TagDeclarationSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal key, SyntaxTokenInternal equalsToken, SyntaxTokenInternal value, DiagnosticInfo[]? diagnostics) : base(kind, diagnostics)
    {
        SlotCount = 3;

        AdjustWidth(key);
        Key = key;

        AdjustWidth(equalsToken);
        EqualsToken = equalsToken;

        AdjustWidth(value);
        Value = value;
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new TagDeclarationSyntaxInternal(Kind, Key, EqualsToken, Value, diagnostics);
    }

    public override GreenNode? GetSlot(int index)
    {
        return index switch
        {
            0 => Key,
            1 => EqualsToken,
            2 => Value,
            _ => null
        };
    }

    public override SyntaxNode CreateRed(SyntaxNode? parent, int position)
    {
        return new TagDeclarationSyntax(this, parent, position);
    }
}