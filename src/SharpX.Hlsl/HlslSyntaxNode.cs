// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;

namespace SharpX.Hlsl;

public abstract class HlslSyntaxNode : SyntaxNode
{
    public override string Language => "HLSL";

    public SyntaxKind Kind => (SyntaxKind)Green.RawKind;

    internal new HlslSyntaxNode? Parent => (HlslSyntaxNode?)base.Parent;

    internal HlslSyntaxNode(GreenNode node, SyntaxNode? parent, int position) : base(node, parent, position) { }

    public override SyntaxNode? GetNodeSlot(int index)
    {
        throw new NotImplementedException();
    }
}