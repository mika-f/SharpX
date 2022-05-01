// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

using Microsoft.CodeAnalysis;

namespace SharpX.Hlsl.SourceGenerator;

internal class SyntaxProviderComparer : IEqualityComparer<ValueTuple<INamedTypeSymbol?, string?>>
{
    public bool Equals((INamedTypeSymbol?, string?) x, (INamedTypeSymbol?, string?) y)
    {
        return (x.Item1?.Equals(y.Item1, SymbolEqualityComparer.Default) ?? false) && x.Item2 == y.Item2;
    }

    public int GetHashCode((INamedTypeSymbol?, string?) obj)
    {
        return SymbolEqualityComparer.Default.GetHashCode(obj.Item1);
    }
}