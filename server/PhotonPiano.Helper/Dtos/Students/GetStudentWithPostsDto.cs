
using PhotonPiano.Helper.Dtos.Users;
using PhotonPiano.Models.Models;

namespace PhotonPiano.Helper.Dtos.Students
{
    public class GetStudentWithPostsDto
    {
        public long Id { get; set; }
        public GetUserWithPostsAndCommentsDto User { get; set; } = null!;
    }
}
