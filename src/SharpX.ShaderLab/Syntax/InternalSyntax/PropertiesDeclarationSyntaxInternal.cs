// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using Microsoft.CodeAnalysis;

using SharpX.Core;
using SharpX.Core.Syntax.InternalSyntax;

using SyntaxNode = SharpX.Core.SyntaxNode;

namespace SharpX.ShaderLab.Syntax.InternalSyntax;

internal class PropertiesDeclarationSyntaxInternal : ShaderLabSyntaxNodeInternal
{
    private readonly GreenNode _properties;

    public SyntaxTokenInternal PropertiesKeyword { get; }

    public SyntaxTokenInternal OpenBraceToken { get; }

    public SyntaxListInternal<PropertyDeclarationSyntaxInternal> Properties => new(_properties);

    public SyntaxTokenInternal CloseBraceToken { get; }

    public PropertiesDeclarationSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal propertiesKeyword, SyntaxTokenInternal openBraceToken, GreenNode properties, SyntaxTokenInternal closeBraceToken) : base(kind)
    {
        SlotCount = 4;

        AdjustWidth(propertiesKeyword);
        PropertiesKeyword = propertiesKeyword;

        AdjustWidth(openBraceToken);
        OpenBraceToken = openBraceToken;

        AdjustWidth(properties);
        _properties = properties;

        AdjustWidth(closeBraceToken);
        CloseBraceToken = closeBraceToken;
    }

    public PropertiesDeclarationSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal propertiesKeyword, SyntaxTokenInternal openBraceToken, GreenNode properties, SyntaxTokenInternal closeBraceToken, DiagnosticInfo[]? diagnostics, SyntaxAnnotation[]? annotations) : base(kind, diagnostics, annotations)
    {
        SlotCount = 4;

        AdjustWidth(propertiesKeyword);
        PropertiesKeyword = propertiesKeyword;

        AdjustWidth(openBraceToken);
        OpenBraceToken = openBraceToken;

        AdjustWidth(properties);
        _properties = properties;

        AdjustWidth(closeBraceToken);
        CloseBraceToken = closeBraceToken;
    }

    public override GreenNode SetAnnotations(SyntaxAnnotation[]? annotations)
    {
        return new PropertiesDeclarationSyntaxInternal(Kind, PropertiesKeyword, OpenBraceToken, _properties, CloseBraceToken, GetDiagnostics(), annotations);
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new PropertiesDeclarationSyntaxInternal(Kind, PropertiesKeyword, OpenBraceToken, _properties, CloseBraceToken, diagnostics, GetAnnotations());
    }

    public override GreenNode? GetSlot(int index)
    {
        return index switch
        {
            0 => PropertiesKeyword,
            1 => OpenBraceToken,
            2 => _properties,
            3 => CloseBraceToken,
            _ => null
        };
    }

    public override SyntaxNode CreateRed(SyntaxNode? parent, int position)
    {
        return new PropertiesDeclarationSyntax(this, parent, position);
    }
}