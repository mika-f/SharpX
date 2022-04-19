// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using System.Collections;

namespace SharpX.Core;

public readonly partial struct SyntaxNodeOrTokenList
{
    public struct Enumerator : IEnumerator<SyntaxNodeOrToken>
    {
        private readonly SyntaxNodeOrTokenList _list;
        private int _index;

        public Enumerator(SyntaxNodeOrTokenList list)
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

        public SyntaxNodeOrToken Current => _list[_index];

        object IEnumerator.Current => Current;

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}