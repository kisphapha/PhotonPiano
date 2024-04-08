
using Microsoft.EntityFrameworkCore;
using PhotonPiano.DataAccess.Interfaces;
using PhotonPiano.Models.Models;

namespace PhotonPiano.DataAccess.Repositories
{
    public class EntranceTestSlotRepository : GenericRepository<EntranceTestSlot>, IEntranceTestSlotRepository
    {
        private readonly PhotonPianoContext _context;
        public EntranceTestSlotRepository(PhotonPianoContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<EntranceTestSlot>> GetPagedEntranceTestSlots(int pageNumber, int pageSize)
        {
            var query = _context.EntranceTestSlots; // Replace YourEntity with your actual entity name

            var paginatedResult = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return paginatedResult;
        }

    }
}
