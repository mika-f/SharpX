// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using Microsoft.CodeAnalysis;

using SharpX.Core;
using SharpX.Core.Syntax.InternalSyntax;

using SyntaxNode = SharpX.Core.SyntaxNode;

namespace SharpX.Hlsl.Syntax.InternalSyntax;

internal class StructDeclarationSyntaxInternal : TypeDeclarationSyntaxInternal
{
    private readonly GreenNode? _members;

    public override SyntaxTokenInternal Keyword { get; }

    public override SyntaxTokenInternal Identifier { get; }

    public override SyntaxTokenInternal OpenBraceToken { get; }

    public override SyntaxListInternal<MemberDeclarationSyntaxInternal> Members => new(_members);

    public override SyntaxTokenInternal CloseBraceToken { get; }

    public SyntaxTokenInternal SemicolonToken { get; }

    public StructDeclarationSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal keyword, SyntaxTokenInternal identifier, SyntaxTokenInternal openBraceToken, GreenNode? members, SyntaxTokenInternal closeBraceToken, SyntaxTokenInternal semicolonToken) : base(kind)
    {
        SlotCount = 6;

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

        AdjustWidth(semicolonToken);
        SemicolonToken = semicolonToken;
    }

    public StructDeclarationSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal keyword, SyntaxTokenInternal identifier, SyntaxTokenInternal openBraceToken, GreenNode? members, SyntaxTokenInternal closeBraceToken, SyntaxTokenInternal semicolonToken,
                                           DiagnosticInfo[]? diagnostics, SyntaxAnnotation[]? annotations) : base(kind, diagnostics, annotations)
    {
        SlotCount = 6;

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

        AdjustWidth(semicolonToken);
        SemicolonToken = semicolonToken;
    }

    public override GreenNode SetAnnotations(SyntaxAnnotation[]? annotations)
    {
        return new StructDeclarationSyntaxInternal(Kind, Keyword, Identifier, OpenBraceToken, _members, CloseBraceToken, SemicolonToken, GetDiagnostics(), annotations);
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new StructDeclarationSyntaxInternal(Kind, Keyword, Identifier, OpenBraceToken, _members, CloseBraceToken, SemicolonToken, diagnostics, GetAnnotations());
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
            5 => SemicolonToken,
            _ => null
        };
    }

    public override SyntaxNode CreateRed(SyntaxNode? parent, int position)
    {
        return new StructDeclarationSyntax(this, parent, position);
    }
}