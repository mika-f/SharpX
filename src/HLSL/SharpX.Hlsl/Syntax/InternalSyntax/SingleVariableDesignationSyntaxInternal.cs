// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using Microsoft.CodeAnalysis;

using SharpX.Core;

using SyntaxNode = SharpX.Core.SyntaxNode;

namespace SharpX.Hlsl.Syntax.InternalSyntax;

[Obsolete]
internal class SingleVariableDesignationSyntaxInternal : VariableDesignationSyntaxInternal
{
    public SyntaxTokenInternal Identifier { get; }

    public SingleVariableDesignationSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal identifier) : base(kind)
    {
        SlotCount = 1;

        AdjustWidth(identifier);
        Identifier = identifier;
    }

    public SingleVariableDesignationSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal identifier, DiagnosticInfo[]? diagnostics, SyntaxAnnotation[]? annotations) : base(kind, diagnostics, annotations)
    {
        SlotCount = 1;

        AdjustWidth(identifier);
        Identifier = identifier;
    }

    public override GreenNode SetAnnotations(SyntaxAnnotation[]? annotations)
    {
        return new SingleVariableDesignationSyntaxInternal(Kind, Identifier, GetDiagnostics(), annotations);
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new SingleVariableDesignationSyntaxInternal(Kind, Identifier, diagnostics, GetAnnotations());
    }

    public override GreenNode? GetSlot(int index)
    {
        return index switch
        {
            0 => Identifier,
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