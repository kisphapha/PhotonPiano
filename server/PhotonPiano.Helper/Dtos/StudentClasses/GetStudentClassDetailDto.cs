
using PhotonPiano.Helper.Dtos.Classes;
using PhotonPiano.Helper.Dtos.StudentClassTuitons;
using PhotonPiano.Helper.Dtos.StudentLessons;
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

        public GetClassWithInstructorDto Class { get; set; } = null!;

        public ICollection<GetStudentClassTuitionDebtDto> StudentClassTuitions { get; set; } = new List<GetStudentClassTuitionDebtDto>();

        public List<GetStudentLessonDto> StudentLessons { get; set; } = new List<GetStudentLessonDto>();

    }
}
