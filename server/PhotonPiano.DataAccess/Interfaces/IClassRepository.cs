
using PhotonPiano.Helper.Dtos.Classes;
using PhotonPiano.Helper.Dtos.Ultilities;
using PhotonPiano.Models.Models;

namespace PhotonPiano.DataAccess.Interfaces
{
    public interface IClassRepostiory : IGenericRepository<Class>
    {
        Task<Class?> GetClassDetailAsync(long classId);

        Task<PaginatedResult<Class>> GetPagedClass(int pageNumber, int pageSize, QueryClassDto queryClassDto);
    }
}
