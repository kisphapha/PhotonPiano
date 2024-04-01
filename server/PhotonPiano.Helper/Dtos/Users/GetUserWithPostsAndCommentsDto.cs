using PhotonPiano.Helper.Dtos.Comments;
using PhotonPiano.Helper.Dtos.CommentVotes;
using PhotonPiano.Helper.Dtos.Posts;
using PhotonPiano.Helper.Dtos.PostVotes;

namespace PhotonPiano.Helper.Dtos.Users
{
    public class GetUserWithPostsAndCommentsDto
    {
        public long Id { get; set; }

        public ICollection<GetCommentVoteDto> CommentVotes { get; set; } = new List<GetCommentVoteDto>();

        public ICollection<GetCommentDto> Comments { get; set; } = new List<GetCommentDto>();

        public ICollection<GetPostVoteDto> PostVotes { get; set; } = new List<GetPostVoteDto>();

        public ICollection<GetPostDto> Posts { get; set; } = new List<GetPostDto>();
    }
}
