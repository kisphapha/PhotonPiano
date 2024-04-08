using PhotonPiano.Helper.Dtos.Locations;

namespace PhotonPiano.BusinessLogic.Interfaces
{
    public interface ILocationSerivce
    {
        Task<GetLocationDto?> GetLocationById(long id, bool isRequired);

        Task<List<GetLocationDto>> GetLocations();
    }
}
