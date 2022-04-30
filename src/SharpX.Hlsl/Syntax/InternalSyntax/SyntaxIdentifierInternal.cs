// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using Microsoft.CodeAnalysis;

using SharpX.Core;

namespace SharpX.Hlsl.Syntax.InternalSyntax;

internal class SyntaxIdentifierInternal : SyntaxTokenInternal
{
    private readonly string _text;

    public override string Text => _text;

    public override object Value => _text;

    public override string ValueText => _text;

    public SyntaxIdentifierInternal(string text) : base(SyntaxKind.IdentifierToken, text.Length)
    {
        _text = text;
    }

    public SyntaxIdentifierInternal(string text, DiagnosticInfo[]? diagnostics, SyntaxAnnotation[]? annotations) : base(SyntaxKind.IdentifierToken, text.Length, diagnostics, annotations)
    {
        _text = text;
    }

    public override GreenNode SetAnnotations(SyntaxAnnotation[]? annotations)
    {
        return new SyntaxIdentifierInternal(_text, GetDiagnostics(), annotations);
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new SyntaxIdentifierInternal(_text, diagnostics, GetAnnotations());
    }
}