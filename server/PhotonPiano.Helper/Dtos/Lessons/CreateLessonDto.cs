using System.ComponentModel.DataAnnotations;

namespace PhotonPiano.Helper.Dtos.Lessons
{
    public class CreateLessonDto
    {
        [Required][Range(1, 8, ErrorMessage = "Shift must be an integer between 1 and 8.")]
        public required int Shift { get; set; }

        [Required] public required long LocationId { get; set; }

        [Required] public required long ClassId { get; set; }

        [Required] public required DateOnly Date { get; set; }

        public string? ExamType { get; set; }
    }
}
