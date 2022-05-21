// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using Microsoft.CodeAnalysis;

using SharpX.Core;
using SharpX.Core.Syntax.InternalSyntax;

using SyntaxNode = SharpX.Core.SyntaxNode;

namespace SharpX.Hlsl.Syntax.InternalSyntax;

internal class VariableDeclarationSyntaxInternal : HlslSyntaxNodeInternal
{
    private readonly GreenNode? _variables;

    public TypeSyntaxInternal Type { get; }

    public SeparatedSyntaxListInternal<VariableDeclaratorSyntaxInternal> Variables => new(new SyntaxListInternal<HlslSyntaxNodeInternal>(_variables));

    public VariableDeclarationSyntaxInternal(SyntaxKind kind, TypeSyntaxInternal type, GreenNode? variables) : base(kind)
    {
        SlotCount = 2;

        AdjustWidth(type);
        Type = type;

        if (variables != null)
        {
            AdjustWidth(variables);
            _variables = variables;
        }
    }

    public VariableDeclarationSyntaxInternal(SyntaxKind kind, TypeSyntaxInternal type, GreenNode? variables, DiagnosticInfo[]? diagnostics, SyntaxAnnotation[]? annotations) : base(kind, diagnostics, annotations)
    {
        SlotCount = 2;

        AdjustWidth(type);
        Type = type;

        if (variables != null)
        {
            AdjustWidth(variables);
            _variables = variables;
        }
    }

    public override GreenNode SetAnnotations(SyntaxAnnotation[]? annotations)
    {
        return new VariableDeclarationSyntaxInternal(Kind, Type, _variables, GetDiagnostics(), annotations);
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new VariableDeclarationSyntaxInternal(Kind, Type, _variables, diagnostics, GetAnnotations());
    }

    public override GreenNode? GetSlot(int index)
    {
        return index switch
        {
            0 => Type,
            1 => _variables,
            _ => null
        };
    }

    public override SyntaxNode CreateRed(SyntaxNode? parent, int position)
    {
        return new VariableDeclarationSyntax(this, parent, position);
    }

    public override TResult? Accept<TResult>(HlslSyntaxVisitorInternal<TResult> visitor) where TResult : default
    {
        return visitor.VisitVariableDeclaration(this);
    }
}