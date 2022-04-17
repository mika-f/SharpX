// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;

namespace SharpX.Hlsl.Syntax.InternalSyntax;

internal class SyntaxIdentifierExtendedInternal : SyntaxIdentifierInternal
{
    private readonly string _value;

    public override SyntaxKind ContextualKind { get; }

    public override string ValueText => _value;

    public override object Value => _value;

    public SyntaxIdentifierExtendedInternal(SyntaxKind kind, string text, string value) : base(text)
    {
        ContextualKind = kind;
        _value = value;
    }

    public SyntaxIdentifierExtendedInternal(SyntaxKind kind, string text, string value, DiagnosticInfo[]? diagnostics) : base(text, diagnostics)
    {
        ContextualKind = kind;
        _value = value;
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new SyntaxIdentifierExtendedInternal(Kind, Text, ValueText, diagnostics);
    }
}