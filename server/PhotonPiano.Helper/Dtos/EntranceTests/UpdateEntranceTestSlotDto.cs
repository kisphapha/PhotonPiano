using System.ComponentModel.DataAnnotations;
using System.Net.Sockets;

namespace PhotonPiano.Helper.Dtos.EntranceTests
{
    public class UpdateEntranceTestSlotDto
    {
        [Required]
        public required long Id { get; set; }
        public long? LocationId { get; set; }

        [Range(1, 8, ErrorMessage = "Shift must be an integer between 1 and 8.")]
        public int? Shift { get; set; }

        public DateOnly? Date { get; set; }

        public long? InstructorId { get; set; }


    }
}
