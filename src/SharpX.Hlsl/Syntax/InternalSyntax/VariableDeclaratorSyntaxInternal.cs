// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using Microsoft.CodeAnalysis;

using SharpX.Core;

using SyntaxNode = SharpX.Core.SyntaxNode;

namespace SharpX.Hlsl.Syntax.InternalSyntax;

internal class VariableDeclaratorSyntaxInternal : HlslSyntaxNodeInternal
{
    public SyntaxTokenInternal Identifier { get; }

    public EqualsValueClauseSyntaxInternal? Initializer { get; }

    public VariableDeclaratorSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal identifier, EqualsValueClauseSyntaxInternal? initializer) : base(kind)
    {
        SlotCount = 2;

        AdjustWidth(identifier);
        Identifier = identifier;

        if (initializer != null)
        {
            AdjustWidth(initializer);
            Initializer = initializer;
        }
    }

    public VariableDeclaratorSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal identifier, EqualsValueClauseSyntaxInternal? initializer, DiagnosticInfo[]? diagnostics, SyntaxAnnotation[]? annotations) : base(kind, diagnostics, annotations)
    {
        SlotCount = 2;

        AdjustWidth(identifier);
        Identifier = identifier;

        if (initializer != null)
        {
            AdjustWidth(initializer);
            Initializer = initializer;
        }
    }

    public override GreenNode SetAnnotations(SyntaxAnnotation[]? annotations)
    {
        return new VariableDeclaratorSyntaxInternal(Kind, Identifier, Initializer, GetDiagnostics(), annotations);
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new VariableDeclaratorSyntaxInternal(Kind, Identifier, Initializer, diagnostics, GetAnnotations());
    }

    public override GreenNode? GetSlot(int index)
    {
        return index switch
        {
            0 => Identifier,
            1 => Initializer,
            _ => null
        };
    }

    public override SyntaxNode CreateRed(SyntaxNode? parent, int position)
    {
        return new VariableDeclaratorSyntax(this, parent, position);
    }

    public override TResult? Accept<TResult>(HlslSyntaxVisitorInternal<TResult> visitor) where TResult : default
    {
        throw new NotImplementedException();
    }
}