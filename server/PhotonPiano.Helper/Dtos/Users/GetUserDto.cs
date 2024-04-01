
using PhotonPiano.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace PhotonPiano.Helper.Dtos.Users
{
    public class GetUserDto
    {
        public long Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Phone { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }

        public string Role { get; set; } = null!;

        public string? Picture { get; set; }

        public DateOnly? DoB { get; set; }

        public string? Address { get; set; }

        public string? Gender { get; set; }

        public string? BankAccount { get; set; }


    }
}
