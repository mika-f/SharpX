// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using Microsoft.CodeAnalysis;

using SharpX.Core;
using SharpX.Core.Syntax.InternalSyntax;

using SyntaxNode = SharpX.Core.SyntaxNode;

namespace SharpX.Hlsl.Syntax.InternalSyntax;

internal class ReturnStatementSyntaxInternal : StatementSyntaxInternal
{
    private readonly GreenNode? _attributeLists;

    public override SyntaxListInternal<AttributeListSyntaxInternal> AttributeLists => new(_attributeLists);

    public SyntaxTokenInternal ReturnKeyword { get; }

    public ExpressionSyntaxInternal? Expression { get; }

    public SyntaxTokenInternal SemicolonToken { get; }


    public ReturnStatementSyntaxInternal(SyntaxKind kind, GreenNode? attributeLists, SyntaxTokenInternal returnKeyword, ExpressionSyntaxInternal? expression, SyntaxTokenInternal semicolonToken) : base(kind)
    {
        SlotCount = 4;

        if (attributeLists != null)
        {
            AdjustWidth(attributeLists);
            _attributeLists = attributeLists;
        }

        AdjustWidth(returnKeyword);
        ReturnKeyword = returnKeyword;

        if (expression != null)
        {
            AdjustWidth(expression);
            Expression = expression;
        }

        AdjustWidth(semicolonToken);
        SemicolonToken = semicolonToken;
    }

    public ReturnStatementSyntaxInternal(SyntaxKind kind, GreenNode? attributeLists, SyntaxTokenInternal returnKeyword, ExpressionSyntaxInternal? expression, SyntaxTokenInternal semicolonToken, DiagnosticInfo[]? diagnostics, SyntaxAnnotation[]? annotations) : base(kind, diagnostics, annotations)
    {
        SlotCount = 4;

        if (attributeLists != null)
        {
            AdjustWidth(attributeLists);
            _attributeLists = attributeLists;
        }

        AdjustWidth(returnKeyword);
        ReturnKeyword = returnKeyword;

        if (expression != null)
        {
            AdjustWidth(expression);
            Expression = expression;
        }

        AdjustWidth(semicolonToken);
        SemicolonToken = semicolonToken;
    }

    public override GreenNode SetAnnotations(SyntaxAnnotation[]? annotations)
    {
        return new ReturnStatementSyntaxInternal(Kind, _attributeLists, ReturnKeyword, Expression, SemicolonToken, GetDiagnostics(), annotations);
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new ReturnStatementSyntaxInternal(Kind, _attributeLists, ReturnKeyword, Expression, SemicolonToken, diagnostics, GetAnnotations());
    }

    public override GreenNode? GetSlot(int index)
    {
        return index switch
        {
            0 => _attributeLists,
            1 => ReturnKeyword,
            2 => Expression,
            3 => SemicolonToken,
            _ => null
        };
    }

    public override SyntaxNode CreateRed(SyntaxNode? parent, int position)
    {
        return new ReturnStatementSyntax(this, parent, position);
    }

    public override TResult? Accept<TResult>(HlslSyntaxVisitorInternal<TResult> visitor) where TResult : default
    {
        return visitor.VisitReturnStatement(this);
    }
}