// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;

namespace SharpX.Hlsl.Syntax.InternalSyntax;

internal class RegisterSyntaxInternal : HlslSyntaxNodeInternal
{
    public SyntaxTokenInternal ColonToken { get; }

    public SyntaxTokenInternal RegisterKeyword { get; }

    public SyntaxTokenInternal OpenParenToken { get; }

    public IdentifierNameSyntaxInternal Register { get; }

    public SyntaxTokenInternal CloseParenToken { get; }

    public RegisterSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal colonToken, SyntaxTokenInternal registerKeyword, SyntaxTokenInternal openParenToken, IdentifierNameSyntaxInternal register, SyntaxTokenInternal closeParenToken) : base(kind)
    {
        SlotCount = 5;

        AdjustWidth(colonToken);
        ColonToken = colonToken;

        AdjustWidth(registerKeyword);
        RegisterKeyword = registerKeyword;

        AdjustWidth(openParenToken);
        OpenParenToken = openParenToken;

        AdjustWidth(register);
        Register = register;

        AdjustWidth(closeParenToken);
        CloseParenToken = closeParenToken;
    }

    public RegisterSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal colonToken, SyntaxTokenInternal registerKeyword, SyntaxTokenInternal openParenToken, IdentifierNameSyntaxInternal register, SyntaxTokenInternal closeParenToken, DiagnosticInfo[]? diagnostics) : base(kind, diagnostics)
    {
        SlotCount = 5;

        AdjustWidth(colonToken);
        ColonToken = colonToken;

        AdjustWidth(registerKeyword);
        RegisterKeyword = registerKeyword;

        AdjustWidth(openParenToken);
        OpenParenToken = openParenToken;

        AdjustWidth(register);
        Register = register;

        AdjustWidth(closeParenToken);
        CloseParenToken = closeParenToken;
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new RegisterSyntaxInternal(Kind, ColonToken, RegisterKeyword, OpenParenToken, Register, CloseParenToken, diagnostics);
    }

    public override GreenNode? GetSlot(int index)
    {
        return index switch
        {
            0 => ColonToken,
            1 => RegisterKeyword,
            2 => OpenParenToken,
            3 => Register,
            4 => CloseParenToken,
            _ => null
        };
    }

    public override SyntaxNode CreateRed(SyntaxNode? parent, int position)
    {
        return new RegisterSyntax(this, parent, position);
    }

    public override TResult? Accept<TResult>(HlslSyntaxVisitorInternal<TResult> visitor) where TResult : default
    {
        return visitor.VisitRegister(this);
    }
}