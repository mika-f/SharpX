// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

namespace SharpX.Compiler.Models;

public record CSharpCompilerOptions(string BaseUrl, List<string>? Sources, string Output, string Target, List<string> Libraries, List<string> Languages, List<string> Plugins)
{
    public static CSharpCompilerOptions Default => new(
        "./src/",
        new List<string> { "./src/" },
        "./out/",
        "none",
        new List<string>(),
        new List<string>(),
        new List<string>()
    );
}