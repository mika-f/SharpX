// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.Core.Syntax.InternalSyntax;

namespace SharpX.Hlsl.Syntax.InternalSyntax;

internal class TypeArgumentListSyntaxInternal : HlslSyntaxNodeInternal
{
    private readonly GreenNode? _arguments;

    public SyntaxTokenInternal LessThanToken { get; }

    public SeparatedSyntaxListInternal<TypeSyntaxInternal> Arguments => new(new SyntaxListInternal<HlslSyntaxNodeInternal>(_arguments));

    public SyntaxTokenInternal GreaterThanToken { get; }

    public TypeArgumentListSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal lessThanToken, GreenNode? arguments, SyntaxTokenInternal greaterThanToken) : base(kind)
    {
        SlotCount = 3;

        AdjustWidth(lessThanToken);
        LessThanToken = lessThanToken;

        if (arguments != null)
        {
            AdjustWidth(arguments);
            _arguments = arguments;
        }

        AdjustWidth(greaterThanToken);
        GreaterThanToken = greaterThanToken;
    }

    public TypeArgumentListSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal lessThanToken, GreenNode? arguments, SyntaxTokenInternal greaterThanToken, DiagnosticInfo[]? diagnostics) : base(kind, diagnostics)
    {
        SlotCount = 3;

        AdjustWidth(lessThanToken);
        LessThanToken = lessThanToken;

        if (arguments != null)
        {
            AdjustWidth(arguments);
            _arguments = arguments;
        }

        AdjustWidth(greaterThanToken);
        GreaterThanToken = greaterThanToken;
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new TypeArgumentListSyntaxInternal(Kind, LessThanToken, _arguments, GreaterThanToken, diagnostics);
    }

    public override GreenNode? GetSlot(int index)
    {
        return index switch
        {
            0 => LessThanToken,
            1 => _arguments,
            2 => GreaterThanToken,
            _ => null
        };
    }

    public override SyntaxNode CreateRed(SyntaxNode? parent, int position)
    {
        return new TypeArgumentListSyntax(this, parent, position);
    }

    public override TResult? Accept<TResult>(HlslSyntaxVisitorInternal<TResult> visitor) where TResult : default
    {
        return visitor.VisitTypeArgumentList(this);
    }
}