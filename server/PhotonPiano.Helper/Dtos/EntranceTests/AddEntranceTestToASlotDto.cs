
using System.ComponentModel.DataAnnotations;

namespace PhotonPiano.Helper.Dtos.EntranceTests
{
    public class AddEntranceTestToASlotDto
    {
        public List<long> EntranceTestIds { get; set; } = new List<long>();

        [Required]
        public required long SlotId;
    }
}
