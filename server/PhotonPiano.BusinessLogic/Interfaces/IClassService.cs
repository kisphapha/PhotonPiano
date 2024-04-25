using PhotonPiano.Helper.Dtos.Classes;
using PhotonPiano.Helper.Dtos.Paginations;
using PhotonPiano.Models.Models;

namespace PhotonPiano.BusinessLogic.Interfaces
{
    public interface IClassService
    {
        Task<GetClassWithInstructorAndLessonsDto> GetClassDetail(long classId);

        Task<Class> GetRequiredClassById(long id);
        Task<PaginatedResult<GetClassWithTotalLessonDto>> GetPagedClasses(int pageNumber, int pageSize, QueryClassDto queryClassDto);
    }
}
