
using Microsoft.EntityFrameworkCore;
using PhotonPiano.DataAccess.Interfaces;
using PhotonPiano.Helper.Dtos.Students;
using PhotonPiano.Helper.Dtos.Users;
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
        public async Task<List<User>> GetQueriedUser(QueryUserDto queryUserDto)
        {
            var users = _context.Users
                .AsQueryable();

            if (queryUserDto.Id.HasValue)
            {
                users = users.Where(u => u.Id == queryUserDto.Id.Value);
            }

            if (!string.IsNullOrEmpty(queryUserDto.Name))
            {
                users = users.Where(u => u.Name.Contains(queryUserDto.Name));
            }

            if (!string.IsNullOrEmpty(queryUserDto.Phone))
            {
                users = users.Where(u => u.Phone != null && u.Phone.Contains(queryUserDto.Phone));
            }

            if (!string.IsNullOrEmpty(queryUserDto.Email))
            {
                users = users.Where(u => u.Email != null && u.Email.Contains(queryUserDto.Email));
            }

            if (!string.IsNullOrEmpty(queryUserDto.Address))
            {
                users = users.Where(u => u.Address != null && u.Address.Contains(queryUserDto.Address));
            }

            if (!string.IsNullOrEmpty(queryUserDto.Gender))
            {
                users = users.Where(u => u.Gender != null && u.Gender == queryUserDto.Gender);
            }

            if (!string.IsNullOrEmpty(queryUserDto.Role))
            {
                users = users.Where(u => u.Gender != null && u.Role == queryUserDto.Role);
            }

            if (!string.IsNullOrEmpty(queryUserDto.BankAccount))
            {
                users = users.Where(u => u.BankAccount != null && u.BankAccount.Contains(queryUserDto.BankAccount));
            }
            return await users.ToListAsync();
           
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
