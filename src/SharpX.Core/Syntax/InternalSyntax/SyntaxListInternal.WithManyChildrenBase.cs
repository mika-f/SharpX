// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using Microsoft.CodeAnalysis;

namespace SharpX.Core.Syntax.InternalSyntax;

public abstract partial class SyntaxListInternal
{
    public abstract class WithManyChildrenBase : SyntaxListInternal
    {
        protected readonly GreenNode[] Elements;

        protected WithManyChildrenBase(GreenNode[] elements)
        {
            Elements = elements;
            SlotCount = Elements.Length;

            Initialize();
        }

        protected WithManyChildrenBase(GreenNode[] elements, DiagnosticInfo[]? diagnostics, SyntaxAnnotation[]? annotations) : base(diagnostics, annotations)
        {
            Elements = elements;
            SlotCount = Elements.Length;

            Initialize();
        }

        private void Initialize()
        {
            foreach (var element in Elements)
                AdjustWidth(element);
        }

        public override GreenNode? GetSlot(int index)
        {
            return Elements[index];
        }

        public override SyntaxNode CreateRed(SyntaxNode? parent, int position)
        {
            return new SyntaxList.WithManyChildren(this, parent, position);
        }

        public override bool TryCreateRed(SyntaxNode? parent, int position, out SyntaxNode node)
        {
            node = CreateRed(parent, position);
            return true;
        }
    }
}