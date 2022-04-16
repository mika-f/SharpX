// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;

namespace SharpX.Hlsl.Syntax.InternalSyntax;

internal class ElementAccessExpressionSyntaxInternal : ExpressionSyntaxInternal
{
    public ExpressionSyntaxInternal Expression { get; }

    public BracketedArgumentListSyntaxInternal ArgumentList { get; }

    public ElementAccessExpressionSyntaxInternal(SyntaxKind kind, ExpressionSyntaxInternal expression, BracketedArgumentListSyntaxInternal argumentList) : base(kind)
    {
        SlotCount = 2;

        AdjustWidth(expression);
        Expression = expression;

        AdjustWidth(argumentList);
        ArgumentList = argumentList;
    }

    public ElementAccessExpressionSyntaxInternal(SyntaxKind kind, ExpressionSyntaxInternal expression, BracketedArgumentListSyntaxInternal argumentList, DiagnosticInfo[]? diagnostics) : base(kind, diagnostics)
    {
        SlotCount = 2;

        AdjustWidth(expression);
        Expression = expression;

        AdjustWidth(argumentList);
        ArgumentList = argumentList;
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new ElementAccessExpressionSyntaxInternal(Kind, Expression, ArgumentList, diagnostics);
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
        throw new NotImplementedException();
    }
}