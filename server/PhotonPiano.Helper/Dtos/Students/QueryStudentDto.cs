
using PhotonPiano.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace PhotonPiano.Helper.Dtos.Students
{
    public class QueryStudentDto
    {
        public long? Id { get; set; }

        public string? Name { get; set; } = string.Empty;

        public string? Phone { get; set; } = string.Empty;

        public string? Email { get; set; } = string.Empty;
        public string? Role { get; set; } = string.Empty;

        public DateOnly? FromDoB { get; set; }
        public DateOnly? ToDoB { get; set; }

        public string? Address { get; set; } = string.Empty;

        public string? Gender { get; set; } = string.Empty;

        public string? BankAccount { get; set; } = string.Empty;

        public long? StudentId { get; set; }

        public DateOnly? FromJoinedDate { get; set; }
        public DateOnly? ToJoinedDate { get; set; }

        public long? CurrentClassId { get; set; }

        public int? Level { get; set; }
        public string? Status { get; set; } = string.Empty;

        public DateTime? FromRegistrationDate { get; set; }
        public DateTime? ToRegistrationDate { get; set; }

        public string? ShortDesc { get; set; } = string.Empty;
    }
}
