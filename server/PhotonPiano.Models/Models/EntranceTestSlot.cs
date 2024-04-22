using System;
using System.Collections.Generic;

namespace PhotonPiano.Models.Models;

public partial class EntranceTestSlot
{
    public long Id { get; set; }

    public long LocationId { get; set; }

    public int Shift { get; set; }

    public DateOnly Date { get; set; }

    public bool IsAnnoucedTime { get; set; }

    public DateTime? AnnounceTime { get; set; }

    public bool IsAnnoucedScore { get; set; }

    public long? InstructorId { get; set; }

    public virtual ICollection<EntranceTest> EntranceTests { get; set; } = new List<EntranceTest>();

    public virtual Instructor? Instructor { get; set; }

    public virtual Location Location { get; set; } = null!;
}
