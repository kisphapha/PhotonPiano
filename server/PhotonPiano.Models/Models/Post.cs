using System;
using System.Collections.Generic;

namespace PhotonPiano.Models.Models;

public partial class Post
{
    public long Id { get; set; }

    public long OwnerId { get; set; }

    public string Content { get; set; } = null!;

    public DateTime PostedDate { get; set; }

    public int Upvote { get; set; }

    public int Downvote { get; set; }

    public long? ClassId { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual User Owner { get; set; } = null!;
}
