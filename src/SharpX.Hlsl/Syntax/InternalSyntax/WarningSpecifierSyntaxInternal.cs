// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.Core.Syntax.InternalSyntax;

namespace SharpX.Hlsl.Syntax.InternalSyntax;

internal class WarningSpecifierSyntaxInternal : HlslSyntaxNodeInternal
{
    private readonly GreenNode _numbers;

    public SyntaxTokenInternal Specifier { get; }

    public SyntaxTokenInternal ColonToken { get; }

    public SeparatedSyntaxListInternal<SyntaxTokenInternal> Numbers => new(new SyntaxListInternal<GreenNode>(_numbers));

    public WarningSpecifierSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal specifier, SyntaxTokenInternal colonToken, GreenNode numbers) : base(kind)
    {
        SlotCount = 3;

        AdjustWidth(specifier);
        Specifier = specifier;

        AdjustWidth(colonToken);
        ColonToken = colonToken;

        AdjustWidth(numbers);
        _numbers = numbers;
    }

    public WarningSpecifierSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal specifier, SyntaxTokenInternal colonToken, GreenNode numbers, DiagnosticInfo[]? diagnostics) : base(kind, diagnostics)
    {
        SlotCount = 3;

        AdjustWidth(specifier);
        Specifier = specifier;

        AdjustWidth(colonToken);
        ColonToken = colonToken;

        AdjustWidth(numbers);
        _numbers = numbers;
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new WarningSpecifierSyntaxInternal(Kind, Specifier, ColonToken, _numbers, diagnostics);
    }

    public override GreenNode? GetSlot(int index)
    {
        return index switch
        {
            0 => Specifier,
            1 => ColonToken,
            2 => _numbers,
            _ => null
        };
    }

    public override SyntaxNode CreateRed(SyntaxNode? parent, int position)
    {
        throw new NotImplementedException();
    }
}