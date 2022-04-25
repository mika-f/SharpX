// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.Hlsl.Syntax.InternalSyntax;

namespace SharpX.Hlsl.Syntax;

public class ForStatementSyntax : StatementSyntax
{
    private SyntaxNode? _attributeLists;
    private ExpressionSyntax? _condition;
    private VariableDeclarationSyntax? _declaration;
    private SyntaxNode? _incrementors;
    private SyntaxNode? _initializers;
    private StatementSyntax? _statement;

    public override SyntaxList<AttributeListSyntax> AttributeLists => new(GetRedAtZero(ref _attributeLists));

    public SyntaxToken ForKeyword => new(this, ((ForStatementSyntaxInternal)Green).ForKeyword, GetChildPosition(1), GetChildIndex(1));

    public SyntaxToken OpenParenToken => new(this, ((ForStatementSyntaxInternal)Green).OpenParenKeyword, GetChildPosition(2), GetChildIndex(2));

    public VariableDeclarationSyntax? Declaration => GetRed(ref _declaration, 3);

    public SeparatedSyntaxList<ExpressionSyntax> Initializers
    {
        get
        {
            var red = GetRed(ref _initializers, 4);
            return red != null ? new SeparatedSyntaxList<ExpressionSyntax>(red, GetChildIndex(4)) : default;
        }
    }

    public SyntaxToken FirstSemicolonToken => new(this, ((ForStatementSyntaxInternal)Green).FirstSemicolonToken, GetChildPosition(5), GetChildIndex(5));

    public ExpressionSyntax? Condition => GetRed(ref _condition, 6);

    public SyntaxToken SecondSemicolonToken => new(this, ((ForStatementSyntaxInternal)Green).SecondSemicolonToken, GetChildPosition(7), GetChildIndex(7));

    public SeparatedSyntaxList<ExpressionSyntax> Incrementors
    {
        get
        {
            var red = GetRed(ref _incrementors, 8);
            return red != null ? new SeparatedSyntaxList<ExpressionSyntax>(red, GetChildIndex(8)) : default;
        }
    }

    public SyntaxToken CloseParenToken => new(this, ((ForStatementSyntaxInternal)Green).CloseParenToken, GetChildPosition(9), GetChildIndex(9));

    public StatementSyntax Statement => GetRed(ref _statement, 10)!;


    internal ForStatementSyntax(HlslSyntaxNodeInternal node, SyntaxNode? parent, int position) : base(node, parent, position) { }

    public override SyntaxNode? GetNodeSlot(int index)
    {
        return index switch
        {
            0 => GetRedAtZero(ref _attributeLists)!,
            3 => GetRed(ref _declaration, 3)!,
            4 => GetRed(ref _initializers, 4)!,
            6 => GetRed(ref _condition, 6)!,
            8 => GetRed(ref _incrementors, 8),
            10 => GetRed(ref _statement, 10),
            _ => null
        };
    }

    public override SyntaxNode? GetCachedSlot(int index)
    {
        return index switch
        {
            0 => _attributeLists,
            3 => _declaration,
            4 => _initializers,
            6 => _condition,
            8 => _incrementors,
            10 => _statement,
            _ => null
        };
    }

    public ForStatementSyntax Update(SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken forKeyword, SyntaxToken openParenToken, VariableDeclarationSyntax? declaration, SeparatedSyntaxList<ExpressionSyntax> initializers, SyntaxToken firstSemicolonToken, ExpressionSyntax? condition,
                                     SyntaxToken secondSemicolonToken, SeparatedSyntaxList<ExpressionSyntax> incrementors, SyntaxToken closeParenToken, StatementSyntax statement)
    {
        if (attributeLists != AttributeLists || forKeyword != ForKeyword || openParenToken != OpenParenToken || declaration != Declaration || initializers != Initializers || firstSemicolonToken != FirstSemicolonToken || condition != Condition || secondSemicolonToken != SecondSemicolonToken ||
            incrementors != Incrementors || closeParenToken != CloseParenToken || statement != Statement)
            return SyntaxFactory.ForStatement(attributeLists, forKeyword, openParenToken, declaration, initializers, firstSemicolonToken, condition, secondSemicolonToken, incrementors, closeParenToken, statement);
        return this;
    }

