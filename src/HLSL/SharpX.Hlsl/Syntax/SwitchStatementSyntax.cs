// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.Hlsl.Syntax.InternalSyntax;

namespace SharpX.Hlsl.Syntax;

public class SwitchStatementSyntax : StatementSyntax
{
    private SyntaxNode? _attributeLists;
    private ExpressionSyntax? _expression;
    private SyntaxNode? _sections;

    public override SyntaxList<AttributeListSyntax> AttributeLists => new(GetRedAtZero(ref _attributeLists));

    public SyntaxToken SwitchKeyword => new(this, ((SwitchStatementSyntaxInternal)Green).SwitchKeyword, GetChildPosition(1), GetChildIndex(1));

    public SyntaxToken OpenParenToken => new(this, ((SwitchStatementSyntaxInternal)Green).OpenParenToken, GetChildPosition(2), GetChildIndex(2));

    public ExpressionSyntax Expression => GetRed(ref _expression, 3)!;

    public SyntaxToken CloseParenToken => new(this, ((SwitchStatementSyntaxInternal)Green).CloseParenToken, GetChildPosition(4), GetChildIndex(4));

    public SyntaxToken OpenBraceToken => new(this, ((SwitchStatementSyntaxInternal)Green).OpenBraceToken, GetChildPosition(5), GetChildIndex(5));

    public SyntaxList<SwitchSectionSyntax> Sections
    {
        get
        {
            var red = GetRed(ref _sections, 6);
            return red != null ? new SyntaxList<SwitchSectionSyntax>(red) : default;
        }
    }

    public SyntaxToken CloseBraceToken => new(this, ((SwitchStatementSyntaxInternal)Green).CloseBraceToken, GetChildPosition(7), GetChildIndex(7));

    internal SwitchStatementSyntax(HlslSyntaxNodeInternal node, SyntaxNode? parent, int position) : base(node, parent, position) { }

    public override SyntaxNode? GetNodeSlot(int index)
    {
        return index switch
        {
            0 => GetRedAtZero(ref _attributeLists)!,
            3 => GetRed(ref _expression, 3)!,
            6 => GetRed(ref _sections, 6)!,
            _ => null
        };
    }

    public override SyntaxNode? GetCachedSlot(int index)
    {
        return index switch
        {
            0 => _attributeLists,
            3 => _expression,
            6 => _sections,
            _ => null
        };
    }

    public SwitchStatementSyntax Update(SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken switchKeyword, SyntaxToken openParenToken, ExpressionSyntax expression, SyntaxToken closeParenToken, SyntaxToken openBraceToken, SyntaxList<SwitchSectionSyntax> sections, SyntaxToken closeBraceToken)
    {
        if (attributeLists != AttributeLists || switchKeyword != SwitchKeyword || openParenToken != OpenParenToken || expression != Expression || closeParenToken != CloseParenToken || openBraceToken != OpenBraceToken || sections != Sections || closeBraceToken != CloseBraceToken)
            return SyntaxFactory.SwitchStatement(attributeLists, switchKeyword, openParenToken, expression, closeParenToken, openBraceToken, sections, closeBraceToken);
        return this;
    }

    public new SwitchStatementSyntax WithAttributeLists(SyntaxList<AttributeListSyntax> attributeLists)
    {
        return Update(attributeLists, SwitchKeyword, OpenParenToken, Expression, CloseParenToken, OpenBraceToken, Sections, CloseBraceToken);
    }

    internal override StatementSyntax WithAttributeListsCore(SyntaxList<AttributeListSyntax> attributeLists)
    {
        return WithAttributeLists(attributeLists);
    }

    public SwitchStatementSyntax WithSwitchKeyword(SyntaxToken switchKeyword)
    {
        return Update(AttributeLists, switchKeyword, OpenParenToken, Expression, CloseParenToken, OpenBraceToken, Sections, CloseBraceToken);
    }

    public SwitchStatementSyntax WithOpenParenToken(SyntaxToken openParenToken)
    {
        return Update(AttributeLists, SwitchKeyword, openParenToken, Expression, CloseParenToken, OpenBraceToken, Sections, CloseBraceToken);
    }

    public SwitchStatementSyntax WithExpression(ExpressionSyntax expression)
    {
        return Update(AttributeLists, SwitchKeyword, OpenParenToken, expression, CloseParenToken, OpenBraceToken, Sections, CloseBraceToken);
    }

    public SwitchStatementSyntax WithCloseParenToken(SyntaxToken closeParenToken)
    {
        return Update(AttributeLists, SwitchKeyword, OpenParenToken, Expression, closeParenToken, OpenBraceToken, Sections, CloseBraceToken);
    }

    public SwitchStatementSyntax WithOpenBraceToken(SyntaxToken openBraceToken)
    {
        return Update(AttributeLists, SwitchKeyword, OpenParenToken, Expression, CloseParenToken, openBraceToken, Sections, CloseBraceToken);
    }

    public SwitchStatementSyntax WithSections(SyntaxList<SwitchSectionSyntax> sections)
    {
        return Update(AttributeLists, SwitchKeyword, OpenParenToken, Expression, CloseParenToken, OpenBraceToken, sections, CloseBraceToken);
    }

    public SwitchStatementSyntax WithCloseBraceToken(SyntaxToken closeBraceToken)
    {
        return Update(AttributeLists, SwitchKeyword, OpenParenToken, Expression, CloseParenToken, OpenBraceToken, Sections, closeBraceToken);
    }

    public new SwitchStatementSyntax AddAttributeLists(params AttributeListSyntax[] items)
    {
        return WithAttributeLists(AttributeLists.AddRange(items));
    }

    internal override StatementSyntax AddAttributeListsCore(params AttributeListSyntax[] items)
    {
        return AddAttributeLists(items);
    }

    public SwitchStatementSyntax AddSections(params SwitchSectionSyntax[] items)
    {
        return WithSections(Sections.AddRange(items));
    }

    public override TResult? Accept<TResult>(HlslSyntaxVisitor<TResult> visitor) where TResult : default
    {
        return visitor.VisitSwitchStatement(this);
    }
}