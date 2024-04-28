using PhotonPiano.Helper.Dtos.Ultilities;
using PhotonPiano.Helper.Dtos.Students;
using PhotonPiano.Models.Models;

namespace PhotonPiano.DataAccess.Interfaces
{
    public interface IInstructorRepository : IGenericRepository<Instructor>
    {
        Task<PaginatedResult<Instructor>> GetPagedInstructors(int pageNumber, int pageSize, QueryInstructorDto queryInstructorDto);
    }
}
