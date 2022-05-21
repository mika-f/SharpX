// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.Hlsl.Syntax.InternalSyntax;

namespace SharpX.Hlsl.Syntax;

public class PassDeclarationSyntax : MemberDeclarationSyntax
{
    private SyntaxNode? _statements;

    public SyntaxToken Keyword => new(this, ((PassDeclarationSyntaxInternal)Green).Keyword, Position, 0);

    public SyntaxToken Identifier => new(this, ((PassDeclarationSyntaxInternal)Green).Identifier, GetChildPosition(1), GetChildIndex(1));

    public SyntaxToken OpenBraceToken => new(this, ((PassDeclarationSyntaxInternal)Green).OpenBraceToken, GetChildPosition(2), GetChildIndex(2));

    public SyntaxList<StatementSyntax> Statements => new(GetRed(ref _statements, 3));

    public SyntaxToken CloseBraceToken => new(this, ((PassDeclarationSyntaxInternal)Green).CloseBraceToken, GetChildPosition(4), GetChildIndex(4));

    internal PassDeclarationSyntax(HlslSyntaxNodeInternal node, SyntaxNode? parent, int position) : base(node, parent, position) { }

    public override SyntaxNode? GetNodeSlot(int index)
    {
        return index == 3 ? GetRed(ref _statements, 3) : null;
    }

    public override SyntaxNode? GetCachedSlot(int index)
    {
        return index == 3 ? _statements : null;
    }

    public PassDeclarationSyntax Update(SyntaxToken keyword, SyntaxToken identifier, SyntaxToken openBraceToken, SyntaxList<StatementSyntax> statements, SyntaxToken closeBraceToken)
    {
        if (keyword != Keyword || identifier != Identifier || openBraceToken != OpenBraceToken || statements != Statements || closeBraceToken != CloseBraceToken)
            return SyntaxFactory.PassDeclaration(keyword, identifier, openBraceToken, statements, closeBraceToken);
        return this;
    }

    public PassDeclarationSyntax WithKeyword(SyntaxToken keyword)
    {
        return Update(keyword, Identifier, OpenBraceToken, Statements, CloseBraceToken);
    }

    public PassDeclarationSyntax WithIdentifier(SyntaxToken identifier)
    {
        return Update(Keyword, identifier, OpenBraceToken, Statements, CloseBraceToken);
    }

    public PassDeclarationSyntax WithOpenParenToken(SyntaxToken openParenToken)
    {
        return Update(Keyword, Identifier, openParenToken, Statements, CloseBraceToken);
    }

    public PassDeclarationSyntax WithStatements(SyntaxList<StatementSyntax> statements)
    {
        return Update(Keyword, Identifier, OpenBraceToken, statements, CloseBraceToken);
    }

    public PassDeclarationSyntax WithCloseBraceToken(SyntaxToken closeBraceToken)
    {
        return Update(Keyword, Identifier, OpenBraceToken, Statements, closeBraceToken);
    }

    public PassDeclarationSyntax AddStatements(params StatementSyntax[] items)
    {
        return WithStatements(Statements.AddRange(items));
    }

    public override TResult? Accept<TResult>(HlslSyntaxVisitor<TResult> visitor) where TResult : default
    {
        return visitor.VisitPassDeclaration(this);
    }
}