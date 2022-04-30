// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using Microsoft.CodeAnalysis;

using SharpX.Core;

using SyntaxNode = SharpX.Core.SyntaxNode;

namespace SharpX.Hlsl.Syntax.InternalSyntax;

internal class ArrayCreationExpressionSyntaxInternal : ExpressionSyntaxInternal
{
    public ArrayTypeSyntaxInternal Type { get; }

    public InitializerExpressionSyntaxInternal? Initializer { get; }

    public ArrayCreationExpressionSyntaxInternal(SyntaxKind kind, ArrayTypeSyntaxInternal type, InitializerExpressionSyntaxInternal? initializer) : base(kind)
    {
        SlotCount = 2;

        AdjustWidth(type);
        Type = type;

        if (initializer != null)
        {
            AdjustWidth(initializer);
            Initializer = initializer;
        }
    }

    public ArrayCreationExpressionSyntaxInternal(SyntaxKind kind, ArrayTypeSyntaxInternal type, InitializerExpressionSyntaxInternal? initializer, DiagnosticInfo[]? diagnostics, SyntaxAnnotation[]? annotations) : base(kind, diagnostics, annotations)
    {
        SlotCount = 2;

        AdjustWidth(type);
        Type = type;

        if (initializer != null)
        {
            AdjustWidth(initializer);
            Initializer = initializer;
        }
    }

    public override GreenNode SetAnnotations(SyntaxAnnotation[]? annotations)
    {
        return new ArrayCreationExpressionSyntaxInternal(Kind, Type, Initializer, GetDiagnostics(), annotations);
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new ArrayCreationExpressionSyntaxInternal(Kind, Type, Initializer, diagnostics, GetAnnotations());
    }

    public override GreenNode? GetSlot(int index)
    {
        return index switch
        {
            0 => Type,
            1 => Initializer,
            _ => null
        };
    }

    public override SyntaxNode CreateRed(SyntaxNode? parent, int position)
    {
        return new ArrayCreationExpressionSyntax(this, parent, position);
    }
}