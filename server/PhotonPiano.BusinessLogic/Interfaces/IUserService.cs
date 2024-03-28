using PhotonPiano.Helper.Dtos.User;

namespace PhotonPiano.BusinessLogic.Interfaces
{
    public interface IUserService
    {
        Task<List<GetUserDto>> GetUsers();
    }
}
