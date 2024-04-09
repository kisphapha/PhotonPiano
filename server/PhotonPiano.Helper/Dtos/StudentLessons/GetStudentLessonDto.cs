
namespace PhotonPiano.Helper.Dtos.StudentLessons
{
    public class GetStudentLessonDto
    {
        public long StudentId { get; set; }

        public long LessonId { get; set; }

        public string Attendence { get; set; } = null!;
    }
}
