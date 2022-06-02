// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;

namespace SharpX.ShaderLab.Syntax.InternalSyntax;

internal class CompilationUnitSyntaxInternal : ShaderLabSyntaxNodeInternal
{
    public ShaderDeclarationSyntaxInternal ShaderDeclaration { get; }

    public SyntaxTokenInternal EndOfFileToken { get; }

    public CompilationUnitSyntaxInternal(SyntaxKind kind, ShaderDeclarationSyntaxInternal decl, SyntaxTokenInternal endOfFileToken) : base(kind)
    {
        SlotCount = 2;

        AdjustWidth(decl);
        ShaderDeclaration = decl;

        AdjustWidth(endOfFileToken);
        EndOfFileToken = endOfFileToken;
    }

    public CompilationUnitSyntaxInternal(SyntaxKind kind, ShaderDeclarationSyntaxInternal decl, SyntaxTokenInternal endOfFileToken, DiagnosticInfo[]? diagnostics) : base(kind, diagnostics)
    {
        SlotCount = 2;

        AdjustWidth(decl);
        ShaderDeclaration = decl;

        AdjustWidth(endOfFileToken);
        EndOfFileToken = endOfFileToken;
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new CompilationUnitSyntaxInternal(Kind, ShaderDeclaration, EndOfFileToken, diagnostics);
    }

    public override GreenNode? GetSlot(int index)
    {
        return index == 0 ? ShaderDeclaration : null;
    }

    public override SyntaxNode CreateRed(SyntaxNode? parent, int position)
    {
        return new CompilationUnitSyntax(this, parent, position);
    }
}