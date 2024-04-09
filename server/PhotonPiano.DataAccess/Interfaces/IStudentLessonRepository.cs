using PhotonPiano.Helper.Dtos.Lessons;
using PhotonPiano.Models.Models;

namespace PhotonPiano.DataAccess.Interfaces
{
    public interface IStudentLessonRepository : IGenericRepository<StudentLesson>
    {
        Task<List<StudentLesson>> GetStudentLessonsByClassIdAndStudentIdAsync(long classId, long studentId);

        Task<List<StudentLesson>> GetQueriedStudentLessonsByClassIdAndStudentIdAsync(long studentId, long classId, QueryLessonDto queryLessonDto);
    }
}
