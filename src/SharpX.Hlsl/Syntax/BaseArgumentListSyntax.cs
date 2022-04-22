// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.Hlsl.Syntax.InternalSyntax;

namespace SharpX.Hlsl.Syntax;

public abstract class BaseArgumentListSyntax : HlslSyntaxNode
{
    public abstract SeparatedSyntaxList<ArgumentSyntax> Arguments { get; }
    internal BaseArgumentListSyntax(HlslSyntaxNodeInternal node, SyntaxNode? parent, int position) : base(node, parent, position) { }

    public BaseArgumentListSyntax WithArguments(SeparatedSyntaxList<ArgumentSyntax> arguments)
    {
        return WithArgumentsCore(arguments);
    }

    internal abstract BaseArgumentListSyntax WithArgumentsCore(SeparatedSyntaxList<ArgumentSyntax> arguments);

    public BaseArgumentListSyntax AddArguments(params ArgumentSyntax[] items)
    {
        return AddArgumentsCore(items);
    }

    internal abstract BaseArgumentListSyntax AddArgumentsCore(ArgumentSyntax[] items);
}