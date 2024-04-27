using PhotonPiano.Helper.Dtos.Lessons;
using PhotonPiano.Helper.Dtos.StudentLessons;
using PhotonPiano.Models.Models;

namespace PhotonPiano.BusinessLogic.Interfaces
{
    public interface IStudentLessonService
    {
        Task<List<GetStudentLessonDto>> GetStudentLessonsByClassIdAndStudentId(long classId, long studentId);

        Task<List<GetStudentLessonWithLocationDto>> GetDetailStudentLessonsByClassIdAndStudentId(long studentId, long classId, QueryLessonDto queryLessonDto);

        Task AddStudentLesson(long studentId, long lessonId);

        Task<List<StudentLesson>> GetStudentLessonsByLessonId(long lessonId);

        Task ClearStudentLessonsByLessonId(long lessonId);
    }
}
