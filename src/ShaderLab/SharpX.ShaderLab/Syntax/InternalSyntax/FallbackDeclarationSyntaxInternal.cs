// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;

namespace SharpX.ShaderLab.Syntax.InternalSyntax;

internal class FallbackDeclarationSyntaxInternal : ShaderLabSyntaxNodeInternal
{
    public SyntaxTokenInternal FallbackKeyword { get; }

    public SyntaxTokenInternal ShaderNameOrOffKeyword { get; }

    public FallbackDeclarationSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal fallbackKeyword, SyntaxTokenInternal shaderNameOrOffKeyword) : base(kind)
    {
        SlotCount = 2;

        AdjustWidth(fallbackKeyword);
        FallbackKeyword = fallbackKeyword;

        AdjustWidth(shaderNameOrOffKeyword);
        ShaderNameOrOffKeyword = shaderNameOrOffKeyword;
    }

    public FallbackDeclarationSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal fallbackKeyword, SyntaxTokenInternal shaderNameOrOffKeyword, DiagnosticInfo[]? diagnostics) : base(kind, diagnostics)
    {
        SlotCount = 2;

        AdjustWidth(fallbackKeyword);
        FallbackKeyword = fallbackKeyword;

        AdjustWidth(shaderNameOrOffKeyword);
        ShaderNameOrOffKeyword = shaderNameOrOffKeyword;
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new FallbackDeclarationSyntaxInternal(Kind, FallbackKeyword, ShaderNameOrOffKeyword, diagnostics);
    }

    public override GreenNode? GetSlot(int index)
    {
        return index switch
        {
            0 => FallbackKeyword,
            1 => ShaderNameOrOffKeyword,
            _ => null
        };
    }

    public override SyntaxNode CreateRed(SyntaxNode? parent, int position)
    {
        return new FallbackDeclarationSyntax(this, parent, position);
    }
}