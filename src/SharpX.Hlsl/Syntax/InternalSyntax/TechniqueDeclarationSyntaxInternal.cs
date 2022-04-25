// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.Core.Syntax.InternalSyntax;

namespace SharpX.Hlsl.Syntax.InternalSyntax;

internal class TechniqueDeclarationSyntaxInternal : TypeDeclarationSyntaxInternal
{
    private readonly GreenNode? _members;

    public override SyntaxTokenInternal Keyword { get; }

    public override SyntaxTokenInternal Identifier { get; }

    public override SyntaxTokenInternal OpenBraceToken { get; }

    public override SyntaxListInternal<MemberDeclarationSyntaxInternal> Members => new(_members);

    public override SyntaxTokenInternal CloseBraceToken { get; }

    public TechniqueDeclarationSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal keyword, SyntaxTokenInternal identifier, SyntaxTokenInternal openBraceToken, GreenNode? members, SyntaxTokenInternal closeBraceToken) : base(kind)
    {
        SlotCount = 5;

        AdjustWidth(keyword);
        Keyword = keyword;

        AdjustWidth(identifier);
        Identifier = identifier;

        AdjustWidth(openBraceToken);
        OpenBraceToken = openBraceToken;

        if (members != null)
        {
            AdjustWidth(members);
            _members = members;
        }

        AdjustWidth(closeBraceToken);
        CloseBraceToken = closeBraceToken;
    }

    public TechniqueDeclarationSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal keyword, SyntaxTokenInternal identifier, SyntaxTokenInternal openBraceToken, GreenNode? members, SyntaxTokenInternal closeBraceToken, DiagnosticInfo[]? diagnostics) : base(kind, diagnostics)
    {
        SlotCount = 5;

        AdjustWidth(keyword);
        Keyword = keyword;

        AdjustWidth(identifier);
        Identifier = identifier;

        AdjustWidth(openBraceToken);
        OpenBraceToken = openBraceToken;

        if (members != null)
        {
            AdjustWidth(members);
            _members = members;
        }

        AdjustWidth(closeBraceToken);
        CloseBraceToken = closeBraceToken;
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new TechniqueDeclarationSyntaxInternal(Kind, Keyword, Identifier, OpenBraceToken, _members, CloseBraceToken, diagnostics);
    }

    public override GreenNode? GetSlot(int index)
    {
        return index switch
        {
            0 => Keyword,
            1 => Identifier,
            2 => OpenBraceToken,
            3 => _members,
            4 => CloseBraceToken,
            _ => null
        };
    }

    public override SyntaxNode CreateRed(SyntaxNode? parent, int position)
    {
        return new TechniqueDeclarationSyntax(this, parent, position);
    }
}