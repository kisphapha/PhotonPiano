
using PhotonPiano.Helper.Dtos.Classes;
using PhotonPiano.Helper.Dtos.Locations;

namespace PhotonPiano.Helper.Dtos.Lessons
{
    public class GetLessonDetailDto
    {
        public long Id { get; set; }

        public int Shift { get; set; }

        public long LocationId { get; set; }

        public string? ExamType { get; set; }

        public long ClassId { get; set; }

        public DateOnly Date { get; set; }

        public bool IsLocked { get; set; }

        public GetClassSimplifiedDto Class { get; set; } = null!;
        public GetLocationDto Location { get; set; } = null!;

    }
}
