using PhotonPiano.Models.Models;

namespace PhotonPiano.DataAccess.Interfaces
{
    public interface IEntranceTestSlotRepository : IGenericRepository<EntranceTestSlot>
    {
        Task<List<EntranceTestSlot>> GetEntranceTestSlotsByYear(int year);

        Task<EntranceTestSlot?> GetEntranceTestSlotDetail(long id);
    }
}
