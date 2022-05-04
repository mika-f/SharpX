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
    private SemanticSyntax? _semantics;
    private TypeSyntax? _type;

    public TypeSyntax Type => GetRedAtZero(ref _type)!;

    public SyntaxToken Identifier => new(this, ((FieldDeclarationSyntaxInternal)Green).Identifier, GetChildPosition(1), GetChildIndex(1));

    public BracketedArgumentListSyntax? Arguments => GetRed(ref _arguments, 2);

    public SemanticSyntax? Semantics => GetRed(ref _semantics, 3);

    public EqualsValueClauseSyntax? Initializer => GetRed(ref _initializer, 4);

    public SyntaxToken SemicolonToken => new(this, ((FieldDeclarationSyntaxInternal)Green).SemicolonToken, GetChildPosition(5), GetChildIndex(5));

    internal FieldDeclarationSyntax(HlslSyntaxNodeInternal node, SyntaxNode? parent, int position) : base(node, parent, position) { }

    public override SyntaxNode? GetNodeSlot(int index)
    {
        return index switch
        {
            0 => GetRedAtZero(ref _type),
            2 => GetRed(ref _arguments, 2),
            3 => GetRed(ref _semantics, 3),
            4 => GetRed(ref _initializer, 4),
            _ => null
        };
    }

    public override SyntaxNode? GetCachedSlot(int index)
    {
        return index switch
        {
            0 => _type,
            2 => _arguments,
            3 => _semantics,
            4 => _initializer,
            _ => null
        };
    }

    public FieldDeclarationSyntax Update(TypeSyntax type, SyntaxToken identifier, BracketedArgumentListSyntax? arguments, SemanticSyntax? semantics, EqualsValueClauseSyntax? initializer, SyntaxToken semicolonToken)
    {
        if (type != Type || identifier != Identifier || arguments != Arguments || semantics != Semantics || initializer != Initializer || semicolonToken != SemicolonToken)
            return SyntaxFactory.FieldDeclaration(type, identifier, arguments, semantics, initializer, semicolonToken);
        return this;
    }

    public FieldDeclarationSyntax WithType(TypeSyntax type)
    {
        return Update(type, Identifier, Arguments, Semantics, Initializer, SemicolonToken);
    }

    public FieldDeclarationSyntax WithIdentifier(SyntaxToken identifier)
    {
        return Update(Type, identifier, Arguments, Semantics, Initializer, SemicolonToken);
    }

    public FieldDeclarationSyntax WithArguments(BracketedArgumentListSyntax? arguments)
    {
        return Update(Type, Identifier, arguments, Semantics, Initializer, SemicolonToken);
    }

    public FieldDeclarationSyntax WithSemantics(SemanticSyntax? semantics)
    {
        return Update(Type, Identifier, Arguments, semantics, Initializer, SemicolonToken);
    }

    public FieldDeclarationSyntax WithInitializer(EqualsValueClauseSyntax? initializer)
    {
        return Update(Type, Identifier, Arguments, Semantics, initializer, SemicolonToken);
    }

    public FieldDeclarationSyntax WithSemicolonToken(SyntaxToken semicolonToken)
    {
        return Update(Type, Identifier, Arguments, Semantics, Initializer, semicolonToken);
    }

    public override TResult? Accept<TResult>(HlslSyntaxVisitor<TResult> visitor) where TResult : default
    {
        return visitor.VisitFieldDeclaration(this);
    }
}