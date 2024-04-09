
namespace PhotonPiano.Helper.Dtos.StudentClassTuitons
{
    public class GetStudentClassTuitionDto
    {
        public long Id { get; set; }

        public long StudentClassId { get; set; }

        public int Month { get; set; }

        public long? Amount { get; set; }

        public bool Status { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime DueDate { get; set; }

        public string? TransactionId { get; set; }

        public DateTime? TransactionDate { get; set; }

        public string? TransactionDescription { get; set; }
    }
}
