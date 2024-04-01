
namespace PhotonPiano.Helper.Dtos.CommentVotes
{
    public class GetCommentVoteDto
    {
        public long CommentId { get; set; }

        public long UserId { get; set; }

        public bool? UpOrDown { get; set; }
    }
}
