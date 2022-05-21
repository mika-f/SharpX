// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using Microsoft.CodeAnalysis;

using SharpX.Core;
using SharpX.Core.Syntax.InternalSyntax;

using SyntaxNode = SharpX.Core.SyntaxNode;

namespace SharpX.Hlsl.Syntax.InternalSyntax;

internal class ForStatementSyntaxInternal : StatementSyntaxInternal
{
    private readonly GreenNode? _attributeLists;

    public override SyntaxListInternal<AttributeListSyntaxInternal> AttributeLists => new(_attributeLists);

    public SyntaxTokenInternal ForKeyword { get; }

    public SyntaxTokenInternal OpenParenKeyword { get; }

    public VariableDeclarationSyntaxInternal? Declaration { get; }

    public GreenNode? Initializers { get; }

    public SyntaxTokenInternal FirstSemicolonToken { get; }

    public ExpressionSyntaxInternal? Condition { get; }

    public SyntaxTokenInternal SecondSemicolonToken { get; }

    public GreenNode? Incrementors { get; }

    public SyntaxTokenInternal CloseParenToken { get; }

    public StatementSyntaxInternal Statement { get; }

    public ForStatementSyntaxInternal(SyntaxKind kind, GreenNode? attributeLists, SyntaxTokenInternal forKeyword, SyntaxTokenInternal openParenToken, VariableDeclarationSyntaxInternal? declaration, GreenNode? initializers, SyntaxTokenInternal firstSemicolonToken, ExpressionSyntaxInternal? condition,
                                      SyntaxTokenInternal secondSemicolonToken, GreenNode? incrementors, SyntaxTokenInternal closeParenToken, StatementSyntaxInternal statement) : base(kind)
    {
        SlotCount = 11;

        if (attributeLists != null)
        {
            AdjustWidth(attributeLists);
            _attributeLists = attributeLists;
        }

        AdjustWidth(forKeyword);
        ForKeyword = forKeyword;

        AdjustWidth(openParenToken);
        OpenParenKeyword = openParenToken;

        if (declaration != null)
        {
            AdjustWidth(declaration);
            Declaration = declaration;
        }

        if (initializers != null)
        {
            AdjustWidth(initializers);
            Initializers = initializers;
        }

        AdjustWidth(firstSemicolonToken);
        FirstSemicolonToken = firstSemicolonToken;

        if (condition != null)
        {
            AdjustWidth(condition);
            Condition = condition;
        }

        AdjustWidth(secondSemicolonToken);
        SecondSemicolonToken = secondSemicolonToken;

        if (incrementors != null)
        {
            AdjustWidth(incrementors);
            Incrementors = incrementors;
        }

        AdjustWidth(closeParenToken);
        CloseParenToken = closeParenToken;

        AdjustWidth(statement);
        Statement = statement;
    }

    public ForStatementSyntaxInternal(SyntaxKind kind, GreenNode? attributeLists, SyntaxTokenInternal forKeyword, SyntaxTokenInternal openParenToken, VariableDeclarationSyntaxInternal? declaration, GreenNode? initializers, SyntaxTokenInternal firstSemicolonToken, ExpressionSyntaxInternal? condition,
                                      SyntaxTokenInternal secondSemicolonToken, GreenNode? incrementors, SyntaxTokenInternal closeParenToken, StatementSyntaxInternal statement, DiagnosticInfo[]? diagnostics, SyntaxAnnotation[]? annotations) : base(kind, diagnostics, annotations)
    {
        SlotCount = 11;

        if (attributeLists != null)
        {
            AdjustWidth(attributeLists);
            _attributeLists = attributeLists;
        }

        AdjustWidth(forKeyword);
        ForKeyword = forKeyword;

        AdjustWidth(openParenToken);
        OpenParenKeyword = openParenToken;

        if (declaration != null)
        {
            AdjustWidth(declaration);
            Declaration = declaration;
        }

        if (initializers != null)
        {
            AdjustWidth(initializers);
            Initializers = initializers;
        }

        AdjustWidth(firstSemicolonToken);
        FirstSemicolonToken = firstSemicolonToken;

        if (condition != null)
        {
            AdjustWidth(condition);
            Condition = condition;
        }

        AdjustWidth(secondSemicolonToken);
        SecondSemicolonToken = secondSemicolonToken;

        if (incrementors != null)
        {
            AdjustWidth(incrementors);
            Incrementors = incrementors;
        }

        AdjustWidth(closeParenToken);
        CloseParenToken = closeParenToken;

        AdjustWidth(statement);
        Statement = statement;
    }

    public override GreenNode SetAnnotations(SyntaxAnnotation[]? annotations)
    {
        return new ForStatementSyntaxInternal(Kind, _attributeLists, ForKeyword, OpenParenKeyword, Declaration, Initializers, FirstSemicolonToken, Condition, SecondSemicolonToken, Incrementors, CloseParenToken, Statement, GetDiagnostics(), annotations);
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new ForStatementSyntaxInternal(Kind, _attributeLists, ForKeyword, OpenParenKeyword, Declaration, Initializers, FirstSemicolonToken, Condition, SecondSemicolonToken, Incrementors, CloseParenToken, Statement, diagnostics, GetAnnotations());
    }

    public override GreenNode? GetSlot(int index)
    {
        return index switch
        {
            0 => _attributeLists,
            1 => ForKeyword,
            2 => OpenParenKeyword,
            3 => Declaration,
            4 => Initializers,
            5 => FirstSemicolonToken,
            6 => Condition,
            7 => SecondSemicolonToken,
            8 => Incrementors,
            9 => CloseParenToken,
            10 => Statement,
            _ => null
        };
    }

    public override SyntaxNode CreateRed(SyntaxNode? parent, int position)
    {
        return new ForStatementSyntax(this, parent, position);
    }

    public override TResult? Accept<TResult>(HlslSyntaxVisitorInternal<TResult> visitor) where TResult : default
    {
        return visitor.VisitForStatement(this);
    }
}