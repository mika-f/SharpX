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

    public SyntaxTokenInternal CloseBraceToken { get; }

    public InitializerExpressionSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal openBraceToken, GreenNode? expressions, SyntaxTokenInternal closeBraceToken) : base(kind)
    {
        SlotCount = 3;

        AdjustWidth(openBraceToken);
        OpenBraceToken = openBraceToken;

        if (expressions != null)
        {
            AdjustWidth(expressions);
            _expressions = expressions;
        }

        AdjustWidth(closeBraceToken);
        CloseBraceToken = closeBraceToken;
    }

    public InitializerExpressionSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal openBraceToken, GreenNode? expressions, SyntaxTokenInternal closeBraceToken, DiagnosticInfo[]? diagnostics) : base(kind, diagnostics)
    {
        SlotCount = 3;

        AdjustWidth(openBraceToken);
        OpenBraceToken = openBraceToken;

        if (expressions != null)
        {
            AdjustWidth(expressions);
            _expressions = expressions;
        }

        AdjustWidth(closeBraceToken);
        CloseBraceToken = closeBraceToken;
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new InitializerExpressionSyntaxInternal(Kind, OpenBraceToken, _expressions, CloseBraceToken, diagnostics);
    }

    public override GreenNode? GetSlot(int index)
    {
        return index switch
        {
            0 => OpenBraceToken,
            1 => _expressions,
            2 => CloseBraceToken,
            _ => null
        };
    }

    public override SyntaxNode CreateRed(SyntaxNode? parent, int position)
    {
        return new InitializerExpressionSyntax(this, parent, position);
    }
}