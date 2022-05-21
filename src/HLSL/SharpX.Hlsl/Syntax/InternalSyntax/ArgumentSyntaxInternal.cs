// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using Microsoft.CodeAnalysis;

using SharpX.Core;

using SyntaxNode = SharpX.Core.SyntaxNode;

namespace SharpX.Hlsl.Syntax.InternalSyntax;

internal class ArgumentSyntaxInternal : HlslSyntaxNodeInternal
{
    public SyntaxTokenInternal? RefKindKeyword { get; }

    public ExpressionSyntaxInternal Expression { get; }

    public ArgumentSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal? refKindKeyword, ExpressionSyntaxInternal expression) : base(kind)
    {
        SlotCount = 2;

        if (refKindKeyword != null)
        {
            AdjustWidth(refKindKeyword);
            RefKindKeyword = refKindKeyword;
        }

        AdjustWidth(expression);
        Expression = expression;
    }

    public ArgumentSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal? refKindKeyword, ExpressionSyntaxInternal expression, DiagnosticInfo[]? diagnostics, SyntaxAnnotation[]? annotations) : base(kind, diagnostics, annotations)
    {
        SlotCount = 2;

        if (refKindKeyword != null)
        {
            AdjustWidth(refKindKeyword);
            RefKindKeyword = refKindKeyword;
        }

        AdjustWidth(expression);
        Expression = expression;
    }

    public override GreenNode SetAnnotations(SyntaxAnnotation[]? annotations)
    {
        return new ArgumentSyntaxInternal(Kind, RefKindKeyword, Expression, GetDiagnostics(), annotations);
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new ArgumentSyntaxInternal(Kind, RefKindKeyword, Expression, diagnostics, GetAnnotations());
    }

    public override GreenNode? GetSlot(int index)
    {
        return index switch
        {
            0 => RefKindKeyword,
            1 => Expression,
            _ => null
        };
    }

    public override SyntaxNode CreateRed(SyntaxNode? parent, int position)
    {
        return new ArgumentSyntax(this, parent, position);
    }

    public override TResult? Accept<TResult>(HlslSyntaxVisitorInternal<TResult> visitor) where TResult : default
    {
        return visitor.VisitArgument(this);
    }
}