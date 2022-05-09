// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------


using SharpX.Core;
using SharpX.ShaderLab.Syntax;

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

    public virtual TResult? VisitArgumentList(ArgumentListSyntax node)
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