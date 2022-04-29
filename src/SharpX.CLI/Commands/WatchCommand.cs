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

namespace SharpX.CLI.Commands;

internal class WatchCommand : CompileCommand
{
    public new async Task<int> RunAsync()
    {
        ConsoleExt.WriteInfo(Chalk.Bold + $"sharpx watch - v{Assembly.GetExecutingAssembly().GetName().Version}");

        ConsoleExt.WriteInfo("start initial compilation...");

        var source = new CancellationTokenSource();
        var compiler = new CSharpCompiler(ToCompilerOptions(FromConfigJson()));
        if (!await compiler.LoadPluginsAsync(source.Token))
        {
            ConsoleExt.WriteError("failed to load plugins");
            return ExitCodes.Failure;
        }

        var sw = new Stopwatch();
        sw.Start();

        var r = await compiler.CompileAsync(source.Token);

        sw.Stop();
        WriteErrorLogs(compiler);

        if (r)
            ConsoleExt.WriteSuccess($"compile successfully in {sw.ElapsedMilliseconds.ToReadableString()}");
        else
            ConsoleExt.WriteError($"compile failure in {sw.ElapsedMilliseconds.ToReadableString()} with {compiler.Errors.Count(w => w.Severity == DiagnosticSeverity.Error)} errors");


        ConsoleExt.WriteInfo("start watching filesystem");

        try
        {
            var watcher = new FileSystemWatcher
            {
                NotifyFilter = NotifyFilters.LastWrite,
                Filter = "*",
                IncludeSubdirectories = true,
                EnableRaisingEvents = true
            };


            var handler = CreateThrottleEventHandler(TimeSpan.FromSeconds(1), (_, _) =>
            {
                // ReSharper disable once AccessToDisposedClosure
                RecompileAsync(compiler, source.Token);
            });

            watcher.Created += handler;
            watcher.Changed += handler;
            watcher.Deleted += handler;

            Console.CancelKeyPress += (_, _) => source.Cancel();

            while (!source.IsCancellationRequested)
                Console.ReadKey(true);

            compiler.Dispose();

            watcher.Created -= handler;
            watcher.Changed -= handler;
            watcher.Deleted -= handler;
            watcher.Dispose();

            return ExitCodes.Success;
        }
        catch (Exception e)
        {
            ConsoleExt.WriteDebug(e.Message);
            ConsoleExt.WriteError("failed to start watching filesystem");
            return ExitCodes.Failure;
        }
    }

    private static void RecompileAsync(CSharpCompiler compiler, CancellationToken ct)
    {
        try
        {
            var sw = new Stopwatch();
            sw.Start();

            var r = compiler.CompileAsync(ct).Result;
            WriteErrorLogs(compiler);

            sw.Stop();
            if (r)
                ConsoleExt.WriteSuccess($"compile successfully in {sw.ElapsedMilliseconds.ToReadableString()}");
            else
                ConsoleExt.WriteError($"compile failure in {sw.ElapsedMilliseconds.ToReadableString()} with {compiler.Errors.Count(w => w.Severity == DiagnosticSeverity.Error)} errors");
        }
        catch (Exception e)
        {
            if (Debugger.IsAttached)
                Debug.WriteLine(e);

            ConsoleExt.WriteDebug(e.StackTrace!);
            ConsoleExt.WriteError($"compile failure with exception: {e.Message}");
        }
    }

    private static void WriteErrorLogs(CSharpCompiler compiler)
    {
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
    }

    private static FileSystemEventHandler CreateThrottleEventHandler(TimeSpan throttle, FileSystemEventHandler handler)
    {
        var isThrottling = false;
        return (sender, e) =>
        {
            if (isThrottling)
                return;

            handler.Invoke(sender, e);
            isThrottling = true;

            Task.Delay(throttle).ContinueWith(_ => isThrottling = false);
        };
    }
}