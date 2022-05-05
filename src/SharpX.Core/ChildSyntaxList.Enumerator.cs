// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace SharpX.Core;

public partial struct ChildSyntaxList
{
    public struct Enumerator : IEnumerator<SyntaxNodeOrToken>
    {
        private readonly int _count;
        private readonly SyntaxNode? _node;

        private int _childIndex;

        public Enumerator(SyntaxNode node, int count)
        {
            _node = node;
            _count = count;
            _childIndex = -1;
        }

        [MemberNotNullWhen(true, nameof(_node))]
        public bool MoveNext()
        {
            var newIndex = _childIndex + 1;
            if (newIndex < _count)
            {
                _childIndex = newIndex;
                Contract.AssertNotNull(_node);

                return true;
            }

            return false;
        }

        public void Reset()
        {
            _childIndex = -1;
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