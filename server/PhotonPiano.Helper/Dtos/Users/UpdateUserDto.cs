
using PhotonPiano.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace PhotonPiano.Helper.Dtos.Users
{
    public class UpdateUserDto
    {
        public string? Name { get; set; } = null!;
        public DateOnly? DoB { get; set; }
        public string? Address { get; set; }

        [EnumDataType(typeof(Gender))]
        public string? Gender { get; set; }
        public string? BankAccount { get; set; }
    }
}
