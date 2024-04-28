using Azure;
using Microsoft.EntityFrameworkCore;
using PhotonPiano.DataAccess.Interfaces;
using PhotonPiano.Helper.Dtos.Ultilities;
using PhotonPiano.Helper.Dtos.Students;
using PhotonPiano.Models.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace PhotonPiano.DataAccess.Repositories
{
    public class InstructorRepository : GenericRepository<Instructor>, IInstructorRepository
    {
        private readonly PhotonPianoContext _context;
        public InstructorRepository(PhotonPianoContext context) : base(context)
        {
            _context = context;
        }

        public async Task<PaginatedResult<Instructor>> GetPagedInstructors(int pageNumber, int pageSize, QueryInstructorDto queryInstructorDto)
        {
            var instructors = _context.Instructors
                .AsQueryable();

            if (queryInstructorDto.Id.HasValue)
            {
                instructors = instructors.Where(i => i.UserId == queryInstructorDto.Id.Value);
            }

            if (!string.IsNullOrEmpty(queryInstructorDto.Name))
            {
                instructors = instructors.Where(i => i.User.Name.Contains(queryInstructorDto.Name));
            }

            if (!string.IsNullOrEmpty(queryInstructorDto.Phone))
            {
                instructors = instructors.Where(i => i.User.Phone != null && i.User.Phone.Contains(queryInstructorDto.Phone));
            }

            if (!string.IsNullOrEmpty(queryInstructorDto.Email))
            {
                instructors = instructors.Where(i => i.User.Email != null && i.User.Email.Contains(queryInstructorDto.Email));
            }

            if (!string.IsNullOrEmpty(queryInstructorDto.Address))
            {
                instructors = instructors.Where(i => i.User.Address != null && i.User.Address.Contains(queryInstructorDto.Address));
            }

            if (!string.IsNullOrEmpty(queryInstructorDto.Gender))
            {
                instructors = instructors.Where(i => i.User.Gender != null && i.User.Gender == queryInstructorDto.Gender);
            }

            if (!string.IsNullOrEmpty(queryInstructorDto.BankAccount))
            {
                instructors = instructors.Where(i => i.User.BankAccount != null && i.User.BankAccount.Contains(queryInstructorDto.BankAccount));
            }

            if (queryInstructorDto.Level.HasValue)
            {
                instructors = instructors.Where(i => i.Level == queryInstructorDto.Level.Value);
            }

            if (queryInstructorDto.FromContributeScore.HasValue)
            {
                instructors = instructors.Where(i => i.ContributeScore >= queryInstructorDto.FromContributeScore.Value);
            }

            if (queryInstructorDto.ToContributeScore.HasValue)
            {
                instructors = instructors.Where(i => i.ContributeScore <= queryInstructorDto.ToContributeScore.Value);
            }

            instructors = instructors.Include(i => i.User);

            var paginatedResult = await instructors
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // Count the total number of records
            int totalRecords = await instructors.CountAsync();

            // Calculate the total number of pages
            int totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);

            var result = new PaginatedResult<Instructor>
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages,
                TotalRecords = totalRecords,
                Results = paginatedResult
            };

            return result;
        }
    }
}