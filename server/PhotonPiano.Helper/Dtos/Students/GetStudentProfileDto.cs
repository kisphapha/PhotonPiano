

using PhotonPiano.Helper.Dtos.StudentClasses;
using PhotonPiano.Helper.Dtos.StudentClassTuitons;
using PhotonPiano.Models.Models;

namespace PhotonPiano.Helper.Dtos.Students
{
    public class GetStudentProfileDto
    {
        public long Id { get; set; }

        public DateOnly? JoinedDate { get; set; }

        public long? CurrentClassId { get; set; }

        public string? InstructorName { get; set; }

        public int Level { get; set; }

        public string Status { get; set; } = null!;

        public long UserId { get; set; }

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

        public int NumberOfPosts { get; set; }

        public int NumberOfComments { get; set; }

        public int NumberOfUpvotes { get; set; }

        public int NumberOfDownvotes { get; set; }

        public int UpvotesGiven { get; set; }

        public int DownvotesGiven { get; set; }

        public decimal? EntranceTestScore { get; set; }

        public string? EntranceTestRank { get; set; }

        public List<GetStudentClassDetailDto> StudentClasses { get; set; } = new List<GetStudentClassDetailDto>();
        public List<GetStudentClassTuitionDebtDto> TutionDebts { get; set; } = new List<GetStudentClassTuitionDebtDto>();
    }
}
