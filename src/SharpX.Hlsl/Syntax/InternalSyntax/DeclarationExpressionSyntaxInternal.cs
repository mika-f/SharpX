// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;

namespace SharpX.Hlsl.Syntax.InternalSyntax;

internal class DeclarationExpressionSyntaxInternal : ExpressionSyntaxInternal
{
    public TypeSyntaxInternal Type { get; }

    public VariableDesignationSyntaxInternal Designation { get; }

    public DeclarationExpressionSyntaxInternal(SyntaxKind kind, TypeSyntaxInternal type, VariableDesignationSyntaxInternal designation) : base(kind)
    {
        SlotCount = 2;

        AdjustWidth(type);
        Type = type;

        AdjustWidth(designation);
        Designation = designation;
    }

    public DeclarationExpressionSyntaxInternal(SyntaxKind kind, TypeSyntaxInternal type, VariableDesignationSyntaxInternal designation, DiagnosticInfo[]? diagnostics) : base(kind, diagnostics)
    {
        SlotCount = 2;

        AdjustWidth(type);
        Type = type;

        AdjustWidth(designation);
        Designation = designation;
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new DeclarationExpressionSyntaxInternal(Kind, Type, Designation, diagnostics);
    }

    public override GreenNode? GetSlot(int index)
    {
        return index switch
        {
            0 => Type,
            1 => Designation,
            _ => null
        };
    }

    public override SyntaxNode CreateRed(SyntaxNode? parent, int position)
    {
        throw new NotImplementedException();
    }
}