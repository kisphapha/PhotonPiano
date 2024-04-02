
using PhotonPiano.Helper.Dtos.Classes;
using PhotonPiano.Helper.Dtos.StudentClassTuitons;
using PhotonPiano.Models.Models;

namespace PhotonPiano.Helper.Dtos.StudentClasses
{
    public class GetStudentClassDetailDto
    {
        public long Id { get; set; }

        public long StudentId { get; set; }

        public long ClassId { get; set; }

        public string? Result { get; set; }

        public decimal? Gpa { get; set; }

        public string? Rank { get; set; }

        public string? InstructorComment { get; set; }

        public virtual GetClassDto Class { get; set; } = null!;

        public virtual ICollection<GetStudentClassTuitionDebtDto> StudentClassTuitions { get; set; } = new List<GetStudentClassTuitionDebtDto>();

    }
}
