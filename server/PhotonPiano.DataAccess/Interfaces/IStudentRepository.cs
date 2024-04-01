
using PhotonPiano.Models.Models;

namespace PhotonPiano.DataAccess.Interfaces
{
    public interface IStudentRepository : IGenericRepository<Student>
    {
        Task<Student?> GetStudentDetailAsync(long id);
        Task<Student?> GetStudentWithPostsAndComments(long id);
    }
}
