// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using Microsoft.CodeAnalysis;

using SharpX.Core;

using SyntaxNode = SharpX.Core.SyntaxNode;

namespace SharpX.Hlsl.Syntax.InternalSyntax;

internal class AttributeArgumentSyntaxInternal : HlslSyntaxNodeInternal
{
    public ExpressionSyntaxInternal Expression { get; }

    public AttributeArgumentSyntaxInternal(SyntaxKind kind, ExpressionSyntaxInternal expression) : base(kind)
    {
        SlotCount = 1;

        AdjustWidth(expression);
        Expression = expression;
    }

    public AttributeArgumentSyntaxInternal(SyntaxKind kind, ExpressionSyntaxInternal expression, DiagnosticInfo[]? diagnostics, SyntaxAnnotation[]? annotations) : base(kind, diagnostics, annotations)
    {
        SlotCount = 1;

        AdjustWidth(expression);
        Expression = expression;
    }

    public override GreenNode SetAnnotations(SyntaxAnnotation[]? annotations)
    {
        return new AttributeArgumentSyntaxInternal(Kind, Expression, GetDiagnostics(), annotations);
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new AttributeArgumentSyntaxInternal(Kind, Expression, diagnostics, GetAnnotations());
    }

    public override GreenNode? GetSlot(int index)
    {
        return index switch
        {
            0 => Expression,
            _ => null
        };
    }

    public override SyntaxNode CreateRed(SyntaxNode? parent, int position)
    {
        return new AttributeArgumentSyntax(this, parent, position);
    }

    public override TResult? Accept<TResult>(HlslSyntaxVisitorInternal<TResult> visitor) where TResult : default
    {
        return visitor.VisitAttributeArgument(this);
    }
}