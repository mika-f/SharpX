﻿// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.Hlsl.Syntax.InternalSyntax;

namespace SharpX.Hlsl.Syntax;

public abstract class SwitchLabelSyntax : HlslSyntaxNode
{
    internal SwitchLabelSyntax(HlslSyntaxNodeInternal node, SyntaxNode? parent, int position) : base(node, parent, position) { }
}