using PhotonPiano.Models.Models;

namespace PhotonPiano.DataAccess.Interfaces
{
    public interface IStudentLessonRepository : IGenericRepository<StudentLesson>
    {
        Task<List<StudentLesson>> GetStudentLessonsByClassIdAndStudentIdAsync(long classId, long studentId);
    }
}
