

using PhotonPiano.Helper.Dtos.Classes;
using PhotonPiano.Helper.Dtos.EntranceTest;
using PhotonPiano.Helper.Dtos.StudentClasses;
using PhotonPiano.Helper.Dtos.StudentClassTuitons;
using PhotonPiano.Helper.Dtos.StudentLessons;
using PhotonPiano.Helper.Dtos.Users;
using PhotonPiano.Models.Models;

namespace PhotonPiano.Helper.Dtos.Students
{
    public class GetStudentProfileDto
    {
        public long Id { get; set; }

        public DateOnly? JoinedDate { get; set; }

        public long? CurrentClassId { get; set; }

        public int Level { get; set; }

        public string Status { get; set; } = null!;

        public long UserId { get; set; }

        public DateTime RegistrationDate { get; set; }

        public GetClassWithInstructorDto? CurrentClass { get; set; }

        public ICollection<GetEntranceTestScoreDto> EntranceTests { get; set; } = new List<GetEntranceTestScoreDto>();

        public ICollection<GetStudentClassDetailDto> StudentClasses { get; set; } = new List<GetStudentClassDetailDto>();

        public GetUserDto User { get; set; } = null!;
    }
}
