// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using System.Diagnostics;

using Kokuban;

namespace SharpX.CLI.Models;

public static class ConsoleExt
{
    private static readonly int MaxLabelLength = "[success]".Length;

    [Conditional("DEBUG")]
    public static void WriteDebug(string message)
    {
        Console.WriteLine(Chalk.BrightBlack + "[debug]".PadRight(MaxLabelLength) + Chalk.White + " " + message);
    }

    public static void WriteSuccess(string message)
    {
        Console.WriteLine(Chalk.BrightGreen + "[success]".PadRight(MaxLabelLength) + Chalk.White + " " + message);
    }

    public static void WriteInfo(string message)
    {
        Console.WriteLine(Chalk.BrightBlue + "[info]".PadRight(MaxLabelLength) + Chalk.White + " " + message);
    }

    public static void WriteWarning(string message)
    {
        Console.WriteLine(Chalk.BgBrightYellow + "[warning]".PadRight(MaxLabelLength) + Chalk.White + " " + message);
    }

    public static void WriteError(string message)
    {
        Console.Error.WriteLine(Chalk.BrightRed + "[error]".PadRight(MaxLabelLength) + Chalk.White + " " + message);
    }
}