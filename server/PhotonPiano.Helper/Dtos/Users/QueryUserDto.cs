
namespace PhotonPiano.Helper.Dtos.Users
{
    public class QueryUserDto
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
    }
}
