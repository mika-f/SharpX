// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using Microsoft.CodeAnalysis;

using SharpX.Compiler.Interfaces;

namespace SharpX.Compiler.Models;

internal class RoslynDiagnostic : IErrorMessage
{
    private readonly Diagnostic _diagnostic;

    public RoslynDiagnostic(Diagnostic diagnostic)
    {
        _diagnostic = diagnostic;
    }

    public DiagnosticSeverity Severity => _diagnostic.Severity;

    public string GetMessage()
    {
        var path = _diagnostic.Location.SourceTree!.FilePath;
        var position = _diagnostic.Location.GetLineSpan().StartLinePosition;
        return $"{path}({position.Line + 1},{position.Character}): {_diagnostic.Severity} {_diagnostic.Id}: {_diagnostic.GetMessage()}";
    }
}