using System;
using System.Collections.Generic;

namespace PhotonPiano.Models.Models;

public partial class Location
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string Status { get; set; } = null!;

    public long Capacity { get; set; }

    public virtual ICollection<EntranceTest> EntranceTests { get; set; } = new List<EntranceTest>();

    public virtual ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
}
