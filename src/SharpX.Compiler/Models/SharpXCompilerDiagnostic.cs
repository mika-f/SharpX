// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using Microsoft.CodeAnalysis;

using SharpX.Compiler.Interfaces;

namespace SharpX.Compiler.Models;

internal class SharpXCompilerDiagnostic : IErrorMessage
{
    private readonly string _message;

    public SharpXCompilerDiagnostic(DiagnosticSeverity severity, string message)
    {
        Severity = severity;
        _message = message;
    }

    public DiagnosticSeverity Severity { get; }

    public string GetMessage()
    {
        return _message;
    }
}