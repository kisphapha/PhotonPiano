
using PhotonPiano.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace PhotonPiano.Helper.Dtos.Students
{
    public class UpdateStudentStatusInBatchDto
    {
        [Required]
        public required List<long> StudentIds { get; set; } = new List<long>();

        [EnumDataType(typeof(StudentStatus))]
        [Required]
        public required string Status { get; set; } = null!;
    }
}
