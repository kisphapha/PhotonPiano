

using PhotonPiano.Helper.Dtos.Classes;

namespace PhotonPiano.BusinessLogic.Interfaces
{
    public interface IClassService
    {
        Task<GetClassWithInstructorAndLessonsDto> GetClassDetail(long classId);
    }
}
