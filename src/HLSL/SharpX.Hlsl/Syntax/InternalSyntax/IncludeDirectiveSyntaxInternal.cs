// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;

namespace SharpX.Hlsl.Syntax.InternalSyntax;

internal class IncludeDirectiveSyntaxInternal : DirectiveTriviaSyntaxInternal
{
    public override SyntaxTokenInternal HashToken { get; }

    public SyntaxTokenInternal IncludeKeyword { get; }

    public SyntaxTokenInternal File { get; }

    public override SyntaxTokenInternal EndOfDirectiveToken { get; }

    public IncludeDirectiveSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal hashToken, SyntaxTokenInternal includeKeyword, SyntaxTokenInternal file, SyntaxTokenInternal endOfDirectiveToken) : base(kind)
    {
        SlotCount = 4;

        AdjustWidth(hashToken);
        HashToken = hashToken;

        AdjustWidth(includeKeyword);
        IncludeKeyword = includeKeyword;

        AdjustWidth(file);
        File = file;

        AdjustWidth(endOfDirectiveToken);
        EndOfDirectiveToken = endOfDirectiveToken;
    }

    public IncludeDirectiveSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal hashToken, SyntaxTokenInternal includeKeyword, SyntaxTokenInternal file, SyntaxTokenInternal endOfDirectiveToken, DiagnosticInfo[]? diagnostics) : base(kind, diagnostics)
    {
        SlotCount = 4;

        AdjustWidth(hashToken);
        HashToken = hashToken;

        AdjustWidth(includeKeyword);
        IncludeKeyword = includeKeyword;

        AdjustWidth(file);
        File = file;

        AdjustWidth(endOfDirectiveToken);
        EndOfDirectiveToken = endOfDirectiveToken;
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new IncludeDirectiveSyntaxInternal(Kind, HashToken, IncludeKeyword, File, EndOfDirectiveToken, diagnostics);
    }

    public override GreenNode? GetSlot(int index)
    {
        return index switch
        {
            0 => HashToken,
            1 => IncludeKeyword,
            2 => File,
            3 => EndOfDirectiveToken,
            _ => null
        };
    }

    public override SyntaxNode CreateRed(SyntaxNode? parent, int position)
    {
        throw new NotImplementedException();
    }

    public override TResult? Accept<TResult>(HlslSyntaxVisitorInternal<TResult> visitor) where TResult : default
    {
        throw new NotImplementedException();
    }
}