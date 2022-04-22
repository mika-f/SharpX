// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;

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

    public AttributeArgumentSyntaxInternal(SyntaxKind kind, ExpressionSyntaxInternal expression, DiagnosticInfo[]? diagnostics) : base(kind, diagnostics)
    {
        SlotCount = 1;

        AdjustWidth(expression);
        Expression = expression;
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new AttributeArgumentSyntaxInternal(Kind, Expression, diagnostics);
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
}