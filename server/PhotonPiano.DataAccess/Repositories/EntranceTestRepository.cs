
using Microsoft.EntityFrameworkCore;
using PhotonPiano.DataAccess.Interfaces;
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

    }
}
