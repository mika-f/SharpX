// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.Hlsl.Syntax.InternalSyntax;

namespace SharpX.Hlsl.Syntax;

public class IncludeDirectiveSyntax : DirectiveTriviaSyntax
{
    public override SyntaxToken HashToken => new(this, ((IncludeDirectiveSyntaxInternal)Green).HashToken, Position, 0);

    public SyntaxToken IncludeKeyword => new(this, ((IncludeDirectiveSyntaxInternal)Green).IncludeKeyword, GetChildPosition(1), GetChildIndex(1));

    public SyntaxToken File => new(this, ((IncludeDirectiveSyntaxInternal)Green).File, GetChildPosition(2), GetChildIndex(2));

    public override SyntaxToken EndOfDirectiveToken => new(this, ((IncludeDirectiveSyntaxInternal)Green).EndOfDirectiveToken, GetChildPosition(3), GetChildIndex(3));

    public IncludeDirectiveSyntax(GreenNode node, SyntaxNode? parent, int position) : base(node, parent, position) { }

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
        return visitor.VisitIncludeDirective(this);
    }

    public IncludeDirectiveSyntax Update(SyntaxToken hashToken, SyntaxToken includeKeyword, SyntaxToken file, SyntaxToken endOfDirectiveToken)
    {
        if (hashToken != HashToken || includeKeyword != IncludeKeyword || file != File || endOfDirectiveToken != EndOfDirectiveToken)
            return SyntaxFactory.IncludeDirectiveTrivia(hashToken, includeKeyword, file, endOfDirectiveToken);
        return this;
    }

    public IncludeDirectiveSyntax WithHashToken(SyntaxToken hashToken)
    {
        return Update(hashToken, IncludeKeyword, File, EndOfDirectiveToken);
    }

    public IncludeDirectiveSyntax WithIncludeKeyword(SyntaxToken includeKeyword)
    {
        return Update(HashToken, includeKeyword, File, EndOfDirectiveToken);
    }

    public IncludeDirectiveSyntax WithFile(SyntaxToken file)
    {
        return Update(HashToken, IncludeKeyword, file, EndOfDirectiveToken);
    }

    public IncludeDirectiveSyntax WithEndOfDirectiveToken(SyntaxToken endOfDirectiveToken)
    {
        return Update(HashToken, IncludeKeyword, File, endOfDirectiveToken);
    }
}