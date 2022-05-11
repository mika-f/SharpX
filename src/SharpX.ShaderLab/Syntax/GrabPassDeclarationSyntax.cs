// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.ShaderLab.Syntax.InternalSyntax;

namespace SharpX.ShaderLab.Syntax;

public class GrabPassDeclarationSyntax : ShaderLabSyntaxNode
{
    private TagsDeclarationSyntax? _tags;
    private NameDeclarationSyntax? _name;

    public SyntaxToken Keyword => new(this, ((GrabPassDeclarationSyntaxInternal)Green).Keyword, Position, 0);

    public SyntaxToken OpenBraceToken => new(this, ((GrabPassDeclarationSyntaxInternal)Green).OpenBraceToken, GetChildPosition(1), GetChildIndex(1));

    public SyntaxToken? Identifier => new(this, ((GrabPassDeclarationSyntaxInternal)Green).Identifier, GetChildPosition(2), GetChildIndex(2));

    public TagsDeclarationSyntax? Tags => GetRed(ref _tags, 3);

    public NameDeclarationSyntax? Name => GetRed(ref _name, 4);

    public SyntaxToken CloseBraceToken => new(this, ((GrabPassDeclarationSyntaxInternal)Green).CloseBraceToken, GetChildPosition(5), GetChildIndex(5));

    internal GrabPassDeclarationSyntax(ShaderLabSyntaxNodeInternal node, SyntaxNode? parent, int position) : base(node, parent, position) { }

    public override SyntaxNode? GetNodeSlot(int index)
    {
        return index switch
        {
            3 => GetRed(ref _tags, 3),
            4 => GetRed(ref _name, 4),
            _ => null
        };
    }

    public override SyntaxNode? GetCachedSlot(int index)
    {
        return index switch
        {
            3 => _tags,
            4 => _name,
            _ => null
        };
    }

    public GrabPassDeclarationSyntax Update(SyntaxToken keyword, SyntaxToken openBraceToken, SyntaxToken? identifier, TagsDeclarationSyntax? tags, NameDeclarationSyntax? name, SyntaxToken closeBraceToken)
    {
        if (keyword != Keyword || openBraceToken != OpenBraceToken || identifier != Identifier || tags != Tags || name != Name || closeBraceToken != CloseBraceToken)
            return SyntaxFactory.GrabPassDeclaration(keyword, openBraceToken, identifier, tags, name, closeBraceToken);
        return this;
    }

    public GrabPassDeclarationSyntax WithKeyword(SyntaxToken keyword)
    {
        return Update(keyword, OpenBraceToken, Identifier, Tags, Name, CloseBraceToken);
    }

    public GrabPassDeclarationSyntax WithOpenBraceToken(SyntaxToken openBraceToken)
    {
        return Update(Keyword, openBraceToken, Identifier, Tags, Name, CloseBraceToken);
    }

    public GrabPassDeclarationSyntax WithIdentifier(SyntaxToken? identifier)
    {
        return Update(Keyword, OpenBraceToken, identifier, Tags, Name, CloseBraceToken);
    }

    public GrabPassDeclarationSyntax WithTags(TagsDeclarationSyntax? tags)
    {
        return Update(Keyword, OpenBraceToken, Identifier, tags, Name, CloseBraceToken);
    }

    public GrabPassDeclarationSyntax WithName(NameDeclarationSyntax? name)
    {
        return Update(Keyword, OpenBraceToken, Identifier, Tags, name, CloseBraceToken);
    }

    public GrabPassDeclarationSyntax WithCloseBraceToken(SyntaxToken closeBraceToken)
    {
        return Update(Keyword, OpenBraceToken, Identifier, Tags, Name, closeBraceToken);
    }

    public override TResult? Accept<TResult>(ShaderLabSyntaxVisitor<TResult> visitor) where TResult : default
    {
        return visitor.VisitGrabPassDeclaration(this);
    }
}