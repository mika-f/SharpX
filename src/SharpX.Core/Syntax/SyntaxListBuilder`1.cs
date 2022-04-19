﻿// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

namespace SharpX.Core.Syntax;

/// <summary>
///     represents <see cref="Microsoft.CodeAnalysis.Syntax.SyntaxListBuilder{T}" />
/// </summary>
/// <typeparam name="TNode"></typeparam>
internal readonly struct SyntaxListBuilder<TNode> where TNode : SyntaxNode
{
    private readonly SyntaxListBuilder? _builder;

    public SyntaxListBuilder(int size) : this(new SyntaxListBuilder(size)) { }

    public static SyntaxListBuilder<TNode> Create()
    {
        return new SyntaxListBuilder<TNode>(8);
    }

    private SyntaxListBuilder(SyntaxListBuilder? builder)
    {
        _builder = builder;
    }

    public bool IsNull => _builder == null;

    public int Count => _builder?.Count ?? 0;

    public void Clear()
    {
        _builder?.Clear();
    }

    public SyntaxListBuilder<TNode> Add(TNode node)
    {
        _builder?.Add(node);
        return this;
    }

    public void AddRange(TNode[] items, int offset, int length)
    {
        _builder?.AddRange(items.Cast<SyntaxNode>().ToArray(), offset, length);
    }

    public void AddRange(SyntaxList<TNode> nodes)
    {
        _builder?.AddRange(nodes);
    }

    public void AddRange(SyntaxList<TNode> nodes, int offset, int length)
    {
        _builder?.AddRange(nodes, offset, length);
    }

    public SyntaxList<TNode> ToList()
    {
        return _builder!.ToList();
    }

    public static implicit operator SyntaxListBuilder?(SyntaxListBuilder<TNode> builder)
    {
        return builder._builder;
    }

    public static implicit operator SyntaxList<TNode>(SyntaxListBuilder<TNode> builder)
    {
        if (builder._builder != null)
            return builder.ToList();

        return default;
    }
}