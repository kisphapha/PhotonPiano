
using PhotonPiano.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace PhotonPiano.Helper.Dtos.Students
{
    public class QueryInstructorDto
    {
        public long? Id { get; set; }

        public string? Name { get; set; } = string.Empty;

        public string? Phone { get; set; } = string.Empty;

        public string? Email { get; set; } = string.Empty;

        public DateOnly? FromDoB { get; set; }
        public DateOnly? ToDoB { get; set; }

        public string? Address { get; set; } = string.Empty;

        public string? Gender { get; set; } = string.Empty;

        public string? BankAccount { get; set; } = string.Empty;

        public int? Level { get; set; }

        public long? FromContributeScore { get; set; }
        public long? ToContributeScore { get; set; }
    }
}
