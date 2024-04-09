
using PhotonPiano.Models.Models;

namespace PhotonPiano.DataAccess.Interfaces
{
    public interface IClassRepostiory : IGenericRepository<Class>
    {
        Task<Class?> GetClassDetailAsync(long classId);
    }
}
