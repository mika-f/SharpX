// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

#pragma warning disable CS8618

namespace SharpX.CLI.Models;

public class CompilerOptions
{
    [JsonPropertyName("target")]
    public string Target { get; set; }

    [JsonPropertyName("lib")]
    public List<string> Libraries { get; set; }

    [JsonPropertyName("outDir")]
    public string OutDir { get; set; }

    [JsonPropertyName("baseUrl")]
    public string BaseUrl { get; set; }
}