
using PhotonPiano.Helper.Dtos.StudentClassTuitons;

namespace PhotonPiano.Helper.Dtos.StudentClasses
{
    public class GetStudentClassDetailDto
    {
        public long StudentId { get; set; }

        public long ClassId { get; set; }

        public string? Result { get; set; }

        public decimal? Gpa { get; set; }

        public string? Rank { get; set; }

        public List<GetStudentClassTuitionDebtDto> TutionDebts { get; set; } = new List<GetStudentClassTuitionDebtDto>();

    }
}
