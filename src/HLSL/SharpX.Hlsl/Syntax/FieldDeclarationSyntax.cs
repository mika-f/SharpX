// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.Hlsl.Syntax.InternalSyntax;

namespace SharpX.Hlsl.Syntax;

public class FieldDeclarationSyntax : MemberDeclarationSyntax
{
    private BracketedArgumentListSyntax? _arguments;
    private EqualsValueClauseSyntax? _initializer;
    private RegisterSyntax? _register;
    private SemanticSyntax? _semantics;
    private TypeSyntax? _type;

    public TypeSyntax Type => GetRedAtZero(ref _type)!;

    public SyntaxToken Identifier => new(this, ((FieldDeclarationSyntaxInternal)Green).Identifier, GetChildPosition(1), GetChildIndex(1));

    public BracketedArgumentListSyntax? Arguments => GetRed(ref _arguments, 2);

    public RegisterSyntax? Register => GetRed(ref _register, 3);

    public SemanticSyntax? Semantics => GetRed(ref _semantics, 4);

    public EqualsValueClauseSyntax? Initializer => GetRed(ref _initializer, 5);

    public SyntaxToken SemicolonToken => new(this, ((FieldDeclarationSyntaxInternal)Green).SemicolonToken, GetChildPosition(6), GetChildIndex(6));

    internal FieldDeclarationSyntax(HlslSyntaxNodeInternal node, SyntaxNode? parent, int position) : base(node, parent, position) { }

    public override SyntaxNode? GetNodeSlot(int index)
    {
        return index switch
        {
            0 => GetRedAtZero(ref _type),
            2 => GetRed(ref _arguments, 2),
            3 => GetRed(ref _register, 3),
            4 => GetRed(ref _semantics, 5),
            5 => GetRed(ref _initializer, 5),
            _ => null
        };
    }

    public override SyntaxNode? GetCachedSlot(int index)
    {
        return index switch
        {
            0 => _type,
            2 => _arguments,
            3 => _register,
            4 => _semantics,
            5 => _initializer,
            _ => null
        };
    }

    public FieldDeclarationSyntax Update(TypeSyntax type, SyntaxToken identifier, BracketedArgumentListSyntax? arguments, RegisterSyntax? register, SemanticSyntax? semantics, EqualsValueClauseSyntax? initializer, SyntaxToken semicolonToken)
    {
        if (type != Type || identifier != Identifier || arguments != Arguments || register != Register || semantics != Semantics || initializer != Initializer || semicolonToken != SemicolonToken)
            return SyntaxFactory.FieldDeclaration(type, identifier, arguments, register, semantics, initializer, semicolonToken);
        return this;
    }

    public FieldDeclarationSyntax WithType(TypeSyntax type)
    {
        return Update(type, Identifier, Arguments, Register, Semantics, Initializer, SemicolonToken);
    }

    public FieldDeclarationSyntax WithIdentifier(SyntaxToken identifier)
    {
        return Update(Type, identifier, Arguments, Register, Semantics, Initializer, SemicolonToken);
    }

    public FieldDeclarationSyntax WithArguments(BracketedArgumentListSyntax? arguments)
    {
        return Update(Type, Identifier, arguments, Register, Semantics, Initializer, SemicolonToken);
    }

    public FieldDeclarationSyntax WithRegister(RegisterSyntax? register)
    {
        return Update(Type, Identifier, Arguments, register, Semantics, Initializer, SemicolonToken);
    }

    public FieldDeclarationSyntax WithSemantics(SemanticSyntax? semantics)
    {
        return Update(Type, Identifier, Arguments, Register, semantics, Initializer, SemicolonToken);
    }

    public FieldDeclarationSyntax WithInitializer(EqualsValueClauseSyntax? initializer)
    {
        return Update(Type, Identifier, Arguments, Register, Semantics, initializer, SemicolonToken);
    }

    public FieldDeclarationSyntax WithSemicolonToken(SyntaxToken semicolonToken)
    {
        return Update(Type, Identifier, Arguments, Register, Semantics, Initializer, semicolonToken);
    }

    public override TResult? Accept<TResult>(HlslSyntaxVisitor<TResult> visitor) where TResult : default
    {
        return visitor.VisitFieldDeclaration(this);
    }
}