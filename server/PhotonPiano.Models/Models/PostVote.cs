using System;
using System.Collections.Generic;

namespace PhotonPiano.Models.Models;

public partial class PostVote
{
    public long PostId { get; set; }

    public long UserId { get; set; }

    public bool? UpOrDown { get; set; }

    public virtual Post Post { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
