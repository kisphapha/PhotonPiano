using PhotonPiano.Helper.Dtos.Locations;

namespace PhotonPiano.BusinessLogic.Interfaces
{
    public interface ILocationService
    {
        Task<GetLocationDto?> GetLocationById(long id, bool isRequired);

        Task<List<GetLocationDto>> GetLocations(QueryLocationDto queryLocationDto);
    }
}
