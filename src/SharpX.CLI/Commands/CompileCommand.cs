// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using System.Diagnostics;
using System.Reflection;

using Kokuban;

using Microsoft.CodeAnalysis;

using SharpX.CLI.Extensions;
using SharpX.CLI.Models;
using SharpX.Compiler;
using SharpX.Compiler.Models;

namespace SharpX.CLI.Commands;

internal class CompileCommand
{
    public async Task<int> RunAsync()
    {
        ConsoleExt.WriteInfo(Chalk.Bold + $"sharpx compile - v{Assembly.GetExecutingAssembly().GetName().Version}");

        var source = new CancellationTokenSource();
        var compiler = new CSharpCompiler(ToCompilerOptions());

        Console.CancelKeyPress += (_, _) => source.Cancel();

        if (!await compiler.LoadPluginsAsync(source.Token))
        {
            ConsoleExt.WriteError("failed to load plugins");
            return ExitCodes.Failure;
        }

        try
        {
            var sw = new Stopwatch();
            sw.Start();

            ConsoleExt.WriteInfo("start compiling SharpX C# sources...");

            var r = await compiler.CompileAsync(source.Token);
            sw.Stop();

            if (compiler.Errors.Any())
                foreach (var error in compiler.Errors)
                    switch (error.Severity)
                    {
                        case DiagnosticSeverity.Info:
                            ConsoleExt.WriteInfo(error.GetMessage());
                            break;

                        case DiagnosticSeverity.Warning:
                            ConsoleExt.WriteWarning(error.GetMessage());
                            break;

                        case DiagnosticSeverity.Error:
                            ConsoleExt.WriteError(error.GetMessage());
                            break;
                    }


            if (r)
            {
                ConsoleExt.WriteSuccess($"compile successfully in {sw.ElapsedMilliseconds.ToReadableString()}");
                return ExitCodes.Success;
            }

            ConsoleExt.WriteError($"compile failure in {sw.ElapsedMilliseconds.ToReadableString()} with {compiler.Errors.Count(w => w.Severity == DiagnosticSeverity.Error)} errors");
            return ExitCodes.Success;
        }
        catch (Exception e)
        {
            if (Debugger.IsAttached)
                Debug.WriteLine(e);

            ConsoleExt.WriteDebug(e.StackTrace!);
            ConsoleExt.WriteError($"compile failure with exception: {e.Message}");

            return ExitCodes.Failure;
        }
    }

    protected CSharpCompilerOptions ToCompilerOptions()
    {
        return CSharpCompilerOptions.Default;
    }
}