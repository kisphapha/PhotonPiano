
using PhotonPiano.Helper.Dtos.EntranceTests;

namespace PhotonPiano.BusinessLogic.Interfaces
{
    public interface IEntranceTestSlotService
    {
        Task<List<GetEntranceTestSlotDto>> GetPagedEntranceTestSlots(int pageNumber, int pageSize);

        Task<GetEntranceTestSlotDto> CreateEntranceTestSlot(CreateEntranceTestSlotDto createEntranceTestSlotDto);

        Task AddEntranceTestToEntranceTestSlot(AddEntranceTestToASlotDto addEntranceTestToASlotDto, long slotId);

        Task AnnouceEntranceTestSlot(long slotId);
    }
}
