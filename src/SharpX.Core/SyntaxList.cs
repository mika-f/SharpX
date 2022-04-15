using System.Collections;

namespace SharpX.Core
{
    public readonly struct SyntaxList<TNode> : IReadOnlyList<TNode>, IEquatable<SyntaxList<TNode>> where TNode : SyntaxNode
    {
        private readonly SyntaxNode? _node;

        public SyntaxList(SyntaxNode? node)
        {
            _node = node;
        }

        public SyntaxList(TNode? node) : this((SyntaxNode?)node) { }

        public SyntaxList(IEnumerable<TNode>? nodes) : this(CreateListNode(nodes)) { }

        private static SyntaxNode? CreateListNode(IEnumerable<TNode>? nodes)
        {
            if (nodes == null)
                return null;
        }

        public IEnumerator<TNode> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Count { get; }

        public TNode this[int index]
        {
            get
            {
                if (_node == null)
                    throw new ArgumentOutOfRangeException();

                if (_node.IsList)
                    if (index < _node.SlotCount)
                        return (TNode)_node.GetNodeSlot(index);
            }
        }

        public bool Equals(SyntaxList<TNode> other)
        {
            throw new NotImplementedException();
        }
    }
}