    public new ForStatementSyntax WithAttributeLists(SyntaxList<AttributeListSyntax> attributeLists)
    {
        return Update(attributeLists, ForKeyword, OpenParenToken, Declaration, Initializers, FirstSemicolonToken, Condition, SecondSemicolonToken, Incrementors, CloseParenToken, Statement);
    }

    internal override StatementSyntax WithAttributeListsCore(SyntaxList<AttributeListSyntax> attributeLists)
    {
        return WithAttributeLists(attributeLists);
    }

    public ForStatementSyntax WithForKeyword(SyntaxToken forKeyword)
    {
        return Update(AttributeLists, forKeyword, OpenParenToken, Declaration, Initializers, FirstSemicolonToken, Condition, SecondSemicolonToken, Incrementors, CloseParenToken, Statement);
    }

    public ForStatementSyntax WithOpenParenToken(SyntaxToken openParenToken)
    {
        return Update(AttributeLists, ForKeyword, openParenToken, Declaration, Initializers, FirstSemicolonToken, Condition, SecondSemicolonToken, Incrementors, CloseParenToken, Statement);
    }

    public ForStatementSyntax WithDeclaration(VariableDeclarationSyntax? declaration)
    {
        return Update(AttributeLists, ForKeyword, OpenParenToken, declaration, Initializers, FirstSemicolonToken, Condition, SecondSemicolonToken, Incrementors, CloseParenToken, Statement);
    }

    public ForStatementSyntax WithInitializers(SeparatedSyntaxList<ExpressionSyntax> initializers)
    {
        return Update(AttributeLists, ForKeyword, OpenParenToken, Declaration, initializers, FirstSemicolonToken, Condition, SecondSemicolonToken, Incrementors, CloseParenToken, Statement);
    }

    public ForStatementSyntax WithFirstSemicolonToken(SyntaxToken firstSemicolonToken)
    {
        return Update(AttributeLists, ForKeyword, OpenParenToken, Declaration, Initializers, firstSemicolonToken, Condition, SecondSemicolonToken, Incrementors, CloseParenToken, Statement);
    }

    public ForStatementSyntax WithCondition(ExpressionSyntax? condition)
    {
        return Update(AttributeLists, ForKeyword, OpenParenToken, Declaration, Initializers, FirstSemicolonToken, condition, SecondSemicolonToken, Incrementors, CloseParenToken, Statement);
    }

    public ForStatementSyntax WithSecondSemicolonToken(SyntaxToken secondSemicolonToken)
    {
        return Update(AttributeLists, ForKeyword, OpenParenToken, Declaration, Initializers, FirstSemicolonToken, Condition, secondSemicolonToken, Incrementors, CloseParenToken, Statement);
    }

    public ForStatementSyntax WithIncrementors(SeparatedSyntaxList<ExpressionSyntax> incrementors)
    {
        return Update(AttributeLists, ForKeyword, OpenParenToken, Declaration, Initializers, FirstSemicolonToken, Condition, SecondSemicolonToken, incrementors, CloseParenToken, Statement);
    }

    public ForStatementSyntax WithCloseParenToken(SyntaxToken closeParenToken)
    {
        return Update(AttributeLists, ForKeyword, OpenParenToken, Declaration, Initializers, FirstSemicolonToken, Condition, SecondSemicolonToken, Incrementors, closeParenToken, Statement);
    }

    public ForStatementSyntax WithStatement(StatementSyntax statement)
    {
        return Update(AttributeLists, ForKeyword, OpenParenToken, Declaration, Initializers, FirstSemicolonToken, Condition, SecondSemicolonToken, Incrementors, CloseParenToken, statement);
    }

    public new ForStatementSyntax AddAttributeLists(params AttributeListSyntax[] items)
    {
        return WithAttributeLists(AttributeLists.AddRange(items));
    }

    internal override StatementSyntax AddAttributeListsCore(params AttributeListSyntax[] items)
    {
        return AddAttributeLists(items);
    }

    public ForStatementSyntax AddInitializers(params ExpressionSyntax[] items)
    {
        return WithInitializers(Initializers.AddRange(items));
    }

    public ForStatementSyntax AddIncrementors(params ExpressionSyntax[] items)
    {
        return WithIncrementors(Incrementors.AddRange(items));
    }
}