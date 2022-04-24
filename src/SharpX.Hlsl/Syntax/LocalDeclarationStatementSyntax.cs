// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.Hlsl.Syntax.InternalSyntax;

namespace SharpX.Hlsl.Syntax;

public class LocalDeclarationStatementSyntax : StatementSyntax
{
    private SyntaxNode? _attributeLists;
    private VariableDeclarationSyntax? _declaration;

    public override SyntaxList<AttributeListSyntax> AttributeLists => new(GetRedAtZero(ref _attributeLists));

    public SyntaxTokenList Modifiers
    {
        get
        {
            var slot = Green.GetSlot(1);
            return slot != null ? new SyntaxTokenList(this, slot, GetChildPosition(1), GetChildIndex(1)) : default;
        }
    }

    public VariableDeclarationSyntax Declaration => GetRed(ref _declaration, 2)!;

    public SyntaxToken SemicolonToken => new(this, ((LocalDeclarationStatementSyntaxInternal)Green).SemicolonToken, GetChildPosition(3), GetChildIndex(3));

    internal LocalDeclarationStatementSyntax(HlslSyntaxNodeInternal node, SyntaxNode? parent, int position) : base(node, parent, position) { }

    public override SyntaxNode? GetNodeSlot(int index)
    {
        return index switch
        {
            0 => GetRedAtZero(ref _attributeLists)!,
            2 => GetRed(ref _declaration, 2),
            _ => null
        };
    }

    public override SyntaxNode? GetCachedSlot(int index)
    {
        return index switch
        {
            0 => _attributeLists,
            2 => _declaration,
            _ => null
        };
    }

    public LocalDeclarationStatementSyntax Update(SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, VariableDeclarationSyntax declaration, SyntaxToken semicolonToken)
    {
        if (attributeLists != AttributeLists || modifiers != Modifiers || declaration != Declaration || semicolonToken != SemicolonToken)
            return SyntaxFactory.LocalDeclaration(attributeLists, modifiers, declaration, semicolonToken);
        return this;
    }

    public new LocalDeclarationStatementSyntax WithAttributeLists(SyntaxList<AttributeListSyntax> attributeLists)
    {
        return Update(attributeLists, Modifiers, Declaration, SemicolonToken);
    }

    internal override StatementSyntax WithAttributeListsCore(SyntaxList<AttributeListSyntax> attributeLists)
    {
        return WithAttributeLists(attributeLists);
    }

    public LocalDeclarationStatementSyntax WithModifiers(SyntaxTokenList modifiers)
    {
        return Update(AttributeLists, modifiers, Declaration, SemicolonToken);
    }

    public LocalDeclarationStatementSyntax WithDeclaration(VariableDeclarationSyntax declaration)
    {
        return Update(AttributeLists, Modifiers, declaration, SemicolonToken);
    }

    public LocalDeclarationStatementSyntax WithSemicolonToken(SyntaxToken semicolonToken)
    {
        return Update(AttributeLists, Modifiers, Declaration, semicolonToken);
    }

    public new LocalDeclarationStatementSyntax AddAttributeLists(params AttributeListSyntax[] items)
    {
        return WithAttributeLists(AttributeLists.AddRange(items));
    }

    internal override StatementSyntax AddAttributeListsCore(params AttributeListSyntax[] items)
    {
        return AddAttributeLists(items);
    }

    public LocalDeclarationStatementSyntax AddModifiers(params SyntaxToken[] items)
    {
        return WithModifiers(Modifiers.AddRange(items));
    }
}