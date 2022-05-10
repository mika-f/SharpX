// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.ShaderLab.Syntax.InternalSyntax;

namespace SharpX.ShaderLab.Syntax;

public class PropertyDeclarationSyntax : ShaderLabSyntaxNode
{
    private ArgumentListSyntax? _argumentList;
    private EqualsValueClauseSyntax? _default;
    private SimpleNameSyntax? _type;

    public SyntaxToken Identifier => new(this, ((PropertyDeclarationSyntaxInternal)Green).Identifier, Position, 0);

    public SyntaxToken OpenParenToken => new(this, ((PropertyDeclarationSyntaxInternal)Green).OpenParenToken, GetChildPosition(1), GetChildIndex(1));

    public SyntaxToken DisplayName => new(this, ((PropertyDeclarationSyntaxInternal)Green).DisplayName, GetChildPosition(2), GetChildIndex(2));

    public SyntaxToken CommaToken => new(this, ((PropertyDeclarationSyntaxInternal)Green).CommaToken, GetChildPosition(3), GetChildIndex(3));

    public SimpleNameSyntax Type => GetRed(ref _type, 4)!;

    public ArgumentListSyntax? ArgumentList => GetRed(ref _argumentList, 5);

    public SyntaxToken CloseParenToken => new(this, ((PropertyDeclarationSyntaxInternal)Green).CloseParenToken, GetChildPosition(6), GetChildIndex(6));

    public EqualsValueClauseSyntax Default => GetRed(ref _default, 7)!;

    internal PropertyDeclarationSyntax(ShaderLabSyntaxNodeInternal node, SyntaxNode? parent, int position) : base(node, parent, position) { }

    public override SyntaxNode? GetNodeSlot(int index)
    {
        return index switch
        {
            4 => GetRed(ref _type, 4),
            5 => GetRed(ref _argumentList, 5),
            7 => GetRed(ref _default, 7),
            _ => null
        };
    }

    public override SyntaxNode? GetCachedSlot(int index)
    {
        return index switch
        {
            4 => _type,
            5 => _argumentList,
            7 => _default,
            _ => null
        };
    }

    public PropertyDeclarationSyntax Update(SyntaxToken identifier, SyntaxToken openParenToken, SyntaxToken displayName, SyntaxToken commaToken, SimpleNameSyntax type, ArgumentListSyntax? argumentList, SyntaxToken closeParenToken, EqualsValueClauseSyntax @default)
    {
        if (identifier != Identifier || openParenToken != OpenParenToken || displayName != DisplayName || commaToken != CommaToken || type != Type || argumentList != ArgumentList || closeParenToken != CloseParenToken || @default != Default)
            return SyntaxFactory.PropertyDeclaration(identifier, openParenToken, displayName, commaToken, type, argumentList, closeParenToken, @default);
        return this;
    }

    public PropertyDeclarationSyntax WithIdentifier(SyntaxToken identifier)
    {
        return Update(identifier, OpenParenToken, DisplayName, CommaToken, Type, ArgumentList, CloseParenToken, Default);
    }

    public PropertyDeclarationSyntax WithOpenParenToken(SyntaxToken openParenToken)
    {
        return Update(Identifier, openParenToken, DisplayName, CommaToken, Type, ArgumentList, CloseParenToken, Default);
    }

    public PropertyDeclarationSyntax WithDisplayName(SyntaxToken displayName)
    {
        return Update(Identifier, OpenParenToken, displayName, CommaToken, Type, ArgumentList, CloseParenToken, Default);
    }

    public PropertyDeclarationSyntax WithCommaToken(SyntaxToken commaToken)
    {
        return Update(Identifier, OpenParenToken, DisplayName, commaToken, Type, ArgumentList, CloseParenToken, Default);
    }

    public PropertyDeclarationSyntax WithType(SimpleNameSyntax type)
    {
        return Update(Identifier, OpenParenToken, DisplayName, CommaToken, type, ArgumentList, CloseParenToken, Default);
    }

    public PropertyDeclarationSyntax WithArgumentList(ArgumentListSyntax? argumentList)
    {
        return Update(Identifier, OpenParenToken, DisplayName, CommaToken, Type, argumentList, CloseParenToken, Default);
    }

    public PropertyDeclarationSyntax WithCloseParenToken(SyntaxToken closeParenToken)
    {
        return Update(Identifier, OpenParenToken, DisplayName, CommaToken, Type, ArgumentList, closeParenToken, Default);
    }

    public PropertyDeclarationSyntax WithDefault(EqualsValueClauseSyntax @default)
    {
        return Update(Identifier, OpenParenToken, DisplayName, CommaToken, Type, ArgumentList, CloseParenToken, @default);
    }

    public override TResult? Accept<TResult>(ShaderLabSyntaxVisitor<TResult> visitor) where TResult : default
    {
        return visitor.VisitPropertyDeclaration(this);
    }
}