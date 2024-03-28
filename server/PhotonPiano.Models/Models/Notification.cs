using System;
using System.Collections.Generic;

namespace PhotonPiano.Models.Models;

public partial class Notification
{
    public long Id { get; set; }

    public string Content { get; set; } = null!;

    public string? Picture { get; set; }

    public long? RecieverId { get; set; }

    public string? RefUrl { get; set; }

    public bool ViewStatus { get; set; }

    public virtual User? Reciever { get; set; }
}
