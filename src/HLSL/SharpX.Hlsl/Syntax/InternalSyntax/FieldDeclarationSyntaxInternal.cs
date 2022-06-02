// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;

namespace SharpX.Hlsl.Syntax.InternalSyntax;

internal class FieldDeclarationSyntaxInternal : MemberDeclarationSyntaxInternal
{
    public TypeSyntaxInternal Type { get; }

    public SyntaxTokenInternal Identifier { get; }

    public BracketedArgumentListSyntaxInternal? Arguments { get; }

    public RegisterSyntaxInternal? Register { get; }

    public SemanticSyntaxInternal? Semantics { get; }

    public EqualsValueClauseSyntaxInternal? Initializer { get; }

    public SyntaxTokenInternal SemicolonToken { get; }

    public FieldDeclarationSyntaxInternal(SyntaxKind kind, TypeSyntaxInternal type, SyntaxTokenInternal identifier, BracketedArgumentListSyntaxInternal? arguments, RegisterSyntaxInternal? register, SemanticSyntaxInternal? semantics, EqualsValueClauseSyntaxInternal? initializer,
                                          SyntaxTokenInternal semicolonToken) : base(kind)
    {
        SlotCount = 7;

        AdjustWidth(type);
        Type = type;

        AdjustWidth(identifier);
        Identifier = identifier;

        if (arguments != null)
        {
            AdjustWidth(arguments);
            Arguments = arguments;
        }

        if (register != null)
        {
            AdjustWidth(register);
            Register = register;
        }

        if (semantics != null)
        {
            AdjustWidth(semantics);
            Semantics = semantics;
        }

        if (initializer != null)
        {
            AdjustWidth(initializer);
            Initializer = initializer;
        }

        AdjustWidth(semicolonToken);
        SemicolonToken = semicolonToken;
    }

    public FieldDeclarationSyntaxInternal(SyntaxKind kind, TypeSyntaxInternal type, SyntaxTokenInternal identifier, BracketedArgumentListSyntaxInternal? arguments, RegisterSyntaxInternal? register, SemanticSyntaxInternal? semantics, EqualsValueClauseSyntaxInternal? initializer,
                                          SyntaxTokenInternal semicolonToken,
                                          DiagnosticInfo[]? diagnostics) : base(kind, diagnostics)
    {
        SlotCount = 7;

        AdjustWidth(type);
        Type = type;

        AdjustWidth(identifier);
        Identifier = identifier;

        if (arguments != null)
        {
            AdjustWidth(arguments);
            Arguments = arguments;
        }

        if (register != null)
        {
            AdjustWidth(register);
            Register = register;
        }

        if (semantics != null)
        {
            AdjustWidth(semantics);
            Semantics = semantics;
        }

        if (initializer != null)
        {
            AdjustWidth(initializer);
            Initializer = initializer;
        }

        AdjustWidth(semicolonToken);
        SemicolonToken = semicolonToken;
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new FieldDeclarationSyntaxInternal(Kind, Type, Identifier, Arguments, Register, Semantics, Initializer, SemicolonToken, diagnostics);
    }

    public override GreenNode? GetSlot(int index)
    {
        return index switch
        {
            0 => Type,
            1 => Identifier,
            2 => Arguments,
            3 => Register,
            4 => Semantics,
            5 => Initializer,
            6 => SemicolonToken,
            _ => null
        };
    }

    public override SyntaxNode CreateRed(SyntaxNode? parent, int position)
    {
        return new FieldDeclarationSyntax(this, parent, position);
    }

    public override TResult? Accept<TResult>(HlslSyntaxVisitorInternal<TResult> visitor) where TResult : default
    {
        return visitor.VisitFieldDeclaration(this);
    }
}