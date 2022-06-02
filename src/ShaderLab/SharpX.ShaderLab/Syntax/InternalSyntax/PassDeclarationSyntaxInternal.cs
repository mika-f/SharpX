// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.Core.Syntax.InternalSyntax;

namespace SharpX.ShaderLab.Syntax.InternalSyntax;

internal class PassDeclarationSyntaxInternal : BasePassDeclarationSyntaxInternal
{
    private readonly GreenNode _commands;

    public override SyntaxTokenInternal Keyword { get; }

    public SyntaxTokenInternal OpenBraceToken { get; }

    public TagsDeclarationSyntaxInternal? Tags { get; }

    public SyntaxListInternal<BaseCommandDeclarationSyntaxInternal> Commands => new(_commands);

    public CgProgramDeclarationSyntaxInternal CgProgram { get; }

    public SyntaxTokenInternal CloseBraceToken { get; }

    public PassDeclarationSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal keyword, SyntaxTokenInternal openBraceToken, TagsDeclarationSyntaxInternal? tags, GreenNode commands, CgProgramDeclarationSyntaxInternal cgProgram,
                                         SyntaxTokenInternal closeBraceToken) : base(kind)
    {
        SlotCount = 6;

        AdjustWidth(keyword);
        Keyword = keyword;

        AdjustWidth(openBraceToken);
        OpenBraceToken = openBraceToken;

        if (tags != null)
        {
            AdjustWidth(tags);
            Tags = tags;
        }

        AdjustWidth(commands);
        _commands = commands;

        AdjustWidth(cgProgram);
        CgProgram = cgProgram;

        AdjustWidth(closeBraceToken);
        CloseBraceToken = closeBraceToken;
    }

    public PassDeclarationSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal keyword, SyntaxTokenInternal openBraceToken, TagsDeclarationSyntaxInternal? tags, GreenNode commands, CgProgramDeclarationSyntaxInternal cgProgram,
                                         SyntaxTokenInternal closeBraceToken, DiagnosticInfo[]? diagnostics) : base(kind, diagnostics)
    {
        SlotCount = 6;

        AdjustWidth(keyword);
        Keyword = keyword;

        AdjustWidth(openBraceToken);
        OpenBraceToken = openBraceToken;

        if (tags != null)
        {
            AdjustWidth(tags);
            Tags = tags;
        }

        AdjustWidth(commands);
        _commands = commands;

        AdjustWidth(cgProgram);
        CgProgram = cgProgram;

        AdjustWidth(closeBraceToken);
        CloseBraceToken = closeBraceToken;
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new PassDeclarationSyntaxInternal(Kind, Keyword, OpenBraceToken, Tags, _commands, CgProgram, CloseBraceToken, diagnostics);
    }

    public override GreenNode? GetSlot(int index)
    {
        return index switch
        {
            0 => Keyword,
            1 => OpenBraceToken,
            2 => Tags,
            3 => _commands,
            4 => CgProgram,
            5 => CloseBraceToken,
            _ => null
        };
    }

    public override SyntaxNode CreateRed(SyntaxNode? parent, int position)
    {
        return new PassDeclarationSyntax(this, parent, position);
    }
}