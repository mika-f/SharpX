// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.CLI.Models;

namespace SharpX.CLI.Commands;

internal class InitCommand
{
    public async Task<int> RunAsync()
    {
        return ExitCodes.Success;
    }
}