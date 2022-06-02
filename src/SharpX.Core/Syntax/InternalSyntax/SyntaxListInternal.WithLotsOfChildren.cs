// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

namespace SharpX.Core.Syntax.InternalSyntax;

public abstract partial class SyntaxListInternal
{
    public class WithLotsOfChildren : WithManyChildrenBase
    {
        private readonly int[] _offsets;

        public WithLotsOfChildren(GreenNode[] nodes) : base(nodes)
        {
            _offsets = CalculateOffsets(nodes);
        }

        public WithLotsOfChildren(GreenNode[] nodes, DiagnosticInfo[]? diagnostic) : base(nodes, diagnostic)
        {
            _offsets = CalculateOffsets(nodes);
        }

        public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
        {
            return new WithLotsOfChildren(Elements, diagnostics);
        }

        private static int[] CalculateOffsets(GreenNode[] elements)
        {
            var n = elements.Length;
            var offsets = new int[n];
            var offset = 0;

            for (var i = 0; i < n; i++)
            {
                offsets[i] = offset;
                offset += elements[i].FullWidth;
            }

            return offsets;
        }

        public override int GetSlotOffset(int index)
        {
            return _offsets[index];
        }
    }
}