using System;
using System.Collections.Generic;

namespace PhotonPiano.Models.Models;

public partial class Instructor
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public int Level { get; set; }

    public long ContributeScore { get; set; }

    public DateOnly? JoinDate { get; set; }

    public string Status { get; set; } = null!;

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();

    public virtual ICollection<EntranceTestSlot> EntranceTestSlots { get; set; } = new List<EntranceTestSlot>();

    public virtual User User { get; set; } = null!;
}
