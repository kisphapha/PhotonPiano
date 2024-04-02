
using Microsoft.EntityFrameworkCore;
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

        public async Task<User?> GetUserWithStudentsAndInstructorsByIdAsync(long id)
        {
            var user = await _context.Users
                .Include(u => u.Students)
                .Include(u => u.Instructors)
                .SingleOrDefaultAsync(u  => u.Id == id);
            return user;
        }
    }
}
