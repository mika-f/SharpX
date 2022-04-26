// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.Hlsl.Syntax.InternalSyntax;

namespace SharpX.Hlsl.Syntax;

public class ParameterListSyntax : HlslSyntaxNode
{
    private SyntaxNode? _parameters;

    public SyntaxToken OpenParenToken => new(this, ((ParameterListSyntaxInternal)Green).OpenParenToken, Position, 0);

    public SeparatedSyntaxList<ParameterSyntax> Parameters
    {
        get
        {
            var red = GetRed(ref _parameters, 1);
            return red != null ? new SeparatedSyntaxList<ParameterSyntax>(red, GetChildIndex(1)) : default;
        }
    }

    public SyntaxToken CloseParenToken => new(this, ((ParameterListSyntaxInternal)Green).CloseParenToken, GetChildPosition(2), GetChildIndex(2));

    internal ParameterListSyntax(HlslSyntaxNodeInternal node, SyntaxNode? parent, int position) : base(node, parent, position) { }

    public override SyntaxNode? GetNodeSlot(int index)
    {
        return index == 1 ? GetRed(ref _parameters, 1) : null;
    }

    public override SyntaxNode? GetCachedSlot(int index)
    {
        return index == 1 ? _parameters : null;
    }

    public ParameterListSyntax Update(SyntaxToken openParenToken, SeparatedSyntaxList<ParameterSyntax> parameters, SyntaxToken closeParenToken)
    {
        if (openParenToken != OpenParenToken || parameters != Parameters || closeParenToken != CloseParenToken)
            return SyntaxFactory.ParameterList(openParenToken, parameters, closeParenToken);
        return this;
    }

    public ParameterListSyntax WithOpenParenToken(SyntaxToken openParenToken)
    {
        return Update(openParenToken, Parameters, CloseParenToken);
    }

    public ParameterListSyntax WithParameters(SeparatedSyntaxList<ParameterSyntax> parameters)
    {
        return Update(OpenParenToken, parameters, CloseParenToken);
    }

    public ParameterListSyntax WithCloseParenToken(SyntaxToken closeParenToken)
    {
        return Update(OpenParenToken, Parameters, closeParenToken);
    }

    public ParameterListSyntax AddParameters(params ParameterSyntax[] items)
    {
        return WithParameters(Parameters.AddRange(items));
    }
}