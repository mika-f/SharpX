// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.Hlsl.Syntax.InternalSyntax;

namespace SharpX.Hlsl.Syntax;

public class CaseSwitchLabelSyntax : SwitchLabelSyntax
{
    private ExpressionSyntax? _value;

    public SyntaxToken CaseKeyword => new(this, ((CaseSwitchLabelSyntaxInternal)Green).CaseKeyword, Position, 0);

    public ExpressionSyntax Value => GetRed(ref _value, 1)!;

    public SyntaxToken ColonToken => new(this, ((CaseSwitchLabelSyntaxInternal)Green).ColonToken, GetChildPosition(2), GetChildIndex(2));

    internal CaseSwitchLabelSyntax(HlslSyntaxNodeInternal node, SyntaxNode? parent, int position) : base(node, parent, position) { }

    public override SyntaxNode? GetNodeSlot(int index)
    {
        return index == 1 ? GetRed(ref _value, 1) : null;
    }

    public override SyntaxNode? GetCachedSlot(int index)
    {
        return index == 1 ? _value : null;
    }

    public CaseSwitchLabelSyntax Update(SyntaxToken caseKeyword, ExpressionSyntax value, SyntaxToken colonToken)
    {
        if (caseKeyword != CaseKeyword || value != Value || colonToken != ColonToken)
            return SyntaxFactory.CaseSwitchLabel(caseKeyword, value, colonToken);
        return this;
    }

    public CaseSwitchLabelSyntax WithCaseKeyword(SyntaxToken caseKeyword)
    {
        return Update(caseKeyword, Value, ColonToken);
    }

    public CaseSwitchLabelSyntax WithValue(ExpressionSyntax value)
    {
        return Update(CaseKeyword, value, ColonToken);
    }

    public CaseSwitchLabelSyntax WithColonToken(SyntaxToken colonToken)
    {
        return Update(CaseKeyword, Value, colonToken);
    }

    public override TResult? Accept<TResult>(HlslSyntaxVisitor<TResult> visitor) where TResult : default
    {
        return visitor.VisitCaseSwitchLabel(this);
    }
}