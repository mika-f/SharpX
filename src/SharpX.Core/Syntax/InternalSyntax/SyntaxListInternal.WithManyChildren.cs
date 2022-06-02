// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

namespace SharpX.Core.Syntax.InternalSyntax;

public abstract partial class SyntaxListInternal
{
    public class WithManyChildren : WithManyChildrenBase
    {
        public WithManyChildren(GreenNode[] elements) : base(elements) { }

        public WithManyChildren(GreenNode[] elements, DiagnosticInfo[]? diagnostics) : base(elements, diagnostics) { }

        public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
        {
            return new WithManyChildren(Elements, diagnostics);
        }
    }
}