using PhotonPiano.Helper.Dtos.Students;
using PhotonPiano.Helper.Dtos.Users;
using PhotonPiano.Models.Models;

namespace PhotonPiano.BusinessLogic.Interfaces
{
    public interface IStudentService
    {
        Task<GetStudentProfileDto?> GetStudentDetailById(long id);
    }
}
