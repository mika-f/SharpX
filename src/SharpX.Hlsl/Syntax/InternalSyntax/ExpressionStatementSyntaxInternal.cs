// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.Core.Syntax.InternalSyntax;

namespace SharpX.Hlsl.Syntax.InternalSyntax;

internal class ExpressionStatementSyntaxInternal : StatementSyntaxInternal
{
    private readonly GreenNode? _attributeLists;

    public override SyntaxListInternal<AttributeListSyntaxInternal> AttributeLists => new(_attributeLists);

    public ExpressionSyntaxInternal Expression { get; }

    public SyntaxTokenInternal SemicolonToken { get; }

    public ExpressionStatementSyntaxInternal(SyntaxKind kind, GreenNode? attributeLists, ExpressionSyntaxInternal expression, SyntaxTokenInternal semicolonToken) : base(kind)
    {
        SlotCount = 3;

        if (attributeLists != null)
        {
            AdjustWidth(attributeLists);
            _attributeLists = attributeLists;
        }

        AdjustWidth(expression);
        Expression = expression;

        AdjustWidth(semicolonToken);
        SemicolonToken = semicolonToken;
    }

    public ExpressionStatementSyntaxInternal(SyntaxKind kind, GreenNode? attributeLists, ExpressionSyntaxInternal expression, SyntaxTokenInternal semicolonToken, DiagnosticInfo[]? diagnostics) : base(kind, diagnostics)
    {
        SlotCount = 3;

        if (attributeLists != null)
        {
            AdjustWidth(attributeLists);
            _attributeLists = attributeLists;
        }

        AdjustWidth(expression);
        Expression = expression;

        AdjustWidth(semicolonToken);
        SemicolonToken = semicolonToken;
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new ExpressionStatementSyntaxInternal(Kind, _attributeLists, Expression, SemicolonToken, diagnostics);
    }

    public override GreenNode? GetSlot(int index)
    {
        return index switch
        {
            0 => _attributeLists,
            1 => Expression,
            2 => SemicolonToken,
            _ => null
        };
    }

    public override SyntaxNode CreateRed(SyntaxNode? parent, int position)
    {
        throw new NotImplementedException();
    }
}