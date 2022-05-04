// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using Microsoft.CodeAnalysis;

using SharpX.Core;
using SharpX.Core.Syntax.InternalSyntax;

using SyntaxNode = SharpX.Core.SyntaxNode;

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

    public WarningSpecifierSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal specifier, SyntaxTokenInternal colonToken, GreenNode numbers, DiagnosticInfo[]? diagnostics, SyntaxAnnotation[]? annotations) : base(kind, diagnostics, annotations)
    {
        SlotCount = 3;

        AdjustWidth(specifier);
        Specifier = specifier;

        AdjustWidth(colonToken);
        ColonToken = colonToken;

        AdjustWidth(numbers);
        _numbers = numbers;
    }

    public override GreenNode SetAnnotations(SyntaxAnnotation[]? annotations)
    {
        return new WarningSpecifierSyntaxInternal(Kind, Specifier, ColonToken, _numbers, GetDiagnostics(), annotations);
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new WarningSpecifierSyntaxInternal(Kind, Specifier, ColonToken, _numbers, diagnostics, GetAnnotations());
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

    public override TResult? Accept<TResult>(HlslSyntaxVisitorInternal<TResult> visitor) where TResult : default
    {
        throw new NotImplementedException();
    }
}