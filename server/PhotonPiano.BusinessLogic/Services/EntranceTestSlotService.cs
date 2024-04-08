
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
        private readonly IEntranceTestService _entranceTestService;
        private readonly ILocationSerivce _locationService;

        public EntranceTestSlotSerivce(IEntranceTestSlotRepository entranceTestSlotRepository, 
            ILocationSerivce locationSerivce, IEntranceTestService entranceTestService)
        {
            _entranceTestSlotRepository = entranceTestSlotRepository;
            _locationService = locationSerivce;
            _entranceTestService = entranceTestService;
        }

        public async Task<EntranceTestSlot> GetRequiredEntranceTestSlotById(long id)
        {
            var entranceTestSlot = await _entranceTestSlotRepository.GetByIdAsync(id);
            if (entranceTestSlot is null)
            {
                throw new NotFoundException($"Entrance test slot {id} not found");
            }
            return entranceTestSlot;
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
            mappedEntranceTestSlot.IsAnnouced = false;
            
            return (await _entranceTestSlotRepository.AddAsync(mappedEntranceTestSlot)).Adapt<GetEntranceTestSlotDto>();
        }
    
        public async Task AddEntranceTestToEntranceTestSlot(AddEntranceTestToASlotDto addEntranceTestToASlotDto, long slotId)
        {
            await GetRequiredEntranceTestSlotById(slotId);
            foreach(var entranceTestId in addEntranceTestToASlotDto.EntranceTestIds)
            {              
                await _entranceTestService.UpdateEntranceTestId(entranceTestId, slotId);               
            }
        }
    
        public async Task AnnouceEntranceTestSlot(long slotId)
        {
            var slot = await GetRequiredEntranceTestSlotById(slotId);
            slot.IsAnnouced = true;
            slot.AnnounceTime = DateTime.Now;

            await _entranceTestSlotRepository.UpdateAsync(slot);
        }

    }
}
