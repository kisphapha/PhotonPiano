using PhotonPiano.DataAccess.Interfaces;
using PhotonPiano.Models.Models;

namespace PhotonPiano.DataAccess.Repositories
{
    public class LocationRepository : GenericRepository<Location>, ILocationRepository
    {
        private readonly PhotonPianoContext _context;
        public LocationRepository(PhotonPianoContext context) : base(context)
        {
            _context = context;
        }

    }
}