

using PhotonPiano.Helper.Dtos.Criteria;
using PhotonPiano.Helper.Dtos.Instructors;
using PhotonPiano.Helper.Dtos.Paginations;
using PhotonPiano.Helper.Dtos.Students;
using PhotonPiano.Models.Models;

namespace PhotonPiano.BusinessLogic.Interfaces
{
    public interface IInstructorService
    {
        Task<Instructor> GetRequiredInstructorById(long id);
        Task<PaginatedResult<GetInstructorWithUserDto>> GetPagedInstructors(int pageNumber, int pageSize, QueryInstructorDto queryInstructorDto);
    }
}
