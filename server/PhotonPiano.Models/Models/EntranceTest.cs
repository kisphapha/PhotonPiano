using System;
using System.Collections.Generic;

namespace PhotonPiano.Models.Models;

public partial class EntranceTest
{
    public long Id { get; set; }

    public long? StudentId { get; set; }

    public long? LocationId { get; set; }

    public int? Shift { get; set; }

    public DateOnly? Date { get; set; }

    public virtual ICollection<EntranceTestResult> EntranceTestResults { get; set; } = new List<EntranceTestResult>();

    public virtual Location? Location { get; set; }

    public virtual Student? Student { get; set; }
}
