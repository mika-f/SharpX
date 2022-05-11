// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using Microsoft.CodeAnalysis;

using SharpX.Core;

using SyntaxNode = SharpX.Core.SyntaxNode;

namespace SharpX.ShaderLab.Syntax.InternalSyntax;

internal class GrabPassDeclarationSyntaxInternal : BasePassDeclarationSyntaxInternal
{
    public override SyntaxTokenInternal Keyword { get; }

    public SyntaxTokenInternal OpenBraceToken { get; }

    public SyntaxTokenInternal? Identifier { get; }

    public TagsDeclarationSyntaxInternal? Tags { get; }

    public NameDeclarationSyntaxInternal? Name { get; }

    public SyntaxTokenInternal CloseBraceToken { get; }

    public GrabPassDeclarationSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal keyword, SyntaxTokenInternal openBraceToken, SyntaxTokenInternal? identifier, TagsDeclarationSyntaxInternal? tags, NameDeclarationSyntaxInternal? name, SyntaxTokenInternal closeBraceToken) : base(kind)
    {
        SlotCount = 6;

        AdjustWidth(keyword);
        Keyword = keyword;

        AdjustWidth(openBraceToken);
        OpenBraceToken = openBraceToken;

        if (identifier != null)
        {
            AdjustWidth(identifier);
            Identifier = identifier;
        }

        if (tags != null)
        {
            AdjustWidth(tags);
            Tags = tags;
        }

        if (name != null)
        {
            AdjustWidth(name);
            Name = name;
        }

        AdjustWidth(closeBraceToken);
        CloseBraceToken = closeBraceToken;
    }

    public GrabPassDeclarationSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal keyword, SyntaxTokenInternal openBraceToken, SyntaxTokenInternal? identifier, TagsDeclarationSyntaxInternal? tags, NameDeclarationSyntaxInternal? name, SyntaxTokenInternal closeBraceToken,
                                             DiagnosticInfo[]? diagnostics, SyntaxAnnotation[]? annotations) : base(kind, diagnostics, annotations)
    {
        SlotCount = 6;

        AdjustWidth(keyword);
        Keyword = keyword;

        AdjustWidth(openBraceToken);
        OpenBraceToken = openBraceToken;

        if (identifier != null)
        {
            AdjustWidth(identifier);
            Identifier = identifier;
        }

        if (tags != null)
        {
            AdjustWidth(tags);
            Tags = tags;
        }

        if (name != null)
        {
            AdjustWidth(name);
            Name = name;
        }

        AdjustWidth(closeBraceToken);
        CloseBraceToken = closeBraceToken;
    }

    public override GreenNode SetAnnotations(SyntaxAnnotation[]? annotations)
    {
        return new GrabPassDeclarationSyntaxInternal(Kind, Keyword, OpenBraceToken, Identifier, Tags, Name, CloseBraceToken, GetDiagnostics(), annotations);
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new GrabPassDeclarationSyntaxInternal(Kind, Keyword, OpenBraceToken, Identifier, Tags, Name, CloseBraceToken, diagnostics, GetAnnotations());
    }

    public override GreenNode? GetSlot(int index)
    {
        return index switch
        {
            0 => Keyword,
            1 => OpenBraceToken,
            2 => Identifier,
            3 => Tags,
            4 => Name,
            5 => CloseBraceToken,
            _ => null
        };
    }

    public override SyntaxNode CreateRed(SyntaxNode? parent, int position)
    {
        return new GrabPassDeclarationSyntax(this, parent, position);
    }
}