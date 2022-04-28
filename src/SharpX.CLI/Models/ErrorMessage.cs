// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.CLI.Interfaces;

namespace SharpX.CLI.Models;

public record ErrorMessage(string Message) : IErrorMessage
{
    public string ToMessageString()
    {
        return Message;
    }
}