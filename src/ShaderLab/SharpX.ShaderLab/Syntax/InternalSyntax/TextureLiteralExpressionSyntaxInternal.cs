// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;

namespace SharpX.ShaderLab.Syntax.InternalSyntax;

internal class TextureLiteralExpressionSyntaxInternal : ExpressionSyntaxInternal
{
    public LiteralExpressionSyntaxInternal Value { get; }

    public SyntaxTokenInternal OpenBraceToken { get; }

    public SyntaxTokenInternal CloseBraceToken { get; }

    public TextureLiteralExpressionSyntaxInternal(SyntaxKind kind, LiteralExpressionSyntaxInternal value, SyntaxTokenInternal openBraceToken, SyntaxTokenInternal closeBraceToken) : base(kind)
    {
        SlotCount = 3;

        AdjustWidth(value);
        Value = value;

        AdjustWidth(openBraceToken);
        OpenBraceToken = openBraceToken;

        AdjustWidth(closeBraceToken);
        CloseBraceToken = closeBraceToken;
    }

    public TextureLiteralExpressionSyntaxInternal(SyntaxKind kind, LiteralExpressionSyntaxInternal value, SyntaxTokenInternal openBraceToken, SyntaxTokenInternal closeBraceToken, DiagnosticInfo[]? diagnostics) : base(kind, diagnostics)
    {
        SlotCount = 3;

        AdjustWidth(value);
        Value = value;

        AdjustWidth(openBraceToken);
        OpenBraceToken = openBraceToken;

        AdjustWidth(closeBraceToken);
        CloseBraceToken = closeBraceToken;
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new TextureLiteralExpressionSyntaxInternal(Kind, Value, OpenBraceToken, CloseBraceToken, diagnostics);
    }

    public override GreenNode? GetSlot(int index)
    {
        return index switch
        {
            0 => Value,
            1 => OpenBraceToken,
            2 => CloseBraceToken,
            _ => null
        };
    }

    public override SyntaxNode CreateRed(SyntaxNode? parent, int position)
    {
        return new TextureLiteralExpressionSyntax(this, parent, position);
    }
}