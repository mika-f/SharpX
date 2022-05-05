// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

namespace SharpX.Core.Syntax;

public partial class SyntaxList
{
    public class WithTowChildren : SyntaxList
    {
        private SyntaxNode? _node0;
        private SyntaxNode? _node1;

        public WithTowChildren(GreenNode node, SyntaxNode? parent, int position) : base(node, parent, position) { }

        public override SyntaxNode? GetNodeSlot(int index)
        {
            return index switch
            {
                0 => GetRedElement(ref _node0, 0),
                1 => GetRedElementIfNotToken(ref _node1),
                _ => null
            };
        }

        public override SyntaxNode? GetCachedSlot(int index)
        {
            return index switch
            {
                0 => _node0,
                1 => _node1,
                _ => null
            };
        }
    }
}