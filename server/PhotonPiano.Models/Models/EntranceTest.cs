using System;
using System.Collections.Generic;

namespace PhotonPiano.Models.Models;

public partial class EntranceTest
{
    public long Id { get; set; }

    public long? StudentId { get; set; }

    public decimal? BandScore { get; set; }

    public string? Rank { get; set; }

    public bool IsAnnouced { get; set; }

    public long? EntranceTestSlot { get; set; }

    public virtual ICollection<EntranceTestResult> EntranceTestResults { get; set; } = new List<EntranceTestResult>();

    public virtual EntranceTestSlot? EntranceTestSlotNavigation { get; set; }

    public virtual Student? Student { get; set; }
}
