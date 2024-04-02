
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

        public async Task<Student?> GetStudentWithPostsAndComments(long id)
        {
            var student = await _context.Students
                .Include(x => x.User.Posts)
                .Include(x => x.User.PostVotes)
                .Include(x => x.User.CommentVotes)
                .Include(x => x.User.Comments)
                .SingleOrDefaultAsync(x => x.Id == id);
            return student;
        }
        public async Task<Student?> GetStudentWithComments(long id)
        {
            var student = await _context.Students
                .Include(x => x.User)
                    .ThenInclude(x => x.CommentVotes)
                .SingleOrDefaultAsync(x => x.Id == id);
            return student;
        }
        public async Task<Student?> GetStudentDetailAsync(long id)
        {
            var student = await _context.Students
                .Include(s => s.User)
                .Include(s => s.StudentClasses)
                    .ThenInclude(sc => sc.Class)
                .Include(s => s.StudentClasses)
                    .ThenInclude(sc => sc.StudentClassTuitions)
                .Include(s => s.CurrentClass)
                    //.ThenInclude(c => c.Instructor)
                    //    .ThenInclude(i => i.User)
                .Include(s => s.StudentLessons)
                .Include(s => s.EntranceTests)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (student?.CurrentClass is not null)
            {
                await _context.Entry(student.CurrentClass).Reference(c => c.Instructor)
                    .LoadAsync();               
                await _context.Entry(student.CurrentClass.Instructor).Reference(c => c.User)
                    .LoadAsync();           
            }
            return student;
        }
    }
}
