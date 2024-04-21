
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
        public async Task<EntranceTestSlot?> GetEntranceTestSlotDetail(long id)
        {
            var slot = await _context.EntranceTestSlots
                .Include(ets => ets.Location)
                .Include(ets => ets.EntranceTests)
                .FirstOrDefaultAsync(ets => ets.Id == id);

            if (slot is not null && slot.EntranceTests is not null)
            {
                foreach (var entranceTest in slot.EntranceTests)
                {
                    if (entranceTest.Student is not null)
                    {
                        // Explicitly load the User of the Student entity
                        await _context.Entry(entranceTest.Student).Reference(s => s.User).LoadAsync();
                    }
                }
            }
            return slot;
        }
        public async Task<List<EntranceTestSlot>> GetEntranceTestSlotsByYear(int year)
        {
            var slots = await _context.EntranceTestSlots
                .Include(ets => ets.Location)
                .Where(ets => ets.Date.Year == year)
                .ToListAsync();

            //foreach (var slot in slots)
            //{
            //    foreach (var entranceTest in slot.EntranceTests)
            //    {
            //        if (entranceTest.StudentId.HasValue && entranceTest.Student != null)
            //        {
            //            // Explicitly load the User of the Student entity
            //            await _context.Entry(entranceTest.Student).Reference(s => s.User).LoadAsync();
            //        }
            //    }
            //}
            return slots;
        }

    }
}
