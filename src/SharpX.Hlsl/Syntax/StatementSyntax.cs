// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.Hlsl.Syntax.InternalSyntax;

namespace SharpX.Hlsl.Syntax;

public abstract class StatementSyntax : HlslSyntaxNode
{
    public abstract SyntaxList<AttributeListSyntax> AttributeLists { get; }
    internal StatementSyntax(HlslSyntaxNodeInternal node, SyntaxNode? parent, int position) : base(node, parent, position) { }

    public StatementSyntax WithAttributeLists(SyntaxList<AttributeListSyntax> attributeLists)
    {
        return WithAttributeListsCore(attributeLists);
    }

    internal abstract StatementSyntax WithAttributeListsCore(SyntaxList<AttributeListSyntax> attributeLists);

    public StatementSyntax AddAttributeLists(params AttributeListSyntax[] items)
    {
        return AddAttributeListsCore(items);
    }

    internal abstract StatementSyntax AddAttributeListsCore(params AttributeListSyntax[] items);
}