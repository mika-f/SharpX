// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.Core.Syntax.InternalSyntax;

namespace SharpX.Hlsl.Syntax.InternalSyntax;

internal class ArrayRankSpecifierSyntaxInternal : HlslSyntaxNodeInternal
{
    private readonly GreenNode? _sizes;

    public SyntaxTokenInternal OpenBracketToken { get; }

    public SeparatedSyntaxListInternal<ExpressionSyntaxInternal> Sizes => new(new SyntaxListInternal<HlslSyntaxNodeInternal>(_sizes));

    public SyntaxTokenInternal CloseBracketToken { get; }

    public ArrayRankSpecifierSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal openBracketToken, GreenNode? sizes, SyntaxTokenInternal closeBracketToken) : base(kind)
    {
        SlotCount = 3;

        AdjustWidth(openBracketToken);
        OpenBracketToken = openBracketToken;

        if (sizes != null)
        {
            AdjustWidth(sizes);
            _sizes = sizes;
        }

        AdjustWidth(closeBracketToken);
        CloseBracketToken = closeBracketToken;
    }

    public ArrayRankSpecifierSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal openBracketToken, GreenNode? sizes, SyntaxTokenInternal closeBracketToken, DiagnosticInfo[]? diagnostics) : base(kind, diagnostics)
    {
        SlotCount = 3;

        AdjustWidth(openBracketToken);
        OpenBracketToken = openBracketToken;

        if (sizes != null)
        {
            AdjustWidth(sizes);
            _sizes = sizes;
        }

        AdjustWidth(closeBracketToken);
        CloseBracketToken = closeBracketToken;
    }


    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new ArrayRankSpecifierSyntaxInternal(Kind, OpenBracketToken, _sizes, CloseBracketToken, diagnostics);
    }

    public override GreenNode? GetSlot(int index)
    {
        return index switch
        {
            0 => OpenBracketToken,
            1 => _sizes,
            2 => CloseBracketToken,
            _ => null
        };
    }

    public override SyntaxNode CreateRed(SyntaxNode? parent, int position)
    {
        return new ArrayRankSpecifierSyntax(this, parent, position);
    }
}