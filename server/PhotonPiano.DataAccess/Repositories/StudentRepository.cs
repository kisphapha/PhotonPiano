
using Microsoft.EntityFrameworkCore;
using PhotonPiano.DataAccess.Interfaces;
using PhotonPiano.Models.Models;

namespace PhotonPiano.DataAccess.Repositories
{
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        private readonly PhotonPianoContext _context;

        public StudentRepository(PhotonPianoContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Student?> GetStudentDetailAsync(long id)
        {
            var student = await _context.Students
                .Include(s => s.User)
                    .ThenInclude(s => s.Posts)
                .Include(s => s.StudentClasses)
                    .ThenInclude(sc => sc.Class)
                .Include(s => s.StudentClasses)
                    .ThenInclude(sc => sc.StudentClassTuitions)
                .Include(s => s.StudentLessons)
                .Include(s => s.EntranceTests)
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == id);

            return student;
        }
    }
}
