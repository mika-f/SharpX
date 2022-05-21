// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using Microsoft.CodeAnalysis;

using SharpX.Core;
using SharpX.Core.Syntax.InternalSyntax;

using SyntaxNode = SharpX.Core.SyntaxNode;

namespace SharpX.Hlsl.Syntax.InternalSyntax;

internal class ParameterListSyntaxInternal : HlslSyntaxNodeInternal
{
    private readonly GreenNode? _parameters;

    public SyntaxTokenInternal OpenParenToken { get; }

    public SeparatedSyntaxListInternal<ParameterSyntaxInternal> Parameters => new(new SyntaxListInternal<HlslSyntaxNodeInternal>(_parameters));

    public SyntaxTokenInternal CloseParenToken { get; }

    public ParameterListSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal openParenToken, GreenNode? parameters, SyntaxTokenInternal closeParenToken) : base(kind)
    {
        SlotCount = 3;

        AdjustWidth(openParenToken);
        OpenParenToken = openParenToken;

        if (parameters != null)
        {
            AdjustWidth(parameters);
            _parameters = parameters;
        }

        AdjustWidth(closeParenToken);
        CloseParenToken = closeParenToken;
    }

    public ParameterListSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal openParenToken, GreenNode? parameters, SyntaxTokenInternal closeParenToken, DiagnosticInfo[]? diagnostics, SyntaxAnnotation[]? annotations) : base(kind, diagnostics, annotations)
    {
        SlotCount = 3;

        AdjustWidth(openParenToken);
        OpenParenToken = openParenToken;

        if (parameters != null)
        {
            AdjustWidth(parameters);
            _parameters = parameters;
        }

        AdjustWidth(closeParenToken);
        CloseParenToken = closeParenToken;
    }

    public override GreenNode SetAnnotations(SyntaxAnnotation[]? annotations)
    {
        return new ParameterListSyntaxInternal(Kind, OpenParenToken, _parameters, CloseParenToken, GetDiagnostics(), annotations);
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new ParameterListSyntaxInternal(Kind, OpenParenToken, _parameters, CloseParenToken, diagnostics, GetAnnotations());
    }

    public override GreenNode? GetSlot(int index)
    {
        return index switch
        {
            0 => OpenParenToken,
            1 => _parameters,
            2 => CloseParenToken,
            _ => null
        };
    }

    public override SyntaxNode CreateRed(SyntaxNode? parent, int position)
    {
        return new ParameterListSyntax(this, parent, position);
    }

    public override TResult? Accept<TResult>(HlslSyntaxVisitorInternal<TResult> visitor) where TResult : default
    {
        return visitor.VisitParameterList(this);
    }
}