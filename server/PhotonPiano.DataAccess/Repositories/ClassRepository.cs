using Microsoft.EntityFrameworkCore;
using PhotonPiano.DataAccess.Interfaces;
using PhotonPiano.Helper.Dtos.Classes;
using PhotonPiano.Helper.Dtos.Paginations;
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
        public async Task<PaginatedResult<Class>> GetPagedClass(int pageNumber, int pageSize, QueryClassDto queryClassDto)
        {
            var classQuery = _context.Classes.AsQueryable();

            if (queryClassDto.Id.HasValue)
            {
                classQuery = classQuery.Where(c => c.Id == queryClassDto.Id.Value);
            }
            if (queryClassDto.Name != null)
            {
                classQuery = classQuery.Where(c => c.Name.Contains(queryClassDto.Name));
            }
            if (queryClassDto.Level.HasValue)
            {
                classQuery = classQuery.Where(c => c.Level == queryClassDto.Level.Value);
            }
            if (queryClassDto.StartDate != null)
            {
                classQuery = classQuery.Where(c => c.StartDate >= queryClassDto.StartDate);
            }
            if (queryClassDto.EndDate != null)
            {
                classQuery = classQuery.Where(c => c.EndDate <= queryClassDto.EndDate);
            }
            if (queryClassDto.Period.HasValue)
            {
                classQuery = classQuery.Where(c => c.StartDate.Year == queryClassDto.Period.Value 
                    || c.EndDate.Year == queryClassDto.Period.Value);
            }
            if (queryClassDto.Status != null)
            {
                classQuery = classQuery.Where(c => c.Status == queryClassDto.Status);
            }
            if (queryClassDto.InstructorId.HasValue)
            {
                classQuery = classQuery.Where(c => c.InstructorId == queryClassDto.InstructorId.Value);
            }
            if (queryClassDto.IsAnnouced.HasValue)
            {
                classQuery = classQuery.Where(c => c.IsAnnouced == queryClassDto.IsAnnouced.Value);
            }
            if (queryClassDto.IsSchedule.HasValue)
            {
                if (queryClassDto.IsSchedule.Value)
                {
                    classQuery = classQuery.Where(c => c.Lessons.Count > 0);
                } else
                {
                    classQuery = classQuery.Where(c => c.Lessons.Count == 0);
                }
            }

            classQuery = classQuery.Include(c => c.Lessons);

            var paginatedResult = await classQuery
               .Skip((pageNumber - 1) * pageSize)
               .Take(pageSize)
               .ToListAsync();

            // Count the total number of records
            int totalRecords = await classQuery.CountAsync();

            // Calculate the total number of pages
            int totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);

            var result = new PaginatedResult<Class>
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages,
                TotalRecords = totalRecords,
                Results = paginatedResult
            };

            return result;
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