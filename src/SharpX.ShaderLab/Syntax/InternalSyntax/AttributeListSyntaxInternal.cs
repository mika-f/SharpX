// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using Microsoft.CodeAnalysis;

using SharpX.Core;
using SharpX.Core.Syntax.InternalSyntax;

using SyntaxNode = SharpX.Core.SyntaxNode;

namespace SharpX.ShaderLab.Syntax.InternalSyntax;

internal class AttributeListSyntaxInternal : ShaderLabSyntaxNodeInternal
{
    private readonly GreenNode _attributes;

    public SyntaxTokenInternal OpenBracketToken { get; }

    public SeparatedSyntaxListInternal<AttributeSyntaxInternal> Attributes => new(new SyntaxListInternal<GreenNode>(_attributes));

    public SyntaxTokenInternal CloseBracketToken { get; }

    public AttributeListSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal openBracketToken, GreenNode attributes, SyntaxTokenInternal closeBracketToken) : base(kind)
    {
        SlotCount = 3;

        AdjustWidth(openBracketToken);
        OpenBracketToken = openBracketToken;

        AdjustWidth(attributes);
        _attributes = attributes;

        AdjustWidth(closeBracketToken);
        CloseBracketToken = closeBracketToken;
    }

    public AttributeListSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal openBracketToken, GreenNode attributes, SyntaxTokenInternal closeBracketToken, DiagnosticInfo[]? diagnostics, SyntaxAnnotation[]? annotations) : base(kind, diagnostics, annotations)
    {
        SlotCount = 3;

        AdjustWidth(openBracketToken);
        OpenBracketToken = openBracketToken;

        AdjustWidth(attributes);
        _attributes = attributes;

        AdjustWidth(closeBracketToken);
        CloseBracketToken = closeBracketToken;
    }

    public override GreenNode SetAnnotations(SyntaxAnnotation[]? annotations)
    {
        return new AttributeListSyntaxInternal(Kind, OpenBracketToken, _attributes, CloseBracketToken, GetDiagnostics(), annotations);
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new AttributeListSyntaxInternal(Kind, OpenBracketToken, _attributes, CloseBracketToken, diagnostics, GetAnnotations());
    }

    public override GreenNode? GetSlot(int index)
    {
        return index switch
        {
            0 => OpenBracketToken,
            1 => _attributes,
            2 => CloseBracketToken,
            _ => null
        };
    }

    public override SyntaxNode CreateRed(SyntaxNode? parent, int position)
    {
        return new AttributeListSyntax(this, parent, position);
    }
}