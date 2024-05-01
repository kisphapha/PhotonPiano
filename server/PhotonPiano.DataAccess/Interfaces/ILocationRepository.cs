using PhotonPiano.Helper.Dtos.Locations;
using PhotonPiano.Models.Models;

namespace PhotonPiano.DataAccess.Interfaces
{
    public interface ILocationRepository : IGenericRepository<Location>
    {
        Task<List<Location>> GetQueriedLocationAsync(QueryLocationDto queryLocationDto);
    }
}
