namespace PhotonPiano.Helper.Dtos.Lessons
{
    public class GetLessonDto
    {
        public long Id { get; set; }

        public int Shift { get; set; }

        public long LocationId { get; set; }

        public long ClassId { get; set; }

        public string? ExamType { get; set; }

        public DateOnly Date { get; set; }

        public bool IsLocked { get; set; }
    }
}
