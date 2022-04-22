// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;

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

    public ArgumentSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal? refKindKeyword, ExpressionSyntaxInternal expression, DiagnosticInfo[]? diagnostics) : base(kind, diagnostics)
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

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new ArgumentSyntaxInternal(Kind, RefKindKeyword, Expression, diagnostics);
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
}