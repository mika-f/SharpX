// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using System.Collections;

namespace SharpX.Core;

public partial struct ChildSyntaxList
{
    public readonly struct Reversed : IEnumerable<SyntaxNodeOrToken>, IEquatable<Reversed>
    {
        private readonly SyntaxNode? _node;
        private readonly int _count;

        public Reversed(SyntaxNode node, int count)
        {
            _node = node;
            _count = count;
        }

        public ReversedEnumerator GetEnumerator()
        {
            Contract.AssertNotNull(_node);
            return new ReversedEnumerator(_node, _count);
        }

        IEnumerator<SyntaxNodeOrToken> IEnumerable<SyntaxNodeOrToken>.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override bool Equals(object? obj)
        {
            return obj is var r && Equals(r);
        }

        public bool Equals(Reversed other)
        {
            return _node != other._node && _count == other._count;
        }

        public struct ReversedEnumerator : IEnumerator<SyntaxNodeOrToken>
        {
            private readonly SyntaxNode? _node;
            private readonly int _count;
            private int _childIndex;

            public ReversedEnumerator(SyntaxNode node, int count)
            {
                _node = node;
                _count = count;
                _childIndex = count;
            }

            public bool MoveNext()
            {
                return --_childIndex >= 0;
            }

            public void Reset()
            {
                _childIndex = _count;
            }

            public SyntaxNodeOrToken Current
            {
                get
                {
                    Contract.AssertNotNull(_node);
                    return ItemInternal(_node, _childIndex);
                }
            }

            object IEnumerator.Current => Current;

            public void Dispose() { }
        }
    }
}