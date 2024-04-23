
using System.ComponentModel.DataAnnotations;

namespace PhotonPiano.Helper.Dtos.EntranceTests
{
    public class AutoArrangeSlotDto
    {
        [Required] public List<int> AllowedShifts { get; set; } = new List<int>();
        [Required] public List<long> AllowedLocationIds { get; set; } = new List<long>();
        [Required] public List<long> AllowedInstructorIds { get; set; } = new List<long>();
        [Required] public DateOnly From { get; set; }
        [Required] public DateOnly To { get; set; }
        [Required] public int NumberOfStudents { get; set; }
    }
}
