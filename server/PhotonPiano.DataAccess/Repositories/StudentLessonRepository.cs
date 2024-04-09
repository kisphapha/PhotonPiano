
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PhotonPiano.DataAccess.Interfaces;
using PhotonPiano.Helper.Dtos.Lessons;
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

        public Task<List<StudentLesson>> GetQueriedStudentLessonsByClassIdAndStudentIdAsync(long studentId, long classId, QueryLessonDto queryLessonDto)
        {
            var studentLessonsQuery = _context.StudentLessons
                .Include(sl => sl.Lesson)
                    .ThenInclude(l => l.Location)
                .Where(sl => sl.StudentId == studentId && sl.Lesson.ClassId == classId);

            if (queryLessonDto.Shift.HasValue)
            {
                studentLessonsQuery = studentLessonsQuery.Where(sl => sl.Lesson.Shift == queryLessonDto.Shift.Value);
            }
            if (queryLessonDto.LocationId.HasValue)
            {
                studentLessonsQuery = studentLessonsQuery.Where(sl => sl.Lesson.LocationId == queryLessonDto.LocationId.Value);
            }
            if (!queryLessonDto.ExamType.IsNullOrEmpty())
            {
                studentLessonsQuery = studentLessonsQuery.Where(sl =>
                    sl.Lesson.ExamType != null && sl.Lesson.ExamType.ToLower().Contains(queryLessonDto.ExamType!.ToLower()));
            }
            if (queryLessonDto.StartDate is not null)
            {
                studentLessonsQuery = studentLessonsQuery.Where(sl => sl.Lesson.Date >= queryLessonDto.StartDate);
            }
            if (queryLessonDto.EndDate is not null)
            {
                studentLessonsQuery = studentLessonsQuery.Where(sl => sl.Lesson.Date <= queryLessonDto.EndDate);
            }
            var studentLessons = studentLessonsQuery.ToListAsync();
            return studentLessons;
        }
    }
}
