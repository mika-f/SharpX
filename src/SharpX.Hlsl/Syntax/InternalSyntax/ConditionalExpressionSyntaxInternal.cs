// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using Microsoft.CodeAnalysis;

using SharpX.Core;

using SyntaxNode = SharpX.Core.SyntaxNode;

namespace SharpX.Hlsl.Syntax.InternalSyntax;

internal class ConditionalExpressionSyntaxInternal : ExpressionSyntaxInternal
{
    public ExpressionSyntaxInternal Condition { get; }

    public SyntaxTokenInternal QuestionToken { get; }

    public ExpressionSyntaxInternal WhenTrue { get; }

    public SyntaxTokenInternal ColonToken { get; }

    public ExpressionSyntaxInternal WhenFalse { get; }

    public ConditionalExpressionSyntaxInternal(SyntaxKind kind, ExpressionSyntaxInternal condition, SyntaxTokenInternal questionToken, ExpressionSyntaxInternal whenTrue, SyntaxTokenInternal colonToken, ExpressionSyntaxInternal whenFalse) : base(kind)
    {
        SlotCount = 5;

        AdjustWidth(condition);
        Condition = condition;

        AdjustWidth(questionToken);
        QuestionToken = questionToken;

        AdjustWidth(whenTrue);
        WhenTrue = whenTrue;

        AdjustWidth(colonToken);
        ColonToken = colonToken;

        AdjustWidth(whenFalse);
        WhenFalse = whenFalse;
    }

    public ConditionalExpressionSyntaxInternal(SyntaxKind kind, ExpressionSyntaxInternal condition, SyntaxTokenInternal questionToken, ExpressionSyntaxInternal whenTrue, SyntaxTokenInternal colonToken, ExpressionSyntaxInternal whenFalse, DiagnosticInfo[]? diagnostics,
                                               SyntaxAnnotation[]? annotations) : base(kind, diagnostics, annotations)
    {
        SlotCount = 5;

        AdjustWidth(condition);
        Condition = condition;

        AdjustWidth(questionToken);
        QuestionToken = questionToken;

        AdjustWidth(whenTrue);
        WhenTrue = whenTrue;

        AdjustWidth(colonToken);
        ColonToken = colonToken;

        AdjustWidth(whenFalse);
        WhenFalse = whenFalse;
    }

    public override GreenNode SetAnnotations(SyntaxAnnotation[]? annotations)
    {
        return new ConditionalExpressionSyntaxInternal(Kind, Condition, QuestionToken, WhenTrue, ColonToken, WhenFalse, GetDiagnostics(), annotations);
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new ConditionalExpressionSyntaxInternal(Kind, Condition, QuestionToken, WhenTrue, ColonToken, WhenFalse, diagnostics, GetAnnotations());
    }

    public override GreenNode? GetSlot(int index)
    {
        return index switch
        {
            0 => Condition,
            1 => QuestionToken,
            2 => WhenTrue,
            3 => ColonToken,
            4 => WhenFalse,
            _ => null
        };
    }

    public override SyntaxNode CreateRed(SyntaxNode? parent, int position)
    {
        return new ConditionalExpressionSyntax(this, parent, position);
    }

    public override TResult? Accept<TResult>(HlslSyntaxVisitorInternal<TResult> visitor) where TResult : default
    {
        return visitor.VisitConditionalExpression(this);
    }
}