// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.ShaderLab.Syntax.InternalSyntax;

namespace SharpX.ShaderLab.Syntax;

public class PropertiesDeclarationSyntax : ShaderLabSyntaxNode
{
    private SyntaxNode? _properties;

    public SyntaxToken PropertiesKeyword => new(this, ((PropertiesDeclarationSyntaxInternal)Green).PropertiesKeyword, Position, 0);

    public SyntaxToken OpenBraceToken => new(this, ((PropertiesDeclarationSyntaxInternal)Green).OpenBraceToken, GetChildPosition(1), GetChildIndex(1));

    public SyntaxList<PropertyDeclarationSyntax> Properties
    {
        get
        {
            var red = GetRed(ref _properties, 2);
            return red != null ? new SyntaxList<PropertyDeclarationSyntax>(red) : default;
        }
    }

    public SyntaxToken CloseBraceToken => new(this, ((PropertiesDeclarationSyntaxInternal)Green).CloseBraceToken, GetChildPosition(3), GetChildIndex(3));

    internal PropertiesDeclarationSyntax(ShaderLabSyntaxNodeInternal node, SyntaxNode? parent, int position) : base(node, parent, position) { }

    public override SyntaxNode? GetNodeSlot(int index)
    {
        return index == 2 ? GetRed(ref _properties, 2) : null;
    }

    public override SyntaxNode? GetCachedSlot(int index)
    {
        return index == 2 ? _properties : null;
    }

    public PropertiesDeclarationSyntax Update(SyntaxToken propertiesKeyword, SyntaxToken openBraceToken, SyntaxList<PropertyDeclarationSyntax> properties, SyntaxToken closeBraceToken)
    {
        if (propertiesKeyword != PropertiesKeyword || openBraceToken != OpenBraceToken || properties != Properties || closeBraceToken != CloseBraceToken)
            return SyntaxFactory.PropertiesDeclaration(propertiesKeyword, openBraceToken, properties, closeBraceToken);
        return this;
    }

    public PropertiesDeclarationSyntax WithPropertiesKeyword(SyntaxToken propertiesKeyword)
    {
        return Update(propertiesKeyword, OpenBraceToken, Properties, CloseBraceToken);
    }

    public PropertiesDeclarationSyntax WithOpenBraceToken(SyntaxToken openBraceToken)
    {
        return Update(PropertiesKeyword, openBraceToken, Properties, CloseBraceToken);
    }

    public PropertiesDeclarationSyntax WithProperties(SyntaxList<PropertyDeclarationSyntax> properties)
    {
        return Update(PropertiesKeyword, OpenBraceToken, properties, CloseBraceToken);
    }

    public PropertiesDeclarationSyntax WithCloseBraceToken(SyntaxToken closeBraceToken)
    {
        return Update(PropertiesKeyword, OpenBraceToken, Properties, closeBraceToken);
    }

    public PropertiesDeclarationSyntax AddProperties(params PropertyDeclarationSyntax[] items)
    {
        return WithProperties(Properties.AddRange(items));
    }

    public override TResult? Accept<TResult>(ShaderLabSyntaxVisitor<TResult> visitor) where TResult : default
    {
        return visitor.VisitPropertiesDeclaration(this);
    }
}