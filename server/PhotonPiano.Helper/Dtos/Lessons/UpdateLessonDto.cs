using System.ComponentModel.DataAnnotations;

namespace PhotonPiano.Helper.Dtos.Lessons
{
    public class UpdateLessonDto
    {
        [Required]
        public required long Id { get; set; }
        [Range(1, 8, ErrorMessage = "Shift must be an integer between 1 and 8.")]
        public int? Shift { get; set; }

        public long? LocationId { get; set; }

        public long? ClassId { get; set; }

        public DateOnly? Date { get; set; }

        public string? ExamType { get; set; }
    }
}
