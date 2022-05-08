// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.ShaderLab.Syntax.InternalSyntax;

namespace SharpX.ShaderLab.Syntax;

public class CustomEditorDeclarationSyntax : ShaderLabSyntaxNode
{
    public SyntaxToken CustomEditorKeyword => new(this, ((CustomEditorDeclarationSyntaxInternal)Green).CustomEditorKeyword, Position, 0);

    public SyntaxToken FullyQualifiedInspectorName => new(this, ((CustomEditorDeclarationSyntaxInternal)Green).FullyQualifiedInspectorName, GetChildPosition(1), GetChildIndex(1));

    internal CustomEditorDeclarationSyntax(GreenNode node, SyntaxNode? parent, int position) : base(node, parent, position) { }

    public override SyntaxNode? GetNodeSlot(int index)
    {
        return null;
    }

    public override SyntaxNode? GetCachedSlot(int index)
    {
        return null;
    }

    public CustomEditorDeclarationSyntax Update(SyntaxToken customEditorKeyword, SyntaxToken fullyQualifiedInspectorName)
    {
        if (customEditorKeyword != CustomEditorKeyword || fullyQualifiedInspectorName != FullyQualifiedInspectorName)
            return SyntaxFactory.CustomEditorDeclaration(customEditorKeyword, fullyQualifiedInspectorName);
        return this;
    }

    public CustomEditorDeclarationSyntax WithCustomEditorKeyword(SyntaxToken customEditorKeyword)
    {
        return Update(customEditorKeyword, FullyQualifiedInspectorName);
    }

    public CustomEditorDeclarationSyntax WithFullyQualifiedInspectorName(SyntaxToken fullyQualifiedInspectorName)
    {
        return Update(CustomEditorKeyword, fullyQualifiedInspectorName);
    }

    public override TResult? Accept<TResult>(ShaderLabSyntaxVisitor<TResult> visitor) where TResult : default
    {
        return visitor.VisitCustomEditorDeclaration(this);
    }
}