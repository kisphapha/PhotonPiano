using PhotonPiano.Helper.Dtos.Lessons;
using PhotonPiano.Helper.Dtos.StudentLessons;

namespace PhotonPiano.BusinessLogic.Interfaces
{
    public interface IStudentLessonService
    {
        Task<List<GetStudentLessonDto>> GetStudentLessonsByClassIdAndStudentId(long classId, long studentId);

        Task<List<GetStudentLessonWithLocationDto>> GetDetailStudentLessonsByClassIdAndStudentId(long studentId, long classId, QueryLessonDto queryLessonDto);
    }
}
