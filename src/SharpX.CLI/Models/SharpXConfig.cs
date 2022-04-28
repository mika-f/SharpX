// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace SharpX.CLI.Models;

public class SharpXConfig
{
    [JsonPropertyName("files")]
    public List<string> Files { get; set; }

    [JsonPropertyName("includes")]
    public List<string> Includes { get; set; }

    [JsonPropertyName("excludes")]
    public List<string> Excludes { get; set; }

    [JsonPropertyName("plugins")]
    public List<string> Plugins { get; set; }
}