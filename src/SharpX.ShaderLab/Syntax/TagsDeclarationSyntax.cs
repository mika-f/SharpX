// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.ShaderLab.Syntax.InternalSyntax;

namespace SharpX.ShaderLab.Syntax;

public class TagsDeclarationSyntax : ShaderLabSyntaxNode
{
    private SyntaxNode? _tags;

    public SyntaxToken TagsKeyword => new(this, ((TagsDeclarationSyntaxInternal)Green).TagsKeyword, Position, 0);

    public SyntaxToken OpenBraceToken => new(this, ((TagsDeclarationSyntaxInternal)Green).OpenBraceToken, GetChildPosition(1), GetChildIndex(1));

    public SyntaxList<TagDeclarationSyntax> Tags
    {
        get
        {
            var red = GetRed(ref _tags, 2);
            return red != null ? new SyntaxList<TagDeclarationSyntax>(red) : default;
        }
    }

    public SyntaxToken CloseBraceToken => new(this, ((TagsDeclarationSyntaxInternal)Green).CloseBraceToken, GetChildPosition(3), GetChildIndex(3));

    internal TagsDeclarationSyntax(ShaderLabSyntaxNodeInternal node, SyntaxNode? parent, int position) : base(node, parent, position) { }

    public override SyntaxNode? GetNodeSlot(int index)
    {
        return index == 1 ? GetRed(ref _tags, 2) : null;
    }

    public override SyntaxNode? GetCachedSlot(int index)
    {
        return index == 1 ? _tags : null;
    }

    public TagsDeclarationSyntax Update(SyntaxToken tagsKeyword, SyntaxToken openBraceToken, SyntaxList<TagDeclarationSyntax> tags, SyntaxToken closeBraceToken)
    {
        if (tagsKeyword != TagsKeyword || openBraceToken != OpenBraceToken || tags != Tags || closeBraceToken != CloseBraceToken)
            return SyntaxFactory.TagsDeclaration(tagsKeyword, openBraceToken, tags, closeBraceToken);
        return this;
    }

    public TagsDeclarationSyntax WithTagsKeyword(SyntaxToken tagsKeyword)
    {
        return Update(tagsKeyword, OpenBraceToken, Tags, CloseBraceToken);
    }

    public TagsDeclarationSyntax WithOpenBraceToken(SyntaxToken openBraceToken)
    {
        return Update(TagsKeyword, openBraceToken, Tags, CloseBraceToken);
    }

    public TagsDeclarationSyntax WithTags(SyntaxList<TagDeclarationSyntax> tags)
    {
        return Update(TagsKeyword, OpenBraceToken, tags, CloseBraceToken);
    }

    public TagsDeclarationSyntax WithCloseBraceToken(SyntaxToken closeBraceToken)
    {
        return Update(TagsKeyword, OpenBraceToken, Tags, closeBraceToken);
    }

    public TagsDeclarationSyntax AddTags(params TagDeclarationSyntax[] items)
    {
        return WithTags(Tags.AddRange(items));
    }

    public override TResult? Accept<TResult>(ShaderLabSyntaxVisitor<TResult> visitor) where TResult : default
    {
        return visitor.VisitTagsDeclaration(this);
    }
}