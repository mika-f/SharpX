// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;

namespace SharpX.Hlsl.Syntax.InternalSyntax;

internal class MemberAccessExpressionSyntaxInternal : ExpressionSyntaxInternal
{
    public ExpressionSyntaxInternal Expression { get; }

    public SyntaxTokenInternal OperatorToken { get; }

    public SimpleNameSyntaxInternal Name { get; }


    public MemberAccessExpressionSyntaxInternal(SyntaxKind kind, ExpressionSyntaxInternal expression, SyntaxTokenInternal operatorToken, SimpleNameSyntaxInternal name) : base(kind)
    {
        SlotCount = 3;

        AdjustWidth(expression);
        Expression = expression;

        AdjustWidth(operatorToken);
        OperatorToken = operatorToken;

        AdjustWidth(name);
        Name = name;
    }

    public MemberAccessExpressionSyntaxInternal(SyntaxKind kind, ExpressionSyntaxInternal expression, SyntaxTokenInternal operatorToken, SimpleNameSyntaxInternal name, DiagnosticInfo[]? diagnostics) : base(kind, diagnostics)
    {
        SlotCount = 3;

        AdjustWidth(expression);
        Expression = expression;

        AdjustWidth(operatorToken);
        OperatorToken = operatorToken;

        AdjustWidth(name);
        Name = name;
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new MemberAccessExpressionSyntaxInternal(Kind, Expression, OperatorToken, Name, diagnostics);
    }

    public override GreenNode? GetSlot(int index)
    {
        return index switch
        {
            0 => Expression,
            1 => OperatorToken,
            2 => Name,
            _ => null
        };
    }

    public override SyntaxNode CreateRed(SyntaxNode? parent, int position)
    {
        return new MemberAccessExpressionSyntax(this, parent, position);
    }

    public override TResult? Accept<TResult>(HlslSyntaxVisitorInternal<TResult> visitor) where TResult : default
    {
        return visitor.VisitMemberAccessExpression(this);
    }
}