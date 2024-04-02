using PhotonPiano.Helper.Dtos.Classes;
using PhotonPiano.Models.Models;

namespace PhotonPiano.Helper.Dtos.StudentClasses
{
    public class GetStudentClassWithStudentLessonsDto
    {
        public long Id { get; set; }
        public virtual GetClassWithLessonsDto Class { get; set; } = null!;
    }
}
