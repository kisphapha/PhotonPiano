
using PhotonPiano.DataAccess.Interfaces;
using PhotonPiano.Models.Models;

namespace PhotonPiano.DataAccess.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly PhotonPianoContext _context;

        public UserRepository(PhotonPianoContext context) : base(context)
        {
            _context = context;
        }
    }
}
