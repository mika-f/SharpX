// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

namespace SharpX.Core.Syntax.InternalSyntax;

/// <summary>
///     represents <see cref="Microsoft.CodeAnalysis.Syntax.InternalSyntax.SyntaxList" />
/// </summary>
public abstract partial class SyntaxListInternal : GreenNode
{
    public override string Language => throw new InvalidOperationException();

    public override string KindText => throw new InvalidOperationException();

    protected SyntaxListInternal() : base(ListKind) { }

    protected SyntaxListInternal(DiagnosticInfo[]? diagnostics) : base(ListKind, diagnostics) { }

    public override bool IsTriviaWithEndOfLine()
    {
        return false;
    }

    public override SyntaxToken CreateSeparator<TNode>(SyntaxNode element)
    {
        throw Exceptions.Unreachable;
    }

    public static GreenNode List(GreenNode node)
    {
        return node;
    }

    public static WithTwoChildren List(GreenNode node1, GreenNode node2)
    {
        return new WithTwoChildren(node1, node2);
    }

    public static WithThreeChildren List(GreenNode node1, GreenNode node2, GreenNode node3)
    {
        return new WithThreeChildren(node1, node2, node3);
    }

    public static GreenNode List(params GreenNode[] nodes)
    {
        return List(nodes, nodes.Length);
    }

    public static GreenNode List(GreenNode[] nodes, int count)
    {
        if (count < 10)
            return new WithManyChildren(nodes);
        return new WithLotsOfChildren(nodes);
    }
}