// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using System.Reflection;
using System.Text.Json;

using Kokuban;

using SharpX.CLI.Attributes;
using SharpX.CLI.Models;

#pragma warning disable CS8618

namespace SharpX.CLI.Commands;

internal class InitCommand
{
    [Option(IsRequired = false, Order = 0)]
    public string RootDir { get; set; }

    public async Task<int> RunAsync()
    {
        ConsoleExt.WriteInfo(Chalk.Bold + $"sharpx init - v{Assembly.GetExecutingAssembly().GetName().Version}");

        var json = JsonSerializer.Serialize(SharpXConfig.Default);

        if (string.IsNullOrWhiteSpace(RootDir))
        {
            await File.WriteAllTextAsync("./sharpx.config.json", json);
            ConsoleExt.WriteSuccess("sharpx.config.json successfully created");
        }
        else
        {
            if (!Directory.Exists(RootDir))
            {
                ConsoleExt.WriteError($"directory {RootDir} not found");
                return ExitCodes.Failure;
            }

            await File.WriteAllTextAsync(Path.Combine(RootDir, "sharpx.config.json"), json);
            ConsoleExt.WriteSuccess($"sharpx.config.json successfully created in {RootDir}");
        }

        return ExitCodes.Success;
    }
}