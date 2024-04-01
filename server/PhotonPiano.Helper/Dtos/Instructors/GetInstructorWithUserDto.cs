

using PhotonPiano.Helper.Dtos.Users;
using PhotonPiano.Models.Models;

namespace PhotonPiano.Helper.Dtos.Instructors
{
    public class GetInstructorWithUserDto
    {
        public long Id { get; set; }

        public long UserId { get; set; }

        public int Level { get; set; }

        public long ContributeScore { get; set; }

        public GetUserDto User { get; set; } = null!;
    }
}
