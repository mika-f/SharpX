// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using Microsoft.CodeAnalysis;

using SharpX.Core;
using SharpX.Core.Syntax.InternalSyntax;

using SyntaxNode = SharpX.Core.SyntaxNode;

namespace SharpX.ShaderLab.Syntax.InternalSyntax;

internal class ArgumentListSyntaxInternal : ShaderLabSyntaxNodeInternal
{
    private readonly GreenNode _arguments;

    public SyntaxTokenInternal OpenParenToken { get; }

    public SeparatedSyntaxListInternal<ArgumentSyntaxInternal> Arguments => new(new SyntaxListInternal<GreenNode>(_arguments));

    public SyntaxTokenInternal CloseParenToken { get; }

    public ArgumentListSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal openParenToken, GreenNode arguments, SyntaxTokenInternal closeParenToken) : base(kind)
    {
        SlotCount = 3;

        AdjustWidth(openParenToken);
        OpenParenToken = openParenToken;

        AdjustWidth(arguments);
        _arguments = arguments;

        AdjustWidth(closeParenToken);
        CloseParenToken = closeParenToken;
    }

    public ArgumentListSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal openParenToken, GreenNode arguments, SyntaxTokenInternal closeParenToken, DiagnosticInfo[]? diagnostics, SyntaxAnnotation[]? annotations) : base(kind, diagnostics, annotations)
    {
        SlotCount = 3;

        AdjustWidth(openParenToken);
        OpenParenToken = openParenToken;

        AdjustWidth(arguments);
        _arguments = arguments;

        AdjustWidth(closeParenToken);
        CloseParenToken = closeParenToken;
    }

    public override GreenNode SetAnnotations(SyntaxAnnotation[]? annotations)
    {
        return new ArgumentListSyntaxInternal(Kind, OpenParenToken, _arguments, CloseParenToken, GetDiagnostics(), annotations);
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new ArgumentListSyntaxInternal(Kind, OpenParenToken, _arguments, CloseParenToken, diagnostics, GetAnnotations());
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