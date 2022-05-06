// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using Microsoft.CodeAnalysis;

namespace SharpX.Core.Syntax.InternalSyntax;

public abstract partial class SyntaxListInternal
{
    public class WithThreeChildren : SyntaxListInternal
    {
        private readonly GreenNode _node1;
        private readonly GreenNode _node2;
        private readonly GreenNode _node3;

        public WithThreeChildren(GreenNode node1, GreenNode node2, GreenNode node3)
        {
            SlotCount = 3;

            AdjustWidth(node1);
            _node1 = node1;

            AdjustWidth(node2);
            _node2 = node2;

            AdjustWidth(node3);
            _node3 = node3;
        }

        public WithThreeChildren(GreenNode node1, GreenNode node2, GreenNode node3, DiagnosticInfo[]? diagnostics, SyntaxAnnotation[]? annotations) : base(diagnostics, annotations)
        {
            SlotCount = 3;

            AdjustWidth(node1);
            _node1 = node1;

            AdjustWidth(node2);
            _node2 = node2;

            AdjustWidth(node3);
            _node3 = node3;
        }

        public override GreenNode? GetSlot(int index)
        {
            return index switch
            {
                0 => _node1,
                1 => _node2,
                2 => _node3,
                _ => null
            };
        }

        public override SyntaxNode CreateRed(SyntaxNode? parent, int position)
        {
            return new SyntaxList.WithThreeChildren(this, parent, position);
        }

        public override bool TryCreateRed(SyntaxNode? parent, int position, out SyntaxNode node)
        {
            node = CreateRed(parent, position);
            return true;
        }

        public override GreenNode SetAnnotations(SyntaxAnnotation[]? annotations)
        {
            return new WithThreeChildren(_node1, _node2, _node3, GetDiagnostics(), annotations);
        }

        public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
        {
            return new WithThreeChildren(_node1, _node2, _node3, diagnostics, GetAnnotations());
        }
    }
}