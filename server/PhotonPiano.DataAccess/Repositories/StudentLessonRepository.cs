
using Microsoft.EntityFrameworkCore;
using PhotonPiano.DataAccess.Interfaces;
using PhotonPiano.Models.Models;

namespace PhotonPiano.DataAccess.Repositories
{
    public class StudentLessonRepository : GenericRepository<StudentLesson>, IStudentLessonRepository
    {
        private readonly PhotonPianoContext _context;
        public StudentLessonRepository(PhotonPianoContext context) : base(context)
        {
            _context = context;
        }

        public Task<List<StudentLesson>> GetStudentLessonsByClassIdAndStudentIdAsync(long classId, long studentId)
        {
            var studentLessons = _context.StudentLessons
                .Include(sl => sl.Lesson)
                .Where(sl => sl.StudentId == studentId && sl.Lesson.ClassId == classId)
                .ToListAsync();

            return studentLessons;
        }
    }
}
