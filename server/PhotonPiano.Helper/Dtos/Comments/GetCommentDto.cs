namespace PhotonPiano.Helper.Dtos.Comments
{
    public class GetCommentDto
    {
        public long Id { get; set; }

        public string Content { get; set; } = null!;

        public long OwnerId { get; set; }

        public long? ReplyToCommentId { get; set; }

        public int Upvote { get; set; }

        public long PostId { get; set; }

        public DateTime? CommentDate { get; set; }
    }
}
