// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;

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

    public NameDeclarationSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal keyword, SyntaxTokenInternal name, DiagnosticInfo[]? diagnostics) : base(kind, diagnostics)
    {
        SlotCount = 2;

        AdjustWidth(keyword);
        Keyword = keyword;

        AdjustWidth(name);
        Name = name;
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new NameDeclarationSyntaxInternal(Kind, Keyword, Name, diagnostics);
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