
using PhotonPiano.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace PhotonPiano.Helper.Dtos.Classes
{
    public class CreateClassDto
    {
        [Required] public required string Name { get; set; } = null!;

        [Required][Range(1, 5, ErrorMessage = "Level must be an integer between 1 and 5.")]
        public required int Level { get; set; }

        [Required] public required DateOnly StartDate { get; set; }

        [Required] public required long InstructorId { get; set; }

        [Required] public required int Size { get; set; }

    }
}
