using System;
using System.Collections.Generic;

namespace PhotonPiano.Models.Models;

public partial class Criterion
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public decimal Weight { get; set; }

    public string? Description { get; set; }

    public string? For { get; set; }

    public virtual ICollection<EntranceTestResult> EntranceTestResults { get; set; } = new List<EntranceTestResult>();

    public virtual ICollection<StudentClassScore> StudentClassScores { get; set; } = new List<StudentClassScore>();
}
