

using PhotonPiano.Helper.Dtos.Lessons;
using PhotonPiano.Models.Models;

namespace PhotonPiano.Helper.Dtos.StudentLessons
{
    public class GetStudentLessonWithLocationDto
    {
        public long StudentId { get; set; }

        public long LessonId { get; set; }

        public string Attendence { get; set; } = null!;

        public GetLessonWithLocationDto Lesson { get; set; } = null!;
    }
}
