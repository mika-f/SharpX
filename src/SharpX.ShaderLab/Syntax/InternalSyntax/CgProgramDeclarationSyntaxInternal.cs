﻿// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using Microsoft.CodeAnalysis;

using SharpX.Core;

using SyntaxNode = SharpX.Core.SyntaxNode;

namespace SharpX.ShaderLab.Syntax.InternalSyntax;

internal class CgProgramDeclarationSyntaxInternal : ShaderLabSyntaxNodeInternal
{
    public SyntaxTokenInternal CgProgramKeyword { get; }

    public GreenNode HlslSourceCode { get; }

    public SyntaxTokenInternal EndCgKeyword { get; }

    public CgProgramDeclarationSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal cgProgramKeyword, GreenNode source, SyntaxTokenInternal endCgKeyword) : base(kind)
    {
        SlotCount = 3;

        AdjustWidth(cgProgramKeyword);
        CgProgramKeyword = cgProgramKeyword;

        AdjustWidth(source);
        HlslSourceCode = source;

        AdjustWidth(endCgKeyword);
        EndCgKeyword = endCgKeyword;
    }

    public CgProgramDeclarationSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal cgProgramKeyword, GreenNode source, SyntaxTokenInternal endCgKeyword, DiagnosticInfo[]? diagnostics, SyntaxAnnotation[]? annotations) : base(kind, diagnostics, annotations)
    {
        SlotCount = 3;

        AdjustWidth(cgProgramKeyword);
        CgProgramKeyword = cgProgramKeyword;

        AdjustWidth(source);
        HlslSourceCode = source;

        AdjustWidth(endCgKeyword);
        EndCgKeyword = endCgKeyword;
    }

    public override GreenNode SetAnnotations(SyntaxAnnotation[]? annotations)
    {
        return new CgProgramDeclarationSyntaxInternal(Kind, CgProgramKeyword, HlslSourceCode, EndCgKeyword, GetDiagnostics(), annotations);
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new CgProgramDeclarationSyntaxInternal(Kind, CgProgramKeyword, HlslSourceCode, EndCgKeyword, diagnostics, GetAnnotations());
    }

    public override GreenNode? GetSlot(int index)
    {
        return index switch
        {
            0 => CgProgramKeyword,
            1 => HlslSourceCode,
            2 => EndCgKeyword,
            _ => null
        };
    }

    public override SyntaxNode CreateRed(SyntaxNode? parent, int position)
    {
        return new CgProgramDeclarationSyntax(this, parent, position);
    }
}