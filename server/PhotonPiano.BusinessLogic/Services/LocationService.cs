using Mapster;
using PhotonPiano.BusinessLogic.Interfaces;
using PhotonPiano.DataAccess.Interfaces;
using PhotonPiano.Helper.Dtos.Locations;
using PhotonPiano.Helper.Exceptions;


namespace PhotonPiano.BusinessLogic.Services
{
    public class LocationSerivce : ILocationSerivce
    {
        private readonly ILocationRepository _locationRepository;
        public LocationSerivce(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public async Task<List<GetLocationDto>> GetLocations()
        {
            return (await _locationRepository.GetAllAsync()).Adapt<List<GetLocationDto>>();
        }
        public async Task<GetLocationDto?> GetLocationById(long id, bool isRequired)
        {
            var location = (await _locationRepository.FindOneAsync(l => l.Id == id));
                
            if (location is null && isRequired)
            {
                throw new NotFoundException("Location not found");
            }
            return location.Adapt<GetLocationDto>();
        }

    }
}
