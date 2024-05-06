
using PhotonPiano.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace PhotonPiano.Helper.Dtos.Classes
{
    public class UpdateClassDto
    {
        [Required] public required long Id { get; set; }

        public string? Name { get; set; } = null!;
        [Range(1, 5, ErrorMessage = "Level must be an integer between 1 and 5.")]

        public int? Level { get; set; }

        public DateOnly? StartDate { get; set; }
        [EnumDataType(typeof(ClassStatus))] 
        public string? Status { get; set; } = null!;

        public long? InstructorId { get; set; }

        public int? Size { get; set; }


    }
}
