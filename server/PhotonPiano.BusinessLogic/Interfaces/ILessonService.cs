using PhotonPiano.Helper.Dtos.Lessons;
using PhotonPiano.Helper.Dtos.Locations;

namespace PhotonPiano.BusinessLogic.Interfaces
{
    public interface ILessonSerivce
    {
        Task<List<GetLessonWithLocationDto>> GetQueriedLessons(QueryLessonDto queryLessonDto);
    }
}
