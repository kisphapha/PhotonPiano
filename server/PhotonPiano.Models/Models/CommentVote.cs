using System;
using System.Collections.Generic;

namespace PhotonPiano.Models.Models;

public partial class CommentVote
{
    public long CommentId { get; set; }

    public long UserId { get; set; }

    public bool? UpOrDown { get; set; }

    public virtual Comment Comment { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
