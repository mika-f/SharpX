// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using Microsoft.CodeAnalysis;

using SharpX.Core;

using SyntaxNode = SharpX.Core.SyntaxNode;

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

    public MemberAccessExpressionSyntaxInternal(SyntaxKind kind, ExpressionSyntaxInternal expression, SyntaxTokenInternal operatorToken, SimpleNameSyntaxInternal name, DiagnosticInfo[]? diagnostics, SyntaxAnnotation[]? annotations) : base(kind, diagnostics, annotations)
    {
        SlotCount = 3;

        AdjustWidth(expression);
        Expression = expression;

        AdjustWidth(operatorToken);
        OperatorToken = operatorToken;

        AdjustWidth(name);
        Name = name;
    }

    public override GreenNode SetAnnotations(SyntaxAnnotation[]? annotations)
    {
        return new MemberAccessExpressionSyntaxInternal(Kind, Expression, OperatorToken, Name, GetDiagnostics(), annotations);
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new MemberAccessExpressionSyntaxInternal(Kind, Expression, OperatorToken, Name, diagnostics, GetAnnotations());
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
}