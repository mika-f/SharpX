// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;

namespace SharpX.Hlsl.Syntax.InternalSyntax;

internal class PredefinedTypeSyntaxInternal : TypeSyntaxInternal
{
    public SyntaxTokenInternal Keyword { get; }

    public PredefinedTypeSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal keyword) : base(kind)
    {
        SlotCount = 1;

        AdjustWidth(keyword);
        Keyword = keyword;
    }

    public PredefinedTypeSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal keyword, DiagnosticInfo[]? diagnostics) : base(kind, diagnostics)
    {
        SlotCount = 1;

        AdjustWidth(keyword);
        Keyword = keyword;
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new PredefinedTypeSyntaxInternal(Kind, Keyword, diagnostics);
    }

    public override GreenNode? GetSlot(int index)
    {
        return index switch
        {
            0 => Keyword,
            _ => null
        };
    }

    public override SyntaxNode CreateRed(SyntaxNode? parent, int position)
    {
        throw new NotImplementedException();
    }
}