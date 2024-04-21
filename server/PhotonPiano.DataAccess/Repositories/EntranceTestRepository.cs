
using Microsoft.EntityFrameworkCore;
using PhotonPiano.DataAccess.Interfaces;
using PhotonPiano.Helper.Dtos.EntranceTests;
using PhotonPiano.Helper.Dtos.Paginations;
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
            return await _context.EntranceTests
                .Include(x => x.EntranceTestResults)
                    .ThenInclude(x => x.Criteria)
                .SingleOrDefaultAsync(x => x.StudentId == studentId);
        }
        

    }
}
