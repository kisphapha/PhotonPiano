using PhotonPiano.Models.Models;

namespace PhotonPiano.DataAccess.Interfaces
{
    public interface IEntranceTestSlotRepository : IGenericRepository<EntranceTestSlot>
    {
        Task<List<EntranceTestSlot>> GetPagedEntranceTestSlots(int pageNumber, int pageSize);
    }
}
