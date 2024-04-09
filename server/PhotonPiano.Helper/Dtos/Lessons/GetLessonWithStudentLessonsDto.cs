
using PhotonPiano.Helper.Dtos.Locations;
using PhotonPiano.Helper.Dtos.StudentLessons;
using PhotonPiano.Models.Models;

namespace PhotonPiano.Helper.Dtos.Lessons
{
    public class GetLessonWithStudentLessonsDto
    {
        public long Id { get; set; }

        public int Shift { get; set; }

        public long LocationId { get; set; }

        public string? ExamType { get; set; }

        public DateOnly? Date { get; set; }

        public virtual ICollection<GetStudentLessonDto> StudentLessons { get; set; } = new List<GetStudentLessonDto>();


    }
}
