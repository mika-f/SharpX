// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.Core.Syntax.InternalSyntax;

namespace SharpX.Hlsl.Syntax.InternalSyntax;

internal class ConstantBufferDeclarationSyntaxInternal : TypeDeclarationSyntaxInternal
{
    private readonly GreenNode? _members;

    public override SyntaxTokenInternal Keyword { get; }

    public override SyntaxTokenInternal Identifier { get; }

    public RegisterSyntaxInternal? Register { get; }

    public override SyntaxTokenInternal OpenBraceToken { get; }

    public override SyntaxListInternal<MemberDeclarationSyntaxInternal> Members => new(_members);

    public override SyntaxTokenInternal CloseBraceToken { get; }

    public SyntaxTokenInternal SemicolonToken { get; }


    public ConstantBufferDeclarationSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal keyword, SyntaxTokenInternal identifier, RegisterSyntaxInternal? register, SyntaxTokenInternal openBraceToken, GreenNode? members, SyntaxTokenInternal closeBraceToken,
                                                   SyntaxTokenInternal semicolonToken) : base(kind)
    {
        SlotCount = 7;

        AdjustWidth(keyword);
        Keyword = keyword;

        AdjustWidth(identifier);
        Identifier = identifier;

        if (register != null)
        {
            AdjustWidth(register);
            Register = register;
        }

        AdjustWidth(openBraceToken);
        OpenBraceToken = openBraceToken;

        if (members != null)
        {
            AdjustWidth(members);
            _members = members;
        }

        AdjustWidth(closeBraceToken);
        CloseBraceToken = closeBraceToken;

        AdjustWidth(semicolonToken);
        SemicolonToken = semicolonToken;
    }

    public ConstantBufferDeclarationSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal keyword, SyntaxTokenInternal identifier, RegisterSyntaxInternal? register, SyntaxTokenInternal openBraceToken, GreenNode? members, SyntaxTokenInternal closeBraceToken, SyntaxTokenInternal semicolonToken,
                                                   DiagnosticInfo[]? diagnostics) : base(kind, diagnostics)
    {
        SlotCount = 7;

        AdjustWidth(keyword);
        Keyword = keyword;

        AdjustWidth(identifier);
        Identifier = identifier;

        if (register != null)
        {
            AdjustWidth(register);
            Register = register;
        }

        AdjustWidth(openBraceToken);
        OpenBraceToken = openBraceToken;

        if (members != null)
        {
            AdjustWidth(members);
            _members = members;
        }

        AdjustWidth(closeBraceToken);
        CloseBraceToken = closeBraceToken;

        AdjustWidth(semicolonToken);
        SemicolonToken = semicolonToken;
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new ConstantBufferDeclarationSyntaxInternal(Kind, Keyword, Identifier, Register, OpenBraceToken, _members, CloseBraceToken, SemicolonToken, diagnostics);
    }

    public override GreenNode? GetSlot(int index)
    {
        return index switch
        {
            0 => Keyword,
            1 => Identifier,
            2 => Register,
            3 => OpenBraceToken,
            4 => _members,
            5 => CloseBraceToken,
            6 => SemicolonToken,
            _ => null
        };
    }

    public override SyntaxNode CreateRed(SyntaxNode? parent, int position)
    {
        return new ConstantBufferDeclarationSyntax(this, parent, position);
    }

    public override TResult? Accept<TResult>(HlslSyntaxVisitorInternal<TResult> visitor) where TResult : default
    {
        return visitor.VisitConstantBufferDeclaration(this);
    }
}