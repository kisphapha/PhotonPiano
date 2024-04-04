using PhotonPiano.DataAccess.Interfaces;
using PhotonPiano.Models.Models;

namespace PhotonPiano.DataAccess.Repositories
{
    public class CriteriaRepository : GenericRepository<Criterion>, ICriteriaRepository
    {
        private readonly PhotonPianoContext _context;
        public CriteriaRepository(PhotonPianoContext context) : base(context)
        {
            _context = context;
        }

    }
}