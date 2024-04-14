
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PhotonPiano.DataAccess.Interfaces;
using PhotonPiano.Helper.Dtos.Students;
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
                        .ThenInclude(sc => sc.Instructor.User)
                .Include(s => s.StudentClasses)
                    .ThenInclude(sc => sc.StudentClassTuitions)
                .Include(s => s.CurrentClass)
                    //.ThenInclude(c => c.Instructor)
                    //    .ThenInclude(i => i.User)
                .Include(s => s.StudentLessons)
                .Include(s => s.EntranceTests)
                    .ThenInclude(et => et.EntranceTestSlot)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (student?.CurrentClass is not null)
            {
                await _context.Entry(student.CurrentClass).Reference(c => c.Instructor)
                    .LoadAsync();               
                await _context.Entry(student.CurrentClass.Instructor).Reference(c => c.User)
                    .LoadAsync();           
            }
            if (student?.EntranceTests.ToList().Count > 0)
            {
                foreach (var entranceTest in student.EntranceTests)
                {
                    if (entranceTest.EntranceTestSlot is not null)
                    {
                        await _context.Entry(entranceTest.EntranceTestSlot).Reference(ets => ets.Location)
                            .LoadAsync();
                    }
                }
            }
            return student;
        }

        public async Task<List<Student>> GetPagedStudents(int pageNumber, int pageSize, QueryStudentDto queryStudentDto)
        {
            var query = _context.Students.AsQueryable();

            if (queryStudentDto.Id.HasValue)
            {
                query = query.Where(s => s.UserId == queryStudentDto.Id.Value);
            }

            if (!string.IsNullOrEmpty(queryStudentDto.Name))
            {
                query = query.Where(s => s.User.Name.Contains(queryStudentDto.Name));
            }

            if (!string.IsNullOrEmpty(queryStudentDto.Phone))
            {
                query = query.Where(s => s.User.Phone != null && s.User.Phone.Contains(queryStudentDto.Phone));
            }

            if (!string.IsNullOrEmpty(queryStudentDto.Email))
            {
                query = query.Where(s => s.User.Email != null && s.User.Email.Contains(queryStudentDto.Email));
            }

            if (!string.IsNullOrEmpty(queryStudentDto.Address))
            {
                query = query.Where(s => s.User.Address != null && s.User.Address.Contains(queryStudentDto.Address));
            }

            if (!string.IsNullOrEmpty(queryStudentDto.Gender))
            {
                query = query.Where(s => s.User.Gender != null && s.User.Gender.Contains(queryStudentDto.Gender));
            }

            if (!string.IsNullOrEmpty(queryStudentDto.BankAccount))
            {
                query = query.Where(s => s.User.BankAccount != null && s.User.BankAccount.Contains(queryStudentDto.BankAccount));
            }

            if (!string.IsNullOrEmpty(queryStudentDto.Role))
            {
                query = query.Where(s => s.User.Role.Equals(queryStudentDto.Role));
            }

            if (queryStudentDto.StudentId.HasValue)
            {
                query = query.Where(s => s.Id == queryStudentDto.StudentId.Value);
            }

            if (!string.IsNullOrEmpty(queryStudentDto.Status))
            {
                query = query.Where(s => s.Status.Equals(queryStudentDto.Status));
            }

            if (!string.IsNullOrEmpty(queryStudentDto.ShortDesc))
            {
                query = query.Where(s => s.ShortDesc != null && s.ShortDesc.Contains(queryStudentDto.ShortDesc));
            }

            if (queryStudentDto.FromDoB.HasValue)
            {
                query = query.Where(s => s.User.DoB >= queryStudentDto.FromDoB.Value);
            }

            if (queryStudentDto.ToDoB.HasValue)
            {
                query = query.Where(s => s.User.DoB <= queryStudentDto.ToDoB.Value);
            }

            if (queryStudentDto.FromJoinedDate.HasValue)
            {
                query = query.Where(s => s.JoinedDate >= queryStudentDto.FromJoinedDate.Value);
            }

            if (queryStudentDto.ToJoinedDate.HasValue)
            {
                query = query.Where(s => s.JoinedDate <= queryStudentDto.ToJoinedDate.Value);
            }

            if (queryStudentDto.FromRegistrationDate.HasValue)
            {
                query = query.Where(s => s.RegistrationDate >= queryStudentDto.FromRegistrationDate.Value);
            }

            if (queryStudentDto.ToRegistrationDate.HasValue)
            {
                query = query.Where(s => s.RegistrationDate <= queryStudentDto.ToRegistrationDate.Value);
            }

            if (queryStudentDto.CurrentClassId.HasValue)
            {
                query = query.Where(s => s.CurrentClassId == queryStudentDto.CurrentClassId.Value);
            }

            if (queryStudentDto.Level.HasValue)
            {
                query = query.Where(s => s.Level == queryStudentDto.Level.Value);
            }

            query = query.Include(s => s.User);

            var paginatedResult = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return paginatedResult;
        }
    }
}
