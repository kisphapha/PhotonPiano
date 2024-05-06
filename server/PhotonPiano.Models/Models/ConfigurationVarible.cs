using System;
using System.Collections.Generic;

namespace PhotonPiano.Models.Models;

public partial class ConfigurationVarible
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Value { get; set; }

    public string Category { get; set; } = null!;
}
