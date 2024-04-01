
namespace PhotonPiano.Helper.Dtos.PostVotes
{
    public class GetPostVoteDto
    {
        public long PostId { get; set; }

        public long UserId { get; set; }

        public bool? UpOrDown { get; set; }
    }
}
