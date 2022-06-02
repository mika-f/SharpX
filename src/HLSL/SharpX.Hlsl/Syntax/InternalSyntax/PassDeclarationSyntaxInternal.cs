// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.Core.Syntax.InternalSyntax;

namespace SharpX.Hlsl.Syntax.InternalSyntax;

internal class PassDeclarationSyntaxInternal : MemberDeclarationSyntaxInternal
{
    private readonly GreenNode? _statements;

    public SyntaxTokenInternal Keyword { get; }

    public SyntaxTokenInternal Identifier { get; }

    public SyntaxTokenInternal OpenBraceToken { get; }

    public SyntaxListInternal<StatementSyntaxInternal> Statements => new(_statements);

    public SyntaxTokenInternal CloseBraceToken { get; }

    public PassDeclarationSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal keyword, SyntaxTokenInternal identifier, SyntaxTokenInternal openBraceToken, GreenNode? statements, SyntaxTokenInternal closeBraceToken) : base(kind)
    {
        SlotCount = 5;

        AdjustWidth(keyword);
        Keyword = keyword;

        AdjustWidth(identifier);
        Identifier = identifier;

        AdjustWidth(openBraceToken);
        OpenBraceToken = openBraceToken;

        if (statements != null)
        {
            AdjustWidth(statements);
            _statements = statements;
        }

        AdjustWidth(closeBraceToken);
        CloseBraceToken = closeBraceToken;
    }

    public PassDeclarationSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal keyword, SyntaxTokenInternal identifier, SyntaxTokenInternal openBraceToken, GreenNode? statements, SyntaxTokenInternal closeBraceToken, DiagnosticInfo[]? diagnostics) :
        base(kind, diagnostics)
    {
        SlotCount = 5;

        AdjustWidth(keyword);
        Keyword = keyword;

        AdjustWidth(identifier);
        Identifier = identifier;

        AdjustWidth(openBraceToken);
        OpenBraceToken = openBraceToken;

        if (statements != null)
        {
            AdjustWidth(statements);
            _statements = statements;
        }

        AdjustWidth(closeBraceToken);
        CloseBraceToken = closeBraceToken;
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new PassDeclarationSyntaxInternal(Kind, Keyword, Identifier, OpenBraceToken, _statements, CloseBraceToken, diagnostics);
    }

    public override GreenNode? GetSlot(int index)
    {
        return index switch
        {
            0 => Keyword,
            1 => Identifier,
            2 => OpenBraceToken,
            3 => _statements,
            4 => CloseBraceToken,
            _ => null
        };
    }

    public override SyntaxNode CreateRed(SyntaxNode? parent, int position)
    {
        return new PassDeclarationSyntax(this, parent, position);
    }

    public override TResult? Accept<TResult>(HlslSyntaxVisitorInternal<TResult> visitor) where TResult : default
    {
        return visitor.VisitPassDeclaration(this);
    }
}