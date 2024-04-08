
using Mapster;
using PhotonPiano.BusinessLogic.Interfaces;
using PhotonPiano.DataAccess.Interfaces;
using PhotonPiano.Helper.Dtos.EntranceTests;
using PhotonPiano.Helper.Exceptions;
using PhotonPiano.Models.Enums;
using PhotonPiano.Models.Models;

namespace PhotonPiano.BusinessLogic.Services
{
    public class EntranceTestSlotSerivce : IEntranceTestSlotService
    {
        private readonly IEntranceTestSlotRepository _entranceTestSlotRepository;
        private readonly ILocationSerivce _locationService;

        public EntranceTestSlotSerivce(IEntranceTestSlotRepository entranceTestSlotRepository, ILocationSerivce locationSerivce)
        {
            _entranceTestSlotRepository = entranceTestSlotRepository;
            _locationService = locationSerivce;
        }

        public async Task<List<GetEntranceTestSlotDto>> GetPagedEntranceTestSlots(int pageNumber, int pageSize)
        {
            var result = await _entranceTestSlotRepository.GetPagedEntranceTestSlots(pageNumber, pageSize);
            return result.Adapt<List<GetEntranceTestSlotDto>>();
        }

        public async Task<GetEntranceTestSlotDto> CreateEntranceTestSlot(CreateEntranceTestSlotDto createEntranceTestSlotDto)
        {
            var location = await _locationService.GetLocationById(createEntranceTestSlotDto.LocationId,true);
            if (location!.Status == LocationStatus.Unavailable.ToString())
            {
                throw new BadRequestException("This location is currently unavailable");
            }

            var mappedEntranceTestSlot = createEntranceTestSlotDto.Adapt<EntranceTestSlot>();
            mappedEntranceTestSlot.isAnnouced = false;
            
            return (await _entranceTestSlotRepository.AddAsync(mappedEntranceTestSlot)).Adapt<GetEntranceTestSlotDto>();
        }
    }
}
