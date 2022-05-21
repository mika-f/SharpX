// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using Microsoft.CodeAnalysis;

using SharpX.Core;

using SyntaxNode = SharpX.Core.SyntaxNode;

namespace SharpX.Hlsl.Syntax.InternalSyntax;

internal class EqualsValueClauseSyntaxInternal : HlslSyntaxNodeInternal
{
    public SyntaxTokenInternal EqualsToken { get; }

    public ExpressionSyntaxInternal Value { get; }

    public EqualsValueClauseSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal equalsToken, ExpressionSyntaxInternal value) : base(kind)
    {
        SlotCount = 2;

        AdjustWidth(equalsToken);
        EqualsToken = equalsToken;

        AdjustWidth(value);
        Value = value;
    }

    public EqualsValueClauseSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal equalsToken, ExpressionSyntaxInternal value, DiagnosticInfo[]? diagnostics, SyntaxAnnotation[]? annotations) : base(kind, diagnostics, annotations)
    {
        SlotCount = 2;

        AdjustWidth(equalsToken);
        EqualsToken = equalsToken;

        AdjustWidth(value);
        Value = value;
    }

    public override GreenNode SetAnnotations(SyntaxAnnotation[]? annotations)
    {
        return new EqualsValueClauseSyntaxInternal(Kind, EqualsToken, Value, GetDiagnostics(), annotations);
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new EqualsValueClauseSyntaxInternal(Kind, EqualsToken, Value, diagnostics, GetAnnotations());
    }

    public override GreenNode? GetSlot(int index)
    {
        return index switch
        {
            0 => EqualsToken,
            1 => Value,
            _ => null
        };
    }

    public override SyntaxNode CreateRed(SyntaxNode? parent, int position)
    {
        return new EqualsValueClauseSyntax(this, parent, position);
    }

    public override TResult? Accept<TResult>(HlslSyntaxVisitorInternal<TResult> visitor) where TResult : default
    {
        return visitor.VisitEqualsValueClause(this);
    }
}