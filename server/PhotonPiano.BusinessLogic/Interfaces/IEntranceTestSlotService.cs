
using PhotonPiano.Helper.Dtos.EntranceTests;

namespace PhotonPiano.BusinessLogic.Interfaces
{
    public interface IEntranceTestSlotService
    {
        Task<GetEntranceTestSlotDetailDto> GetEntranceTestSlotDetail(long id);
        Task<List<GetEntranceTestSlotWithLocationDto>> GetEntranceTestSlotsByYear(int year);

        Task<GetEntranceTestSlotDto> CreateEntranceTestSlot(CreateEntranceTestSlotDto createEntranceTestSlotDto);

        Task UpdateEntranceTestSlot(UpdateEntranceTestSlotDto updateEntranceTestSlotDto);

        Task UpsertStudentsToEntranceTestSlot(AddStudentsToASlotDto addStudentsToASlot);

        Task AnnouceEntranceTestSlot(long slotId);

        Task AnnouceEntranceTestScoreSlot(long slotId);

        Task AnnouceTimeAllEntranceTestSlot();

        Task AnnouceScoreAllEntranceTestSlot();
        Task DeleteEntranceTestSlot(long slotId);

        Task AutoArrangeEntranceTests(AutoArrangeSlotDto autoArrangeSlotDto);
    }
}
