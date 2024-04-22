
using System.ComponentModel.DataAnnotations;

namespace PhotonPiano.Helper.Dtos.EntranceTests
{
    public class AddStudentsToASlotDto
    {
        [Required]
        public required long SlotId { get; set; }

        public List<long> StudentIds { get; set; } = new List<long>();

    }
}
