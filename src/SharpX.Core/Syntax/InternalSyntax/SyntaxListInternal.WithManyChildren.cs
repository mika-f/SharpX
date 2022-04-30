// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using Microsoft.CodeAnalysis;

namespace SharpX.Core.Syntax.InternalSyntax;

public abstract partial class SyntaxListInternal
{
    public class WithManyChildren : WithManyChildrenBase
    {
        public WithManyChildren(GreenNode[] elements) : base(elements) { }

        public WithManyChildren(GreenNode[] elements, DiagnosticInfo[]? diagnostics, SyntaxAnnotation[]? annotations) : base(elements, diagnostics, annotations) { }

        public override GreenNode SetAnnotations(SyntaxAnnotation[]? annotations)
        {
            return new WithManyChildren(Elements, GetDiagnostics(), annotations);
        }

        public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
        {
            return new WithManyChildren(Elements, diagnostics, GetAnnotations());
        }
    }
}