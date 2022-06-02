// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;

namespace SharpX.ShaderLab.Syntax.InternalSyntax;

internal class ArgumentSyntaxInternal : ShaderLabSyntaxNodeInternal
{
    public ExpressionSyntaxInternal Expression { get; }

    public ArgumentSyntaxInternal(SyntaxKind kind, ExpressionSyntaxInternal expression) : base(kind)
    {
        SlotCount = 1;

        AdjustWidth(expression);
        Expression = expression;
    }

    public ArgumentSyntaxInternal(SyntaxKind kind, ExpressionSyntaxInternal expression, DiagnosticInfo[]? diagnostics) : base(kind, diagnostics)
    {
        SlotCount = 1;

        AdjustWidth(expression);
        Expression = expression;
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new ArgumentSyntaxInternal(Kind, Expression, diagnostics);
    }

    public override GreenNode? GetSlot(int index)
    {
        return index == 0 ? Expression : null;
    }

    public override SyntaxNode CreateRed(SyntaxNode? parent, int position)
    {
        return new ArgumentSyntax(this, parent, position);
    }
}