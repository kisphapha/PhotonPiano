using System.ComponentModel.DataAnnotations;

namespace PhotonPiano.Helper.Dtos.EntranceTests
{
    public class CreateEntranceTestSlotDto
    {

        [Required] 
        public required long LocationId { get; set; }

        [Required]
        [Range(1, 8, ErrorMessage = "Shift must be an integer between 1 and 8.")]
        public required int Shift { get; set; }

        [Required] 
        public required DateOnly Date { get; set; }

        public long? InstructorId { get; set; }


    }
}
