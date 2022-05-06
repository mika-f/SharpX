// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using System.Collections;

namespace SharpX.Core;

public readonly partial struct SeparatedSyntaxList<TNode>
{
    public struct Enumerator : IEnumerator<TNode>
    {
        private readonly SeparatedSyntaxList<TNode> _list;
        private int _index;

        public Enumerator(SeparatedSyntaxList<TNode> list)
        {
            _list = list;
            _index = -1;
        }

        public bool MoveNext()
        {
            if (_index < _list.Count)
                _index++;

            return _index < _list.Count;
        }

        public void Reset()
        {
            _index = -1;
        }

        public TNode Current => _list[_index];

        object IEnumerator.Current => Current;

        public void Dispose() { }
    }
}