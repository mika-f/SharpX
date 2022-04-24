// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.Core.Syntax.InternalSyntax;

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

    public ReturnStatementSyntaxInternal(SyntaxKind kind, GreenNode? attributeLists, SyntaxTokenInternal returnKeyword, ExpressionSyntaxInternal? expression, SyntaxTokenInternal semicolonToken, DiagnosticInfo[]? diagnostics) : base(kind, diagnostics)
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

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new ReturnStatementSyntaxInternal(Kind, _attributeLists, ReturnKeyword, Expression, SemicolonToken, diagnostics);
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
}