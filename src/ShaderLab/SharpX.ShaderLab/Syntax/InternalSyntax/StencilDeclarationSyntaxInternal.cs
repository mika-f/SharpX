// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using Microsoft.CodeAnalysis;

using SharpX.Core;
using SharpX.Core.Syntax.InternalSyntax;

using SyntaxNode = SharpX.Core.SyntaxNode;

namespace SharpX.ShaderLab.Syntax.InternalSyntax;

internal class StencilDeclarationSyntaxInternal : BaseCommandDeclarationSyntaxInternal
{
    private readonly GreenNode _commands;

    public override SyntaxTokenInternal Keyword { get; }

    public SyntaxTokenInternal OpenBraceToken { get; }

    public SyntaxListInternal<CommandDeclarationSyntaxInternal> Commands => new(_commands);

    public SyntaxTokenInternal CloseBraceToken { get; }

    public StencilDeclarationSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal keyword, SyntaxTokenInternal openBraceToken, GreenNode commands, SyntaxTokenInternal closeBraceToken) : base(kind)
    {
        SlotCount = 4;

        AdjustWidth(keyword);
        Keyword = keyword;

        AdjustWidth(openBraceToken);
        OpenBraceToken = openBraceToken;

        AdjustWidth(commands);
        _commands = commands;

        AdjustWidth(closeBraceToken);
        CloseBraceToken = closeBraceToken;
    }

    public StencilDeclarationSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal keyword, SyntaxTokenInternal openBraceToken, GreenNode commands, SyntaxTokenInternal closeBraceToken, DiagnosticInfo[]? diagnostics, SyntaxAnnotation[]? annotations) : base(kind, diagnostics, annotations)
    {
        SlotCount = 4;

        AdjustWidth(keyword);
        Keyword = keyword;

        AdjustWidth(openBraceToken);
        OpenBraceToken = openBraceToken;

        AdjustWidth(commands);
        _commands = commands;

        AdjustWidth(closeBraceToken);
        CloseBraceToken = closeBraceToken;
    }

    public override GreenNode SetAnnotations(SyntaxAnnotation[]? annotations)
    {
        return new StencilDeclarationSyntaxInternal(Kind, Keyword, OpenBraceToken, _commands, CloseBraceToken, GetDiagnostics(), annotations);
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new StencilDeclarationSyntaxInternal(Kind, Keyword, OpenBraceToken, _commands, CloseBraceToken, diagnostics, GetAnnotations());
    }

    public override GreenNode? GetSlot(int index)
    {
        return index switch
        {
            0 => Keyword,
            1 => OpenBraceToken,
            2 => _commands,
            3 => CloseBraceToken,
            _ => null
        };
    }

    public override SyntaxNode CreateRed(SyntaxNode? parent, int position)
    {
        return new StencilDeclarationSyntax(this, parent, position);
    }
}