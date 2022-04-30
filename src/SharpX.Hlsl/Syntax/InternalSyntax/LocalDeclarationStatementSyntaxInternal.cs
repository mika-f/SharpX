// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using Microsoft.CodeAnalysis;

using SharpX.Core;
using SharpX.Core.Syntax.InternalSyntax;

using SyntaxNode = SharpX.Core.SyntaxNode;

namespace SharpX.Hlsl.Syntax.InternalSyntax;

internal class LocalDeclarationStatementSyntaxInternal : StatementSyntaxInternal
{
    private readonly GreenNode? _attributeLists;
    private readonly GreenNode? _modifiers;

    public override SyntaxListInternal<AttributeListSyntaxInternal> AttributeLists => new(_attributeLists);

    public SyntaxListInternal<SyntaxTokenInternal> Modifiers => new(_modifiers);

    public VariableDeclarationSyntaxInternal Declaration { get; }

    public SyntaxTokenInternal SemicolonToken { get; }

    public LocalDeclarationStatementSyntaxInternal(SyntaxKind kind, GreenNode? attributeLists, GreenNode? modifiers, VariableDeclarationSyntaxInternal declaration, SyntaxTokenInternal semicolonToken) : base(kind)
    {
        if (attributeLists != null)
        {
            AdjustWidth(attributeLists);
            _attributeLists = attributeLists;
        }

        if (modifiers != null)
        {
            AdjustWidth(modifiers);
            _modifiers = modifiers;
        }

        AdjustWidth(declaration);
        Declaration = declaration;

        AdjustWidth(semicolonToken);
        SemicolonToken = semicolonToken;
    }

    public LocalDeclarationStatementSyntaxInternal(SyntaxKind kind, GreenNode? attributeLists, GreenNode? modifiers, VariableDeclarationSyntaxInternal declaration, SyntaxTokenInternal semicolonToken, DiagnosticInfo[]? diagnostics, SyntaxAnnotation[]? annotations) :
        base(kind, diagnostics, annotations)
    {
        if (attributeLists != null)
        {
            AdjustWidth(attributeLists);
            _attributeLists = attributeLists;
        }

        if (modifiers != null)
        {
            AdjustWidth(modifiers);
            _modifiers = modifiers;
        }

        AdjustWidth(declaration);
        Declaration = declaration;

        AdjustWidth(semicolonToken);
        SemicolonToken = semicolonToken;
    }

    public override GreenNode SetAnnotations(SyntaxAnnotation[]? annotations)
    {
        return new LocalDeclarationStatementSyntaxInternal(Kind, _attributeLists, _modifiers, Declaration, SemicolonToken, GetDiagnostics(), annotations);
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new LocalDeclarationStatementSyntaxInternal(Kind, _attributeLists, _modifiers, Declaration, SemicolonToken, diagnostics, GetAnnotations());
    }

    public override GreenNode? GetSlot(int index)
    {
        return index switch
        {
            0 => _attributeLists,
            1 => _modifiers,
            2 => Declaration,
            3 => SemicolonToken,
            _ => null
        };
    }

    public override SyntaxNode CreateRed(SyntaxNode? parent, int position)
    {
        return new LocalDeclarationStatementSyntax(this, parent, position);
    }
}