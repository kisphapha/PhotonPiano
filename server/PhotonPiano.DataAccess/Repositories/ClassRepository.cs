using Microsoft.EntityFrameworkCore;
using PhotonPiano.DataAccess.Interfaces;
using PhotonPiano.Models.Models;

namespace PhotonPiano.DataAccess.Repositories
{
    public class ClassRepository : GenericRepository<Class>, IClassRepostiory
    {
        private readonly PhotonPianoContext _context;
        public ClassRepository(PhotonPianoContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Class?> GetClassDetailAsync(long classId)
        {
            var class_ = await _context.Classes
                .Include(c => c.Instructor)
                    .ThenInclude(i => i.User)
                .Include(c => c.Lessons)
                .SingleOrDefaultAsync(c => c.Id == classId);

            return class_;
                
        }

    }
}