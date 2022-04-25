// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.Core.Syntax.InternalSyntax;

namespace SharpX.Hlsl.Syntax.InternalSyntax;

internal class SwitchStatementSyntaxInternal : StatementSyntaxInternal
{
    private readonly GreenNode? _attributeLists;
    private readonly GreenNode? _sections;

    public override SyntaxListInternal<AttributeListSyntaxInternal> AttributeLists => new(_attributeLists);

    public SyntaxTokenInternal SwitchKeyword { get; }

    public SyntaxTokenInternal OpenParenToken { get; }

    public ExpressionSyntaxInternal Expression { get; }

    public SyntaxTokenInternal CloseParenToken { get; }

    public SyntaxTokenInternal OpenBraceToken { get; }

    public SyntaxListInternal<SwitchSectionSyntaxInternal> Sections => new(_sections);

    public SyntaxTokenInternal CloseBraceToken { get; }

    public SwitchStatementSyntaxInternal(SyntaxKind kind, GreenNode? attributeLists, SyntaxTokenInternal switchKeyword, SyntaxTokenInternal openParenToken, ExpressionSyntaxInternal expression, SyntaxTokenInternal closeParenToken, SyntaxTokenInternal openBraceToken, GreenNode? sections,
                                         SyntaxTokenInternal closeBraceToken) : base(kind)
    {
        SlotCount = 8;

        if (attributeLists != null)
        {
            AdjustWidth(attributeLists);
            _attributeLists = attributeLists;
        }

        AdjustWidth(switchKeyword);
        SwitchKeyword = switchKeyword;

        AdjustWidth(openParenToken);
        OpenParenToken = openParenToken;

        AdjustWidth(expression);
        Expression = expression;

        AdjustWidth(closeParenToken);
        CloseParenToken = closeParenToken;

        AdjustWidth(openBraceToken);
        OpenBraceToken = openBraceToken;

        if (sections != null)
        {
            AdjustWidth(sections);
            _sections = sections;
        }

        AdjustWidth(closeBraceToken);
        CloseBraceToken = closeBraceToken;
    }

    public SwitchStatementSyntaxInternal(SyntaxKind kind, GreenNode? attributeLists, SyntaxTokenInternal switchKeyword, SyntaxTokenInternal openParenToken, ExpressionSyntaxInternal expression, SyntaxTokenInternal closeParenToken, SyntaxTokenInternal openBraceToken, GreenNode? sections,
                                         SyntaxTokenInternal closeBraceToken, DiagnosticInfo[]? diagnostics) : base(kind, diagnostics)
    {
        SlotCount = 8;

        if (attributeLists != null)
        {
            AdjustWidth(attributeLists);
            _attributeLists = attributeLists;
        }

        AdjustWidth(switchKeyword);
        SwitchKeyword = switchKeyword;

        AdjustWidth(openParenToken);
        OpenParenToken = openParenToken;

        AdjustWidth(expression);
        Expression = expression;

        AdjustWidth(closeParenToken);
        CloseParenToken = closeParenToken;

        AdjustWidth(openBraceToken);
        OpenBraceToken = openBraceToken;

        AdjustWidth(sections);
        _sections = sections;

        AdjustWidth(closeBraceToken);
        CloseBraceToken = closeBraceToken;
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new SwitchStatementSyntaxInternal(Kind, _attributeLists, SwitchKeyword, OpenParenToken, Expression, CloseParenToken, OpenBraceToken, _sections, CloseBraceToken, diagnostics);
    }

    public override GreenNode? GetSlot(int index)
    {
        return index switch
        {
            0 => _attributeLists,
            1 => SwitchKeyword,
            2 => OpenParenToken,
            3 => Expression,
            4 => CloseParenToken,
            5 => OpenBraceToken,
            6 => _sections,
            7 => CloseBraceToken,
            _ => null
        };
    }

    public override SyntaxNode CreateRed(SyntaxNode? parent, int position)
    {
        return new SwitchStatementSyntax(this, parent, position);
    }
}