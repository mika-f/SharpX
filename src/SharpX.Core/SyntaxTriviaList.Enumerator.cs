// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using System.Collections;

namespace SharpX.Core;

public readonly partial struct SyntaxTriviaList
{
    public struct Enumerator : IEnumerator<SyntaxTrivia>
    {
        private SyntaxToken _token;
        private GreenNode? _singleNodeOrList;
        private int _baseIndex;
        private int _count;

        private int _index;
        private GreenNode? _current;
        private int _position;

        public Enumerator(SyntaxTriviaList list)
        {
            _token = list.Token;
            _singleNodeOrList = list.Node;
            _baseIndex = list.Index;
            _count = list.Count;

            _index = -1;
            _current = null;
            _position = list.Position;
        }

        private void InitializeFrom(in SyntaxToken token, GreenNode greenNode, int index, int position)
        {
            _token = token;
            _singleNodeOrList = greenNode;
            _baseIndex = index;
            _count = greenNode.IsList ? greenNode.SlotCount : 1;

            _index = -1;
            _current = null;
            _position = position;
        }

        public void InitializeFromLeadingTrivia(SyntaxToken token)
        {
            Contract.AssertNotNull(token.Node);
            var node = token.Node.GetLeadingTrivia();

            Contract.AssertNotNull(node);
            InitializeFrom(token, node, 0, token.Position);
        }

        public void InitializeFromTrailingTrivia(SyntaxToken token)
        {
            Contract.AssertNotNull(token.Node);
            var leading = token.Node.GetLeadingTrivia();
            var index = 0;

            if (leading != null)
                index = leading.IsList ? leading.SlotCount : 1;

            var trailing = token.Node.GetTrailingTrivia();
            var position = token.Position + token.FullWidth;
            if (trailing != null)
                position -= trailing.FullWidth;

            Contract.AssertNotNull(trailing);
            InitializeFrom(token, trailing, index, position);
        }


        public bool MoveNext()
        {
            var newIndex = _index + 1;
            if (newIndex >= _count)
            {
                _current = null;
                return false;
            }

            _index = newIndex;
            if (_current != null)
                _position += _current.FullWidth;

            Contract.AssertNotNull(_singleNodeOrList);
            _current = GetGreenNodeAt(_singleNodeOrList, newIndex);
            return true;
        }

        public void Reset()
        {
            _index = -1;
        }

        public SyntaxTrivia Current => _current == null ? throw new InvalidOperationException() : new SyntaxTrivia(_token, _current, _position, _baseIndex + _index);

        object IEnumerator.Current => Current;

        public void Dispose() { }
    }
}