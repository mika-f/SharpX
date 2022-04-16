// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.Core.Syntax.InternalSyntax;

namespace SharpX.Hlsl.Syntax.InternalSyntax;

internal class InitializerExpressionSyntaxInternal : ExpressionSyntaxInternal
{
    private readonly GreenNode? _expressions;

    public SyntaxTokenInternal OpenBraceToken { get; }

    public SeparatedSyntaxListInternal<ExpressionSyntaxInternal> Expressions => new(new SyntaxListInternal<HlslSyntaxNodeInternal>(_expressions));

    public SyntaxTokenInternal CloseParenToken { get; }

    public InitializerExpressionSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal openBraceToken, GreenNode? expressions, SyntaxTokenInternal closeParenToken) : base(kind)
    {
        SlotCount = 3;

        AdjustWidth(openBraceToken);
        OpenBraceToken = openBraceToken;

        if (expressions != null)
        {
            AdjustWidth(expressions);
            _expressions = expressions;
        }

        AdjustWidth(closeParenToken);
        CloseParenToken = closeParenToken;
    }

    public InitializerExpressionSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal openBraceToken, GreenNode? expressions, SyntaxTokenInternal closeParenToken, DiagnosticInfo[]? diagnostics) : base(kind, diagnostics)
    {
        SlotCount = 3;

        AdjustWidth(openBraceToken);
        OpenBraceToken = openBraceToken;

        if (expressions != null)
        {
            AdjustWidth(expressions);
            _expressions = expressions;
        }

        AdjustWidth(closeParenToken);
        CloseParenToken = closeParenToken;
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new InitializerExpressionSyntaxInternal(Kind, OpenBraceToken, _expressions, CloseParenToken);
    }

    public override GreenNode? GetSlot(int index)
    {
        return index switch
        {
            0 => OpenBraceToken,
            1 => _expressions,
            2 => CloseParenToken,
            _ => null
        };
    }

    public override SyntaxNode CreateRed(SyntaxNode? parent, int position)
    {
        throw new NotImplementedException();
    }
}