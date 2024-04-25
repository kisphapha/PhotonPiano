
namespace PhotonPiano.Helper.Dtos.Classes
{
    public class GetClassWithTotalLessonDto
    {
        public long Id { get; set; }

        public string Name { get; set; } = null!;

        public int Level { get; set; }

        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }

        public string Status { get; set; } = null!;

        public long InstructorId { get; set; }

        public bool IsAnnouced { get; set; }

        public int TotalLessons { get; set; }
    }
}
