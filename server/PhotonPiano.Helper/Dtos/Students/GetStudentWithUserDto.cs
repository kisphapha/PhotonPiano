
using PhotonPiano.Helper.Dtos.Users;
using PhotonPiano.Models.Models;

namespace PhotonPiano.Helper.Dtos.Students
{
    public class GetStudentWithUserDto
    {
        public long Id { get; set; }

        public DateOnly? JoinedDate { get; set; }

        public long? CurrentClassId { get; set; }

        public int Level { get; set; }

        public string Status { get; set; } = null!;

        public long UserId { get; set; }

        public DateTime RegistrationDate { get; set; }
    
        public string? ShortDesc { get; set; }

        public GetUserDto User { get; set; } = null!;
    }
}
