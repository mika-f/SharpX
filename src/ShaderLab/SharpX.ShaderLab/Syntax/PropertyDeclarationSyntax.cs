// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.ShaderLab.Syntax.InternalSyntax;

namespace SharpX.ShaderLab.Syntax;

public class PropertyDeclarationSyntax : ShaderLabSyntaxNode
{
    private AttributeListSyntax? _attributeList;
    private ArgumentListSyntax? _argumentList;
    private EqualsValueClauseSyntax? _default;
    private SimpleNameSyntax? _type;

    public AttributeListSyntax AttributeList => GetRedAtZero(ref _attributeList)!;

    public SyntaxToken Identifier => new(this, ((PropertyDeclarationSyntaxInternal)Green).Identifier, GetChildPosition(1), GetChildIndex(1));

    public SyntaxToken OpenParenToken => new(this, ((PropertyDeclarationSyntaxInternal)Green).OpenParenToken, GetChildPosition(2), GetChildIndex(2));

    public SyntaxToken DisplayName => new(this, ((PropertyDeclarationSyntaxInternal)Green).DisplayName, GetChildPosition(3), GetChildIndex(3));

    public SyntaxToken CommaToken => new(this, ((PropertyDeclarationSyntaxInternal)Green).CommaToken, GetChildPosition(4), GetChildIndex(4));

    public SimpleNameSyntax Type => GetRed(ref _type, 5)!;

    public ArgumentListSyntax? ArgumentList => GetRed(ref _argumentList, 6);

    public SyntaxToken CloseParenToken => new(this, ((PropertyDeclarationSyntaxInternal)Green).CloseParenToken, GetChildPosition(7), GetChildIndex(7));

    public EqualsValueClauseSyntax Default => GetRed(ref _default, 8)!;

    internal PropertyDeclarationSyntax(ShaderLabSyntaxNodeInternal node, SyntaxNode? parent, int position) : base(node, parent, position) { }

    public override SyntaxNode? GetNodeSlot(int index)
    {
        return index switch
        {
            0 => GetRedAtZero(ref _attributeList),
            5 => GetRed(ref _type, 5),
            6 => GetRed(ref _argumentList, 6),
            8 => GetRed(ref _default, 8),
            _ => null
        };
    }

    public override SyntaxNode? GetCachedSlot(int index)
    {
        return index switch
        {
            0 => _attributeList,
            5 => _type,
            6 => _argumentList,
            8 => _default,
            _ => null
        };
    }

    public PropertyDeclarationSyntax Update(AttributeListSyntax? attributeList, SyntaxToken identifier, SyntaxToken openParenToken, SyntaxToken displayName, SyntaxToken commaToken, SimpleNameSyntax type, ArgumentListSyntax? argumentList, SyntaxToken closeParenToken, EqualsValueClauseSyntax @default)
    {
        if (attributeList != AttributeList || identifier != Identifier || openParenToken != OpenParenToken || displayName != DisplayName || commaToken != CommaToken || type != Type || argumentList != ArgumentList || closeParenToken != CloseParenToken || @default != Default)
            return SyntaxFactory.PropertyDeclaration(attributeList, identifier, openParenToken, displayName, commaToken, type, argumentList, closeParenToken, @default);
        return this;
    }

    public PropertyDeclarationSyntax WithAttributeList(AttributeListSyntax? attributeList)
    {
        return Update(attributeList, Identifier, OpenParenToken, DisplayName, CommaToken, Type, ArgumentList, CloseParenToken, Default);
    }

    public PropertyDeclarationSyntax WithIdentifier(SyntaxToken identifier)
    {
        return Update(AttributeList, identifier, OpenParenToken, DisplayName, CommaToken, Type, ArgumentList, CloseParenToken, Default);
    }

    public PropertyDeclarationSyntax WithOpenParenToken(SyntaxToken openParenToken)
    {
        return Update(AttributeList, Identifier, openParenToken, DisplayName, CommaToken, Type, ArgumentList, CloseParenToken, Default);
    }

    public PropertyDeclarationSyntax WithDisplayName(SyntaxToken displayName)
    {
        return Update(AttributeList, Identifier, OpenParenToken, displayName, CommaToken, Type, ArgumentList, CloseParenToken, Default);
    }

    public PropertyDeclarationSyntax WithCommaToken(SyntaxToken commaToken)
    {
        return Update(AttributeList, Identifier, OpenParenToken, DisplayName, commaToken, Type, ArgumentList, CloseParenToken, Default);
    }

    public PropertyDeclarationSyntax WithType(SimpleNameSyntax type)
    {
        return Update(AttributeList, Identifier, OpenParenToken, DisplayName, CommaToken, type, ArgumentList, CloseParenToken, Default);
    }

    public PropertyDeclarationSyntax WithArgumentList(ArgumentListSyntax? argumentList)
    {
        return Update(AttributeList, Identifier, OpenParenToken, DisplayName, CommaToken, Type, argumentList, CloseParenToken, Default);
    }

    public PropertyDeclarationSyntax WithCloseParenToken(SyntaxToken closeParenToken)
    {
        return Update(AttributeList, Identifier, OpenParenToken, DisplayName, CommaToken, Type, ArgumentList, closeParenToken, Default);
    }

    public PropertyDeclarationSyntax WithDefault(EqualsValueClauseSyntax @default)
    {
        return Update(AttributeList, Identifier, OpenParenToken, DisplayName, CommaToken, Type, ArgumentList, CloseParenToken, @default);
    }

    public override TResult? Accept<TResult>(ShaderLabSyntaxVisitor<TResult> visitor) where TResult : default
    {
        return visitor.VisitPropertyDeclaration(this);
    }
}