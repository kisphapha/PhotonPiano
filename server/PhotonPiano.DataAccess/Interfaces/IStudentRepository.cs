
using PhotonPiano.Helper.Dtos.Paginations;
using PhotonPiano.Helper.Dtos.Students;
using PhotonPiano.Models.Models;

namespace PhotonPiano.DataAccess.Interfaces
{
    public interface IStudentRepository : IGenericRepository<Student>
    {
        Task<Student?> GetStudentDetailAsync(long id);
        Task<Student?> GetStudentWithPostsAndComments(long id);

        Task<PaginatedResult<Student>> GetPagedStudents(int pageNumber, int pageSize, QueryStudentDto queryStudentDto);
    }
}
