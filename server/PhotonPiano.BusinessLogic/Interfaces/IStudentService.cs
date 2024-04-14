using PhotonPiano.Helper.Dtos.Students;
using PhotonPiano.Helper.Dtos.Users;
using PhotonPiano.Models.Models;

namespace PhotonPiano.BusinessLogic.Interfaces
{
    public interface IStudentService
    {
        Task<GetStudentProfileDto?> GetStudentDetailById(long id);
        Task<GetStudentDto> CreateStudentAfterCreatedUser(long userId);
        Task<GetStudentWithPostsDto?> GetStudentWithPostsAndComments(long id);

        Task<bool> CheckStudentExists(long studentId);

        Task<Student> GetRequiredStudentById(long studentId);
        Task ChangeStatusOfStudent(long studentId, string status);

        Task UpdateStudentShortDescription(long studentId, string desc);

        Task<List<GetStudentWithUserDto>> GetPagedStudentList(int pageNumber, int pageSize, QueryStudentDto queryStudentDto);

    }
}
