// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;

namespace SharpX.ShaderLab.Syntax.InternalSyntax;

internal class UsePassDeclarationSyntaxInternal : BasePassDeclarationSyntaxInternal
{
    public override SyntaxTokenInternal Keyword { get; }

    public SyntaxTokenInternal PassReference { get; }


    public UsePassDeclarationSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal keyword, SyntaxTokenInternal passReference) : base(kind)
    {
        SlotCount = 2;

        AdjustWidth(keyword);
        Keyword = keyword;

        AdjustWidth(passReference);
        PassReference = passReference;
    }

    public UsePassDeclarationSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal keyword, SyntaxTokenInternal passReference, DiagnosticInfo[]? diagnostics) : base(kind, diagnostics)
    {
        SlotCount = 2;

        AdjustWidth(keyword);
        Keyword = keyword;

        AdjustWidth(passReference);
        PassReference = passReference;
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new UsePassDeclarationSyntaxInternal(Kind, Keyword, PassReference, diagnostics);
    }

    public override GreenNode? GetSlot(int index)
    {
        return index switch
        {
            0 => Keyword,
            1 => PassReference,
            _ => null
        };
    }

    public override SyntaxNode CreateRed(SyntaxNode? parent, int position)
    {
        return new UsePassDeclarationSyntax(this, parent, position);
    }
}