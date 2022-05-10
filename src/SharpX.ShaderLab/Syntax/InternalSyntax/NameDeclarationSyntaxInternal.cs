// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using Microsoft.CodeAnalysis;

using SharpX.Core;

using SyntaxNode = SharpX.Core.SyntaxNode;

namespace SharpX.ShaderLab.Syntax.InternalSyntax;

internal class NameDeclarationSyntaxInternal : BaseCommandDeclarationSyntaxInternal
{
    public override SyntaxTokenInternal Keyword { get; }

    public SyntaxTokenInternal Name { get; }

    public NameDeclarationSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal keyword, SyntaxTokenInternal name) : base(kind)
    {
        SlotCount = 2;

        AdjustWidth(keyword);
        Keyword = keyword;

        AdjustWidth(name);
        Name = name;
    }

    public NameDeclarationSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal keyword, SyntaxTokenInternal name, DiagnosticInfo[]? diagnostics, SyntaxAnnotation[]? annotations) : base(kind, diagnostics, annotations)
    {
        SlotCount = 2;

        AdjustWidth(keyword);
        Keyword = keyword;

        AdjustWidth(name);
        Name = name;
    }

    public override GreenNode SetAnnotations(SyntaxAnnotation[]? annotations)
    {
        return new NameDeclarationSyntaxInternal(Kind, Keyword, Name, GetDiagnostics(), annotations);
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new NameDeclarationSyntaxInternal(Kind, Keyword, Name, diagnostics, GetAnnotations());
    }

    public override GreenNode? GetSlot(int index)
    {
        return index switch
        {
            0 => Keyword,
            1 => Name,
            _ => null
        };
    }

    public override SyntaxNode CreateRed(SyntaxNode? parent, int position)
    {
        return new NameDeclarationSyntax(this, parent, position);
    }
}