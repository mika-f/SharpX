// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using Microsoft.CodeAnalysis;

using SharpX.Compiler.Models;

namespace SharpX.Compiler.Extensions;

internal static class DiagnosticsExtensions
{
    public static RoslynDiagnostic ToErrorMessage(this Diagnostic diagnostic)
    {
        return new RoslynDiagnostic(diagnostic);
    }
}