
using Microsoft.EntityFrameworkCore;
using PhotonPiano.DataAccess.Interfaces;
using PhotonPiano.Helper.Dtos.EntranceTests;
using PhotonPiano.Helper.Dtos.Ultilities;
using PhotonPiano.Helper.Dtos.Students;
using PhotonPiano.Models.Models;

namespace PhotonPiano.DataAccess.Repositories
{
    public class EntranceTestRepository : GenericRepository<EntranceTest>, IEntranceTestRepository
    {
        private readonly PhotonPianoContext _context;
        public EntranceTestRepository(PhotonPianoContext context) : base(context)
        {
            _context = context;
        }
        public async Task<EntranceTest?> GetEntranceTestWithResultByStudentId(long studentId)
        {
            var entranceTest =  await _context.EntranceTests
                .Include(x => x.EntranceTestResults)
                    .ThenInclude(x => x.Criteria)
                .Include(x => x.EntranceTestSlot)
                .SingleOrDefaultAsync(x => x.StudentId == studentId);

            if (entranceTest?.EntranceTestSlot is not null)
            {
                await _context.Entry(entranceTest.EntranceTestSlot).Reference(s => s.Location)
                    .LoadAsync();
            }

            return entranceTest;
        }


    }
}
