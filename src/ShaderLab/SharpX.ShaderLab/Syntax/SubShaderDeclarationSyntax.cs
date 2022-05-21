// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.ShaderLab.Syntax.InternalSyntax;

namespace SharpX.ShaderLab.Syntax;

public class SubShaderDeclarationSyntax : ShaderLabSyntaxNode
{
    private TagsDeclarationSyntax? _tags;
    private SyntaxNode? _commands;
    private CgIncludeDeclarationSyntax? _cgInclude;
    private SyntaxNode? _passes;

    public SyntaxToken SubShaderKeyword => new(this, ((SubShaderDeclarationSyntaxInternal)Green).SubShaderKeyword, Position, 0);

    public SyntaxToken OpenBraceToken => new(this, ((SubShaderDeclarationSyntaxInternal)Green).OpenBraceToken, GetChildPosition(1), GetChildIndex(1));

    public TagsDeclarationSyntax? Tags => GetRed(ref _tags, 2);

    public SyntaxList<CommandDeclarationSyntax> Commands
    {
        get
        {
            var red = GetRed(ref _commands, 3);
            return red != null ? new SyntaxList<CommandDeclarationSyntax>(red) : default;
        }
    }

    public CgIncludeDeclarationSyntax? CgInclude => GetRed(ref _cgInclude, 4);

    public SyntaxList<BasePassDeclarationSyntax> Passes
    {
        get
        {
            var red = GetRed(ref _passes, 5);
            return red != null ? new SyntaxList<BasePassDeclarationSyntax>(red) : default;
        }
    }

    public SyntaxToken CloseBraceToken => new(this, ((SubShaderDeclarationSyntaxInternal)Green).CloseBraceToken, GetChildPosition(6), GetChildIndex(6));

    internal SubShaderDeclarationSyntax(ShaderLabSyntaxNodeInternal node, SyntaxNode? parent, int position) : base(node, parent, position) { }

    public override SyntaxNode? GetNodeSlot(int index)
    {
        return index switch
        {
            2 => GetRed(ref _tags, 2),
            3 => GetRed(ref _commands, 3),
            4 => GetRed(ref _cgInclude, 4),
            5 => GetRed(ref _passes, 5),
            _ => null
        };
    }

    public override SyntaxNode? GetCachedSlot(int index)
    {
        return index switch
        {
            2 => _tags,
            3 => _commands,
            4 => _cgInclude,
            5 => _passes,
            _ => null
        };
    }

    public SubShaderDeclarationSyntax Update(SyntaxToken subShaderKeyword, SyntaxToken openBraceToken, TagsDeclarationSyntax? tags, SyntaxList<CommandDeclarationSyntax> commands, CgIncludeDeclarationSyntax? cgInclude, SyntaxList<BasePassDeclarationSyntax> passes, SyntaxToken closeBraceToken)
    {
        if (subShaderKeyword != SubShaderKeyword || openBraceToken != OpenBraceToken || tags != Tags || commands != Commands || cgInclude != CgInclude || passes != Passes || closeBraceToken != CloseBraceToken)
            return SyntaxFactory.SubShaderDeclaration(subShaderKeyword, openBraceToken, tags, commands, cgInclude, passes, closeBraceToken);
        return this;
    }

    public SubShaderDeclarationSyntax WithSubShaderKeyword(SyntaxToken subShaderKeyword)
    {
        return Update(subShaderKeyword, OpenBraceToken, Tags, Commands, CgInclude, Passes, CloseBraceToken);
    }

    public SubShaderDeclarationSyntax WithOpenBraceToken(SyntaxToken openBraceToken)
    {
        return Update(SubShaderKeyword, openBraceToken, Tags, Commands, CgInclude, Passes, CloseBraceToken);
    }

    public SubShaderDeclarationSyntax WithTags(TagsDeclarationSyntax? tags)
    {
        return Update(SubShaderKeyword, OpenBraceToken, tags, Commands, CgInclude, Passes, CloseBraceToken);
    }

    public SubShaderDeclarationSyntax WithCommands(SyntaxList<CommandDeclarationSyntax> commands)
    {
        return Update(SubShaderKeyword, OpenBraceToken, Tags, commands, CgInclude, Passes, CloseBraceToken);
    }

    public SubShaderDeclarationSyntax WithCgInclude(CgIncludeDeclarationSyntax? cgInclude)
    {
        return Update(SubShaderKeyword, OpenBraceToken, Tags, Commands, cgInclude, Passes, CloseBraceToken);
    }

    public SubShaderDeclarationSyntax WithPasses(SyntaxList<BasePassDeclarationSyntax> passes)
    {
        return Update(SubShaderKeyword, OpenBraceToken, Tags, Commands, CgInclude, passes, CloseBraceToken);
    }

    public SubShaderDeclarationSyntax WithCloseBraceToken(SyntaxToken closeBraceToken)
    {
        return Update(SubShaderKeyword, OpenBraceToken, Tags, Commands, CgInclude, Passes, closeBraceToken);
    }

    public SubShaderDeclarationSyntax AddCommands(params CommandDeclarationSyntax[] items)
    {
        return WithCommands(Commands.AddRange(items));
    }

    public SubShaderDeclarationSyntax AddPasses(params BasePassDeclarationSyntax[] items)
    {
        return WithPasses(Passes.AddRange(items));
    }

    public override TResult? Accept<TResult>(ShaderLabSyntaxVisitor<TResult> visitor) where TResult : default
    {
        return visitor.VisitSubShaderDeclaration(this);
    }
}