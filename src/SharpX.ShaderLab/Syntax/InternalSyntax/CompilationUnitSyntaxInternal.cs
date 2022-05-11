// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using Microsoft.CodeAnalysis;

using SharpX.Core;

using SyntaxNode = SharpX.Core.SyntaxNode;

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

    public CompilationUnitSyntaxInternal(SyntaxKind kind, ShaderDeclarationSyntaxInternal decl, SyntaxTokenInternal endOfFileToken, DiagnosticInfo[]? diagnostics, SyntaxAnnotation[]? annotations) : base(kind, diagnostics, annotations)
    {
        SlotCount = 2;

        AdjustWidth(decl);
        ShaderDeclaration = decl;

        AdjustWidth(endOfFileToken);
        EndOfFileToken = endOfFileToken;
    }

    public override GreenNode SetAnnotations(SyntaxAnnotation[]? annotations)
    {
        return new CompilationUnitSyntaxInternal(Kind, ShaderDeclaration, EndOfFileToken, GetDiagnostics(), annotations);
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new CompilationUnitSyntaxInternal(Kind, ShaderDeclaration, EndOfFileToken, diagnostics, GetAnnotations());
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