
using PhotonPiano.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace PhotonPiano.Helper.Dtos.Users
{
    public class CreateUserDto
    {
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        [RegularExpression(@"^(0)[1-9]\d{8}$", ErrorMessage = "Invalid phone number. It should be in the format '0xxxxxxxxx'.")]
        public string? Phone { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        public DateOnly? DoB { get; set; }
        //public DateTime? CreateDob { get; set; }
        [Required]
        public string? Address { get; set; }

        [EnumDataType(typeof(Gender))]
        public string? Gender { get; set; }

        public string? BankAccount { get; set; }
    }
}
