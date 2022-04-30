// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using Microsoft.CodeAnalysis;

using SharpX.Core;
using SharpX.Core.Syntax.InternalSyntax;

using SyntaxNode = SharpX.Core.SyntaxNode;

namespace SharpX.Hlsl.Syntax.InternalSyntax;

internal class IfStatementSyntaxInternal : StatementSyntaxInternal
{
    private readonly GreenNode? _attributeLists;

    public override SyntaxListInternal<AttributeListSyntaxInternal> AttributeLists => new(_attributeLists);

    public SyntaxTokenInternal IfKeyword { get; }

    public SyntaxTokenInternal OpenParenToken { get; }

    public ExpressionSyntaxInternal Condition { get; }

    public SyntaxTokenInternal CloseParenToken { get; }

    public StatementSyntaxInternal Statement { get; }

    public ElseClauseSyntaxInternal? Else { get; }

    public IfStatementSyntaxInternal(SyntaxKind kind, GreenNode? attributeLists, SyntaxTokenInternal ifKeyword, SyntaxTokenInternal openParenToken, ExpressionSyntaxInternal condition, SyntaxTokenInternal closeParenToken, StatementSyntaxInternal statement,
                                     ElseClauseSyntaxInternal? @else) : base(kind)
    {
        SlotCount = 7;

        if (attributeLists != null)
        {
            AdjustWidth(attributeLists);
            _attributeLists = attributeLists;
        }

        AdjustWidth(ifKeyword);
        IfKeyword = ifKeyword;

        AdjustWidth(openParenToken);
        OpenParenToken = openParenToken;

        AdjustWidth(condition);
        Condition = condition;

        AdjustWidth(closeParenToken);
        CloseParenToken = closeParenToken;

        AdjustWidth(statement);
        Statement = statement;

        if (@else != null)
        {
            AdjustWidth(@else);
            Else = @else;
        }
    }

    public IfStatementSyntaxInternal(SyntaxKind kind, GreenNode? attributeLists, SyntaxTokenInternal ifKeyword, SyntaxTokenInternal openParenToken, ExpressionSyntaxInternal condition, SyntaxTokenInternal closeParenToken, StatementSyntaxInternal statement, ElseClauseSyntaxInternal? @else,
                                     DiagnosticInfo[]? diagnostics, SyntaxAnnotation[]? annotations) : base(kind, diagnostics, annotations)
    {
        SlotCount = 7;

        if (attributeLists != null)
        {
            AdjustWidth(attributeLists);
            _attributeLists = attributeLists;
        }

        AdjustWidth(ifKeyword);
        IfKeyword = ifKeyword;

        AdjustWidth(openParenToken);
        OpenParenToken = openParenToken;

        AdjustWidth(condition);
        Condition = condition;

        AdjustWidth(closeParenToken);
        CloseParenToken = closeParenToken;

        AdjustWidth(statement);
        Statement = statement;

        if (@else != null)
        {
            AdjustWidth(@else);
            Else = @else;
        }
    }

    public override GreenNode SetAnnotations(SyntaxAnnotation[]? annotations)
    {
        return new IfStatementSyntaxInternal(Kind, _attributeLists, IfKeyword, OpenParenToken, Condition, CloseParenToken, Statement, Else, GetDiagnostics(), annotations);
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new IfStatementSyntaxInternal(Kind, _attributeLists, IfKeyword, OpenParenToken, Condition, CloseParenToken, Statement, Else, diagnostics, GetAnnotations());
    }

    public override GreenNode? GetSlot(int index)
    {
        return index switch
        {
            0 => _attributeLists,
            1 => IfKeyword,
            2 => OpenParenToken,
            3 => Condition,
            4 => CloseParenToken,
            5 => Statement,
            6 => Else,
            _ => null
        };
    }

    public override SyntaxNode CreateRed(SyntaxNode? parent, int position)
    {
        return new IfStatementSyntax(this, parent, position);
    }
}