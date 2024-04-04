using PhotonPiano.DataAccess.Interfaces;
using PhotonPiano.Models.Models;

namespace PhotonPiano.DataAccess.Repositories
{
    public class EntranceTestResultRepository : GenericRepository<EntranceTestResult>, IEntranceTestResultRepository
    {
        private readonly PhotonPianoContext _context;
        public EntranceTestResultRepository(PhotonPianoContext context) : base(context)
        {
            _context = context;
        }

    }
}