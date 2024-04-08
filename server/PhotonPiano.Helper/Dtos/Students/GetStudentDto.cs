
namespace PhotonPiano.Helper.Dtos.Students
{
    public class GetStudentDto
    {
        public long Id { get; set; }

        public DateOnly? JoinedDate { get; set; }

        public long? CurrentClassId { get; set; }

        public int Level { get; set; }

        public string Status { get; set; } = null!;

        public long UserId { get; set; }

        public DateTime RegistrationDate { get; set; }
    
        public string? ShortDesc { get; set; }

    }
}
