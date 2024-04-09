
using PhotonPiano.Helper.Dtos.Locations;

namespace PhotonPiano.Helper.Dtos.Lessons
{
    public class GetLessonWithLocationDto
    {
        public long Id { get; set; }

        public int Shift { get; set; }

        public long LocationId { get; set; }

        public string? ExamType { get; set; }

        public DateOnly? Date { get; set; }

        public GetLocationDto Location { get; set; } = null!;

    }
}
