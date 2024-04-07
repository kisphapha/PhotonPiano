
using System.ComponentModel.DataAnnotations;

namespace PhotonPiano.Helper.Dtos.EntranceTests
{
    public class CreateEntranceTestDto
    {
        [Required]
        public long StudentId { get; set; }
    }
}
