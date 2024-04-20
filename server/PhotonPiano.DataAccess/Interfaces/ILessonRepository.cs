using PhotonPiano.Helper.Dtos.Lessons;
using PhotonPiano.Models.Models;

namespace PhotonPiano.DataAccess.Interfaces
{
    public interface ILessonRepository : IGenericRepository<Lesson>
    {
        Task<List<Lesson>> GetQueriedLessonsAsync(QueryLessonDto queryLessonDto);
    }
}
