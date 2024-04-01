using System;
using System.Collections.Generic;

namespace PhotonPiano.Models.Models;

public partial class Comment
{
    public long Id { get; set; }

    public string Content { get; set; } = null!;

    public long OwnerId { get; set; }

    public long? ReplyToCommentId { get; set; }

    public int Upvote { get; set; }

    public long PostId { get; set; }

    public DateTime? CommentDate { get; set; }

    public virtual ICollection<CommentVote> CommentVotes { get; set; } = new List<CommentVote>();

    public virtual ICollection<Comment> InverseReplyToComment { get; set; } = new List<Comment>();

    public virtual Post Post { get; set; } = null!;

    public virtual Comment? ReplyToComment { get; set; }
}
