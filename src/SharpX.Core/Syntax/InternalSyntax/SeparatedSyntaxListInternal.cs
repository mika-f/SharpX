namespace SharpX.Core.Syntax.InternalSyntax
{
    public readonly struct SeparatedSyntaxListInternal<TNode> where TNode : GreenNode
    {
        private readonly SyntaxListInternal<GreenNode> _list;

        public SeparatedSyntaxListInternal(SyntaxListInternal<GreenNode> list)
        {
            _list = list;
        }

        public GreenNode? Node => _list.Node;

        public int Count => (_list.Count + 1) >> 1;

        public int SeparatorCount => _list.Count >> 1;

        public TNode? this[int index] => (TNode?)_list[index << 1];

        public GreenNode? GetSeparator(int index)
        {
            return _list[(index << 1) + 1];
        }

        public SyntaxListInternal<GreenNode> GetWithSeparator()
        {
            return _list;
        }
    }
}
