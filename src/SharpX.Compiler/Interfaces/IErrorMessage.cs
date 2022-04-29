// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using Microsoft.CodeAnalysis;

namespace SharpX.Compiler.Interfaces;

public interface IErrorMessage
{
    DiagnosticSeverity Severity { get; }

    string GetMessage();
}