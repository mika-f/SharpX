// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.ShaderLab.Syntax.InternalSyntax;

namespace SharpX.ShaderLab.Syntax;

public class QualifiedNameSyntax : NameSyntax
{
    private NameSyntax? _left;
    private SimpleNameSyntax? _right;

    public NameSyntax Left => GetRedAtZero(ref _left)!;

    public SyntaxToken DotToken => new(this, ((QualifiedNameSyntaxInternal)Green).DotToken, GetChildPosition(1), GetChildIndex(1));

    public SimpleNameSyntax Right => GetRed(ref _right, 2)!;

    internal QualifiedNameSyntax(ShaderLabSyntaxNodeInternal node, SyntaxNode? parent, int position) : base(node, parent, position) { }

    public override SyntaxNode? GetNodeSlot(int index)
    {
        return index switch
        {
            0 => GetRedAtZero(ref _left),
            2 => GetRed(ref _right, 2),
            _ => null
        };
    }

    public override SyntaxNode? GetCachedSlot(int index)
    {
        return index switch
        {
            0 => _left,
            2 => _right,
            _ => null
        };
    }

    public QualifiedNameSyntax Update(NameSyntax left, SyntaxToken dotToken, SimpleNameSyntax right)
    {
        if (left != Left || dotToken != DotToken || right != Right)
            return SyntaxFactory.QualifiedName(left, dotToken, right);
        return this;
    }

    public override TResult? Accept<TResult>(ShaderLabSyntaxVisitor<TResult> visitor) where TResult : default
    {
        return visitor.VisitQualifiedName(this);
    }
}