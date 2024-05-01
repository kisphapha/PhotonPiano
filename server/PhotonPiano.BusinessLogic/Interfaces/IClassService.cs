using PhotonPiano.Helper.Dtos.Classes;
using PhotonPiano.Helper.Dtos.Ultilities;
using PhotonPiano.Models.Enums;
using PhotonPiano.Models.Models;

namespace PhotonPiano.BusinessLogic.Interfaces
{
    public interface IClassService
    {
        Task<GetClassDetailDto> GetClassDetail(long classId);

        Task<Class> GetRequiredClassById(long id);
        Task<PaginatedResult<GetClassWithTotalLessonDto>> GetPagedClasses(int pageNumber, int pageSize, QueryClassDto queryClassDto);

        Task AnnounceAClass(long id);

        Task AnnounceAllClass();

        Task<List<Class>> GetClassesBasedOnOption(ScheduleClassesOption option);
    }
}
