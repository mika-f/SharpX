// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using Microsoft.CodeAnalysis;

using SharpX.Core;

using SyntaxNode = SharpX.Core.SyntaxNode;

namespace SharpX.ShaderLab.Syntax.InternalSyntax;

internal class IdentifierNameSyntaxInternal : SimpleNameSyntaxInternal
{
    public override SyntaxTokenInternal Identifier { get; }

    public IdentifierNameSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal identifier) : base(kind)
    {
        SlotCount = 1;

        AdjustWidth(identifier);
        Identifier = identifier;
    }

    public IdentifierNameSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal identifier, DiagnosticInfo[]? diagnostics, SyntaxAnnotation[]? annotations) : base(kind, diagnostics, annotations)
    {
        SlotCount = 1;

        AdjustWidth(identifier);
        Identifier = identifier;
    }

    public override GreenNode SetAnnotations(SyntaxAnnotation[]? annotations)
    {
        return new IdentifierNameSyntaxInternal(Kind, Identifier, GetDiagnostics(), annotations);
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new IdentifierNameSyntaxInternal(Kind, Identifier, diagnostics, GetAnnotations());
    }

    public override GreenNode? GetSlot(int index)
    {
        return index == 0 ? Identifier : null;
    }

    public override SyntaxNode CreateRed(SyntaxNode? parent, int position)
    {
        return new IdentifierNameSyntax(this, parent, position);
    }
}