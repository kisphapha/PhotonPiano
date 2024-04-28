
namespace PhotonPiano.Helper.Dtos.Lessons
{
    public class QueryLessonDto
    {
        public long? Id { get; set; }
        public int? Shift { get; set; }

        public long? LocationId { get; set; }

        public string? ExamType { get; set; }

        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }

        public long? ClassId { get; set; }
    }
}
