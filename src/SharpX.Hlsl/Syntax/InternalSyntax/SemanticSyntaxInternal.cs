// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using Microsoft.CodeAnalysis;

using SharpX.Core;

using SyntaxNode = SharpX.Core.SyntaxNode;

namespace SharpX.Hlsl.Syntax.InternalSyntax;

internal class SemanticSyntaxInternal : HlslSyntaxNodeInternal
{
    public SyntaxTokenInternal ColonToken { get; }

    public IdentifierNameSyntaxInternal Identifier { get; }

    public SemanticSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal colonToken, IdentifierNameSyntaxInternal identifier) : base(kind)
    {
        SlotCount = 2;

        AdjustWidth(colonToken);
        ColonToken = colonToken;

        AdjustWidth(identifier);
        Identifier = identifier;
    }

    public SemanticSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal colonToken, IdentifierNameSyntaxInternal identifier, DiagnosticInfo[]? diagnostics, SyntaxAnnotation[]? annotations) : base(kind, diagnostics, annotations)
    {
        SlotCount = 2;

        AdjustWidth(colonToken);
        ColonToken = colonToken;

        AdjustWidth(identifier);
        Identifier = identifier;
    }

    public override GreenNode SetAnnotations(SyntaxAnnotation[]? annotations)
    {
        return new SemanticSyntaxInternal(Kind, ColonToken, Identifier, GetDiagnostics(), annotations);
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new SemanticSyntaxInternal(Kind, ColonToken, Identifier, diagnostics, GetAnnotations());
    }

    public override GreenNode? GetSlot(int index)
    {
        return index switch
        {
            0 => ColonToken,
            1 => Identifier,
            _ => null
        };
    }

    public override SyntaxNode CreateRed(SyntaxNode? parent, int position)
    {
        return new SemanticSyntax(this, parent, position);
    }
}