// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.Core.Syntax.InternalSyntax;

namespace SharpX.Hlsl.Syntax.InternalSyntax;

internal class WhileStatementSyntaxInternal : StatementSyntaxInternal
{
    private readonly GreenNode? _attributeLists;

    public override SyntaxListInternal<AttributeListSyntaxInternal> AttributeLists => new(_attributeLists);

    public SyntaxTokenInternal WhileKeyword { get; }

    public SyntaxTokenInternal OpenParenToken { get; }

    public ExpressionSyntaxInternal Condition { get; }

    public SyntaxTokenInternal CloseParenToken { get; }

    public StatementSyntaxInternal Statement { get; }

    public WhileStatementSyntaxInternal(SyntaxKind kind, GreenNode? attributeLists, SyntaxTokenInternal whileKeyword, SyntaxTokenInternal openParenToken, ExpressionSyntaxInternal condition, SyntaxTokenInternal closeParenToken, StatementSyntaxInternal statement) : base(kind)
    {
        SlotCount = 6;

        if (attributeLists != null)
        {
            AdjustWidth(attributeLists);
            _attributeLists = attributeLists;
        }

        AdjustWidth(whileKeyword);
        WhileKeyword = whileKeyword;

        AdjustWidth(openParenToken);
        OpenParenToken = openParenToken;

        AdjustWidth(condition);
        Condition = condition;

        AdjustWidth(closeParenToken);
        CloseParenToken = closeParenToken;

        AdjustWidth(statement);
        Statement = statement;
    }

    public WhileStatementSyntaxInternal(SyntaxKind kind, GreenNode? attributeLists, SyntaxTokenInternal whileKeyword, SyntaxTokenInternal openParenToken, ExpressionSyntaxInternal condition, SyntaxTokenInternal closeParenToken, StatementSyntaxInternal statement,
                                        DiagnosticInfo[]? diagnostics) : base(kind, diagnostics)
    {
        SlotCount = 6;

        if (attributeLists != null)
        {
            AdjustWidth(attributeLists);
            _attributeLists = attributeLists;
        }

        AdjustWidth(whileKeyword);
        WhileKeyword = whileKeyword;

        AdjustWidth(openParenToken);
        OpenParenToken = openParenToken;

        AdjustWidth(condition);
        Condition = condition;

        AdjustWidth(closeParenToken);
        CloseParenToken = closeParenToken;

        AdjustWidth(statement);
        Statement = statement;
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new WhileStatementSyntaxInternal(Kind, _attributeLists, WhileKeyword, OpenParenToken, Condition, CloseParenToken, Statement, diagnostics);
    }

    public override GreenNode? GetSlot(int index)
    {
        return index switch
        {
            0 => _attributeLists,
            1 => WhileKeyword,
            2 => OpenParenToken,
            3 => Condition,
            4 => CloseParenToken,
            5 => Statement,
            _ => null
        };
    }

    public override SyntaxNode CreateRed(SyntaxNode? parent, int position)
    {
        throw new NotImplementedException();
    }
}