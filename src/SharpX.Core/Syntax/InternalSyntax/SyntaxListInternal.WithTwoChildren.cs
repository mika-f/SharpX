// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using Microsoft.CodeAnalysis;

namespace SharpX.Core.Syntax.InternalSyntax;

public abstract partial class SyntaxListInternal
{
    public class WithTwoChildren : SyntaxListInternal
    {
        private readonly GreenNode _node1;
        private readonly GreenNode _node2;

        public WithTwoChildren(GreenNode node1, GreenNode node2)
        {
            SlotCount = 2;

            AdjustWidth(node1);
            _node1 = node1;

            AdjustWidth(node2);
            _node2 = node2;
        }

        public WithTwoChildren(GreenNode node1, GreenNode node2, DiagnosticInfo[]? diagnostics, SyntaxAnnotation[]? annotations) : base(diagnostics, annotations)
        {
            SlotCount = 2;

            AdjustWidth(node1);
            _node1 = node1;

            AdjustWidth(node2);
            _node2 = node2;
        }

        public override GreenNode? GetSlot(int index)
        {
            return index switch
            {
                0 => _node1,
                1 => _node2,
                _ => null
            };
        }

        public override SyntaxNode CreateRed(SyntaxNode? parent, int position)
        {
            return new SyntaxList.WithTowChildren(this, parent, position);
        }

        public override GreenNode SetAnnotations(SyntaxAnnotation[]? annotations)
        {
            return new WithTwoChildren(_node1, _node2, GetDiagnostics(), annotations);
        }

        public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
        {
            return new WithTwoChildren(_node1, _node2, diagnostics, GetAnnotations());
        }
    }
}