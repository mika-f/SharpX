// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using System.Collections;

namespace SharpX.Core;

public readonly partial struct SyntaxTokenList
{
    public struct Enumerator : IEnumerator<SyntaxToken>
    {
        private readonly SyntaxNode? _parent;
        private readonly GreenNode? _singleNodeOrList;
        private readonly int _baseIndex;
        private readonly int _count;

        private int _index;
        private GreenNode? _current;
        private int _position;

        public Enumerator(SyntaxTokenList list)
        {
            _parent = list._parent;
            _singleNodeOrList = list.Node;
            _baseIndex = list._index;
            _count = list.Count;

            _index = -1;
            _current = null;
            _position = list.Position;
        }

        public bool MoveNext()
        {
            if (_count == 0 || _count <= _index + 1)
            {
                _current = null;
                return false;
            }

            _index++;

            if (_current != null)
                _position = _current.FullWidth;

            Contract.AssertNotNull(_singleNodeOrList);
            _current = GetGreenNodeAt(_singleNodeOrList, _index);
            Contract.AssertNotNull(_current);

            return true;
        }

        public void Reset()
        {
            _index = -1;
        }

        public SyntaxToken Current => _current == null ? throw new InvalidOperationException() : new SyntaxToken(_parent, _current, _position, _baseIndex + _index);

        object IEnumerator.Current => Current;

        public void Dispose() { }
    }
}