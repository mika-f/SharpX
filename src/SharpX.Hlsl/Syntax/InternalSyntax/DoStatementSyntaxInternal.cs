// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using Microsoft.CodeAnalysis;

using SharpX.Core;
using SharpX.Core.Syntax.InternalSyntax;

using SyntaxNode = SharpX.Core.SyntaxNode;

namespace SharpX.Hlsl.Syntax.InternalSyntax;

internal class DoStatementSyntaxInternal : StatementSyntaxInternal
{
    private readonly GreenNode? _attributeLists;

    public override SyntaxListInternal<AttributeListSyntaxInternal> AttributeLists => new(_attributeLists);

    public SyntaxTokenInternal DoKeyword { get; }

    public StatementSyntaxInternal Statement { get; }

    public SyntaxTokenInternal WhileKeyword { get; }

    public SyntaxTokenInternal OpenParenToken { get; }

    public ExpressionSyntaxInternal Condition { get; }

    public SyntaxTokenInternal CloseParenToken { get; }

    public SyntaxTokenInternal SemicolonToken { get; }

    public DoStatementSyntaxInternal(SyntaxKind kind, GreenNode? attributeLists, SyntaxTokenInternal doKeyword, StatementSyntaxInternal statement, SyntaxTokenInternal whileKeyword, SyntaxTokenInternal openParenToken, ExpressionSyntaxInternal condition, SyntaxTokenInternal closeParenToken,
                                     SyntaxTokenInternal semicolonToken)
        : base(kind)
    {
        SlotCount = 8;

        if (attributeLists != null)
        {
            AdjustWidth(attributeLists);
            _attributeLists = attributeLists;
        }

        AdjustWidth(doKeyword);
        DoKeyword = doKeyword;

        AdjustWidth(statement);
        Statement = statement;

        AdjustWidth(whileKeyword);
        WhileKeyword = whileKeyword;

        AdjustWidth(openParenToken);
        OpenParenToken = openParenToken;

        AdjustWidth(condition);
        Condition = condition;

        AdjustWidth(closeParenToken);
        CloseParenToken = closeParenToken;

        AdjustWidth(semicolonToken);
        SemicolonToken = semicolonToken;
    }

    public DoStatementSyntaxInternal(SyntaxKind kind, GreenNode? attributeLists, SyntaxTokenInternal doKeyword, StatementSyntaxInternal statement, SyntaxTokenInternal whileKeyword, SyntaxTokenInternal openParenToken, ExpressionSyntaxInternal condition, SyntaxTokenInternal closeParenToken,
                                     SyntaxTokenInternal semicolonToken, DiagnosticInfo[]? diagnostics, SyntaxAnnotation[]? annotations) : base(kind, diagnostics, annotations)
    {
        SlotCount = 8;

        if (attributeLists != null)
        {
            AdjustWidth(attributeLists);
            _attributeLists = attributeLists;
        }

        AdjustWidth(doKeyword);
        DoKeyword = doKeyword;

        AdjustWidth(statement);
        Statement = statement;

        AdjustWidth(whileKeyword);
        WhileKeyword = whileKeyword;

        AdjustWidth(openParenToken);
        OpenParenToken = openParenToken;

        AdjustWidth(condition);
        Condition = condition;

        AdjustWidth(closeParenToken);
        CloseParenToken = closeParenToken;

        AdjustWidth(semicolonToken);
        SemicolonToken = semicolonToken;
    }

    public override GreenNode SetAnnotations(SyntaxAnnotation[]? annotations)
    {
        return new DoStatementSyntaxInternal(Kind, _attributeLists, DoKeyword, Statement, WhileKeyword, OpenParenToken, Condition, CloseParenToken, SemicolonToken, GetDiagnostics(), annotations);
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new DoStatementSyntaxInternal(Kind, _attributeLists, DoKeyword, Statement, WhileKeyword, OpenParenToken, Condition, CloseParenToken, SemicolonToken, diagnostics, GetAnnotations());
    }

    public override GreenNode? GetSlot(int index)
    {
        return index switch
        {
            0 => _attributeLists,
            1 => DoKeyword,
            2 => Statement,
            3 => WhileKeyword,
            4 => OpenParenToken,
            5 => Condition,
            6 => CloseParenToken,
            7 => SemicolonToken,
            _ => null
        };
    }

    public override SyntaxNode CreateRed(SyntaxNode? parent, int position)
    {
        return new DoStatementSyntax(this, parent, position);
    }

    public override TResult? Accept<TResult>(HlslSyntaxVisitorInternal<TResult> visitor) where TResult : default
    {
        return visitor.VisitDoStatement(this);
    }
}