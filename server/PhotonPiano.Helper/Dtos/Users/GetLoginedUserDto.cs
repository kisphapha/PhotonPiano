
using PhotonPiano.Helper.Dtos.Instructors;
using PhotonPiano.Helper.Dtos.Students;
using PhotonPiano.Models.Models;

namespace PhotonPiano.Helper.Dtos.Users
{
    public class GetLoginedUserDto
    {
        public long Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Phone { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }

        public string Role { get; set; } = null!;

        public string? Picture { get; set; }

        public DateOnly? DoB { get; set; }

        public string? Address { get; set; }

        public string? Gender { get; set; }

        public string? BankAccount { get; set; }
        public ICollection<GetInstructorDto> Instructors { get; set; } = new List<GetInstructorDto>();
        public ICollection<GetStudentDto> Students { get; set; } = new List<GetStudentDto>();

    }
}
