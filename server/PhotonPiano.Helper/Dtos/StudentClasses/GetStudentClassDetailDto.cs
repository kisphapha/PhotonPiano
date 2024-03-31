
namespace PhotonPiano.Helper.Dtos.StudentClasses
{
    public class GetStudentClassDetailDto
    {
        public long StudentId { get; set; }

        public long ClassId { get; set; }

        public string? Result { get; set; }

        public decimal? Gpa { get; set; }

        public string? Rank { get; set; }

        public string? InstructorComment { get; set; }

        public int TotalLesson { get; set; } = 0;

        public int LessonAttended { get; set; } = 0;

        public int Year { get; set; } = DateTime.Now.Year;
    }
}
