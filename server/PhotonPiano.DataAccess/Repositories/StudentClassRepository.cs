
using Microsoft.EntityFrameworkCore;
using PhotonPiano.DataAccess.Interfaces;
using PhotonPiano.Models.Models;

namespace PhotonPiano.DataAccess.Repositories
{
    public class StudentClassRepository : GenericRepository<StudentClass>, IStudentClassRepository
    {
        private readonly PhotonPianoContext _context;
        public StudentClassRepository(PhotonPianoContext context) : base(context)
        {
            _context = context;
        }
        
        //public async Task<StudentClass?> GetStudentClassWithStudentLessonsById(long classId ,long studentId)
        //{
        //    var studentClass = await _context.StudentClasses
        //        .Include(x => x.Class)
        //            .ThenInclude(x => x.Lessons)
        //                .ThenInclude(x => x.StudentLessons)
        //        .SingleOrDefaultAsync(x => x.StudentId == studentId && x.ClassId == classId);
        //    return studentClass;
        //}
    }
}
