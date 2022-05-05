// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

namespace SharpX.Core.Syntax;

public partial class SyntaxList
{
    public class WithManyChildren : SyntaxList
    {
        private readonly SyntaxNode?[] _elements;

        public WithManyChildren(GreenNode node, SyntaxNode? parent, int position) : base(node, parent, position)
        {
            _elements = new SyntaxNode[node.SlotCount];
        }

        public override SyntaxNode? GetNodeSlot(int index)
        {
            return GetRedElement(ref _elements[index], index);
        }

        public override SyntaxNode? GetCachedSlot(int index)
        {
            return _elements[index];
        }
    }
}