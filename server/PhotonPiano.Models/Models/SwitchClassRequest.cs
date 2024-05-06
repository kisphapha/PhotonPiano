using System;
using System.Collections.Generic;

namespace PhotonPiano.Models.Models;

public partial class SwitchClassRequest
{
    public long Id { get; set; }

    public long? StudentId { get; set; }

    public long? OldClassId { get; set; }

    public long? NewClassId { get; set; }

    public DateTime? RequestDate { get; set; }

    public DateTime? ExpiredAt { get; set; }

    public virtual Class? NewClass { get; set; }

    public virtual Class? OldClass { get; set; }

    public virtual Student? Student { get; set; }
}
