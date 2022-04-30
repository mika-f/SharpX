// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using Microsoft.CodeAnalysis;

using SharpX.Core;

using SyntaxNode = SharpX.Core.SyntaxNode;

namespace SharpX.Hlsl.Syntax.InternalSyntax;

internal class InvocationExpressionSyntaxInternal : ExpressionSyntaxInternal
{
    public ExpressionSyntaxInternal Expression { get; }

    public ArgumentListSyntaxInternal ArgumentList { get; }

    public InvocationExpressionSyntaxInternal(SyntaxKind kind, ExpressionSyntaxInternal expression, ArgumentListSyntaxInternal argumentList) : base(kind)
    {
        SlotCount = 2;

        AdjustWidth(expression);
        Expression = expression;

        AdjustWidth(argumentList);
        ArgumentList = argumentList;
    }

    public InvocationExpressionSyntaxInternal(SyntaxKind kind, ExpressionSyntaxInternal expression, ArgumentListSyntaxInternal argumentList, DiagnosticInfo[]? diagnostics, SyntaxAnnotation[]? annotations) : base(kind, diagnostics, annotations)
    {
        SlotCount = 2;

        AdjustWidth(expression);
        Expression = expression;

        AdjustWidth(argumentList);
        ArgumentList = argumentList;
    }

    public override GreenNode SetAnnotations(SyntaxAnnotation[]? annotations)
    {
        return new InvocationExpressionSyntaxInternal(Kind, Expression, ArgumentList, GetDiagnostics(), annotations);
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new InvocationExpressionSyntaxInternal(Kind, Expression, ArgumentList, diagnostics, GetAnnotations());
    }

    public override GreenNode? GetSlot(int index)
    {
        return index switch
        {
            0 => Expression,
            1 => ArgumentList,
            _ => null
        };
    }

    public override SyntaxNode CreateRed(SyntaxNode? parent, int position)
    {
        return new InvocationExpressionSyntax(this, parent, position);
    }
}