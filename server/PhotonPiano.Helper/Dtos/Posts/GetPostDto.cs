namespace PhotonPiano.Helper.Dtos.Posts
{
    public class GetPostDto
    {
        public long Id { get; set; }

        public long OwnerId { get; set; }

        public string Content { get; set; } = null!;

        public DateTime PostedDate { get; set; }

        public int Upvote { get; set; }

        public int Downvote { get; set; }

        public long? ClassId { get; set; }
    }
}
