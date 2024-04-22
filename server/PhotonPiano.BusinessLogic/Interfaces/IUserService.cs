using PhotonPiano.Helper.Dtos.Users;
using PhotonPiano.Models.Models;

namespace PhotonPiano.BusinessLogic.Interfaces
{
    public interface IUserService
    {
        Task<List<GetUserDto>> GetUsers(QueryUserDto queryUserDto);

        Task<User?> GetUserById(long id);
        Task<GetUserDto> VerifyLogin(string? emailOrPhone, string? password);
        Task<GetLoginedUserDto> GetUserWithStudentsAndInstructors(long id);

        Task<GetUserWithStudentDto> CreateUser(CreateUserDto createUserDto);

        Task UpdateUser(long userId, UpdateUserDto updateUserDto);
    }
}
