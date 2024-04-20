using Microsoft.EntityFrameworkCore;
using PhotonPiano.DataAccess.Interfaces;
using PhotonPiano.Helper.Dtos.Lessons;
using PhotonPiano.Models.Models;

namespace PhotonPiano.DataAccess.Repositories
{
    public class LessonRepository : GenericRepository<Lesson>, ILessonRepository
    {
        private readonly PhotonPianoContext _context;
        public LessonRepository(PhotonPianoContext context) : base(context)
        {
            _context = context;
        }

        public Task<List<Lesson>> GetQueriedLessonsAsync(QueryLessonDto queryLessonDto)
        {
            var lessonQuery = _context.Lessons
                .Include(l => l.Location)
                .AsQueryable();

            if (queryLessonDto.Shift.HasValue)
            {
                lessonQuery = lessonQuery.Where(l => l.Shift == queryLessonDto.Shift.Value);
            }
            if (queryLessonDto.LocationId.HasValue)
            {
                lessonQuery = lessonQuery.Where(l => l.LocationId == queryLessonDto.LocationId.Value);
            }
            if (queryLessonDto.ExamType is not null)
            {
                lessonQuery = lessonQuery.Where(l =>
                    l.ExamType != null && l.ExamType.ToLower().Contains(queryLessonDto.ExamType!.ToLower()));
            }
            if (queryLessonDto.StartDate is not null)
            {
                lessonQuery = lessonQuery.Where(l => l.Date >= queryLessonDto.StartDate);
            }
            if (queryLessonDto.EndDate is not null)
            {
                lessonQuery = lessonQuery.Where(l => l.Date <= queryLessonDto.EndDate);
            }
            var lessons = lessonQuery.ToListAsync();
            return lessons;
        }
    }
}