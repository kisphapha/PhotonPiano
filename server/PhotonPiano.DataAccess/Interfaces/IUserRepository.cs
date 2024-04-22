
using PhotonPiano.Helper.Dtos.Users;
using PhotonPiano.Models.Models;

namespace PhotonPiano.DataAccess.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<List<User>> GetQueriedUser(QueryUserDto queryUserDto);
        Task<User?> GetUserWithStudentsAndInstructorsByIdAsync(long id);
    }
}
