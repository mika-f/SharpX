// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.Core.Syntax.InternalSyntax;

namespace SharpX.ShaderLab.Syntax.InternalSyntax;

internal class ArgumentListSyntaxInternal : ShaderLabSyntaxNodeInternal
{
    private readonly GreenNode? _arguments;

    public SyntaxTokenInternal OpenParenToken { get; }

    public SeparatedSyntaxListInternal<ArgumentSyntaxInternal> Arguments => new(new SyntaxListInternal<GreenNode>(_arguments));

    public SyntaxTokenInternal CloseParenToken { get; }

    public ArgumentListSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal openParenToken, GreenNode? arguments, SyntaxTokenInternal closeParenToken) : base(kind)
    {
        SlotCount = 3;

        AdjustWidth(openParenToken);
        OpenParenToken = openParenToken;

        if (arguments != null)
        {
            AdjustWidth(arguments);
            _arguments = arguments;
        }

        AdjustWidth(closeParenToken);
        CloseParenToken = closeParenToken;
    }

    public ArgumentListSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal openParenToken, GreenNode? arguments, SyntaxTokenInternal closeParenToken, DiagnosticInfo[]? diagnostics) : base(kind, diagnostics)
    {
        SlotCount = 3;

        AdjustWidth(openParenToken);
        OpenParenToken = openParenToken;

        if (arguments != null)
        {
            AdjustWidth(arguments);
            _arguments = arguments;
        }

        AdjustWidth(closeParenToken);
        CloseParenToken = closeParenToken;
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new ArgumentListSyntaxInternal(Kind, OpenParenToken, _arguments, CloseParenToken, diagnostics);
    }

    public override GreenNode? GetSlot(int index)
    {
        return index switch
        {
            0 => OpenParenToken,
            1 => _arguments,
            2 => CloseParenToken,
            _ => null
        };
    }

    public override SyntaxNode CreateRed(SyntaxNode? parent, int position)
    {
        return new ArgumentListSyntax(this, parent, position);
    }
}