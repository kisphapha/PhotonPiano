using PhotonPiano.Helper.Dtos.User;
using PhotonPiano.Models.Models;

namespace PhotonPiano.BusinessLogic.Interfaces
{
    public interface IStudentService
    {
        Task<Student?> GetStudentDetailById(long id);
    }
}
