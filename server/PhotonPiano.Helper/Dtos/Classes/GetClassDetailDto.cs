
using PhotonPiano.Helper.Dtos.Instructors;
using PhotonPiano.Helper.Dtos.Lessons;
using PhotonPiano.Helper.Dtos.Students;
using PhotonPiano.Models.Models;

namespace PhotonPiano.Helper.Dtos.Classes
{
    public class GetClassDetailDto
    {
        public long Id { get; set; }

        public string Name { get; set; } = null!;

        public int Level { get; set; }

        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }

        public string Status { get; set; } = null!;

        public long InstructorId { get; set; }

        public GetInstructorWithUserDto Instructor { get; set; } = null!;

        public virtual ICollection<GetStudentWithUserDto> Students { get; set; } = new List<GetStudentWithUserDto>();
        public virtual ICollection<GetLessonDto> Lessons { get; set; } = new List<GetLessonDto>();

    }
}
