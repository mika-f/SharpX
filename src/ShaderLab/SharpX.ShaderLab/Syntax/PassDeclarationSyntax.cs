// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.ShaderLab.Syntax.InternalSyntax;

namespace SharpX.ShaderLab.Syntax;

public class PassDeclarationSyntax : BasePassDeclarationSyntax
{
    private TagsDeclarationSyntax? _tags;
    private SyntaxNode? _commands;
    private CgProgramDeclarationSyntax? _cgProgram;

    public override SyntaxToken Keyword => new(this, ((PassDeclarationSyntaxInternal)Green).Keyword, Position, 0);

    public SyntaxToken OpenBraceToken => new(this, ((PassDeclarationSyntaxInternal)Green).OpenBraceToken, GetChildPosition(1), GetChildIndex(1));

    public TagsDeclarationSyntax? Tags => GetRed(ref _tags, 2);

    public SyntaxList<BaseCommandDeclarationSyntax> Commands
    {
        get
        {
            var red = GetRed(ref _commands, 3);
            return red != null ? new SyntaxList<BaseCommandDeclarationSyntax>(red) : default;
        }
    }

    public CgProgramDeclarationSyntax CgProgram => GetRed(ref _cgProgram, 4)!;

    public SyntaxToken CloseBraceToken => new(this, ((PassDeclarationSyntaxInternal)Green).CloseBraceToken, GetChildPosition(5), GetChildIndex(5));

    internal PassDeclarationSyntax(ShaderLabSyntaxNodeInternal node, SyntaxNode? parent, int position) : base(node, parent, position) { }

    public override SyntaxNode? GetNodeSlot(int index)
    {
        return index switch
        {
            2 => GetRed(ref _tags, 2),
            3 => GetRed(ref _commands, 3),
            4 => GetRed(ref _cgProgram, 4),
            _ => null
        };
    }

    public override SyntaxNode? GetCachedSlot(int index)
    {
        return index switch
        {
            2 => _tags,
            3 => _commands,
            4 => _cgProgram,
            _ => null
        };
    }

    public PassDeclarationSyntax Update(SyntaxToken keyword, SyntaxToken openBraceToken, TagsDeclarationSyntax? tags, SyntaxList<BaseCommandDeclarationSyntax> commands, CgProgramDeclarationSyntax cgProgram, SyntaxToken closeBraceToken)
    {
        if (keyword != Keyword || openBraceToken != OpenBraceToken || tags != Tags || commands != Commands || cgProgram != CgProgram || closeBraceToken != CloseBraceToken)
            return SyntaxFactory.PassDeclaration(keyword, openBraceToken, tags, commands, cgProgram, closeBraceToken);
        return this;
    }

    public PassDeclarationSyntax WithKeyword(SyntaxToken keyword)
    {
        return Update(keyword, OpenBraceToken, Tags, Commands, CgProgram, CloseBraceToken);
    }

    public PassDeclarationSyntax WithOpenBraceToken(SyntaxToken openBraceToken)
    {
        return Update(Keyword, openBraceToken, Tags, Commands, CgProgram, CloseBraceToken);
    }

    public PassDeclarationSyntax WithTags(TagsDeclarationSyntax? tags)
    {
        return Update(Keyword, OpenBraceToken, tags, Commands, CgProgram, CloseBraceToken);
    }

    public PassDeclarationSyntax WithCommands(SyntaxList<BaseCommandDeclarationSyntax> commands)
    {
        return Update(Keyword, OpenBraceToken, Tags, commands, CgProgram, CloseBraceToken);
    }

    public PassDeclarationSyntax WithCgProgram(CgProgramDeclarationSyntax cgProgram)
    {
        return Update(Keyword, OpenBraceToken, Tags, Commands, cgProgram, CloseBraceToken);
    }

    public PassDeclarationSyntax WithCloseBraceToken(SyntaxToken closeBraceToken)
    {
        return Update(Keyword, OpenBraceToken, Tags, Commands, CgProgram, closeBraceToken);
    }

    public override TResult? Accept<TResult>(ShaderLabSyntaxVisitor<TResult> visitor) where TResult : default
    {
        return visitor.VisitPassDeclaration(this);
    }
}