// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

namespace SharpX.Core.Syntax;

public struct SeparatedSyntaxListBuilder<TNode> where TNode : SyntaxNode
{
    private readonly SyntaxListBuilder _builder;
    private bool _expectedSeparator;

    public SeparatedSyntaxListBuilder(int size) : this(new SyntaxListBuilder(size)) { }

    public SeparatedSyntaxListBuilder(SyntaxListBuilder builder)
    {
        _builder = builder;
        _expectedSeparator = false;
    }

    public static SeparatedSyntaxListBuilder<TNode> Create()
    {
        return new SeparatedSyntaxListBuilder<TNode>(8);
    }

    public bool IsNull => _builder == null;

    public int Count => _builder.Count;

    public void Clear()
    {
        _builder.Clear();
    }

    private void CheckExpectedElement()
    {
        if (_expectedSeparator) throw new InvalidOperationException();
    }

    private void CheckExpectedSeparator()
    {
        if (!_expectedSeparator) throw new InvalidOperationException();
    }


    public SeparatedSyntaxListBuilder<TNode> Add(TNode node)
    {
        CheckExpectedElement();
        _expectedSeparator = true;
        _builder.Add(node);

        return this;
    }

    public SeparatedSyntaxListBuilder<TNode> AddSeparator(SyntaxToken separator)
    {
        CheckExpectedSeparator();
        _expectedSeparator = false;
        _builder.Add(separator.Node);

        return this;
    }

    public SeparatedSyntaxListBuilder<TNode> AddRange(SeparatedSyntaxList<TNode> items)
    {
        CheckExpectedElement();
        var list = items.GetWithSeparators();
        _builder.AddRange(list);
        _expectedSeparator = (_builder.Count & 1) != 0;

        return this;
    }

    public SeparatedSyntaxListBuilder<TNode> AddRange(SeparatedSyntaxList<TNode> items, int count)
    {
        CheckExpectedElement();
        var list = items.GetWithSeparators();
        _builder.AddRange(list, Count, Math.Min(count << 1, list.Count));
        _expectedSeparator = (_builder.Count & 1) != 0;

        return this;
    }

    public SeparatedSyntaxList<TNode> ToList()
    {
        return _builder?.ToSeparatedList<TNode>() ?? new SeparatedSyntaxList<TNode>();
    }

    public SeparatedSyntaxList<TDerived> ToList<TDerived>() where TDerived : TNode
    {
        return _builder?.ToSeparatedList<TDerived>() ?? new SeparatedSyntaxList<TDerived>();
    }

    public static implicit operator SyntaxListBuilder(SeparatedSyntaxListBuilder<TNode> builder)
    {
        return builder._builder;
    }

    public static implicit operator SeparatedSyntaxList<TNode>(SeparatedSyntaxListBuilder<TNode> builder)
    {
        return builder.ToList();
    }
}