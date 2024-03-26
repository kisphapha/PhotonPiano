using System;
using System.Collections.Generic;

namespace PhotonPiano.Models;

public partial class Criterion
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public decimal Weight { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<EntranceTest> EntranceTests { get; set; } = new List<EntranceTest>();

    public virtual ICollection<StudentClassScore> StudentClassScores { get; set; } = new List<StudentClassScore>();
}
