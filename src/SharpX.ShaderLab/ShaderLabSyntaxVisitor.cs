// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------


using SharpX.Core;
using SharpX.ShaderLab.Syntax;
using SharpX.ShaderLab.Syntax.InternalSyntax;

namespace SharpX.ShaderLab;

public abstract class ShaderLabSyntaxVisitor<TResult>
{
    public virtual TResult? VisitIdentifierName(IdentifierNameSyntax node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitQualifiedName(QualifiedNameSyntax node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitEqualsValueClause(EqualsValueClauseSyntax node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitArgumentList(ArgumentListSyntax node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitArgument(ArgumentSyntax node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitAttributeList(AttributeListSyntax node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitAttribute(AttributeSyntax node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitLiteralExpression(LiteralExpressionSyntax node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitTextureLiteralExpression(TextureLiteralExpressionSyntax node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitPropertiesDeclaration(PropertiesDeclarationSyntax node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitPropertyDeclaration(PropertyDeclarationSyntax node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitCgIncludeDeclaration(CgIncludeDeclarationSyntax node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitCgProgramDeclaration(CgProgramDeclarationSyntax node)
    {
        return DefaultVisit(node);
    }


    public virtual TResult? VisitGrabPassDeclaration(GrabPassDeclarationSyntax node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitCommandDeclaration(CommandDeclarationSyntax node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitNameDeclaration(NameDeclarationSyntax node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitTagsDeclaration(TagsDeclarationSyntax node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitTagDeclaration(TagDeclarationSyntax node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitFallbackDeclaration(FallbackDeclarationSyntax node)
    {
        return DefaultVisit(node);
    }

    public virtual TResult? VisitCustomEditorDeclaration(CustomEditorDeclarationSyntax node)
    {
        return DefaultVisit(node);
    }

    #region Base Implementations

    public virtual TResult? Visit(SyntaxNode? node)
    {
        if (node != null)
            return ((ShaderLabSyntaxNode)node).Accept(this);

        return default;
    }

    public virtual TResult? DefaultVisit(SyntaxNode node)
    {
        return default;
    }

    #endregion
}