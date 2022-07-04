// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.Hlsl.Syntax.InternalSyntax;

namespace SharpX.Hlsl.Syntax;

public class PragmaDirectiveTriviaSyntax : DirectiveTriviaSyntax
{
    public override SyntaxToken HashToken => new(this, ((PragmaDirectiveTriviaSyntaxInternal)Green).HashToken, Position, 0);

    public SyntaxToken PragmaKeyword => new(this, ((PragmaDirectiveTriviaSyntaxInternal)Green).PragmaKeyword, GetChildPosition(1), GetChildIndex(1));

    public SyntaxTokenList Arguments => new(this, ((PragmaDirectiveTriviaSyntaxInternal)Green).Arguments, GetChildPosition(2), GetChildIndex(2));

    public override SyntaxToken EndOfDirectiveToken => new(this, ((PragmaDirectiveTriviaSyntaxInternal)Green).EndOfDirectiveToken, GetChildPosition(3), GetChildIndex(3));

    public PragmaDirectiveTriviaSyntax(GreenNode node, SyntaxNode? parent, int position) : base(node, parent, position) { }

    public override SyntaxNode? GetNodeSlot(int index)
    {
        return null;
    }

    public override SyntaxNode? GetCachedSlot(int index)
    {
        return null;
    }

    public override TResult? Accept<TResult>(HlslSyntaxVisitor<TResult> visitor) where TResult : default
    {
        return visitor.VisitPragmaDirectiveTrivia(this);
    }

    public PragmaDirectiveTriviaSyntax Update(SyntaxToken hashToken, SyntaxToken pragmaKeyword, SyntaxTokenList arguments, SyntaxToken endOfDirectiveToken)
    {
        if (hashToken != HashToken || pragmaKeyword != PragmaKeyword || arguments != Arguments || endOfDirectiveToken != EndOfDirectiveToken)
            return SyntaxFactory.PragmaDirectiveTrivia(hashToken, pragmaKeyword, arguments, endOfDirectiveToken);
        return this;
    }

    public PragmaDirectiveTriviaSyntax WithHashToken(SyntaxToken hashToken)
    {
        return Update(hashToken, PragmaKeyword, Arguments, EndOfDirectiveToken);
    }

    public PragmaDirectiveTriviaSyntax WithPragmaKeyword(SyntaxToken pragmaKeyword)
    {
        return Update(HashToken, pragmaKeyword, Arguments, EndOfDirectiveToken);
    }

    public PragmaDirectiveTriviaSyntax WithArguments(SyntaxTokenList arguments)
    {
        return Update(HashToken, PragmaKeyword, arguments, EndOfDirectiveToken);
    }

    public PragmaDirectiveTriviaSyntax WithEndOfDirectiveToken(SyntaxToken endOfDirectiveToken)
    {
        return Update(HashToken, PragmaKeyword, Arguments, endOfDirectiveToken);
    }
}