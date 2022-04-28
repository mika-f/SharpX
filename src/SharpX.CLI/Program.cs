// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.CLI.Commands;
using SharpX.CLI.Models;

var initCommand = SubCommand.Create<InitCommand>(args => args.RunAsync());
var compileCommand = SubCommand.Create<CompileCommand>(args => args.RunAsync());
var watchCommand = SubCommand.Create<WatchCommand>(args => args.RunAsync());

return await ConsoleHost.Create<CompileCommand>(args => args.RunAsync())
                        .AddCommand("init", initCommand)
                        .AddCommand("compile", compileCommand)
                        .AddCommand("watch", watchCommand)
                        .RunAsync(args);