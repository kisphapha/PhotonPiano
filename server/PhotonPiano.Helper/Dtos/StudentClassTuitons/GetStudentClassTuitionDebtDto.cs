
namespace PhotonPiano.Helper.Dtos.StudentClassTuitons
{
    public class GetStudentClassTuitionDebtDto
    {
        public long Id { get; set; }

        public long StudentClassId { get; set; }

        public int Month { get; set; }

        public long? Amount { get; set; }

        public bool Status { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime DueDate { get; set; }

    }
}
