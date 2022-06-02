// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.Core.Syntax.InternalSyntax;

namespace SharpX.ShaderLab.Syntax.InternalSyntax;

internal class SubShaderDeclarationSyntaxInternal : ShaderLabSyntaxNodeInternal
{
    private readonly GreenNode? _commands;
    private readonly GreenNode? _passes;

    public SyntaxTokenInternal SubShaderKeyword { get; }

    public SyntaxTokenInternal OpenBraceToken { get; }

    public TagsDeclarationSyntaxInternal? Tags { get; }

    public SyntaxListInternal<CommandDeclarationSyntaxInternal> Commands => new(_commands);

    public CgIncludeDeclarationSyntaxInternal? CgInclude { get; }

    public SyntaxListInternal<BasePassDeclarationSyntaxInternal> Passes => new(_passes);

    public SyntaxTokenInternal CloseBraceToken { get; }

    public SubShaderDeclarationSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal subShaderKeyword, SyntaxTokenInternal openBraceToken, TagsDeclarationSyntaxInternal? tags, GreenNode? commands, CgIncludeDeclarationSyntaxInternal? cgInclude, GreenNode? passes,
                                              SyntaxTokenInternal closeBraceToken) : base(kind)
    {
        SlotCount = 7;

        AdjustWidth(subShaderKeyword);
        SubShaderKeyword = subShaderKeyword;

        AdjustWidth(openBraceToken);
        OpenBraceToken = openBraceToken;

        if (tags != null)
        {
            AdjustWidth(tags);
            Tags = tags;
        }

        if (commands != null)
        {
            AdjustWidth(commands);
            _commands = commands;
        }

        if (cgInclude != null)
        {
            AdjustWidth(cgInclude);
            CgInclude = cgInclude;
        }

        if (passes != null)
        {
            AdjustWidth(passes);
            _passes = passes;
        }

        AdjustWidth(closeBraceToken);
        CloseBraceToken = closeBraceToken;
    }

    public SubShaderDeclarationSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal subShaderKeyword, SyntaxTokenInternal openBraceToken, TagsDeclarationSyntaxInternal? tags, GreenNode? commands, CgIncludeDeclarationSyntaxInternal? cgInclude, GreenNode? passes,
                                              SyntaxTokenInternal closeBraceToken, DiagnosticInfo[]? diagnostics) : base(kind, diagnostics)
    {
        SlotCount = 7;

        AdjustWidth(subShaderKeyword);
        SubShaderKeyword = subShaderKeyword;

        AdjustWidth(openBraceToken);
        OpenBraceToken = openBraceToken;

        if (tags != null)
        {
            AdjustWidth(tags);
            Tags = tags;
        }

        if (commands != null)
        {
            AdjustWidth(commands);
            _commands = commands;
        }

        if (cgInclude != null)
        {
            AdjustWidth(cgInclude);
            CgInclude = cgInclude;
        }

        if (passes != null)
        {
            AdjustWidth(passes);
            _passes = passes;
        }


        AdjustWidth(closeBraceToken);
        CloseBraceToken = closeBraceToken;
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new SubShaderDeclarationSyntaxInternal(Kind, SubShaderKeyword, OpenBraceToken, Tags, _commands, CgInclude, _passes, CloseBraceToken, diagnostics);
    }

    public override GreenNode? GetSlot(int index)
    {
        return index switch
        {
            0 => SubShaderKeyword,
            1 => OpenBraceToken,
            2 => Tags,
            3 => _commands,
            4 => CgInclude,
            5 => _passes,
            6 => CloseBraceToken,
            _ => null
        };
    }

    public override SyntaxNode CreateRed(SyntaxNode? parent, int position)
    {
        return new SubShaderDeclarationSyntax(this, parent, position);
    }
}