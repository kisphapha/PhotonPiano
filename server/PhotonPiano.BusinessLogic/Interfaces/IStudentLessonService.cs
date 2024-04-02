using PhotonPiano.Helper.Dtos.StudentLessons;

namespace PhotonPiano.BusinessLogic.Interfaces
{
    public interface IStudentLessonService
    {
        Task<List<GetStudentLessonDto>> GetStudentLessonsByClassIdAndStudentId(long classId, long studentId);
    }
}
